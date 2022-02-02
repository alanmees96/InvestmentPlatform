using System;
using WalletCore.Extension;

namespace WalletCore.Model.Error
{
    public class ErrorResponse
    {
        public string Message { get; private set; }

        public int? ErrorCode { get; private set; }

        public bool HasError { get => !String.IsNullOrEmpty(Message); }

        public ErrorResponse(ErrorCode error)
        {
            ErrorCode = ((int)error);
            Message = error.GetEnumDescription();
        }

        protected ErrorResponse()
        {}
    }
}