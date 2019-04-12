using KiewitTeamBinder.Common.ExcelInterop;
using KiewitTeamBinder.Common.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TestProject
{
    [TestClass]
    public class Test : UITestBase
    {
        [TestMethod]
        public void DigiKey_Test()
        {
            try
            {

                //var driver = Browser.Open("http://google.com/", "chrome");
                //DigikeyMain digiMain = new DigikeyMain(driver);

                //var filePath = "D:\\test.xls";
                //var sheetName = "Words";

                //var excelHelper = ExcelDriver.getExcelHelper(filePath);
                //excelHelper.Open(filePath, "Words");
                //excelHelper.OpenExcelfileToView(filePath, sheetName, 5);

                //Console.WriteLine(ExcelUtils.GetNumberOfRows(filePath, sheetName).ToString());

                // JSON ======== https://jsoneditoronline.org/

                var path = Path.GetFullPath(@"..\..\TestData\test.json");
                List<Employee> lists = JsonHandler.ReadDataFromJson(path);
                Console.WriteLine(lists.Count);
                
                Employee p1 = lists[0];

                Console.WriteLine(p1.firstName + " | " + p1.lastName);
                

            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        

    }
}
