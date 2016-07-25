using RedisClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPerformanceTest
{
    static class RedisMain
    {
        static void Main(string[] args)
        {
            RedisPubSubManager.Init("localhost");
            RedisProfilingSetup(true);
            new PersonCachePopulator().Populate();
            RedisProfilingSetup(false);
            Console.ReadLine();

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

                foreach (var item in data)
                {
                    Console.WriteLine(item.ElapsedTime);
                }
            }
        }
    }

    public class PersonCachePopulator
    {
        public void Populate()
        {
            var personData = RandomizePersons();

            //new System.Threading.Tasks.TaskFactory().StartNew(() =>
            //{

            //});
            //personData.ForEach(p =>
            //            {
            //                RedisCacheManager.Add2Cache<Person>(p.Id.ToString(), p);
            //            });
            Parallel.ForEach(personData, (p) =>
            {
                Task.Run(() => RedisCacheManager.Add2Cache<Person>(p.Id.ToString(), p));
            });
        }

        public List<Person> RandomizePersons()
        {
            var persons = new List<Person>();
            int i = 0;

            while (i++ < 100000)
                persons.Add(new Person
                {
                    Id = Guid.NewGuid(),
                    Email = Path.GetRandomFileName() + "@gmail.com",
                    Name = Path.GetRandomFileName()
                });

            return persons;
        }

    }

    public class Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
    }
}
