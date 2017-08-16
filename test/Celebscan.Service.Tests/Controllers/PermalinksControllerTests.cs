using System.Collections.Generic;
using System.Threading.Tasks;
using Celebscan.Service.Controllers;
using Celebscan.Service.Models;
using Celebscan.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Celebscan.Service.Tests.Controllers
{
    public class PermalinksControllerTests
    {
        private readonly Mock<IPermalinkUrlTranslator> _permalinkUrlTranslator;
        private readonly Mock<IPermalinkGenerator> _permalinkGenerator;
        private readonly PermalinksController _controller;

        public PermalinksControllerTests()
        {
            _permalinkGenerator = new Mock<IPermalinkGenerator>();
            _permalinkUrlTranslator = new Mock<IPermalinkUrlTranslator>();
            _controller = new PermalinksController(_permalinkGenerator.Object,_permalinkUrlTranslator.Object);
        }

        [Fact]
        public async Task GeneratePermalinkReturnsPermalinkUrl()
        {
            _permalinkUrlTranslator
                .Setup(mock => mock.Translate(It.IsAny<IUrlHelper>(), It.IsAny<string>()))
                .Returns((IUrlHelper urlHelper, string code) => $"http://localhost:8080/api/permalinks/{code}");

            _permalinkGenerator
                .Setup(mock => mock.Generate(It.IsAny<PermalinkGeneratorParameters>()))
                .ReturnsAsync("bladiebla");
            
            var result = await _controller.CreatePermalink(new PermalinkGeneratorParameters()
            {
                ImageData = "bla",
                Label = "bla",
                Score = 0.1,
                Scores = new List<ScanResult>()
            });

            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);

            var jsonResult = (JsonResult) result;
            var url = ((PermalinkData) jsonResult.Value).Url;
            
            Assert.Equal(url, "http://localhost:8080/api/permalinks/bladiebla");
        }
    }
}