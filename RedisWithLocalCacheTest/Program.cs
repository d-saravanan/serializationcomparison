using RedisClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisWithLocalCacheTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisWithLocalCacheManager.init();

            var d = new Data
            {
                Id = "ab",
                Name = Path.GetRandomFileName()
            };

            RedisWithLocalCacheManager.Add2Cache("data_" + d.Id, d);

            Console.WriteLine("call the other app to get the data and display");
            Console.ReadLine();

            Thread.Sleep(2500);

            RedisWithLocalCacheManager.Delete("data_" + d.Id);
            d.Id += "c";
            RedisWithLocalCacheManager.Add2Cache("data_" + d.Id, d);
            Thread.Sleep(2500);

            Console.WriteLine("updated the data in the cache...");
            Console.WriteLine("call the other app to read from cache and see if the data is ok i.e. id = abc");
            Console.ReadLine();

            d = RedisWithLocalCacheManager.Get<Data>("data_abcd");
            Console.WriteLine(" id is : " + d.Id + " & name is : " + d.Name);
            Console.ReadLine();
        }


    }

    public class Data
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
