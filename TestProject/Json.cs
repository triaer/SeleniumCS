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
    public class Json : UITestBase
    {
        [TestMethod]
        public void json()
        {
            try
            {

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
