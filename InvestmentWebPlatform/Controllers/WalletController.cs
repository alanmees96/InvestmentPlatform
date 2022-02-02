using InvestmentWebPlatform.Models;
using InvestmentWebPlatform.Models.Wallet;
using InvestmentWebPlatform.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InvestmentWebPlatform.Controllers
{
    public class WalletController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public WalletController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        // GET: WalletController/Create
        public async Task<ActionResult> AddCashAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var cashView = new AddCashViewModel()
            {
                CPF = long.Parse(currentUser.CPF),
                Name = currentUser.Name
            };

            return View(cashView);
        }

        // POST: WalletController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCashAsync(AddCashViewModel collection)
        {
            var addCash = new AddMoneyPayload()
            {
                CPF = collection.CPF,
                Value = collection.Value
            };

            var addCashJson = new StringContent(
                JsonSerializer.Serialize(addCash),
                Encoding.UTF8,
                "application/json");

            var client = new HttpClient();

            var url = "http://localhost:5001/Wallet/AddMoney";

            await client.PostAsync(url, addCashJson);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalletController/Edit/5
        public ActionResult AddShare(string symbol)
        {


            return RedirectToAction(nameof(Index));
        }

        // POST: WalletController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalletController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalletController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
