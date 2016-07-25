using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache.Invalidation.Store
{
    public static class InMemoryStore<T>
    {
        private static MemoryCache _default = MemoryCache.Default;

        public static void Store(string key, T data)
        {
            if (data == null) return;

            // publish to redis about this new addition

            _default.Add(key, data, new CacheItemPolicy
            {

            });
        }

        public static T Get(string key)
        {
            if (_default.Contains(key))
                return (T)_default[key];
            return default(T);
        }

        public static void Delete(string key)
        {
            if (_default.Contains(key))
                _default.Remove(key);
        }
    }

}
