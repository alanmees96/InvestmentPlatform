using InvestmentWebPlatform.Models;
using InvestmentWebPlatform.Models.Wallet;
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
        private async Task<ActionResult> WalletResponseDefineView<T>(T payload, HttpResponseMessage walletResponseMessage)
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
                    TempData["AlertMessageContent"] = "Saldo adicionado com sucesso!";

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

        // GET: WalletController/Create
        public async Task<ActionResult> AddCashAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var cashView = new AddCashViewModel()
            {
                CPF = currentUser.CPF,
                Name = currentUser.Name
            };

            return View(cashView);
        }

        // POST: WalletController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCashAsync(AddCashViewModel payload)
        {
            var addCash = new AddMoneyPayload()
            {
                CPF = payload.CPF,
                Value = payload.Value
            };

            var addCashJson = JsonSerializer.Serialize(addCash);

            var addCashContent = new StringContent(
                addCashJson,
                Encoding.UTF8,
                "application/json");

            var client = new HttpClient();

            var url = "http://localhost:5001/Wallet/AddMoney";

            try
            {
                var response = await client.PostAsync(url, addCashContent);

                return await WalletResponseDefineView(payload, response);
            }
            catch
            {
                TempData["AlertMessageType"] = "error";
                TempData["AlertMessageContent"] = "Instabilidade no sistema entre em contato com nosso suporte.";

                return View(payload);
            }
        }

        // GET: WalletController/Edit/5
        public ActionResult AddShare(string symbol, double price)
        {
            var viewModel = new AddShareViewModel()
            {
                Symbol = symbol,
                Price = price
            };

            return View(viewModel);
        }

        // POST: WalletController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddShareAsync(AddShareViewModel payload)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var newShare = new AddSharePayload()
            {
                CPF = currentUser.CPF,
                Quantity = payload.Quantity,
                Symbol = payload.Symbol,
                PurchasePrice = payload.Price
            };

            var newShareJson = new StringContent(
                JsonSerializer.Serialize(newShare),
                Encoding.UTF8,
                "application/json");

            var client = new HttpClient();

            var url = "http://localhost:5001/Wallet/AddShare";

            try
            {
                var response = await client.PostAsync(url, newShareJson);

                return await WalletResponseDefineView(payload, response);
            }
            catch
            {
                TempData["AlertMessageType"] = "error";
                TempData["AlertMessageContent"] = "Instabilidade no sistema entre em contato com nosso suporte.";

                return View(payload);
            }
        }

        public WalletController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
    }
}