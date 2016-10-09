using Alachisoft.NCache.Web.Caching;
using Candidates.Core.Cache.Abstractions;

namespace Candidates.Core.Cache
{
    public class DistributedCache : ICache
    {
        private Alachisoft.NCache.Web.Caching.Cache _cache;
        public DistributedCache()
        {
            _cache = NCache.InitializeCache("Notes");
        }
        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Add(string key, object item)
        {
            _cache.Add(key, item);
        }
    }
}