using WalletCore.Extension;

namespace WalletCore.Model
{
    public class ShareBase
    {
        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public double AVGPurchasePrice 
        { 
            get => AVGPurchasePrice; 
            set => AVGPurchasePrice = value.CurrencyRound(); 
        }

        public ShareBase(ShareBase share)
        {
            Symbol = share.Symbol;
            Quantity = share.Quantity;
            AVGPurchasePrice = share.AVGPurchasePrice;
        }
        public ShareBase() {}
    }
}