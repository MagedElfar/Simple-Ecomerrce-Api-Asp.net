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
    public class RedisRepository<T>:IRedisRepository<T> where T : BaseEntity
    {
        private readonly IDatabase database;
        public RedisRepository(IConnectionMultiplexer redis)
        {
            database = redis.GetDatabase();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await database.KeyDeleteAsync(id);
        }

        public async Task<T?> GetAsync(string id)
        {
            var data = await database.StringGetAsync(id);


            return data.IsNullOrEmpty ? default(T) : JsonSerializer.Deserialize<T>(data);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var created = await database.StringSetAsync(entity.Id.ToString(),
                JsonSerializer.Serialize(entity), TimeSpan.FromDays(5));

            return created;
        }
    }
}
