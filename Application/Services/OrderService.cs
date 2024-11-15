using AutoMapper;
using Core.DTOS.Order;
using Core.DTOS.Product;
using Core.DTOS.Shared;
using Core.Entities;
using Core.Errors;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications.SpecificationBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService : IOrderService
    {

        private readonly ICartService cartService;
        private readonly IPaymentMethodService paymentMethodService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IStockService stockService;
        private readonly IMapper mapper;

        public OrderService(
            ICartService cartService, 
            IPaymentMethodService paymentMethodService, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IStockService stockService)
        {
            this.cartService = cartService;
            this.paymentMethodService = paymentMethodService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.stockService = stockService;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            
            //Get user cart
            var cart = await cartService.GetCartByIdAsync(createOrderDto.UserId);

            if (cart == null || cart.Items == null || cart.Items.Count() == 0)
                throw new BadRequestException("Cart is empty");

            var paymentMethod = await unitOfWork.Repository<PaymentMethod>().GetByIdAsync(createOrderDto.PaymentMethodId);

            if (paymentMethod == null)
                throw new BadRequestException("Invalid Payment method");

            await CheckProductsQty(cart.Items);

            List<OrderItem> items = await MapOrderItems(cart.Items);

            var order = new Order
            {
                UserId = createOrderDto.UserId,
                Email = createOrderDto.CustomerEmail,
                Items = items,
                SubTotal = items.Sum(x => x.Price * x.Quantity),
                PaymentMethodId = paymentMethod.Id,
                ShippingAddress = new ShippingAddress
                {
                    City = createOrderDto.City,
                    FirstName = createOrderDto.FirstName,
                    LastName = createOrderDto.LastName,
                    State = createOrderDto.State,
                    Street = createOrderDto.Street,
                    Zipcode = createOrderDto.Zipcode,
                    Phone = createOrderDto.Phone,
                }
            };

            await unitOfWork.Repository<Order>().AddAsync(order);

            await unitOfWork.Compleate();

            return mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> GetUderOrderByIdAsync(int id, int userId)
        {
            var bulder = new OrderSpecificationBuilder()
            .WithUserId(userId)
            .WithId(id)
            .Include("Items")
            .Include("PaymentMethod");

            var order = await unitOfWork.Repository<Order>().GetOneAsync(bulder.Build());

            if (order == null)
                throw new NotFoundException("Order not Found");

            return mapper.Map<OrderDto>(order);


        }

        public async Task<ListWithCountDto<OrderDto>> GetUserOrdersCountAll(OrderQueryDto orderQueryDto)
        {
            var spec = orderQueryDto.BuildSpecification()
               .Build();

            var orders = await unitOfWork.Repository<Order>().GetAllAsync(spec);
            var count = await unitOfWork.Repository<Order>().GetCountAsync(spec);

            return new ListWithCountDto<OrderDto>
            {
                Count = count,
                Items = mapper.Map<IEnumerable<OrderDto>>(orders)
            };
        }

        private async Task CheckProductsQty(IEnumerable<CartItem> cartItems)
        {
            var nonExistentProducts = new List<int>();

            foreach (var cartItem in cartItems) { 
                var isExist = await stockService.ISExist(cartItem.ProductId, cartItem.Quantity);

                if(!isExist)
                    nonExistentProducts.Add(cartItem.ProductId);
            }

            if (nonExistentProducts.Any())
            {
                string errorMessage = $"Insufficient stock. Products with IDs {string.Join(", ", nonExistentProducts)}.";
                throw new BadRequestException(errorMessage);
            }

        }

        private async Task<List<OrderItem>> MapOrderItems(IEnumerable<CartItem> cartItems)
        {
            var items = new List<OrderItem>();

            var repo = unitOfWork.Repository<Product>();

            foreach (var item in cartItems)
            {
                var product = await repo.GetByIdAsync(item.ProductId);

                items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductImage = product.ImageUrl,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            return items;
        }
    }
}
