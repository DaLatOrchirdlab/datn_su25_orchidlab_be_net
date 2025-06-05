using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.Infrastructure.Repository
{
    public class RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger) : ICacheService
    {
        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(5)
            });
            logger.LogInformation("Set cache for key {Key}", key);
        }

        public async Task<string?> GetAsync(string key)
        {
            var value = await cache.GetStringAsync(key);
            logger.LogInformation("Get cache for key {Key}: {Value}", key, value);
            return value;
        }

        public async Task RemoveAsync(string key)
        {
            await cache.RemoveAsync(key);
            logger.LogInformation("Removed cache for key {Key}", key);
        }
    }
}
