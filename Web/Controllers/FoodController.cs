using System.Collections.Generic;
using System.Web.Http;
using Data.Dto;
using ExcelImport;
using System.Linq;

namespace Web.Controllers
{
    public class FoodController : ApiController
    {
        private static readonly List<Food> _foods;

        static FoodController()
        {
            var importer = new Importer();
            List<MainCategory> mainCategories;
            List<SubCategory> subCategories;
            importer.Import(out _foods, out mainCategories,  out subCategories);    
        }

        // GET: api/food
        [HttpGet]
        public IEnumerable<Food> Get()
        {
            return _foods.Take(10);
        }
    }
}
