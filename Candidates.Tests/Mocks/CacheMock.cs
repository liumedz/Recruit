using System.Collections.Generic;
using Candidates.Core.Cache;
using Candidates.Core.Cache.Abstractions;

namespace Candidates.Tests.Mocks
{
    public class CacheMock : ICache
    {
        private Dictionary<string, object> _cache = new Dictionary<string, object>();
        public bool GetCalled { get; set; }
        public bool AddCalled { get; set; }
        public bool RemoveCalled { get; set; }
        public void Add(string key, object item)
        {
            _cache.Add(key, item);
            AddCalled = true;
        }

        public object Get(string key)
        {
            GetCalled = true;
            object value;
            _cache.TryGetValue(key, out value);
            return value;
        }

        public void Remove(string key)
        {
            RemoveCalled = true;
            _cache.Remove(key);
        }
    }
}
