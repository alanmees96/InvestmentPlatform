using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WalletAPI.Model;
using WalletCore.Interface.Action;
using WalletCore.Model.Action;
using WalletCore.Model.Response;

namespace WalletAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IBuyShareAction _buyShareAction;
        private readonly IAddMoneyAvailableAction _addMoneyAvailableAction;
        private readonly ICreateWalletAction _createWalletAction;

        public WalletController(
            IBuyShareAction buyShareAction,
            IAddMoneyAvailableAction addMoneyAvailableAction,
            ICreateWalletAction createWalletAction)
        {
            _buyShareAction = buyShareAction;
            _addMoneyAvailableAction = addMoneyAvailableAction;
            _createWalletAction = createWalletAction;
        }

        [HttpPost]
        [Route("AddShare")]
        public async Task<ObjectResult> AddSharePost(BuySharePayload payload)
        {
            var newShare = new BuyShare(payload);

            var actionResponse = await _buyShareAction.ExecuteAsync(newShare, payload.AccountNumber);

            if (actionResponse.HasError)
            {
                if (actionResponse.ErrorCode == (int)ErrorCode.WalletNotFound)
                {
                    return NotFound(actionResponse);
                }

                return BadRequest(actionResponse);
            }

            return Ok(actionResponse);
        }

        [HttpPost]
        [Route("AddMoney")]
        public async Task<ObjectResult> AddMoneyPost(AddMoneyPayload payload)
        {
            var actionResponse = await _addMoneyAvailableAction.ExecuteAsync(payload);

            if (actionResponse.HasError)
            {
                if (actionResponse.ErrorCode == (int)ErrorCode.WalletNotFound)
                {
                    return NotFound(actionResponse);
                }

                return BadRequest(actionResponse);
            }

            return Ok(actionResponse);
        }

        [HttpPost]
        [Route("Create")]
        public async Task CreateWalletPost(CreateWalletPayload payload)
        {
            var walletPayload = new CreateWallet()
            {
                AccountNumber = payload.AccountNumber,
                CPF = payload.CPF,
                Name = payload.Name
            };

            await _createWalletAction.ExecuteAsync(walletPayload);
        }
    }
}