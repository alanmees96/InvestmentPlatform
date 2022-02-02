using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletCore.Model.Response
{
    public class DontHaveError : ActionResponse
    {
        public DontHaveError(string message)
        {
            Message = message;
        }

        public DontHaveError()
        {

        }
    }
}
