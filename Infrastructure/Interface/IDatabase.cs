using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Interface
{
    public interface IDatabase
    {
        public Task InsertAsync<TDocument>(TDocument data);

        public Task<IEnumerable<TDocument>> FindAsync<TDocument>(FilterDefinition<TDocument> filter, FindOptions<TDocument, TDocument> options = null);

        public IMongoCollection<TDocument> GetCollection<TDocument>();

        public Task UpdateAsync<TDocument>(Expression<Func<TDocument, bool>> condition, TDocument data);
    }
}