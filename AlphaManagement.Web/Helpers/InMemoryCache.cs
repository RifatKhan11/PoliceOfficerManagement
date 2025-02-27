using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaManagement.Web.Helpers
{
    public class InMemoryCache
    {
        private IMemoryCache cache;

        public InMemoryCache(IMemoryCache cache)
        {
            this.cache = cache;
        }
        private IEnumerable<T> CacheGetOrCreate<T>(string cacheKey,List<T> objectList)
        {
            var data = new List<T>();
            if (!cache.TryGetValue(cacheKey, out data))
            {
                data =objectList;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(600))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(700));

                cache.Set(cacheKey, data, cacheEntryOptions);
            }

            return data;
        }
    }
}
