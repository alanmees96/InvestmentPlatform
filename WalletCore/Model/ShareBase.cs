namespace WalletCore.Model
{
    public class ShareBase
    {
        public string Symbol { get; set; }

        public int Amount { get; set; }

        public double PurchasePrice { get; set; }

        public ShareBase(ShareBase share)
        {
            Symbol = share.Symbol;
            Amount = share.Amount;
            PurchasePrice = share.PurchasePrice;
        }
        public ShareBase() {}
    }
}