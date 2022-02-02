using System.Threading.Tasks;

namespace WalletCore.Interface.Action
{
    public interface ICreateWalletAction
    {
        public Task ExecuteAsync(string name, string cpf);
    }
}