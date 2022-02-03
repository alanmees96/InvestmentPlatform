namespace InvestmentWebPlatform.Models.Wallet
{
    public class AddSharePayload
    {
        public string AccountNumber { get; set; }

        public int Quantity { get; set; }

        public double AVGPurchasePrice { get; set; }

        public string Symbol { get; set; }
    }
}