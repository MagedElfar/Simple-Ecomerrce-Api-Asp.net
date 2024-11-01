using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string id);

        Task<bool> Update(Cart cart);

        Task<bool> DeleteAsync(string id);
    }
}
