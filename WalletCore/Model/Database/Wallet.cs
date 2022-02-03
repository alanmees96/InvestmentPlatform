using MongoDB.Bson;
using System.Collections.Generic;
using WalletCore.Extension;

namespace WalletCore.Model.Database
{
    public class Wallet
    {
        private double _moneyAvailable;
        private double _moneyInvested;
        public ObjectId _id { get; set; }

        public double MoneyAvailable
        {
            get => _moneyAvailable;
            set => _moneyAvailable = value.CurrencyRound();
        }

        public double MoneyInvested
        {
            get => _moneyInvested;
            set => _moneyInvested = value.CurrencyRound();
        }

        public Owner Owner { get; set; }
        public List<Share> Shares { get; set; }
    }
}