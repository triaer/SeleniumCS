using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            var excelHelper = (New_ExcelHelper)ExcelDriver.getExcelHelper(xlsxPath);

            excelHelper.LoadExcelSheetData(xlsxPath, sheet1);


            //excelHelper.PrintDataTable();
            

            
            
            



            
            



            //ExcelUtils.OpenFiletoView(xlsPath, sheet1, 5);
        }
    }
}
