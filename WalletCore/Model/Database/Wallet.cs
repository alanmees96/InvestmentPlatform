using MongoDB.Bson;
using System.Collections.Generic;
using WalletCore.Extension;

namespace WalletCore.Model.Database
{
    public class Wallet
    {
        public ObjectId _id { get; set; }

        public Owner Owner { get; set; }

        public double MoneyAvailable 
        { 
            get => MoneyAvailable; 
            set => MoneyAvailable = value.CurrencyRound(); 
        }

        public double MoneyInvested 
        { 
            get => MoneyInvested; 
            set => MoneyInvested = value.CurrencyRound(); 
        }

        public List<Share> Shares { get; set; }
    }
}