using System.Collections.Generic;
using Data.Dto;
using Xunit;

namespace RawImport.Tests
{
    public class ImporterTests
    {
        //[Fact]
        public void Foo()
        {
            var importer = new Importer();
            List<Food> foods;
            List<MainCategory> mainCategories;
            List<SubCategory> subCategories;
            importer.Import(out foods, out mainCategories, out subCategories);
        }
    }
}
