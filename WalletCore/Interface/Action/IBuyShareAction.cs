using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletCore.Model.Action;

namespace WalletCore.Interface.Action
{
    public interface IBuyShareAction
    {
        public Task ExecuteAsync(BuyShare newShare, long cpf);
    }
}
