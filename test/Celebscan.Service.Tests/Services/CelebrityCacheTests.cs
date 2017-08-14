using System;
using System.Threading.Tasks;
using Celebscan.Service.Models;
using Celebscan.Service.Services;
using Moq;
using Xunit;

namespace Celebscan.Service.Tests.Services
{
    public class CelebrityCacheTests
    {
        private readonly Mock<ICelebrityStorage> _storage;
        private readonly ICelebrityCache _cache;
        
        public CelebrityCacheTests()
        {
            _storage = new Mock<ICelebrityStorage>();
            _cache = new CelebrityCache(_storage.Object);    
        }

        [Fact]
        public async Task FindForNonCachedItemExecutesCallback()
        {
            var callbackInvoked = false;

            _storage.Setup(mock => mock.FindByName(It.IsAny<String>())).ReturnsAsync((Celebrity)null);
            
            var callback = new Func<string, Task<Celebrity>>(name =>
            {
                callbackInvoked = true;
                return Task.FromResult(new Celebrity("Jimmy", "", "", ""));
            });
            
            var result = await _cache.Find("Jimmy", callback);
            
            Assert.NotNull(result);
            Assert.True(callbackInvoked);
        }

        [Fact]
        public async Task FindForCachedItemReturnsThatItem()
        {
            var callbackInvoked = false;
            var celebrity = new Celebrity("Jimmy", "", "", "");
            
            _storage.Setup(mock => mock.FindByName(It.IsAny<String>())).ReturnsAsync(celebrity);
            
            var callback = new Func<string, Task<Celebrity>>(name =>
            {
                callbackInvoked = true;
                
                return Task.FromResult(celebrity);
            });
            
            var result = await _cache.Find("Jimmy", callback);
            
            Assert.Equal(celebrity, result);
            Assert.False(callbackInvoked);
        }
    }
}