using System.Collections.Generic;
using System.Threading.Tasks;
using WalletCore.Interface;
using WalletCore.Interface.Action;
using WalletCore.Model.Action;
using WalletCore.Model.Database;

namespace WalletCore.Action
{
    public class CreateWalletAction : ICreateWalletAction
    {
        private readonly IWalletDatabase _walletDatabase;

        public CreateWalletAction(IWalletDatabase walletDatabase)
        {
            _walletDatabase = walletDatabase;
        }

        public async Task ExecuteAsync(CreateWallet walletPayload)
        {
            var newWallet = new Wallet()
            {
                Owner = new Owner()
                {
                    CPF = walletPayload.CPF,
                    Name = walletPayload.Name,
                    AccountNumber = walletPayload.Name
                },
                Shares = new List<Share>()
            };

            await _walletDatabase.InsertAsync(newWallet);
        }
    }
}