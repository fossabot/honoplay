using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Honoplay.AdminWebAPI.Controllers
{
    public class StatusController : BaseController
    {
        private readonly IDistributedCache _cache;

        public StatusController(IDistributedCache cache)
        {
            _cache = cache;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(HonoHost);
        }

        [HttpGet,Route("RedisTest")]
        public string RedisTest()
        {
            var cacheKey = "TheTime";
            var existingTime = _cache.GetString(cacheKey);
            string cacheData;

            if (!string.IsNullOrEmpty(existingTime))
            {
                cacheData = "Fetched from cache : " + existingTime;
            }
            else
            {
                existingTime = DateTime.UtcNow.ToString();
                _cache.SetString(cacheKey, existingTime);
                cacheData = "Added to cache : " + existingTime;
            }

            return cacheData;
        }
    }
}