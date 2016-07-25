using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClient
{
    public static class RedisCacheManager
    {
        private static IDatabase _db = null;

        static RedisCacheManager()
        {
            //RedisPubSubManager.Init("localhost");
            _db = RedisPubSubManager.redis.GetDatabase(0);
        }

        public static void Add2Cache<T>(string key, T o)
        {
            var value = JsonConvert.SerializeObject(o);
            _db.StringSet(key, value);
        }

        public static T Get<T>(string key)
        {
            var val = _db.StringGet(key);
            if (!string.IsNullOrEmpty(val))
                return JsonConvert.DeserializeObject<T>(val);
            return default(T);
        }

        public static void Delete(string key)
        {
            _db.StringSet(key, string.Empty);
        }
    }
}
