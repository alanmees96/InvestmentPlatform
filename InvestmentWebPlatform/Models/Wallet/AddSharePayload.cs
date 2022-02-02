namespace InvestmentWebPlatform.Models.Wallet
{
    public class AddSharePayload
    {
        public string CPF { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public string Symbol { get; set; }
    }
}