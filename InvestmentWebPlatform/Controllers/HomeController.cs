using InvestmentWebPlatform.Models;
using InvestmentWebPlatform.Service;
using InvestmentWebPlatform.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvestmentWebPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StockExchangeService _stockService;

        public HomeController(ILogger<HomeController> logger,
            IHttpClientFactory clientFactory,
            StockExchangeService stockService)
        {
            _logger = logger;
            _stockService = stockService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var trendShares = await _stockService.GetTrendShares();

            var viewModel = new IndexViewModel() { Shares = trendShares };

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