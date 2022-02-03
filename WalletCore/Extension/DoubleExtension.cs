using System;

namespace WalletCore.Extension
{
    public static class DoubleExtension
    {
        public static double CurrencyRound(this double value)
        {
            return Math.Round(value, 2);
        }
    }
}