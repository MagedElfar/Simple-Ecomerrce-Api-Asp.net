using API.Extensions;
using AutoMapper;
using Core.DTOS.Order;
using Core.DTOS.Shared;
using Core.Entities;
using Core.Errors;
using Core.Interfaces.Services;
using Core.Specifications;
using Core.Specifications.SpecificationBuilder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService ordersService;

        public OrdersController(IOrderService ordersService)
        {
            this.ordersService = ordersService;
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            createOrderDto.UserId = HttpContext.User.GetUserId();

            return Ok(await ordersService.CreateOrderAsync(createOrderDto));
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            var userId = HttpContext.User.GetUserId();

        

            return Ok(await ordersService.GetUderOrderByIdAsync(id , userId));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListWithCountDto<OrderDto>>>> GetUserOrders([FromQuery] OrderQueryDto orderQuery)
        {
            return Ok(await ordersService.GetUserOrdersCountAll(orderQuery));
        }
    }
}
