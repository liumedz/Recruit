using Candidates.Tests.Mocks;
using Candidates.Core.Cache;
using Xunit;

namespace Candidates.Tests
{
    public class CacheServiceTests
    {
        [Fact]
        public void Should_Add_New_Item_To_Cache()
        {
            var cache = new CacheMock();
            var cacheService = new CacheService(cache);

            var item = new object();
            cacheService.Get("mykey", () => { return item; });
            var cachedItem = cacheService.Get("mykey", () => { return item; });
            Assert.Same(item, cachedItem);
            Assert.True(cache.GetCalled);
            Assert.False(cache.RemoveCalled);
            Assert.True(cache.AddCalled);
        }
        [Fact]
        public void Should_Remove_Item_From_Cache()
        {
            var cache = new CacheMock();
            var cacheService = new CacheService(cache);

            var item = new object();
            cacheService.Get("mykey", () => { return item; });
            cacheService.Invalidate("mykey");
            var updatedItem = new object();
            var cachedItem = cacheService.Get("mykey", () => { return updatedItem; });
            Assert.Same(updatedItem, cachedItem);
            Assert.True(cache.GetCalled);
            Assert.True(cache.RemoveCalled);
            Assert.True(cache.AddCalled);


        }
    }
}
