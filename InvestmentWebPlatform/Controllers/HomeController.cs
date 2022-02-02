using InvestmentWebPlatform.Models;
using InvestmentWebPlatform.Models.StockExchange;
using InvestmentWebPlatform.Models.Wallet;
using InvestmentWebPlatform.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvestmentWebPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var client = new HttpClient();

            var url = "http://localhost:5000/StockExchange/Trend";

            var response = await client.GetAsync(url);

            var text = await response.Content.ReadAsStringAsync();

            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var shares = JsonSerializer.Deserialize<IEnumerable<Share>>(text, jsonOptions);

            var viewModel = new IndexViewModel() { Shares = shares };

            return View(viewModel);
        }

        public IActionResult PrivacyAsync()
        {
            

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
