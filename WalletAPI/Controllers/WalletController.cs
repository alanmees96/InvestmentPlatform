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
        private readonly IAddMoneyAvailableAction _addMoneyAvailableAction;
        private readonly IBuyShareAction _buyShareAction;
        private readonly ICreateWalletAction _createWalletAction;
        private readonly IFindWalletPatrimonyAction _findWalletPatrimonyAction;

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

        [HttpGet]
        [Route("FndWalletPatrimony")]
        public async Task<ObjectResult> FindWalletPatrimonyGET(string accountNumber)
        {
            var actionResponse = await _findWalletPatrimonyAction.ExecuteAsync(accountNumber);

            if (actionResponse.ActionResponse.HasError)
            {
                if (actionResponse.ActionResponse.ErrorCode == (int)ErrorCode.WalletNotFound)
                {
                    return NotFound(actionResponse);
                }

                return BadRequest(actionResponse);
            }

            return Ok(actionResponse);
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

        public WalletController(
                                            IAddMoneyAvailableAction addMoneyAvailableAction,
            IBuyShareAction buyShareAction,
            ICreateWalletAction createWalletAction,
            IFindWalletPatrimonyAction findWalletPatrimonyAction)
        {
            _addMoneyAvailableAction = addMoneyAvailableAction;
            _buyShareAction = buyShareAction;
            _createWalletAction = createWalletAction;
            _findWalletPatrimonyAction = findWalletPatrimonyAction;
        }
    }
}