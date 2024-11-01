using Core.Entities;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ICartService
    {
        Task<Cart> GetCartByIdAsync(string id);

        Task<Cart> UpdateCart(Cart cart);
        Task<bool> DeleteCart(string cart);
    }
}
