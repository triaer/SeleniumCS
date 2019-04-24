using Breeze.Common.ExcelInterop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Breeze.UI.Tests
{
    [TestClass]
    class Sandbox
    {
        [TestMethod]
        public void Test()
        {
            var filePath = "D:\test.xlsx";

            var helper = ExcelDriver.getExcelHelper(filePath);

            
        }
    }
}
