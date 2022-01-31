using MongoDB.Bson;

namespace ToroTest
{
    public class Share
    {
        public ObjectId _id { get; set; }

        public double CurrentPrice { get; set; }
        public string Symbol { get; set; }
        public int SelledQuantity { get; set; }
    }
}