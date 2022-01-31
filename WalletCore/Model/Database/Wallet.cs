using MongoDB.Bson;
using System.Collections.Generic;

namespace WalletCore.Model.Database
{
    public class Wallet
    {
        public ObjectId _id { get; set; }

        public Owner Owner { get; set; }

        public double Avaliable { get; set; }

        public double Invested { get; set; }

        public List<Share> Shares { get; set; }
    }
}