using Shop.Application.Interfaces;
using Shop.Domain.Entities;
using StackExchange.Redis;
using System.Text.Json;

namespace Shop.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public CartService(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase();
        }

        public async Task<bool> DeleteCartAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<ShoppingCart?> GetCartAsync(string key)
        {
            var data = await _database.StringGetAsync(key);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart?>(data!);
        }

        public async Task<ShoppingCart?> SetCartAsync(ShoppingCart cart)
        {
            var created = await _database.StringSetAsync(cart.Id, 
                JsonSerializer.Serialize(cart), TimeSpan.FromSeconds(60*5));

            if (!created) return null;

            return await GetCartAsync(cart.Id);
        }
    }
}
