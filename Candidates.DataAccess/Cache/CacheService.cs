using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
namespace Candidates.DataAccess.Cache
{
    public class CacheService : ICacheService
    {
        public T Get<T>(string key, Func<T> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(key) as T;
            if (item == null)
            {   
                item = getItemCallback();
                MemoryCache.Default.Add(key, item, DateTime.Now.AddMinutes(10));
            }
            return item;
        }

        public void Invalidate(string key)
        {
            MemoryCache.Default.Remove(key);
        }
    }
}
