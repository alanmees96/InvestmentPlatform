using System.Threading.Tasks;
using WalletCore.Model.Response;

namespace WalletCore.Interface.Action
{
    public interface IAddMoneyAvailableAction
    {
        public Task<ActionResponse> ExecuteAsync(string cpf, double newMoney);
    }
}