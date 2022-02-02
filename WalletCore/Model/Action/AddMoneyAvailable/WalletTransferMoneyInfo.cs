namespace WalletCore.Model.Action.AddMoneyAvailable
{
    public class WalletTransferMoneyInfo
    {
        public string Event { get; set; }

        public TargetInfo Target { get; set; }

        public OriginInfo Origin { get; set; }

        public double Amount { get; set; }
    }
}