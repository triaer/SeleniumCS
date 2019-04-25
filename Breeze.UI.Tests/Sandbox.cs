using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net;

namespace Breeze.UI.Tests
{
    [TestClass]
    class Sandbox
    {
        [TestMethod]
        public void Test()
        {
            //var filePath = "D:\test.xlsx";
            //var helper = (ExcelHelper)ExcelDriver.getExcelHelper(filePath);
            //helper.UpdateCellValue(filePath, "test", );

            WebClient webClient = new WebClient();
            string page = webClient.DownloadString("file:///C:/temp/testresults/TC001_20190425023754.html");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            var table = doc.DocumentNode.SelectNodes("(//div[@class='test-heading'])[2]").ToList();
            string name = table[0].ChildNodes[1].InnerText.Trim();
            string date = table[0].ChildNodes[3].InnerText.Trim();
            string result = table[0].ChildNodes[5].InnerText.Trim();


            //return table;

        }
    }
}
