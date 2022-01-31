using Infrastructure.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletCore.Model.Database;

namespace WalletCore.Infrastructure
{
    public class WalletDatabase
    {
        private readonly IDatabase _database;

        public WalletDatabase(IDatabase database)
        {
            _database = database;
        }

        public void UpdateWallet(Wallet wallet, long cpf)
        {
            var update = Builders<Wallet>.Update.Set(x => x.Avaliable, wallet.Avaliable);
            update = update.Set(x => x.Invested, wallet.Invested);
            update = update.Set(x => x.Shares, wallet.Shares);

            var teste = _database.GetCollection<Wallet>();

            teste.ReplaceOne()

            _database.UpdateAsync(x => x.Owner.CPF.Equals(cpf), update);
        }
    }
}
