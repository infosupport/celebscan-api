using System.Threading.Tasks;
using Celebscan.Service.Models;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Generates permalinks for scans
    /// </summary>
    public interface IPermalinkGenerator
    {
        /// <summary>
        /// Generates a permalink for a scan performed by the user
        /// </summary>
        /// <param name="parameters">Parameters to use for the generator</param>
        /// <returns></returns>
        Task<string> Generate(PermalinkGeneratorParameters parameters);
    }
}