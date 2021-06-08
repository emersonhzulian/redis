using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace ApresentacaoRedis.API.Extensions
{
    //ImTimCorey
    public static class RedisExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, 
            string recordId, 
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions();
            
            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusedExpireTime;

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static bool TryGetRecord<T>(this IDistributedCache cache, 
            string recordId, out T result)
        {

            var jsonData = cache.GetString(recordId);

            if(jsonData is null)
            {
                result = default(T);
                return false;
            }

            result = JsonSerializer.Deserialize<T>(jsonData);

            return true;
        }
    }
}
