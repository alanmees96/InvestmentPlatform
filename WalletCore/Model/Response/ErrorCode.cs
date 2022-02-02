using System.ComponentModel;

namespace WalletCore.Model.Response
{
    public enum ErrorCode
    {
        [Description("Carteira não encontrada")]
        WalletNotFound = 1,

        [Description("Saldo insuficiente")]
        InsufficientFunds = 2,

        [Description("CPF de origem diferente do destinatário")]
        TransferCPFDoesntMatch = 3
    }
}