namespace Candidates.Core.Cache.Abstractions
{
    public interface ICache
    {
        object Get(string key);
        void Remove(string key);
        void Add(string key, object item);
    }
}