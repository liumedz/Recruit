using System;
using Candidates.Core.Cache.Abstractions;

namespace Candidates.Core.Cache
{
    public class CacheService : ICacheService
    {
        private ICache _cache;
        public CacheService(ICache cache)
        {
            _cache = cache;
        }
        public T Get<T>(string key, Func<T> getItemCallback) where T : class
        {
            T item = _cache.Get(key) as T;
            if (item == null)
            {
                item = getItemCallback();
                _cache.Add(key, item);
            }
            return item;
        }

        public void Invalidate(string key)
        {
            _cache.Remove(key);
        }
    }
}
