using System;
using System.Runtime.Caching;
using Candidates.Core.Cache.Abstractions;

namespace Candidates.Core.Cache
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
