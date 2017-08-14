using System;
using System.Threading.Tasks;
using Celebscan.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Celebscan.Service.Controllers
{
    /// <summary>
    /// Retrieves information about people on wikipedia
    /// </summary>
    public class CelebritiesController : Controller
    {
        private readonly IWikipediaBrowser _browser;
        private readonly ICelebrityCache _cache;

        /// <summary>
        /// Initializes a new instance of <see cref="CelebritiesController"/>
        /// </summary>
        /// <param name="browser">Wikipedia browser to use</param>
        /// <param name="cache">Cache to use</param>
        public CelebritiesController(IWikipediaBrowser browser, ICelebrityCache cache)
        {
            _browser = browser;
            _cache = cache;
        }

        /// <summary>
        /// Gets the wikipedia information for a person
        /// </summary>
        /// <param name="name">Name of the person to locate</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/celebrities")]
        public async Task<IActionResult> Get([FromQuery] string name)
        {
            var result = await _cache.Find(name, key => _browser.FindCelebrity(key));

            if (result == null)
            {
                return NotFound();
            }

            return Json(result);
        }
    }
}