using System.Threading.Tasks;
using Celebscan.Service.Models;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Provides storage for permalinks
    /// </summary>
    public interface IPermalinkStorage
    {
        /// <summary>
        /// Saves a new permalink in the database
        /// </summary>
        /// <param name="link">Link data to store</param>
        /// <returns>Returns the stored permalink</returns>
        Task<Permalink> Save(Permalink link);

        /// <summary>
        /// Finds a single permalink by its generated code
        /// </summary>
        /// <param name="code">The code for the permalink</param>
        /// <returns>Returns the permalink</returns>
        Task<Permalink> FindByCode(string code);
    }
}