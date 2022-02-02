using System.Threading.Tasks;
using WalletCore.Model.Database;

namespace WalletCore.Interface
{
    public interface IWalletDatabase
    {
        public Task<Wallet> FindByAccountNumberAsync(string accountNumber);

        public Task UpdateAsync(Wallet wallet);

        public Task InsertAsync(Wallet wallet);
    }
}