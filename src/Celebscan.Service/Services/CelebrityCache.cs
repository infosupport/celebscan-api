using System;
using System.Threading.Tasks;
using Celebscan.Service.Models;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Caches celebrity data
    /// </summary>
    public class CelebrityCache : ICelebrityCache
    {
        private readonly ICelebrityStorage _storage;

        /// <summary>
        /// Initializes a new instance of <see cref="CelebrityCache"/>
        /// </summary>
        /// <param name="storage">Storage to use for the cache</param>
        public CelebrityCache(ICelebrityStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Finds a celebrity in the cache. 
        /// When the celebrity is not in the cache the fallback function is invoked
        /// to retrieve the celebrity. The result is automatically stored in the cache.
        /// </summary>
        /// <param name="key">Key for the celebrity</param>
        /// <param name="fallbackAction">Fallback action to use</param>
        /// <returns>Returns the found celebrity</returns>
        public async Task<Celebrity> Find(string key, Func<string, Task<Celebrity>> fallbackAction)
        {
            var foundCelebrity = await _storage.FindByName(key);

            if (foundCelebrity == null)
            {
                foundCelebrity = await fallbackAction(key);

                if (foundCelebrity != null)
                {
                    await _storage.Save(foundCelebrity);
                }
            }

            return foundCelebrity;
        }
    }
}