using System;
using Microsoft.AspNetCore.Mvc;

namespace Celebscan.Service.Controllers
{
    /// <summary>
    /// Serves raw base64 data as a perma link
    /// </summary>
    public class ImageController : Controller
    {
        /// <summary>
        /// Gets a PNG image based on the provided base-64 data
        /// </summary>
        /// <param name="data">Data to serve</param>
        /// <returns>Returns the PNG image file</returns>
        [HttpGet]
        [Route("/api/image")]
        public IActionResult Get([FromQuery] string data)
        {
            if (String.IsNullOrEmpty(data))
            {
                return NotFound();
            }
            
            var pngData = Convert.FromBase64String(data);
            return File(pngData, "image/png");
        }
    }
}