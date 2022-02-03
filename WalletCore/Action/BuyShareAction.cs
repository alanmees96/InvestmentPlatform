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
        private Share AddNewShareInWallet(BuyShareContext context)
        {
            var share = BuildShare(context);

            context.CurrentWallet.Shares.Add(share);

            return share;
        }

        private Share BuildShare(BuyShareContext context)
        {
            var share = new Share()
            {
                Quantity = context.NewShare.Quantity,
                AVGPurchasePrice = context.NewShare.AVGPurchasePrice,
                Symbol = context.NewShare.Symbol
            };

            return share;
        }

        private double CalculateAVGBuyPrice(BuyShareContext context)
        {
            var currentTotalShareValue = context.CurrentShare.Quantity * context.CurrentShare.AVGPurchasePrice;
            var totalQuantityOperation = context.CurrentShare.Quantity + context.NewShare.Quantity;

            var avgPrice = (currentTotalShareValue + context.PurchaseTotalShareValue) / totalQuantityOperation;

            return avgPrice;
        }

        private async Task<BuyShareContext> CreateBuyShareContext(BuyShare newShare, string accountNumber)
        {
            var wallet = await FindWallet(accountNumber);

            var context = new BuyShareContext(wallet, newShare);

            return context;
        }

        private void DeductBuyShareFromMoneyAvailable(BuyShareContext context)
        {
            context.CurrentWallet.MoneyAvailable -= context.PurchaseTotalShareValue;
        }

        private async Task<Wallet> FindWallet(string accountNumber)
        {
            return await _walletDatabase.FindByAccountNumberAsync(accountNumber);
        }

        private void RemoveOldValuationMoneyInvested(BuyShareContext context)
        {
            context.CurrentWallet.MoneyInvested -= context.CurrentShare.Quantity * context.CurrentShare.AVGPurchasePrice;
        }

        private void UpdateCurrentShare(BuyShareContext context, double avgPrice)
        {
            context.CurrentShare.AVGPurchasePrice = avgPrice;
            context.CurrentShare.Quantity += context.NewShare.Quantity;
        }

        private void UpdateMoneyInvested(Wallet wallet, Share share)
        {
            wallet.MoneyInvested += share.Quantity * share.AVGPurchasePrice;
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

            var avgPrice = CalculateAVGBuyPrice(context);

            UpdateWalletShareAlreadyExists(context, avgPrice);
        }

        private void UpdateWalletShareAlreadyExists(BuyShareContext context, double avgPrice)
        {
            RemoveOldValuationMoneyInvested(context);

            UpdateCurrentShare(context, avgPrice);

            UpdateMoneyInvested(context.CurrentWallet, context.CurrentShare);
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

        private readonly IWalletDatabase _walletDatabase;

        public BuyShareAction(IWalletDatabase walletDatabase)
        {
            _walletDatabase = walletDatabase;
        }

        public async Task<ActionResponse> ExecuteAsync(BuyShare newShare, string accountNumber)
        {
            var context = await CreateBuyShareContext(newShare, accountNumber);

            var response = Validation(context);

            if (response.HasError)
            {
                return response;
            }

            UpdateSharesInWallet(context);

            await _walletDatabase.UpdateAsync(context.CurrentWallet);

            return response;
        }
    }
}