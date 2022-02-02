﻿using InvestmentWebPlatform.Models;
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
                CPF = currentUser.CPF,
                Name = currentUser.Name
            };

            return View(cashView);
        }

        // POST: WalletController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCashAsync(AddCashViewModel collection)
        {

            //var currentUser = await _userManager.GetUserAsync(User);

            var addCash = new AddMoneyPayload()
            {
                CPF = collection.CPF,
                Value = collection.Value
            };

            var addCashJson = JsonSerializer.Serialize(addCash);

            var addCashContent = new StringContent(
                addCashJson,
                Encoding.UTF8,
                "application/json");

            var client = new HttpClient();

            var url = "http://localhost:5001/Wallet/AddMoney";

            await client.PostAsync(url, addCashContent);

            try
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            catch
            {
                return View();
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

            await client.PostAsync(url, newShareJson);

            try
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            catch
            {
                return View();
            }
        }

    }
}
