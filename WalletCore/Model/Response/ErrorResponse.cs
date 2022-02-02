using System;
using WalletCore.Extension;

namespace WalletCore.Model.Response
{
    public class ErrorResponse : ActionResponse
    {
        public ErrorResponse(ErrorCode error)
        {
            ErrorCode = ((int)error);
            Message = error.GetEnumDescription();
        }
    }
}