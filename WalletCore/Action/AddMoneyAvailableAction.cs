using System.Threading.Tasks;
using WalletCore.Interface;
using WalletCore.Interface.Action;
using WalletCore.Model.Response;

namespace WalletCore.Action
{
    public class AddMoneyAvailableAction : IAddMoneyAvailableAction
    {
        private readonly IWalletDatabase _walletDatabase;

        public AddMoneyAvailableAction(IWalletDatabase walletDatabase)
        {
            _walletDatabase = walletDatabase;
        }

        public async Task<ActionResponse> ExecuteAsync(string cpf, double newMoney)
        {
            var wallet = await _walletDatabase.FindByCPFAsync(cpf);

            if (wallet == default)
            {
                return new ErrorResponse(ErrorCode.WalletNotFound);
            }

            wallet.MoneyAvailable += newMoney;

            await _walletDatabase.UpdateAsync(wallet);

            return new DontHaveError();
        }
    }
}