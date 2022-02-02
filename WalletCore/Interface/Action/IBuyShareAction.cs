using System.Threading.Tasks;
using WalletCore.Model.Action;
using WalletCore.Model.Response;

namespace WalletCore.Interface.Action
{
    public interface IBuyShareAction
    {
        public Task<ActionResponse> ExecuteAsync(BuyShare newShare, string cpf);
    }
}