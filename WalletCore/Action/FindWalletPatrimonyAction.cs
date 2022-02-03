using System.Threading.Tasks;
using WalletCore.Interface;
using WalletCore.Interface.Action;
using WalletCore.Model.Response;

namespace WalletCore.Action
{
    public class FindWalletPatrimonyAction : IFindWalletPatrimonyAction
    {
        private readonly IWalletDatabase _walletDatabase;

        public async Task<WalletPatrimonyResponse> ExecuteAsync(string accountNumber)
        {
            var wallet = await _walletDatabase.FindByAccountNumberAsync(accountNumber);

            if (wallet == null)
            {
                return new WalletPatrimonyResponse(ErrorCode.WalletNotFound);
            }

            var response = new WalletPatrimonyResponse()
            {
                MoneyAvailable = wallet.MoneyAvailable,
                Shares = wallet.Shares,
                Patrimony = wallet.MoneyAvailable + wallet.MoneyInvested
            };

            return response;
        }

        public FindWalletPatrimonyAction(IWalletDatabase walletDatabase)
        {
            _walletDatabase = walletDatabase;
        }
    }
}