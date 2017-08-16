using Microsoft.AspNetCore.Mvc;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Implementation of the permalink URL translator
    /// </summary>
    public class PermalinkUrlTranslator: IPermalinkUrlTranslator
    {
        /// <summary>
        /// Translates the permalink into a proper URL 
        /// </summary>
        /// <param name="urlHelper">URL helper to use</param>
        /// <param name="code">Permalink code</param>
        /// <returns>Returns the translated URL</returns>
        public string Translate(IUrlHelper urlHelper, string code)
        {
            return urlHelper.Action("GetPermalink", "Permalinks", new {code = ""});
        }
    }
}