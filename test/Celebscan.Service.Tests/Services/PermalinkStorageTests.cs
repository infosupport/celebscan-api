using System.Collections.Generic;
using System.Threading.Tasks;
using Celebscan.Service.Configuration;
using Celebscan.Service.Models;
using Celebscan.Service.Services;
using Celebscan.Service.Tests.Helpers;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Xunit;

namespace Celebscan.Service.Tests.Services
{
    public class PermalinkStorageTests
    {
        private readonly IMongoCollection<Permalink> _permalinks;
        private readonly IPermalinkStorage _permalinkStorage;

        public PermalinkStorageTests()
        {
            var connectionString = "mongodb://localhost:27017/";

            _permalinks = MongoDatabase.GetCollectionForTest<Permalink>(
                connectionString, "celebscan", "permalinks");

            _permalinkStorage = new PermalinkStorage(new OptionsWrapper<DatabaseSettings>(new DatabaseSettings()
            {
                ConnectionString = connectionString
            }));
        }

        [Fact]
        public async Task SaveStoresThePermalink()
        {
            var result = await _permalinkStorage.Save(new Permalink()
            {
                Id = "bla",
                ImageData = "bla",
                Label = "bla",
                Score = 1.0,
                Scores = new List<ScanResult>()
            });

            var cursor = await _permalinks.FindAsync(Builders<Permalink>.Filter.Eq(x => x.Id, "bla"));
            var storedRecord = await cursor.FirstOrDefaultAsync();
            
            Assert.NotNull(storedRecord);
        }
    }
}