using System;

namespace Candidates.Core.Cache.Abstractions
{
    public interface ICacheService
    {
        T Get<T>(string key, Func<T> getItemCallback) where T : class;
        void Invalidate(string key);
    }
}
