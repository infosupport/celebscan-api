using System.Threading.Tasks;
using Celebscan.Service.Configuration;
using Celebscan.Service.Models;
using Celebscan.Service.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Implementation of the celebrity storage
    /// </summary>
    public class CelebrityStorage : ICelebrityStorage
    {
        private readonly IMongoCollection<CelebrityCacheRecord> _celebrities;

        /// <summary>
        /// Initializes a new instance of <see cref="CelebrityStorage"/>
        /// </summary>
        /// <param name="options">Options for the storage</param>
        public CelebrityStorage(IOptions<DatabaseSettings> options)
        {
            const string collectionName = "celebrities";
            const string databaseName = "cache";
            
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var database = mongoClient.GetDatabase(databaseName);

            _celebrities = database.GetCollection<CelebrityCacheRecord>(collectionName);

            if (_celebrities == null)
            {
                database.CreateCollection(collectionName);
                _celebrities = database.GetCollection<CelebrityCacheRecord>(collectionName);
            }
        }

        /// <summary>
        /// Saves a celebrity in the storage
        /// </summary>
        /// <param name="celebrity">Celebrity to save</param>
        /// <returns>Returns the stored celebrity</returns>
        public async Task<Celebrity> Save(Celebrity celebrity)
        {
            await _celebrities.InsertOneAsync(CelebrityCacheRecord.FromCelebrity(celebrity));
            return celebrity;
        }

        /// <summary>
        /// Finds the celebrity by its name
        /// </summary>
        /// <param name="name">Name to find</param>
        /// <returns>Returns the found celebrity</returns>
        public async Task<Celebrity> FindByName(string name)
        {
            var filter = Builders<CelebrityCacheRecord>.Filter.Eq(x => x.Key, name.UnicodeNormalize().ToLower());
            
            var cursor = await _celebrities.FindAsync(filter);
            var foundRecord = await cursor.SingleOrDefaultAsync();

            if (foundRecord != null)
            {
                return Celebrity.FromCacheRecord(foundRecord);
            }

            return null;
        }
    }
}