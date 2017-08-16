using Celebscan.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Translates permalink codes into a full URL
    /// </summary>
    public interface IPermalinkUrlTranslator
    {
        /// <summary>
        /// Translates the permalink into a proper URL 
        /// </summary>
        /// <param name="urlHelper">URL helper to use</param>
        /// <param name="code">Permalink code</param>
        /// <returns>Returns the translated URL</returns>
        string Translate(IUrlHelper urlHelper, string code);
    }
}