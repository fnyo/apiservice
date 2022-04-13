
using MongoDB.Bson;

namespace Fnyo.Learn.Jenkins.Toolkits.Mongodb
{
    public class Order
    {
        public ObjectId Id { get; set; }
        public string Customer { get; set; }
        public string OrderNo { get; set; }
        public string Goods { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
    }
}
