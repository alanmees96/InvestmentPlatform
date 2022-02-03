using InvestmentWebPlatform.Models;
using InvestmentWebPlatform.Models.Wallet;
using InvestmentWebPlatform.Models.Wallet.AddMoneyPayload;
using InvestmentWebPlatform.Service;
using InvestmentWebPlatform.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvestmentWebPlatform.Controllers
{
    public class WalletController : Controller
    {
        private async Task<ActionResult> WalletResponseDefineView<T>(T payload, HttpResponseMessage walletResponseMessage, string successMessage)
        {
            var contentAsText = await walletResponseMessage.Content.ReadAsStringAsync();

            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var walletResponse = JsonSerializer.Deserialize<WalletResponse>(contentAsText, jsonOptions);

            switch (walletResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    TempData["AlertMessageType"] = "success";
                    TempData["AlertMessageContent"] = successMessage;

                    return RedirectToAction(nameof(Index), "Home");

                case HttpStatusCode.NotFound:
                    TempData["AlertMessageType"] = "error";
                    TempData["AlertMessageContent"] = walletResponse.Message;

                    return View(payload);

                case HttpStatusCode.BadRequest:
                    TempData["AlertMessageType"] = "error";
                    TempData["AlertMessageContent"] = walletResponse.Message;

                    return View(payload);

                default:
                    TempData["AlertMessageType"] = "error";
                    TempData["AlertMessageContent"] = "Retorno inesperado";

                    return View(payload);
            }
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WalletService _walletService;

        public async Task<ActionResult> AddCashAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var cashView = new AddCashViewModel()
            {
                AccountNumber = currentUser.AccountNumber.ToString()
            };

            return View(cashView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCashAsync(AddCashViewModel payload)
        {
            var addCash = new AddWalletMoneyPayload()
            {
                Event = "TRANSFER",
                Origin = new OriginInfo()
                {
                    Bank = payload.OriginBank,
                    Branch = payload.OriginBranch,
                    CPF = payload.OriginCPFOwner
                },
                Target = new TargetInfo()
                {
                    Bank = "352",
                    Branch = "0001",
                    Account = payload.AccountNumber
                },
                Amount = payload.Value
            };

            try
            {
                var response = await _walletService.AddMoneyAsync(addCash);

                var successMessage = "Saldo adicionado com sucesso!";

                return await WalletResponseDefineView(payload, response, successMessage);
            }
            catch
            {
                TempData["AlertMessageType"] = "error";
                TempData["AlertMessageContent"] = "Instabilidade no sistema entre em contato com nosso suporte.";

                return View(payload);
            }
        }

        public ActionResult AddShare(string symbol, double price)
        {
            var viewModel = new AddShareViewModel()
            {
                Symbol = symbol,
                Price = price
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddShareAsync(AddShareViewModel payload)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var newShare = new AddSharePayload()
            {
                AccountNumber = currentUser.AccountNumber.ToString(),
                Quantity = payload.Quantity,
                Symbol = payload.Symbol,
                AVGPurchasePrice = payload.Price
            };

            try
            {
                var response = await _walletService.AddShareAsync(newShare);

                var successMessage = $"Ação {payload.Symbol} comparada com sucesso!";

                return await WalletResponseDefineView(payload, response, successMessage);
            }
            catch
            {
                TempData["AlertMessageType"] = "error";
                TempData["AlertMessageContent"] = "Instabilidade no sistema entre em contato com nosso suporte.";

                return View(payload);
            }
        }

        public WalletController(UserManager<ApplicationUser> userManager, WalletService walletService)
        {
            _userManager = userManager;
            _walletService = walletService;
        }
    }
}