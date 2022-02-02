using InvestmentWebPlatform.Models.StockExchange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentWebPlatform.ViewModel
{
    public class IndexViewModel 
    {
        public IEnumerable<Share> Shares { get; set; }
    }
}
