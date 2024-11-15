using Core.DTOS.Cart;
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
        Task<CartDto> MappingCart(int id);
        Task<Cart> GetCartByIdAsync(int id);
        Task<CartDto> UpdateCart(int id, UpdateCartDto updateCartDto);
        Task<bool> DeleteCart(int id);
    }
}
