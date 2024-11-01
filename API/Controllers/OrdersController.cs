using API.Extensions;
using AutoMapper;
using Core.Dtos.Order;
using Core.Dtos.Pagning;
using Core.Dtos.Products;
using Core.Entities;
using Core.Errors;
using Core.Interfaces.Services;
using Core.Specifications;
using Core.Specifications.Products;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;

        public OrdersController(IOrdersService ordersService , IMapper mapper)
        {
            this.ordersService = ordersService;
            this.mapper = mapper;
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            createOrderDto.UserId = HttpContext.User.GetUserId();

            var order = await ordersService.CreateOrderAsync(createOrderDto);

            return Ok(mapper.Map<Order , OrderDto>(order));
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var userId = HttpContext.User.GetUserId();

            var order = await ordersService.FindOneAync(new BaseSpecifications<Order>(
                criteria: x => x.UserId == userId && x.Id == id,
                includes: new string[] {"Items" , "PaymentMethod"}
            ));

            if(order == null) 
                return NotFound(new ApiErrorResponse(404 , "Order not Found"));

            return Ok(mapper.Map<Order, OrderDto>(order));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagningListDto<OrderDto>>>> GetUserOrders([FromQuery] OrderQueryDto orderQuery) 
        {

            var spec = new OrderFilterSpecifications(
                orderBy: "OrderDate",
                fromDate: orderQuery.FromDate,
                toDate: orderQuery.ToDate,
                userId: HttpContext.User.GetUserId(),
                status:orderQuery.Status,
                limit:orderQuery.Limit,
                page:orderQuery.Page
            );

            var result = await ordersService.FindAndCountAll(spec);


            return Ok(new PagningListDto<OrderDto>
            {
                Count = result.Count,
                Items = mapper.Map<List<OrderDto>>(result.Rows)
            });

        }
    }
}
