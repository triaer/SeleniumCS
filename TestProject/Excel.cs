using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Test.ExcelInterop;
using TestProject.ExcelInterop;

namespace TestProject
{
    [TestClass]
    public class Excel : UITestBase
    {
        [TestMethod]
        public void excel()
        {
            var xlsPath = "D:\\test.xls";
            var xlsxPath = "D:\\test.xlsx";
            //var csvPath = "D:\\test.csv";

            //var sheet = "test";
            var sheet1 = "Words";
            var sheet2 = "Rules";

            var excelHelper = (Old_ExcelHelper)ExcelDriver.getExcelHelper(xlsPath);

            excelHelper.LoadExcelSheetData(xlsPath, sheet1);

            var array = excelHelper.SearchAllCellsContain("que", false);
            foreach (var a in array)
                Console.WriteLine(a);
            

        }
    }
}
