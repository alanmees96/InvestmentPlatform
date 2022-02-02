using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletCore.Interface;
using WalletCore.Interface.Action;
using WalletCore.Model.Action;
using WalletCore.Model.Database;

namespace WalletCore.Action
{
    public class BuyShareAction : IBuyShareAction
    {
        private readonly IWalletDatabase _walletDatabase;

        public BuyShareAction(IWalletDatabase walletDatabase)
        {
            _walletDatabase = walletDatabase;
        }

        private Share BuildShare(BuyShare newShare)
        {
            var share = new Share()
            {
                Quantity = newShare.Quantity,
                PurchasePrice = newShare.PurchasePrice,
                Symbol = newShare.Symbol
            };

            return share;
        }

        private void CalculateAVGPrice(Share share, BuyShare newShare)
        {
            var currentTotalShareValue = share.Quantity * share.PurchasePrice;
            var purchaseTotalShareValue = newShare.Quantity * newShare.PurchasePrice;

            var totalQuantityOperation = share.Quantity + newShare.Quantity;

            var avgPrice = (currentTotalShareValue + purchaseTotalShareValue) / totalQuantityOperation;

            
            share.Quantity += newShare.Quantity;
            share.PurchasePrice = avgPrice;
        }

        private void CalculateShare(Wallet wallet, BuyShare newShare)
        {
            var share = wallet.Shares.FirstOrDefault(x => x.Symbol.Equals(newShare.Symbol));

            if (share == default)
            {
                share = BuildShare(newShare);

                wallet.Shares.Add(share);

                wallet.MoneyInvested += share.Quantity * share.PurchasePrice;

                return;
            }

            var currentTotalShareValue = share.Quantity * share.PurchasePrice;

            wallet.MoneyInvested -= currentTotalShareValue;

            CalculateAVGPrice(share, newShare);

            wallet.MoneyInvested += share.Quantity * share.PurchasePrice;
        }

        public async Task ExecuteAsync(BuyShare newShare, string cpf)
        {
            var wallet = await _walletDatabase.FindByCPFAsync(cpf);

            if(wallet == default)
            {
                return; // TODO Carteira não encontrada
            }

            var purchaseTotalShareValue = newShare.Quantity * newShare.PurchasePrice;

            if(purchaseTotalShareValue > wallet.MoneyAvailable)
            {
                return;// TODO saldo insuficiente
            }

            wallet.MoneyAvailable -= purchaseTotalShareValue;

            CalculateShare(wallet, newShare);

            await _walletDatabase.UpdateAsync(wallet);
        }
    }
}
