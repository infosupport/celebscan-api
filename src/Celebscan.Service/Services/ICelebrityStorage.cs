using System.Threading.Tasks;
using Celebscan.Service.Models;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Provides a way to store celebrities
    /// </summary>
    public interface ICelebrityStorage
    {
        /// <summary>
        /// Saves a celebrity in the storage
        /// </summary>
        /// <param name="celebrity">Celebrity to save</param>
        /// <returns>Returns the stored celebrity</returns>
        Task<Celebrity> Save(Celebrity celebrity);

        /// <summary>
        /// Finds the celebrity by its name
        /// </summary>
        /// <param name="name">Name to find</param>
        /// <returns>Returns the found celebrity</returns>
        Task<Celebrity> FindByName(string name);
    }
}