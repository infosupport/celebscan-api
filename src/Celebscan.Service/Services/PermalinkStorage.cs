using System.Threading.Tasks;
using Celebscan.Service.Configuration;
using Celebscan.Service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Celebscan.Service.Services
{
    /// <summary>
    /// Implementation of the permalink storage
    /// </summary>
    public class PermalinkStorage: IPermalinkStorage
    {
        private readonly IMongoCollection<Permalink> _permalinks;

        /// <summary>
        /// Initializes a new instance of <see cref="PermalinkStorage"/>
        /// </summary>
        /// <param name="options"></param>
        public PermalinkStorage(IOptions<DatabaseSettings> options)
        {
            const string collectionName = "permalinks";
            const string databaseName = "celebscan";
            
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var database = mongoClient.GetDatabase(databaseName);

            _permalinks = database.GetCollection<Permalink>(collectionName);
         
            if (_permalinks == null)
            {
                database.CreateCollection(collectionName);
                _permalinks = database.GetCollection<Permalink>(collectionName);
            }
        }

        /// <summary>
        /// Saves a new permalink in the database
        /// </summary>
        /// <param name="link">Link data to store</param>
        /// <returns>Returns the stored permalink</returns>
        public async Task<Permalink> Save(Permalink link)
        {
            await _permalinks.InsertOneAsync(link);

            return link;
        }
    }
}