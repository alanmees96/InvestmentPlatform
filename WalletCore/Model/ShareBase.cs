using WalletCore.Extension;

namespace WalletCore.Model
{
    public class ShareBase
    {
        public string Symbol { get; set; }

        public int Quantity { get; set; }
        private double _avgPurchasePrice;
        public double AVGPurchasePrice 
        { 
            get => _avgPurchasePrice; 
            set => _avgPurchasePrice = value.CurrencyRound(); 
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