using System.Threading.Tasks;
using WalletCore.Interface;
using WalletCore.Interface.Action;
using WalletCore.Model.Action.AddMoneyAvailable;
using WalletCore.Model.Database;
using WalletCore.Model.Response;

namespace WalletCore.Action
{
    public class AddMoneyAvailableAction : IAddMoneyAvailableAction
    {
        private ActionResponse Validation(Wallet wallet, WalletTransferMoneyInfo transferInfo)
        {
            if (wallet == default)
            {
                return new ErrorResponse(ErrorCode.WalletNotFound);
            }

            if (wallet.Owner.CPF != transferInfo.Origin.CPF)
            {
                return new ErrorResponse(ErrorCode.TransferCPFDoesntMatch);
            }

            return new DontHaveError();
        }

        private readonly IWalletDatabase _walletDatabase;

        public AddMoneyAvailableAction(IWalletDatabase walletDatabase)
        {
            _walletDatabase = walletDatabase;
        }

        public async Task<ActionResponse> ExecuteAsync(WalletTransferMoneyInfo transferInfo)
        {
            var wallet = await _walletDatabase.FindByAccountNumberAsync(transferInfo.Target.Account);

            var response = Validation(wallet, transferInfo);

            if (response.HasError)
            {
                return response;
            }

            wallet.MoneyAvailable += transferInfo.Amount;

            await _walletDatabase.UpdateAsync(wallet);

            return response;
        }
    }
}