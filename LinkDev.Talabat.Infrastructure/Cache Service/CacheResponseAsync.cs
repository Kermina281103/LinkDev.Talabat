using LinkDev.Talabat.Application.Abstraction.Common.Contracts.Infrastructure;
using StackExchange.Redis;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Cache_Service
{
    class CacheResponseAsync(IConnectionMultiplexer redis) : IResponseCacheService
    {
        private readonly IDatabase _database = redis.GetDatabase();
        public async Task<string?> GetCachedResponseAsync(string key)
        {
            var response = await _database.StringGetAsync(key);
            if (response.IsNull) return null;

            return response;
        }

      async  Task IResponseCacheService.CacheResponseAsync(string key, object response, TimeSpan timeToLive)
        {
            if (response is null) return;
            var serializeOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serlizeResponse = JsonSerializer.Serialize(response,serializeOptions);

            await _database.StringSetAsync(key, serlizeResponse, timeToLive);
        }
    }
}
