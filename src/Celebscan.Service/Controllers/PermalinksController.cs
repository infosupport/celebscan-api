using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Celebscan.Service.Models;
using Celebscan.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Celebscan.Service.Controllers
{
    public class PermalinksController: Controller
    {
        private readonly IPermalinkGenerator _permalinkGenerator;
        private readonly IPermalinkUrlTranslator _permalinkUrlTranslator;

        public PermalinksController(
            IPermalinkGenerator permalinkGenerator, 
            IPermalinkUrlTranslator permalinkUrlTranslator)
        {
            _permalinkGenerator = permalinkGenerator;
            _permalinkUrlTranslator = permalinkUrlTranslator;
        }

        [HttpPost]
        [Route("/api/permalinks")]
        public async Task<IActionResult> CreatePermalink([FromBody] PermalinkGeneratorParameters parameters)
        {
            TryValidateModel(parameters);
            
            if (!ModelState.IsValid)
            {
                return BadRequest(TranslateErrors());
            }
            
            var linkCode = await _permalinkGenerator.Generate(parameters);
            var linkUrl = _permalinkUrlTranslator.Translate(Url, linkCode);

            return Json(new PermalinkData(linkUrl));
        }

        private Dictionary<string, List<string>> TranslateErrors()
        {
            return ModelState.Keys.ToDictionary(
                key => key, 
                key => ModelState[key].Errors.Select(error => error.ErrorMessage).ToList());
        }
    }
}