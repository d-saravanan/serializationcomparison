using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using RedisCache.Invalidation.Store;
using RedisClient;

namespace Redis.CacheInvalidation.Sample
{
    class Program
    {
        static void RedisInit()
        {
            RedisPubSubManager.Init("localhost");
            RedisPubSubManager.SetSubscriber((channel, message) =>
            {
                Console.WriteLine("channel: " + channel + " , message: " + message);
            });
            RedisProfilingSetup();
        }

        static string profileId = null;
        static RedisAppClientProfiler _profiler = new RedisAppClientProfiler();

        static void RedisProfilingSetup(bool start = true)
        {
            if (start)
            {
                var ctx = _profiler.CreateContext();
                profileId = ctx.Item1;
                RedisPubSubManager.StartProfiling(ctx.Item2);
            }
            else
            {
                var ctx = _profiler.GetContext(profileId);
                var data = RedisPubSubManager.EndProfiling(ctx);
            }
        }

        static void Main(string[] args)
        {
            RedisInit();
            Console.WriteLine("data setter");
            string testid = "8e25417c-7e48-4389-bad7-ce64027fd467";

            var me = new person
            {
                age = 25,
                email = "someone@cient1.com",
                id = Guid.Parse(testid),
                name = "client 1 user"
            };

            //1. store
            new PersonService().Store(me);

            //2. cache and get
            var p = new PersonService().Get(testid);

            Console.WriteLine("Now, get the data from the other app and press enter key");
            string pause = Console.ReadLine();

            //3. update, all the other caches become stale now
            p.name += "...";
            new PersonService().Update(p);

            Console.WriteLine("update completed");
            //4. get and see the new data just for verification
            p = null;
            p = new PersonService().Get(testid);
            if (p.name.EndsWith("..."))
                Console.WriteLine("Awesome job...");
            else
                Console.WriteLine("Somethin is screwed up dude...");

            RedisProfilingSetup(false);

            Console.ReadKey();
        }
    }

}

