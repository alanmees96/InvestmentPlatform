using System.Threading.Tasks;

namespace WalletCore.Interface.Action
{
    public interface IAddMoneyAvailableAction
    {
        public Task ExecuteAsync(string cpf, double newMoney);
    }
}