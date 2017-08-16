using System.Collections.Generic;
using System.Threading.Tasks;
using Celebscan.Service.Models;
using Celebscan.Service.Services;
using Moq;
using Xunit;

namespace Celebscan.Service.Tests.Services
{
    public class PermalinkGeneratorTests
    {
        private readonly Mock<IPermalinkStorage> _permalinkStorage;
        private readonly IPermalinkGenerator _permalinkGenerator;

        public PermalinkGeneratorTests()
        {
            _permalinkStorage = new Mock<IPermalinkStorage>();
            _permalinkGenerator = new PermalinkGenerator(_permalinkStorage.Object);
        }
        
        [Fact]
        public async Task GenerateReturnsValidUrlIdentifier()
        {
            _permalinkStorage
                .Setup(mock => mock.Save(It.IsAny<Permalink>()))
                .ReturnsAsync((Permalink link) => link);
            
            var result = await _permalinkGenerator.Generate(new PermalinkGeneratorParameters()
            {
                ImageData = "bla",
                Label = "bla",
                Score = 0.1,
                Scores = new List<ScanResult>()
            });
            
            Assert.NotNull(result);
        }
    }
}