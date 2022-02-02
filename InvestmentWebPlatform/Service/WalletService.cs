using InvestmentWebPlatform.Models.Wallet;
using InvestmentWebPlatform.Models.Wallet.AddMoneyPayload;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvestmentWebPlatform.Service
{
    public class WalletService : ServiceBase
    {
        protected override string BaseUrl()
        {
            return "http://localhost:5001/Wallet";
        }

        public async Task<HttpResponseMessage> AddMoneyAsync(AddWalletMoneyPayload addMoney)
        {
            var response = await PostAsync("/AddMoney", addMoney);

            return response;
        }

        public async Task AddNewWalletAsync(CreateWalletPayload wallet)
        {
            await PostAsync("/Create", wallet);
        }

        public async Task<HttpResponseMessage> AddShareAsync(AddSharePayload addShare)
        {
            var response = await PostAsync("/AddShare", addShare);

            return response;
        }

        public WalletService(HttpClient client) : base(client)
        { }
    }
}