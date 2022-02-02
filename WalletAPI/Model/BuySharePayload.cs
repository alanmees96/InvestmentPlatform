using WalletCore.Model;

namespace WalletAPI.Model
{
    public class BuySharePayload : ShareBase
    {
        public BuySharePayload(ShareBase share) : base(share)
        {
        }

        public BuySharePayload() {}

        public string AccountNumber { get; set; }
    }
}