using System.Threading.Tasks;
using WalletCore.Model.Action;

namespace WalletCore.Interface.Action
{
    public interface ICreateWalletAction
    {
        public Task ExecuteAsync(CreateWallet walletPayload);
    }
}