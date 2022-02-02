using System.ComponentModel;

namespace WalletCore.Model.Error
{
    public enum ErrorCode
    {
        [Description("Carteira não encontrada")]
        WalletNotFound = 1,

        [Description("Saldo insuficiente")]
        InsufficientFunds = 2
    }
}