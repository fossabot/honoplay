using Honoplay.Application._Exceptions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Honoplay.Application
{
    public class RedisExtensions
    {
        private readonly IDistributedCache _cache;

        public RedisExtensions(IDistributedCache cache)
        {
            _cache = cache;
        }
        
    }
}
