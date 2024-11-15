using Core.DTOS.Order;
using Core.DTOS.Product;
using Core.DTOS.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<OrderDto> GetUderOrderByIdAsync(int id , int userId);

        Task<ListWithCountDto<OrderDto>> GetUserOrdersCountAll(OrderQueryDto orderQueryDto);


    }
}
