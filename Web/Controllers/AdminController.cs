using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;
using Data.Dto;
using MongoDB.Driver;
using RawImport;

namespace Web.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        [HttpPut]
        [Route("repopulate")]
        public async Task Repopulate()
        {
            var importer = new Importer();
            List<Food> foods;
            List<MainCategory> mainCategories;
            List<SubCategory> subCategories;
            importer.Import(out foods, out mainCategories, out subCategories);

            var client = new MongoClient(ConfigurationManager.AppSettings["MongoUri"]);
            var database = client.GetDatabase("celery");
            await database.DropCollectionAsync("food");
            var collection = database.GetCollection<Food>("food");
            await collection.InsertManyAsync(foods);
        }
    }
}