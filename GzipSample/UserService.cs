using System;
using System.Collections.Generic;
namespace GzipSample
{
    public class JSONStoreBasedUserService
    {
        public string Add(UserDetails userDetails)
        {
            if (userDetails == null) return null;

            var dataString = PerfTimer.Time(Newtonsoft.Json.JsonConvert.SerializeObject, userDetails, userDetails.Identifier);
            //var dataString = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);

            //PerfMonitor.Time(UserStore.Store, userDetails.Identifier, dataString, userDetails.Identifier);
            UserStore.Store(dataString, userDetails.Identifier);

            return userDetails.Identifier;
        }

        public string AddWithCompression(UserDetails userDetails)
        {
            if (userDetails == null) return null;

            string dataString = null;
            PerfTimer.Time((user) =>
            {
                dataString = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                dataString = CompressionWrapper.Compress(dataString);
                return dataString;
            }, userDetails, userDetails.Identifier);

            UserStore.Store(dataString, userDetails.Identifier);

            return userDetails.Identifier;
        }

        public UserDetails Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;

            var dataString = UserStore.Get(id);

            var result = PerfTimer.Time(Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>, dataString, id);

            return result;
        }

        public IEnumerable<UserDetails> GetAll()
        {
            foreach (var item in UserStore.GetAll())
            {
                yield return PerfTimer.Time(Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>, item, Guid.NewGuid().ToString());
            }
        }

        public IEnumerable<UserDetails> GetAllUnCompressed()
        {
            foreach (string item in UserStore.GetAll())
            {
                yield return PerfTimer.Time((comprValue) =>
                {
                    comprValue = CompressionWrapper.DeCompress(comprValue);
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetails>(comprValue);
                }, item, Guid.NewGuid().ToString());
            }
        }
    }
}
