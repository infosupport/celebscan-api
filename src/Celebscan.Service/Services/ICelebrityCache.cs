using System;
using System.Threading.Tasks;
using Celebscan.Service.Models;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Performs automatic caching of celebrity data
    /// </summary>
    public interface ICelebrityCache
    {
        /// <summary>
        /// Finds a celebrity in the cache. 
        /// When the celebrity is not in the cache the fallback function is invoked
        /// to retrieve the celebrity. The result is automatically stored in the cache.
        /// </summary>
        /// <param name="key">Key for the celebrity</param>
        /// <param name="fallbackAction">Fallback action to use</param>
        /// <returns>Returns the found celebrity</returns>
        Task<Celebrity> Find(string key, Func<string, Task<Celebrity>> fallbackAction);
    }
}