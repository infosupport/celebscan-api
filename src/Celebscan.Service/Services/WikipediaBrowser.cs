using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Celebscan.Service.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Celebscan.Service.Utils;
    
namespace Celebscan.Service.Services
{
    /// <summary>
    /// Implementation of the wikipedia browser
    /// </summary>
    public class WikipediaBrowser : IWikipediaBrowser
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of <see cref="WikipediaBrowser"/>
        /// </summary>
        public WikipediaBrowser()
        {
            _httpClient = new HttpClient(new HttpClientHandler());
            _httpClient.BaseAddress = new Uri("https://en.wikipedia.org/w/api.php", UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Finds a single celebrity on Wikipedia
        /// </summary>
        /// <param name="name">Name of the famous person</param>
        /// <returns>Returns the famous person if it could be found in Wikipedia</returns>
        public async Task<Celebrity> FindCelebrity(string name)
        {
            var searchResults = await Search(name);

            if (!searchResults.Any()) return null;

            var mostLikelyResult = MostLikelyResult(searchResults, name);

            if (mostLikelyResult == null) return null;
            
            var lookupResults = await Task.WhenAll(
                FindPage(mostLikelyResult.PageId),
                FindImage(mostLikelyResult.PageImage));

            return new Celebrity(mostLikelyResult.Title,
                lookupResults[0], lookupResults[1],
                mostLikelyResult.Extract);
        }

        /// <summary>
        /// Finds the most likely match in the set of search results
        /// </summary>
        /// <param name="searchResults">Search results to enumerate</param>
        /// <param name="name">Name of the person we're looking for</param>
        /// <returns></returns>
        private WikipediaMatch MostLikelyResult(IEnumerable<WikipediaMatch> searchResults, string name)
        {
            var normalizedName = name.UnicodeNormalize().ToLower();
            
            return searchResults.FirstOrDefault(m =>
                String.Compare(m.Title.UnicodeNormalize(), normalizedName, StringComparison.OrdinalIgnoreCase) == 0 ||
                m.Title.UnicodeNormalize().ToLower().StartsWith(normalizedName));
        }

        private async Task<List<WikipediaMatch>> Search(string name)
        {
            var requestUrl =
                "?action=query&generator=search&format=json&exintro&exsentences=1&exlimit=max&gsrlimit=20&" +
                $"gsrsearch=hastemplate:Birth_date_and_age+{Uri.EscapeDataString(name)}&pithumbsize=100&pilimit=max&" +
                "prop=pageimages%7Cextracts&origin=*";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.OK) return null;

            dynamic responseData = await DeserializeResponse(response);

            var pages = (JObject) responseData.query?.pages;
            var result = new List<WikipediaMatch>();

            if (pages == null) return result;
            
            foreach (var key in pages.Properties().Select(x => x.Name))
            {
                dynamic page = pages[key];
                
                result.Add(new WikipediaMatch(
                    page.pageid.ToString(),
                    page.title.ToString(),
                    page.extract.ToString(),
                    page.pageimage?.ToString()));
            }

            return result;
        }

        /// <summary>
        /// Find the full URL for the specified page ID
        /// </summary>
        /// <param name="pageId">Page ID to lookup</param>
        /// <returns>Returns the URL for the page</returns>
        private async Task<string> FindPage(string pageId)
        {
            var requestUrl = "https://en.wikipedia.org/w/api.php?action=query&" +
                             $"prop=info&pageids={pageId}&inprop=url&origin=*&format=json";
            
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.OK) return null;

            dynamic responseData = await DeserializeResponse(response);
            string url = responseData.query?.pages?[pageId].fullurl;

            return url;
        }

        /// <summary>
        /// Find the image for the specified image source
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private async Task<string> FindImage(string source)
        {
            if (source == null) return null;

            var requestUrl = $"?action=query&titles=Image:{Uri.EscapeDataString(source)}&prop=imageinfo&iiprop=url&origin=*&format=json";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode != HttpStatusCode.OK) return null;

            dynamic responseData = await DeserializeResponse(response);
            string url = responseData.query?.pages?["-1"].imageinfo[0].url;

            return url;
        }

        /// <summary>
        /// Deserializes the response
        /// </summary>
        /// <param name="responseMessage">Response message to read</param>
        /// <returns>Deserialized response content</returns>
        private async Task<dynamic> DeserializeResponse(HttpResponseMessage responseMessage)
        {
            var rawContent = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<dynamic>(rawContent);
        }
    }
}