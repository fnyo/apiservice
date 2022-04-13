using Fnyo.Learn.Jenkins.Filter;
using Fnyo.Learn.Jenkins.Toolkits.Mongodb;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Controllers
{
    [ApiController]
    [Route("mongodb")]
    public class MongodbController:ControllerBase
    {
        //public MongodbHelper MongoHelper { get; set; }
        private readonly MongodbHelper _mongo;

        public MongodbController(MongodbHelper mongo)
        {
            _mongo = mongo; 
        }



        [HttpGet("insert")]
        [QueryTimeFilter]
        public async Task<bool> InsertAsync()
        {
            var order = new Order()
            {
                Goods = "经济学原理",
                Price = 100,
                Customer = "magicargo",
                Amount = 1,
                OrderNo = "202201221837"
            };

            var orderCollection = _mongo.GetCollection<Order>("Order");

            await orderCollection.InsertOneAsync(order);

            return true;
        }

        [HttpGet("query")]
        [QueryTimeFilter]
        public async Task<List<Order>> QueryAsync()
        {
            var orderCollection = _mongo.GetCollection<Order>("Order");

            var data = Builders<Order>.Filter.Where(x=>x.Goods.Equals("经济学原理"));

            var result = await orderCollection.FindAsync(data);

            return result.ToList();

        }
    }
}
