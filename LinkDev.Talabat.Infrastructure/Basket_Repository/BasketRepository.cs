using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LinkDev.Talabat.Domain.Contract.Infrastructure;
using LinkDev.Talabat.Domain.Entities.Basket;
using StackExchange.Redis;

namespace LinkDev.Talabat.Infrastructure.Basket_Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;


        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<Basket?> GetAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(basket);
        }

        public async Task<Basket?> UpdateAsync(Basket basket, TimeSpan timeToLive)
        {
            var serializedBasket = JsonSerializer.Serialize(basket);
            var updated = await _database.StringSetAsync(basket.Id,
                serializedBasket, timeToLive);

            if (!updated) return null;
            return basket;
                
                
         }
        public async Task DeleteAsync(string id)
        => await _database.KeyDeleteAsync(id);

    }
}
