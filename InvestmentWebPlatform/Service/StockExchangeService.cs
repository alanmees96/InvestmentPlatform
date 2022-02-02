using InvestmentWebPlatform.Models.StockExchange;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvestmentWebPlatform.Service
{
    public class StockExchangeService : ServiceBase
    {
        protected override string BaseUrl()
        {
            return "http://localhost:5000/StockExchange";
        }

        public async Task<IEnumerable<Share>> GetTrendShares()
        {
            var trendShares = await GetAsync<IEnumerable<Share>>("/Trend");

            return trendShares;
        }

        public StockExchangeService(HttpClient client) : base(client)
        { }
    }
}