using Core.Dtos.Order;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Infrastructure.Services
{
    public class OrderaService : BaseSerivce<Order>, IOrdersService
    {

        private readonly ICartService cartService;
        private readonly IProductsService productsService;
        private readonly IBaseService<PaymentMethod> paymentMethodService;

        public OrderaService(
            ICartService cartService,
            IProductsService productsService,
            IBaseService<PaymentMethod> paymentMethodService,
            IGenericRepository<Order> repository
        ) : base(repository)
        {
            this.cartService = cartService;
            this.productsService = productsService;
            this.paymentMethodService = paymentMethodService;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto createOrderDto)
        {

            //Get user cart
            var cart = await cartService.GetCartByIdAsync(createOrderDto.UserId.ToString());

            if (cart == null || cart.Items == null || cart.Items.Count() == 0)
                throw new BadRequsetException(err: new [] { "Cart is empty" });

            var paymentMethod = await paymentMethodService.FindByIdAsync(createOrderDto.PaymentMethodId);

            if (paymentMethod == null)
                throw new BadRequsetException("Invalid Payment method");

            List<OrderItem> items = await SetOrderItems(cart);

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

            await CreateAync(order);

            return order;
        }

        private async Task<List<OrderItem>> SetOrderItems(Cart cart)
        {
            var items = new List<OrderItem>();

            foreach (var item in cart.Items)
            {
                var product = await productsService.FindByIdAsync(item.ProductId);

                if (product == null)
                    throw new NotFoundException($"Peoduct with Id = ${item.ProductId} not exsist");

                items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductImage = product.imageUrl,
                    Quantity = item.Quantity,
                    Price = product.Price
                });
            }

            return items;
        }
    }
}
