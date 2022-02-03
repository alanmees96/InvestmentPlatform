using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockExchange.Core;
using StockExchange.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockExchangeController : ControllerBase
    {
        private readonly ILogger<StockExchangeController> _logger;
        private readonly IDatabase _database;

        public StockExchangeController(ILogger<StockExchangeController> logger, IDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet]
        [Route("Trend")]
        public async Task<IEnumerable<Share>> TrendAsync()
        {
            var teste = new ShareWorker(_database);

            var trendList = await teste.Trend(5);

            return trendList;
        }
    }
}