using System;
using System.Threading.Tasks;
using Celebscan.Service.Controllers;
using Celebscan.Service.Models;
using Celebscan.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Celebscan.Service.Tests.Controllers
{
    public class CelebritiesControllerTests
    {
        private readonly CelebritiesController _controller;
        private readonly Mock<IWikipediaBrowser> _browser;
        private readonly Mock<ICelebrityCache> _celebrityCache;

        public CelebritiesControllerTests()
        {
            _celebrityCache = new Mock<ICelebrityCache>();
            _browser = new Mock<IWikipediaBrowser>();
            
            _controller = new CelebritiesController(_browser.Object, _celebrityCache.Object);
        }
        
        [Fact]
        public async Task GetPersonReturnsAnExistingPerson()
        {
            _celebrityCache
                .Setup(mock => mock.Find(It.IsAny<string>(), It.IsAny<Func<string, Task<Celebrity>>>()))
                .ReturnsAsync(new Celebrity("Doug Jones", "","",""));
            
            var response = await _controller.Get("Doug Jones");
            
            Assert.IsType<JsonResult>(response);
        }

        [Fact]
        public async Task GetPersonForNonExistingPersonReturnsNotFound()
        {
            _celebrityCache
                .Setup(mock => mock.Find(It.IsAny<string>(), It.IsAny<Func<string, Task<Celebrity>>>()))
                .ReturnsAsync((Celebrity)null);
            
            var response = await _controller.Get("Doug Jones");
            Assert.IsType<NotFoundResult>(response);
        }
    }
}