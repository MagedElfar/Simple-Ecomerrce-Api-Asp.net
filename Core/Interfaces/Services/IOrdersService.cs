using Core.Dtos.Order;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IOrdersService:IBaseService<Order>
    {
        Task<Order> CreateOrderAsync(CreateOrderDto createOrderDto);

    }
}
