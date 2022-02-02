using Microsoft.AspNetCore.Mvc;
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
        public async Task AddSharePost(BuySharePayload payload)
        {
            var newShare = new BuyShare(payload);

            await _buyShareAction.ExecuteAsync(newShare, payload.CPF);
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