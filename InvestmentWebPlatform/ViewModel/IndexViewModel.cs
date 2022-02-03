using InvestmentWebPlatform.Models.StockExchange;
using System.Collections.Generic;

namespace InvestmentWebPlatform.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Share> Shares { get; set; }
    }
}