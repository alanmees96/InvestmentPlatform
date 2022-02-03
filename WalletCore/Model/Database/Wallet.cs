using MongoDB.Bson;
using System.Collections.Generic;
using WalletCore.Extension;

namespace WalletCore.Model.Database
{
    public class Wallet
    {
        public ObjectId _id { get; set; }

        public Owner Owner { get; set; }

        private double _moneyAvailable;

        public double MoneyAvailable 
        { 
            get => _moneyAvailable; 
            set => _moneyAvailable = value.CurrencyRound(); 
        }

        private double _moneyInvested;
        public double MoneyInvested 
        { 
            get => _moneyInvested; 
            set => _moneyInvested = value.CurrencyRound(); 
        }

        public List<Share> Shares { get; set; }
    }
}