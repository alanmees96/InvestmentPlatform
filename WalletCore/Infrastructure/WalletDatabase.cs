using Infrastructure.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletCore.Interface;
using WalletCore.Model.Database;

namespace WalletCore.Infrastructure
{
    public class WalletDatabase : IWalletDatabase
    {
        private readonly IDatabase _database;

        public WalletDatabase(IDatabase database)
        {
            _database = database;
        }

        public async Task<Wallet> FindByCPFAsync(string cpf)
        {
            var filter = Builders<Wallet>.Filter.Eq(x => x.Owner.CPF, cpf);

            var wallets = await _database.FindAsync(filter);

            return wallets.FirstOrDefault();
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            await _database.UpdateAsync(x => x.Owner.CPF.Equals(wallet.Owner.CPF), wallet);
        }

        public async Task InsertAsync(Wallet wallet)
        {
            await _database.InsertAsync(wallet);
        }
    }
}
