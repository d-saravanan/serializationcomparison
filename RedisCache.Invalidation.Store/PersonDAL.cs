using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisCache.Invalidation.Store
{
    public class PersonDAL
    {
        public person Get(string id)
        {
            using (var ctx = new PersonEntities())
            {
                var gid = Guid.Parse(id);
                return ctx.people.FirstOrDefault(p => p.id == gid);
            }
        }

        public void Add(person p)
        {
            using (var ctx = new PersonEntities())
            {
                try
                {
                    ctx.people.Add(p);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Trace.Write(ex);
                }
            }
        }

        public void Update(person p)
        {
            using (var ctx = new PersonEntities())
            {
                ctx.people.Attach(p);
                ctx.Entry<person>(p).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
