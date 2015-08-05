using Data.Dto;
using MongoDB.Driver;
using Xunit;

namespace Data.Tests
{
    public class MongoApiTests
    {
        [Fact]
        public async void CreateData()
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);

            var db = client.GetDatabase("sample");
            var foodCollection = db.GetCollection<Food>("food");

            var food = new Food();
            food.Id = "1";
            food.Name = "Food1";
            food.Calories = "20";

            //await foodCollection.InsertOneAsync(food);
        }
    }
}
