using RedisClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisWithLocalCache
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisWithLocalCacheManager.init();

            var d = RedisWithLocalCacheManager.Get<Data>("data_ab");

            if (d == null)
            {
                d = new Data { Id = "data_ab", Name = Path.GetRandomFileName() };
                RedisWithLocalCacheManager.Add2Cache(d.Id, d);
            }

            Console.WriteLine(" id is : " + d.Id + " & name is : " + d.Name);

            Thread.Sleep(2500);


            Console.WriteLine("call the other app to update the data");
            Console.ReadLine();

            d = RedisWithLocalCacheManager.Get<Data>("data_abc");
            Console.WriteLine(" id is : " + d.Id + " & name is : " + d.Name);
            Console.ReadLine();

            d = new Data { Id = "data_abcd", Name = "test" };
            RedisWithLocalCacheManager.Delete(d.Id);
            RedisWithLocalCacheManager.Add2Cache(d.Id, d);

            Thread.Sleep(5000);
            Console.WriteLine("app 2 has updated data, use app 1 to get and test ");
            Console.ReadLine();

        }
    }


    public class Data
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
