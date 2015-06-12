using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;

namespace TestExcelUnwrapping
{
    [TestClass]
    public class ExcelUnwrapper
    {
        private const string Filename = "Matvaretabellen_2014_eng.xlsx";

        [TestMethod]
        public void Unwrap()
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
                    if(sheet == null)
                        throw new InvalidOperationException("Worksheet not found");

                    var foodRows = ReadSheet(sheet);
                }
            }
        }

        private List<FoodRow> ReadSheet(ExcelWorksheet sheet)
        {
            const int idCol = 1;
            const int name = 2;
            const int ediblePartPercent = 3;
            const int waterGrams = 5;
            const int kiloJoules = 7;
            const int calories = 9;

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

                var foodRow = new FoodRow
                {
                    Id = id,
                    Name = sheet.Cells[rowNo, name].GetValue<string>(),
                    EdiblePartPercent = sheet.Cells[rowNo, ediblePartPercent].GetValue<string>(),
                    WaterGrams = sheet.Cells[rowNo, waterGrams].GetValue<string>(),
                    KiloJoules = sheet.Cells[rowNo, kiloJoules].GetValue<string>(),
                    Calories = sheet.Cells[rowNo, calories].GetValue<string>(),
                };
                list.Add(foodRow);
                rowNo++;
            }

            return list;
        }
    }
}
