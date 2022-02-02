namespace WalletCore.Model
{
    public class ShareBase
    {
        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public ShareBase(ShareBase share)
        {
            Symbol = share.Symbol;
            Quantity = share.Quantity;
            PurchasePrice = share.PurchasePrice;
        }
        public ShareBase() {}
    }
}