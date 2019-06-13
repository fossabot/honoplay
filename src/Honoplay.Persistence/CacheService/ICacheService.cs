using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Persistence.CacheService
{
    public interface ICacheService
    {
        Task<T> RedisCacheAsync<T>(string redisKey, Func<IDistributedCache, T> redisLogic,
            CancellationToken cancellationToken);

        Task RedisCacheUpdateAsync<T>(string redisKey, Func<IDistributedCache, IList<T>> redisLogic,
            CancellationToken cancellationToken) where T : new();
    }
}
