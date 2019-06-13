using Honoplay.Common._Exceptions;
using Honoplay.Persistence.CacheService;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Persistence.CacheManager
{
    public class CacheManager : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public CacheManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> RedisCacheAsync<T>(string redisKey, Func<IDistributedCache, T> redisLogic, CancellationToken cancellationToken) where T : class
        {
            if (string.IsNullOrEmpty(redisKey))
            {
                throw new ArgumentNullException(nameof(redisKey));
            }

            var serializedRedisList = await _distributedCache.GetStringAsync(redisKey, cancellationToken);
            T redisList;

            if (!string.IsNullOrEmpty(serializedRedisList))
            {
                redisList = JsonConvert.DeserializeObject<T>(serializedRedisList);
            }
            else
            {
                redisList = redisLogic?.Invoke(_distributedCache);
                if (redisList is null)
                {
                    throw new NotFoundException();
                }

                await _distributedCache.SetStringAsync(redisKey, JsonConvert.SerializeObject(redisList), cancellationToken);
            }

            return redisList;
        }

        public async Task RedisCacheUpdateAsync<T>(string redisKey, Func<IDistributedCache, T> redisLogic, CancellationToken cancellationToken) where T : class
        {
            if (string.IsNullOrEmpty(redisKey))
            {
                throw new ArgumentNullException(nameof(redisKey));
            }
            var databaseList = redisLogic?.Invoke(_distributedCache);
            if (databaseList != null)
            {
                await _distributedCache.SetStringAsync(redisKey, JsonConvert.SerializeObject(databaseList), cancellationToken);
            }

        }
    }
}
