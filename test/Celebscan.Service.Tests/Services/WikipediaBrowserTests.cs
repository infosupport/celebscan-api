using System.Threading.Tasks;
using Celebscan.Service.Services;
using Xunit;

namespace Celebscan.Service.Tests.Services
{
    public class WikipediaBrowserTests
    {
        private readonly IWikipediaBrowser _wikipediaBrowser;

        public WikipediaBrowserTests()
        {
            _wikipediaBrowser = new WikipediaBrowser();
        }

        [Fact]
        public async Task FindCelebrityForExistingPersonReturnsResult()
        {
            var result = await _wikipediaBrowser.FindCelebrity("Uhuru Kenyatta");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindCelebrityForNonExistingPersonReturnsNull()
        {
            var result = await _wikipediaBrowser.FindCelebrity("Wacka Persona");
            Assert.Null(result);
        }

        [Fact]
        public async Task FindCelebrityWithoutFullMatchReturnsProperResult()
        {
            var result = await _wikipediaBrowser.FindCelebrity("Maria Valverde");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindCelebrityWithPostfixMatchReturnsProperResult()
        {
            var result = await _wikipediaBrowser.FindCelebrity("Doug Jones");
            Assert.NotNull(result);
        }
    }
}