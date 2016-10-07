using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Candidates.DataAccess.Cache
{
    public class InMemoryCache : ICache
    {
        public void Add(string key, object item)
        {
            MemoryCache.Default.Add(key, item, DateTime.Now.AddMinutes(10));
        }

        public object Get(string key) 
        {
            return MemoryCache.Default.Get(key);
        }

        public void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }
    }
}
