using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClient
{
    public class RedisAppClientProfiler : StackExchange.Redis.IProfiler
    {
        static Dictionary<string, object> _redisContext = new Dictionary<string, object>();

        public Tuple<string, object> CreateContext()
        {
            var id = Guid.NewGuid().ToString();
            var ctx = new Object();
            _redisContext.Add(id, ctx);
            return new Tuple<string, object>(id, ctx);
        }

        public object GetContext()
        {
            var id = Guid.NewGuid().ToString();
            var ctx = new Object();
            _redisContext.Add(id, ctx);
            return ctx;
        }

        public object GetContext(string id)
        {
            if (_redisContext.ContainsKey(id))
            {
                var ctx = _redisContext[id];
                _redisContext.Remove(id);
                return ctx;
            }

            return null;
        }
    }

}
