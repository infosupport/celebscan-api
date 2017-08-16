using MongoDB.Driver;

namespace Celebscan.Service.Tests.Helpers
{
    /// <summary>
    /// Test helper class to make access to MongoDB slightly easier
    /// </summary>
    public class MongoDatabase
    {
        /// <summary>
        /// Gets a mongodb collection for test purposes
        /// </summary>
        /// <param name="connectionString">Connection string to the mongodb host</param>
        /// <param name="databaseName">Name of the database</param>
        /// <param name="collectionName">Name of the collection</param>
        /// <typeparam name="T">Type of document stored in the collection</typeparam>
        /// <returns>Returns the mongodb collection</returns>
        public static IMongoCollection<T> GetCollectionForTest<T>(
            string connectionString, 
            string databaseName,
            string collectionName)
        {
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(databaseName);

            // When the database exists, drop it. 
            // We want a clean database and collection for each new test.
            if (database != null)
            {
                mongoClient.DropDatabase(databaseName);
            }

            return database.GetCollection<T>(collectionName);
        }
    }
}