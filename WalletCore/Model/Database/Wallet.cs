using MongoDB.Bson;
using System.Collections.Generic;

namespace WalletCore.Model.Database
{
    public class Wallet
    {
        public ObjectId _id { get; set; }

        public Owner Owner { get; set; }

        public double MoneyAvailable { get; set; }

        public double MoneyInvested { get; set; }

        public List<Share> Shares { get; set; }
    }
}