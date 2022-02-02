namespace InvestmentWebPlatform.Models.Wallet.AddMoneyPayload
{
    public class AddWalletMoneyPayload
    {
        public string Event { get; set; }

        public TargetInfo Target { get; set; }

        public OriginInfo Origin { get; set; }

        public double Amount { get; set; }
    }
}