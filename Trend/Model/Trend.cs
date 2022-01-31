using MongoDB.Bson;

namespace Trend.Model
{
    public class Trend
    {
        public ObjectId _id { get; set; }

        public double CurrentPrice { get; set; }
        public string Symbol { get; set; }
    }
}