using Infrastructure.Interface;
using MongoDB.Driver;
using StockExchange.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchange.Core
{
    public class ShareWorker
    {
        private readonly IDatabase _database;

        public ShareWorker(IDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Share>> Trend(int size)
        {
            var filter = Builders<Share>.Filter.Empty;
            var sort = Builders<Share>.Sort.Descending(x => x.SelledQuantity);

            var options = new FindOptions<Share, Share>()
            {
                Sort = sort,
                Limit = 5
            };

            var sharesTrend = await _database.FindAsync(filter, options);

            return sharesTrend;
        }
    }
}