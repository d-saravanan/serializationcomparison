using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GzipSample
{
    public static class UserStore
    {
        internal static ConcurrentDictionary<string, string> _userStore = new ConcurrentDictionary<string, string>();

        public static string Store(string userDetails, string id)
        {
            if (userDetails == null) return id;

            if (!_userStore.TryAdd(id, userDetails))
            {
                Trace.Write("oopsy daisy, somethin went wrong...");
            }
            return id;
        }

        public static string Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            string result = null;
            if (!_userStore.TryGetValue(id, out result))
                Trace.WriteLine("Ouch, we a'int gettin the values...");

            return result;
        }

        public static IEnumerable<string> GetAll()
        {
            foreach (var item in _userStore)
            {
                yield return item.Value;
            }
        }
    }
}
