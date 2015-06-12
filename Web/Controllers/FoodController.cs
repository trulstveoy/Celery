using System.Collections.Generic;
using Data.Dto;
using Microsoft.AspNet.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class FoodController : Controller
    {
        // GET: api/food
        [HttpGet]
        public IEnumerable<Food> Get()
        {
            return new[]
            {
                new Food {Category = "Fruit", Name="Banana"},
                new Food {Category = "Meat", Name="Pork"},
            };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
