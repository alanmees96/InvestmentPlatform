using System.Threading.Tasks;
using WalletCore.Interface;
using WalletCore.Interface.Action;

namespace WalletCore.Action
{
    public class AddMoneyAvailableAction : IAddMoneyAvailableAction
    {
        private readonly IWalletDatabase _walletDatabase;

        public AddMoneyAvailableAction(IWalletDatabase walletDatabase)
        {
            _walletDatabase = walletDatabase;
        }

        public async Task ExecuteAsync(string cpf, double newMoney)
        {
            var wallet = await _walletDatabase.FindByCPFAsync(cpf);

            if (wallet == default)
            {
                return; // TODO Carteira não encontrada
            }

            wallet.MoneyAvailable += newMoney;

            await _walletDatabase.UpdateAsync(wallet);
        }
    }
}