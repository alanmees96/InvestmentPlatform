using System.Collections.Generic;
using WalletCore.Model.Database;

namespace WalletCore.Model.Response
{
    public class WalletPatrimonyResponse
    {
        public double MoneyAvailable { get; set; }

        public List<Share> Shares { get; set; }

        public double Patrimony { get; set; }

        public ActionResponse ActionResponse { get; private set; }

        public WalletPatrimonyResponse(ErrorCode errorCode)
        {
            ActionResponse = new ErrorResponse(errorCode);
        }

        public WalletPatrimonyResponse()
        {
            ActionResponse = new DontHaveError();
        }
    }
}