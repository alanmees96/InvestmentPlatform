using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletAPI.Model;

namespace WalletAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private async Task<Wallet> FindWallet(long cpf)
        {
            var filter = Builders<Wallet>.Filter.Eq(x => x.Owner.CPF, cpf);

            var wallets = await _database.FindAsync(filter);

            return wallets.FirstOrDefault();
        }

        private async Task InsertNewWaller()
        {
            var newWallet = new Wallet()
            {
                Owner = new Owner()
                {
                    CPF = 30279747349,
                    Name = "Ester Gabrielly Ribeiro"
                },
                Shares = new List<Share>()
            };

            await _database.InsertAsync(newWallet);
        }

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDatabase _database;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        private async Task AddBuyShare(long cpf, Share newShare)
        {
            var wallet = await FindWallet(cpf);

            wallet.Shares.Add(newShare);

            var total = newShare.Amount * newShare.PurchasePrice;

            wallet.Avaliable -= total;
            wallet.Invested += total;

            wallet.Shares.Add(newShare);

            var collection = _database.GetCollection<Wallet>();

            var update = Builders<Wallet>.Update.Set(x => x.Avaliable, wallet.Avaliable);
            update = update.Set(x => x.Invested, wallet.Invested);
            update = update.Set(x => x.Shares, wallet.Shares);

            await collection.UpdateOneAsync(x => x._id == wallet._id, update);
        }

        [HttpGet]
        public async Task<Wallet> Get()
        {

            var cpf = 30279747349;

            var newShare = new Share()
            {
                Symbol = "SANB11",
                Amount = 3,
                PurchasePrice = 40.77
            };

            await AddBuyShare(cpf, newShare);

            //return sharesTrend.FirstOrDefault();
            return default;
        }
    }
}
