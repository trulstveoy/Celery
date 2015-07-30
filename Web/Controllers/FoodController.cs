using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using Data.Dto;
using MongoDB.Driver;

namespace Web.Controllers
{
    public class FoodController : ApiController
    {
        // GET: api/food
        [HttpGet]
        public async Task<IEnumerable<Food>> Get(string query)
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["MongoUri"]);
            var database = client.GetDatabase("celery");
            var foodCollection = database.GetCollection<Food>("food");

            var filter = Builders<Food>.Filter.Where(x => x.Name.Contains(query));
            return await foodCollection.Find(filter).ToListAsync();
        }
    }
}
