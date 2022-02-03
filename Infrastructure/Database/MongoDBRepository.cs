using Infrastructure.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class MongoDBRepository : IDatabase
    {
        public IMongoCollection<TDocument> GetCollection<TDocument>()
        {
            return _database.GetCollection<TDocument>(typeof(TDocument).Name);
        }

        private readonly IMongoDatabase _database;

        public MongoDBRepository(string connectionString, string database)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);

            _database = client.GetDatabase(database);
        }

        public async Task InsertAsync<TDocument>(TDocument data)
        {
            var collectionTest = GetCollection<TDocument>();

            await collectionTest.InsertOneAsync(data);
        }

        public async Task<IEnumerable<TDocument>> FindAsync<TDocument>(FilterDefinition<TDocument> filter, FindOptions<TDocument, TDocument> options = null)
        {
            var collection = GetCollection<TDocument>();

            var result = await collection.FindAsync(filter, options);

            return result.ToEnumerable();
        }

        public async Task UpdateAsync<TDocument>(Expression<Func<TDocument, bool>> condition, TDocument data)
        {
            var collection = GetCollection<TDocument>();

            await collection.ReplaceOneAsync(condition, data);
        }
    }
}