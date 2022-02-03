using System.Threading.Tasks;
using WalletCore.Model.Response;

namespace WalletCore.Interface.Action
{
    public interface IFindWalletPatrimonyAction
    {
        public Task<WalletPatrimonyResponse> ExecuteAsync(string accountNumber);
    }
}