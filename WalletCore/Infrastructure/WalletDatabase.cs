using Infrastructure.Interface;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using WalletCore.Interface;
using WalletCore.Model.Database;

namespace WalletCore.Infrastructure
{
    public class WalletDatabase : IWalletDatabase
    {
        private readonly IDatabase _database;

        public async Task<Wallet> FindByAccountNumberAsync(string accountNumber)
        {
            var filter = Builders<Wallet>.Filter.Eq(x => x.Owner.AccountNumber, accountNumber);

            var wallets = await _database.FindAsync(filter);

            return wallets.FirstOrDefault();
        }

        public async Task InsertAsync(Wallet wallet)
        {
            await _database.InsertAsync(wallet);
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            await _database.UpdateAsync(x => x.Owner.AccountNumber.Equals(wallet.Owner.AccountNumber), wallet);
        }

        public WalletDatabase(IDatabase database)
        {
            _database = database;
        }
    }
}