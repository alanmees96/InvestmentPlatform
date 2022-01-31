using Infrastructure.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trend.Core
{
    public class TrendWorker
    {
        private readonly IDatabase _database;

        public TrendWorker(IDatabase database)
        {
            _database = database;
        }

        public void GetTopFiveMoreSelled()
        {
            var filter = Builders<Trend>.Filter.Empty;
            var sort = Builders<Trend>.Sort.Descending(x => x.SelledQuantity);

            var collection = mongoClient.GetCollection<Trend>();

            var result = await collection.FindAsync(filter, new FindOptions<Trend, Trend>()
            {
                Sort = sort,
                Limit = 5
            });
        }
    }
}
