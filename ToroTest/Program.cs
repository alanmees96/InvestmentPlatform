using Infrastructure.Database;
using Infrastructure.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToroTest
{
    class Program
    {
        static async Task<IEnumerable<Share>> FindAlgo(MongoDBRepository mongoClient)
        {
            var filter = Builders<Share>.Filter.Empty;
            var sort = Builders<Share>.Sort.Descending(x => x.SelledQuantity);

            var collection = mongoClient.GetCollection<Share>();

            var result = await collection.FindAsync(filter, new FindOptions<Share, Share>()
            {
                Sort = sort,
                Limit = 5
            });

            return result.ToEnumerable();
        }

        static async Task FillMongo(MongoDBRepository mongoClient)
        {
            await mongoClient.InsertAsync(new Share()
            {
                CurrentPrice = 28.44,
                Symbol = "PETR4",
            });

            await mongoClient.InsertAsync(new Share()
            {
                CurrentPrice = 25.91,
                Symbol = "MGLU3",
                SelledQuantity = 3
            });


            await mongoClient.InsertAsync(new Share()
            {
                CurrentPrice = 25.91,
                Symbol = "VVAR3",
                SelledQuantity = 4
            });

            await mongoClient.InsertAsync(new Share()
            {
                CurrentPrice = 40.77,
                Symbol = "SANB11",
                SelledQuantity = 5
            });

            await mongoClient.InsertAsync(new Share()
            {
                CurrentPrice = 115.98,
                Symbol = "TORO4",
                SelledQuantity = 8
            });

            await mongoClient.InsertAsync(new Share()
            {
                CurrentPrice = 30.12,
                Symbol = "ITUB4"
            });

        }

        static async Task Main(string[] args)
        {

            var trend = new Share()
            {
                CurrentPrice = 30.12,
                Symbol = "PETR4"
            };

            var connectionString = "mongodb+srv://ToroInvestimento:gTS34bx5B7szNXqf@toroteste.z1llg.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
            var database = "Toro";

            var mongoClient = new MongoDBRepository(connectionString, database);

            var collection = mongoClient.GetCollection<Share>();

            await FillMongo(mongoClient);

            //var teste = await FindAlgo(mongoClient);

            //foreach (var item in teste)
            //{
            //    Console.WriteLine($"{item.Symbol} -- {item.SelledQuantity}");
            //}

            //Console.WriteLine("Hello World!");
        }
    }
}
