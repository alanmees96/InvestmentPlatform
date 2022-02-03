using WalletCore.Extension;

namespace WalletCore.Model
{
    public class ShareBase
    {
        private double _avgPurchasePrice;
        public double AVGPurchasePrice
        {
            get => _avgPurchasePrice;
            set => _avgPurchasePrice = value.CurrencyRound();
        }

        public int Quantity { get; set; }
        public ShareBase(ShareBase share)
        {
            Symbol = share.Symbol;
            Quantity = share.Quantity;
            AVGPurchasePrice = share.AVGPurchasePrice;
        }

        public ShareBase()
        { }

        public string Symbol { get; set; }
    }
}