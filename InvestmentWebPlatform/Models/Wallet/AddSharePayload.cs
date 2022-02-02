namespace InvestmentWebPlatform.Models.Wallet
{
    public class AddSharePayload
    {
        public long CPF { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string Symbol { get; set; }
    }
}