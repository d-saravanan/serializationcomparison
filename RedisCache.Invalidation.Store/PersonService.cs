using RedisClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache.Invalidation.Store
{
    public class PersonService
    {
        static PersonDAL _dal;

        public PersonService()
        {
            _dal = new PersonDAL();
        }

        public void Store(person p)
        {
            if (p == null) return;
            InMemoryStore<person>.Store("person_" + p.id, p);
            _dal.Add(p);
        }

        public person Get(string id)
        {
            var data = InMemoryStore<person>.Get("person_" + id);

            if (data != null) return data;
            data = _dal.Get(id);
            InMemoryStore<person>.Store("person_" + data.id, data);
            return data;
        }

        public void Update(person p)
        {
            InMemoryStore<person>.Delete("person_" + p.id);
            RedisPubSubManager.Publish(p.id.ToString());
            InMemoryStore<person>.Store("person_" + p.id, p);
            _dal.Update(p);
        }

        public void DeleteFromCache(string id)
        {
            // on callback, remove the key.
            // on delete, publish the removal of the key
            InMemoryStore<person>.Delete(id);
            RedisPubSubManager.Publish(id);

        }
    }
}
