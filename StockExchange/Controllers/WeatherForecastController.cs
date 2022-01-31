using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockExchange.Core;
using StockExchange.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockExchange.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDatabase _database;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDatabase database)
        {
            _logger = logger;
            _database = database;
        }

        [HttpGet]
        public async Task<IEnumerable<Share>> GetAsync()
        {
            var teste = new ShareWorker(_database);

            var trendList = await teste.Trend(5);

            return trendList;
        }
    }
}
