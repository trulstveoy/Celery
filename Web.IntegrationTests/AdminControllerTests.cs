using System.Configuration;
using Data.Dto;
using MongoDB.Driver;
using Web.Controllers;
using Xunit;

namespace Web.IntegrationTests
{
    public class AdminControllerTests
    {
        //[Fact]
        public async void RepopulateTest()
        {
            var c = new AdminController();
            await c.Repopulate();

            var client = new MongoClient(ConfigurationManager.AppSettings["MongoUri"]);
            var database = client.GetDatabase("celery");

            var foodCollection = database.GetCollection<Food>("food");

            var filter = Builders<Food>.Filter.Where(x => x.Name =="Cheese, for burgers");
            var food = await foodCollection.Find(filter).FirstOrDefaultAsync();

            Assert.Equal("Cheese, for burgers", food.Name);
        }
    }
}
