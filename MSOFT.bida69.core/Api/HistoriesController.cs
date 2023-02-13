using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MSOFT.Entities;

namespace MSOFT.bida69.core.Api
{
    [Route("histories")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        public IDistributedCache _distributedCache { get; set; }
        public HistoriesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public IActionResult GetHistory()
        {
            var cacheKey = "TheTime";
            var currentTime = DateTime.Now.ToString();
            var cachedTime = _distributedCache.GetString(cacheKey);
            if (string.IsNullOrEmpty(cachedTime))
            {
                // cachedTime = "Expired";
                // Cache expire trong 5s
                var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(5));
                // Nạp lại giá trị mới cho cache
                _distributedCache.SetString(cacheKey, currentTime, options);
                cachedTime = _distributedCache.GetString(cacheKey);
            }
            var result = $"Current Time : {currentTime} \nCached  Time : {cachedTime}";
            return Ok(result);
        }

        [HttpPost]
        public IActionResult SetHistory([FromBody] History history)
        {
            return Ok(history);
        }
    }
}
