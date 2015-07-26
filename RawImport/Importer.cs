using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Data.Dto;
using OfficeOpenXml;

namespace ExcelImport
{
    public class Importer
    {
        private const string Filename = "Matvaretabellen_2014_eng.xlsx";
        public void Import(out List<Food> foods, out List<MainCategory> mainCategories, out List<SubCategory> subCategories )
        {
            foods = new List<Food>();
            mainCategories = new List<MainCategory>();
            subCategories = new List<SubCategory>();

            var foodRows = ReadFromExcel();
            foreach (var foodRow in foodRows)
            {
                if (mainCategories.All(x => x.Id != foodRow.MainCategoryId))
                {
                    mainCategories.Add(new MainCategory
                    {
                        Id = foodRow.MainCategoryId,
                        Name = foodRow.MainCategoryName
                    });
                }

                if (subCategories.All(x => x.Id != foodRow.SubCategoryId))
                {
                    subCategories.Add(new SubCategory
                    {
                        Id = foodRow.MainCategoryId,
                        Name = foodRow.MainCategoryName
                    });
                }

                foods.Add(new Food
                {
                    Id = foodRow.Id,
                    Name = foodRow.Name,
                    MainCategory = mainCategories.FirstOrDefault(x => x.Id == foodRow.MainCategoryId),
                    SubCategory = subCategories.FirstOrDefault(x => x.Id == foodRow.SubCategoryId),
                    Calories = foodRow.Calories,
                    EdiblePartPercent = foodRow.EdiblePartPercent,
                    KiloJoules = foodRow.KiloJoules,
                    WaterGrams = foodRow.WaterGrams
                });
            }

            //var mainCategories = foodRows.Where(IsMainCategory).Select(ToMainCategory).ToList();
            //var subCategories = foodRows.Where(IsSubCategory).Select(ToSubCategory).ToList();
            //var products = foodRows.Where(x => !IsMainCategory(x) && !IsSubCategory(x)).Select(x => ToFood(x, mainCategories, subCategories)).ToList();


        }

        

        

        private bool IsSubCategory(string id)
        {
            var tokens = id.Split('.');
            return tokens.Length == 2 && tokens[1].Length < 3;
        }

        private bool IsMainCategory(string id)
        {
            return !id.Contains('.');
        }


        private List<FoodRow> ReadFromExcel()
        {
            using (var stream = new FileStream(Filename, FileMode.Open, FileAccess.Read))
            {
                var package = new ExcelPackage();
                package.Load(stream);

                var workbook = package.Workbook;
                if (workbook == null)
                    throw new InvalidOperationException("Workbook not found");

                if (workbook.Worksheets.Count > 0)
                {
                    var sheet = package.Workbook.Worksheets["Foods"];
                    if (sheet == null)
                        throw new InvalidOperationException("Worksheet not found");

                    return ReadSheet(sheet);
                }
            }

            return new List<FoodRow>();
        }

        private List<FoodRow> ReadSheet(ExcelWorksheet sheet)
        {
            const int idCol = 1;
            const int name = 2;
            const int ediblePartPercent = 3;
            const int waterGrams = 5;
            const int kiloJoules = 7;
            const int calories = 9;

            string currentMainCategoryId = "";
            string currentMainCategoryName = "";
            string currentSubCategoryId = "";
            string currentSubCategoryName = "";

            var list = new List<FoodRow>();
            int rowNo = 1;
            int maxRowNo = sheet.Dimension.End.Row;
            while (rowNo <= maxRowNo)
            {
                var id = sheet.Cells[rowNo, idCol].GetValue<string>();
                if (string.IsNullOrWhiteSpace(id))
                {
                    rowNo++;
                    continue;
                }

                if (IsMainCategory(id))
                {
                    currentMainCategoryId = id;
                    currentMainCategoryName = sheet.Cells[rowNo, name].GetValue<string>();
                    rowNo++;
                    continue;
                }

                if (IsSubCategory(id))
                {
                    currentSubCategoryId = id;
                    currentSubCategoryName = sheet.Cells[rowNo, name].GetValue<string>();
                    rowNo++;
                    continue;
                }


                var foodRow = new FoodRow
                {
                    Id = id,
                    MainCategoryId = currentMainCategoryId,
                    MainCategoryName = currentMainCategoryName,
                    SubCategoryId = currentSubCategoryId,
                    SubCategoryName = currentSubCategoryName,
                    Name = sheet.Cells[rowNo, name].GetValue<string>(),
                    EdiblePartPercent = sheet.Cells[rowNo, ediblePartPercent].GetValue<string>(),
                    WaterGrams = sheet.Cells[rowNo, waterGrams].GetValue<string>(),
                    KiloJoules = sheet.Cells[rowNo, kiloJoules].GetValue<string>(),
                    Calories = sheet.Cells[rowNo, calories].GetValue<string>(),
                };
                list.Add(foodRow);
                rowNo++;
            }

            return list.Where(IsValid).ToList();
        }

        private bool IsValid(FoodRow foodRow)
        {
            decimal temp;
            return decimal.TryParse(foodRow.Id, NumberStyles.Any, CultureInfo.InvariantCulture, out temp);
        }
    }
}
