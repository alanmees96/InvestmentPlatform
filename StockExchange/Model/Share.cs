using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace StockExchange.Model
{
    public class Share
    {
        [JsonIgnore]
        public ObjectId _id { get; set; }

        public double CurrentPrice { get; set; }
        public string Symbol { get; set; }
        [JsonIgnore]
        public int SelledQuantity { get; set; }
    }
}
