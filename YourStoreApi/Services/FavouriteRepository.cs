using System;
using System.Text.Json;
using System.Threading.Tasks;

using StackExchange.Redis;
using YourStoreApi.Models;

namespace YourStoreApi.Services
{
    public class FavouriteRepository : IFavouriteRepository
    {
          private readonly IDatabase _database;
        public FavouriteRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string favouriteId)
        {
            return await _database.KeyDeleteAsync(favouriteId);
        }

        public async Task<CustomerFavourite> GetBasketAsync(string favouriteId)
        {
            var data = await _database.StringGetAsync(favouriteId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerFavourite>(data);
        }

        public async Task<CustomerFavourite> UpdateBasketAsync(CustomerFavourite favourite)
        {
            var created = await _database.StringSetAsync(favourite.Id, JsonSerializer.Serialize(favourite), 
                TimeSpan.FromDays(30));

            if (!created) return null;

            return await GetBasketAsync(favourite.Id);
        }
    }
}