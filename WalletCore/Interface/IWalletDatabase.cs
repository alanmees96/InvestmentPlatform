using System.Threading.Tasks;
using WalletCore.Model.Database;

namespace WalletCore.Interface
{
    public interface IWalletDatabase
    {
        public Task<Wallet> FindByCPFAsync(long cpf);

        public Task UpdateAsync(Wallet wallet);
    }
}