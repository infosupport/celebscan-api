using System.Threading.Tasks;
using Celebscan.Service.Models;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Provides a way to browse wikipedia for data
    /// </summary>
    public interface IWikipediaBrowser
    {
        /// <summary>
        /// Finds a single celebrity on Wikipedia
        /// </summary>
        /// <param name="name">Name of the famous person</param>
        /// <returns>Returns the famous person if it could be found in Wikipedia</returns>
        Task<Celebrity> FindCelebrity(string name);
    }
}