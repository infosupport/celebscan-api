using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Celebscan.Service.Models;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Implementation of the permalink generator
    /// </summary>
    public class PermalinkGenerator: IPermalinkGenerator
    {
        private readonly IPermalinkStorage _storage;

        /// <summary>
        /// Initializes a new instance of <see cref="PermalinkGenerator"/>
        /// </summary>
        /// <param name="storage"></param>
        public PermalinkGenerator(IPermalinkStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// Generates a permalink for a scan performed by the user
        /// </summary>
        /// <param name="parameters">Parameters to use for the generator</param>
        /// <returns></returns>
        public async Task<string> Generate(PermalinkGeneratorParameters parameters)
        {
            var permalink = await _storage.Save(Permalink.FromParameters(ComputeHashKey(parameters), parameters));
            return permalink.Id;
        }

        /// <summary>
        /// Computes the hash key for the permalink
        /// </summary>
        /// <param name="parameters">Parameters to use for the hashing algorithm</param>
        /// <returns>Returns the generated identifier</returns>
        private string ComputeHashKey(PermalinkGeneratorParameters parameters)
        {
            HashAlgorithm alg = new HMACSHA512();

            using (var hashStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(hashStream))
                {
                    writer.Write(parameters.ImageData);
                    writer.Write(parameters.Label);
                    writer.Write(parameters.Score);

                    foreach (var score in parameters.Scores)
                    {
                        writer.Write(score.Label);
                        writer.Write(score.Score);
                    }
                
                    var hash = alg.ComputeHash(hashStream);

                    return Convert.ToBase64String(hash);
                }
            }
        }
    }
}