
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
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

    public static class RedisWithLocalCacheManager
    {
        private static IDatabase _db = null;
        private static System.Runtime.Caching.MemoryCache _memCache = MemoryCache.Default;
        internal static ConnectionMultiplexer redis = null;

        //static RedisWithLocalCacheManager()
        //{
        //    redis = ConnectionMultiplexer.Connect("localhost");
        //    _db = RedisPubSubManager.redis.GetDatabase(0);
        //}
        public static void init()
        {
            redis = ConnectionMultiplexer.Connect("localhost");
            _db = redis.GetDatabase(0);
        }

        public static void Add2Cache<T>(string key, T o)
        {
            var localKey = string.Format("{0}_time", key);

            var ts = DateTime.UtcNow.Ticks.ToString();

            _db.KeyDelete(localKey);
            _db.StringSet(localKey, ts);
            _memCache.Add(localKey, ts, new CacheItemPolicy());
            _memCache.Add(key, o, new CacheItemPolicy());

            var value = JsonConvert.SerializeObject(o);
            _db.StringSet(key, value);
        }

        private static bool IsLocalFresh(string local, string server)
        {
            long srcTs = Convert.ToInt64(local), tgtTs = Convert.ToInt64(server);
            return srcTs <= tgtTs;
        }

        public static T Get<T>(string key)
        {
            var localKey = string.Format("{0}_time", key);
            var ts = DateTime.UtcNow.Ticks.ToString();

            string serverTime = _db.StringGet(localKey);
            string localTime = _memCache.Get(localKey) as string;

            if (string.IsNullOrEmpty(serverTime)) // no data in server...
            {
                _memCache.Remove(key);
                _memCache.Remove(localKey);
                return default(T);
            }

            if (string.IsNullOrEmpty(localTime)) // server has data, local has no data
            {
                var serverString = _db.StringGet(key);
                var serverData = JsonConvert.DeserializeObject<T>(serverString);
                _memCache.Add(localKey, serverTime, new CacheItemPolicy());
                _memCache.Add(key, serverData, new CacheItemPolicy());
                return serverData;
            }

            if (IsLocalFresh(localTime, serverTime))
                return (T)_memCache.Get(key);

            var val = _db.StringGet(key);
            if (!string.IsNullOrEmpty(val))
            {
                var result = JsonConvert.DeserializeObject<T>(val);
                _memCache.Add(key, result, new CacheItemPolicy());
                _memCache.Add(localKey, serverTime, new CacheItemPolicy());
            }
            return default(T);
        }

        public static void Delete(string key)
        {
            var localKey = string.Format("{0}_time", key);
            _memCache.Remove(localKey);
            _db.KeyDelete(localKey);

            _memCache.Remove(key);
            _db.KeyDelete(key);
        }
    }
}
