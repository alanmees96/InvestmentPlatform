using System.Collections.Generic;

namespace InvestmentWebPlatform.Models.Wallet
{
    public class WalletPatrimonyResponse
    {
        public WalletResponse ActionResponse { get; private set; }
        public double MoneyAvailable { get; set; }

        public double Patrimony { get; set; }
        public List<WalletPatrimonyShare> Shares { get; set; }
    }
}