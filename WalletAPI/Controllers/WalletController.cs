using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletAPI.Model;
using WalletCore.Interface.Action;
using WalletCore.Model.Action;

namespace WalletAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IBuyShareAction _buyShareAction;
        private readonly IAddMoneyAvailableAction _addMoneyAvailableAction;

        public WalletController(
            IBuyShareAction buyShareAction,
            IAddMoneyAvailableAction addMoneyAvailableAction)
        {
            _buyShareAction = buyShareAction;
            _addMoneyAvailableAction = addMoneyAvailableAction;
        }

        [HttpPost]
        [Route("AddShare")]
        public async Task AddSharePost(BuySharePayload payload)
        {
            var newShare = new BuyShare(payload);

            await _buyShareAction.ExecuteAsync(newShare, payload.CPF);
        }

        [HttpPost]
        [Route("AddMoney")]
        public async Task AddMoneyPost(AddMoneyPayload payload)
        {
            await _addMoneyAvailableAction.ExecuteAsync(payload.CPF, payload.value);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
