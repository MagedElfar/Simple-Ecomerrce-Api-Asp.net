using Core.Entities;
using Core.Interfaces.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase database;

        public CartRepository(IConnectionMultiplexer redis)
        {
            database = redis.GetDatabase();
        }
        public async Task<bool> DeleteAsync(string id)
        {
            return await database.KeyDeleteAsync(id);
        }

        public async Task<Cart> GetCartAsync(string id)
        {
            var data = await database.StringGetAsync(id);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(data);
        }

        public async Task<bool> Update(Cart cart)
        {
            var oldCart = await GetCartAsync(cart.Id);

            if(oldCart != null && !string.IsNullOrEmpty(oldCart.PaymentIntentId))
            {
                cart.PaymentIntentId = oldCart.PaymentIntentId;
                cart.ClientSecret = oldCart.ClientSecret;
            }

            var created = await database.StringSetAsync(cart.Id , 
                JsonSerializer.Serialize(cart) , TimeSpan.FromDays(5));

            return created;
        }
    }
}
