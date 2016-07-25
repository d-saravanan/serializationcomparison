using System;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace GzipSample
{
    public static class PerfTimer
    {
        internal static ConcurrentDictionary<string, long> _perfDataStore = new ConcurrentDictionary<string, long>();
        private static Stopwatch sw = new Stopwatch();

        public static B Time<A, C, B>(Func<A, C, B> func, A arg0, C arg1, string key)
        {
            sw.Reset();
            sw.Start();
            B res = func(arg0, arg1);
            sw.Stop();
            _perfDataStore.TryAdd(key, sw.ElapsedTicks);
            return res;
        }

        public static B Time<A, B>(Func<A, B> func, A arg, string key)
        {
            sw.Reset();
            sw.Start();
            B res = func(arg);
            sw.Stop();
            _perfDataStore.TryAdd(key, sw.ElapsedTicks);
            return res;
        }
    }
}
