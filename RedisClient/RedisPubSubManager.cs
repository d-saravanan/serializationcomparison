using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisClient
{
    public static class RedisPubSubManager
    {
        internal static ConnectionMultiplexer redis = null;
        static RedisChannel rc = new RedisChannel();
        static ISubscriber subscriber = null;

        // client app should call this to ensure that the MuX is initialized b4 doing anything
        public static void Init(string connectionString)
        {
            redis = ConnectionMultiplexer.Connect(connectionString);
            rc = new RedisChannel("cache/invalidate", RedisChannel.PatternMode.Literal);
            subscriber = redis.GetSubscriber();

            // use any IoC to detect if there is any profiler registered and if so, add them here...
            redis.RegisterProfiler(new RedisAppClientProfiler());
        }

        public static void StartProfiling(object forContext)
        {
            redis.BeginProfiling(forContext);
        }

        public static ProfiledCommandEnumerable EndProfiling(object forContext)
        {
            return redis.FinishProfiling(forContext, false);
        }

        public static void SetSubscriber(Action<RedisChannel, RedisValue> clientSubscriber)
        {
            if (subscriber != null && clientSubscriber != null)
                subscriber.Subscribe(rc, clientSubscriber);
        }

        public static void Publish(RedisValue value)
        {
            if (subscriber != null)
                subscriber.Publish(rc, value);
        }
    }
}
