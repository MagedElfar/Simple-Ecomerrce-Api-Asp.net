using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IStockService
    {
        Task<int> UpdateStock(int productId, int quantity);
        Task<int> AddStockAsync(int productId, int quantity);
        Task<int> ReduceStockAsync(int productId, int quantity);
        Task<bool> ISExist(int productId, int quantity);
        Task UpdateStockAfterOrder(IEnumerable<OrderItem> items);
    }
}
