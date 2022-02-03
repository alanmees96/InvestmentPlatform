using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletCore.Model.Database;

namespace WalletCore.Model.Action
{
    public class BuyShareContext
    {
        public readonly Wallet? CurrentWallet;
        public readonly Share? CurrentShare;
        public readonly BuyShare? NewShare;
        public readonly double PurchaseTotalShareValue;

        public BuyShareContext(Wallet currentWalet, BuyShare newShare)
        {
            CurrentWallet = currentWalet;
            NewShare = newShare;

            PurchaseTotalShareValue = newShare.Quantity * newShare.PurchasePrice;
            
            if(currentWalet != null)
            {
                CurrentShare = currentWalet.Shares.FirstOrDefault(x => x.Symbol.Equals(newShare.Symbol));
            }
        }

        public bool HasFundsToThisOperation()
        {
            if(CurrentWallet == null)
            {
                return false;
            }

            return PurchaseTotalShareValue > CurrentWallet.MoneyAvailable;
        }

        public bool IsNeedCreateShareInWalletShares()
        {
            return CurrentShare == null;
        }
    }
}
