using InvestmentWebPlatform.Models;
using InvestmentWebPlatform.Service;
using InvestmentWebPlatform.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace InvestmentWebPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly StockExchangeService _stockService;
        private readonly WalletService _walletService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            StockExchangeService stockService,
            WalletService walletService)
        {
            _userManager = userManager;
            _stockService = stockService;
            _walletService = walletService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var trendShares = await _stockService.GetTrendShares();

            var viewModel = new IndexViewModel() { Shares = trendShares };

            return View(viewModel);
        }

        public async Task<IActionResult> PatrimonyAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var patrimonyResponse = await _walletService.GetPatrimonyAsync(currentUser.AccountNumber.ToString());

            var viewModel = new PatrimonyViewModel()
            {
                MoneyAvailable = patrimonyResponse.MoneyAvailable,
                Patrimony = patrimonyResponse.Patrimony,
                Shares = patrimonyResponse.Shares
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}