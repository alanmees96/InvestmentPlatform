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

            var actionResponse = await _buyShareAction.ExecuteAsync(newShare, payload.CPF);

            if (actionResponse.HasError)
            {
                if(actionResponse.ErrorCode == (int) ErrorCode.InsufficientFunds)
                {
                    return BadRequest(actionResponse);
                }

                if (actionResponse.ErrorCode == (int)ErrorCode.WalletNotFound)
                {
                    return NotFound(actionResponse);
                }
            }

            return Ok(actionResponse);
        }

        [HttpPost]
        [Route("AddMoney")]
        public async Task AddMoneyPost(AddMoneyPayload payload)
        {
            await _addMoneyAvailableAction.ExecuteAsync(payload.CPF, payload.Value);
        }

        [HttpPost]
        [Route("Create")]
        public async Task CreateWalletPost(CreateWalletPayload payload)
        {
            await _createWalletAction.ExecuteAsync(payload.Name, payload.CPF);
        }
    }
}