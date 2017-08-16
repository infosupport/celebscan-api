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
    public class CelebrityStorageTests
    {
        private readonly IMongoCollection<CelebrityCacheRecord> _cacheRecords;
        private readonly ICelebrityStorage _celebrityStorage;

        public CelebrityStorageTests()
        {
            const string connectionString = "mongodb://localhost:27017";
            
            _cacheRecords = MongoDatabase.GetCollectionForTest<CelebrityCacheRecord>(
                connectionString, "cache","celebrities");

            _celebrityStorage = new CelebrityStorage(new OptionsWrapper<DatabaseSettings>(new DatabaseSettings()
            {
                ConnectionString = connectionString
            }));
        }

        [Fact]
        public async Task SaveStoresRecordInCollection()
        {
            var result = await _celebrityStorage.Save(new Celebrity("test", "test", "test", "test"));

            var cursor = await _cacheRecords.FindAsync(Builders<CelebrityCacheRecord>.Filter.Eq(x => x.Name, "test"));
            var storedRecord = await cursor.FirstOrDefaultAsync();
            
            Assert.NotNull(storedRecord);
        }

        [Fact]
        public async Task FindReturnsCelebrity()
        {
            await _celebrityStorage.Save(new Celebrity("test", "test", "test", "test"));
            
            var result = await _celebrityStorage.FindByName("test");
            
            Assert.NotNull(result);
        }
    }
}