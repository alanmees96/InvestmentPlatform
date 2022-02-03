using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletCore.Interface;
using WalletCore.Interface.Action;
using WalletCore.Model.Action;
using WalletCore.Model.Database;
using WalletCore.Model.Response;

namespace WalletCore.Action
{
    public class BuyShareAction : IBuyShareAction
    {
        private readonly IWalletDatabase _walletDatabase;

        public BuyShareAction(IWalletDatabase walletDatabase)
        {
            _walletDatabase = walletDatabase;
        }
        private async Task<Wallet> FindWallet(string accountNumber)
        {
            return await _walletDatabase.FindByAccountNumberAsync(accountNumber);
        }

        private async Task<BuyShareContext> CreateBuyShareContext(BuyShare newShare, string accountNumber)
        {
            var wallet = await FindWallet(accountNumber);

            var context = new BuyShareContext(wallet, newShare);

            return context;
        }

        private ActionResponse Validation(BuyShareContext context)
        {
            if (context.CurrentWallet == default)
            {
                return new ErrorResponse(ErrorCode.WalletNotFound);
            }

            if (context.HasFundsToThisOperation())
            {
                return new ErrorResponse(ErrorCode.InsufficientFunds);
            }

            return new DontHaveError();
        }

        private Share BuildShare(BuyShareContext context)
        {
            var share = new Share()
            {
                Quantity = context.NewShare.Quantity,
                PurchasePrice = context.NewShare.PurchasePrice,
                Symbol = context.NewShare.Symbol
            };

            return share;
        }

        //private void CalculateAVGPrice(Share share, BuyShareContext context)
        //{
        //    var currentTotalShareValue = share.Quantity * share.PurchasePrice;
        //    var totalQuantityOperation = share.Quantity + context.Quantity;

        //    var avgPrice = (currentTotalShareValue + context.PurchaseTotalShareValue) / totalQuantityOperation;

        //    share.Quantity += context.Quantity;
        //    share.PurchasePrice = avgPrice;
        //}

        private double CalculateAVGPrice(BuyShareContext context)
        {
            var currentTotalShareValue = context.CurrentShare.Quantity * context.CurrentShare.PurchasePrice;
            var totalQuantityOperation = context.CurrentShare.Quantity + context.NewShare.Quantity;

            var avgPrice = (currentTotalShareValue + context.PurchaseTotalShareValue) / totalQuantityOperation;

            return avgPrice;
        }

        private void CalculateShare(Wallet wallet, BuyShareContext context)
        {
            //var share = wallet.Shares.FirstOrDefault(x => x.Symbol.Equals(context.Symbol));

            //if (share == default)
            //{
            //    share = BuildShare(context);

            //    wallet.Shares.Add(share);

            //    wallet.MoneyInvested += share.Quantity * share.PurchasePrice;

            //    return;
            //}

            //var currentTotalShareValue = share.Quantity * share.PurchasePrice;

            //wallet.MoneyInvested -= currentTotalShareValue;

            //CalculateAVGPrice(share, context);

            //wallet.MoneyInvested += share.Quantity * share.PurchasePrice;
        }

        private void DeductBuyShareFromMoneyAvailable(BuyShareContext context)
        {
            context.CurrentWallet.MoneyAvailable -= context.PurchaseTotalShareValue;
        }

        private void UpdateMoneyInvested(Wallet wallet, Share share)
        {
            wallet.MoneyInvested += share.Quantity * share.PurchasePrice;
        }

        private Share AddNewShareInWallet(BuyShareContext context)
        {
            var share = BuildShare(context);

            context.CurrentWallet.Shares.Add(share);

            return share;
        }

        public void RemoveOldValuationMoneyInvested(BuyShareContext context)
        {
            context.CurrentWallet.MoneyInvested -= context.CurrentShare.Quantity * context.CurrentShare.PurchasePrice;
        }

        public void UpdateCurrentShare(BuyShareContext context, double avgPrice)
        {
            context.CurrentShare.PurchasePrice = avgPrice;
            context.CurrentShare.Quantity += context.NewShare.Quantity;
        }

        public void UpdateWalletShareAlreadyExists(BuyShareContext context, double avgPrice)
        {
            RemoveOldValuationMoneyInvested(context);

            UpdateCurrentShare(context, avgPrice);

            UpdateMoneyInvested(context.CurrentWallet, context.CurrentShare);
        }

        private void UpdateSharesInWallet(BuyShareContext context)
        {
            DeductBuyShareFromMoneyAvailable(context);

            if (context.IsNeedCreateShareInWalletShares())
            {
                var newShare = AddNewShareInWallet(context);

                UpdateMoneyInvested(context.CurrentWallet, newShare);

                return;
            }

            var avgPrice = CalculateAVGPrice(context);

            UpdateWalletShareAlreadyExists(context, avgPrice);
        }

        public async Task<ActionResponse> ExecuteAsync(BuyShare newShare, string accountNumber)
        {
            var context = await CreateBuyShareContext(newShare, accountNumber);

            var response = Validation(context);

            if(response.HasError)
            {
                return response; 
            }

            //wallet.MoneyAvailable -= purchaseTotalShareValue;

            UpdateSharesInWallet(context);

            await _walletDatabase.UpdateAsync(context.CurrentWallet);

            return response;
        }
    }
}
