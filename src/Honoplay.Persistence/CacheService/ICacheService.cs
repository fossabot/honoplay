using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Persistence.CacheService
{
    public interface ICacheService
    {
        Task<T> RedisCacheAsync<T>(string redisKey, Func<IDistributedCache, T> redisLogic,
            CancellationToken cancellationToken) where T : class;

        Task RedisCacheUpdateAsync<T>(string redisKey, Func<IDistributedCache, T> redisLogic,
            CancellationToken cancellationToken) where T : class;
    }
}
