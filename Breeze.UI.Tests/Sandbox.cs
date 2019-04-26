using Breeze.Common.ExcelInterop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Breeze.UI.Tests
{
    [TestClass]
    public class Sandbox
    {
        [TestMethod]
        public void TestCollectHTML()
        {
            List<string[]> results = new List<string[]>();

            WebClient webClient = new WebClient();

            DirectoryInfo dir = new DirectoryInfo(@"C:\temp\testresults");
            FileInfo[] Files = dir.GetFiles("*.html");
            
            foreach (FileInfo file in Files)
            {
                var filePath = "file:///C:/temp/testresults/" + file.Name;
                string page = webClient.DownloadString(filePath);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);

                var table = doc.DocumentNode.SelectNodes("(//div[@class='test-heading'])[2]").ToList();
                string testID = table[0].ChildNodes[1].InnerText.Trim();
                string executeDate = table[0].ChildNodes[3].InnerText.Trim();
                string executeResult = table[0].ChildNodes[5].InnerText.Trim();

                results.Add(new string[3] { testID, executeDate, executeResult });
            }

            var tempFilePath = "D:\\TempResult.txt";
            StreamWriter sw;

            if (File.Exists(tempFilePath))
                sw = new StreamWriter(tempFilePath, append: true);
            else
                sw = new StreamWriter(tempFilePath, append: false);
            

            foreach (string[] item in results)
            {
                try
                {
                    var line = "";
                    for (int i = 0; i < item.Count(); i++)
                    {
                        line += item[i] + " | ";
                    }
                    sw.WriteLine(line);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            sw.Close();

        }

        [TestMethod]
        public void TestExcel()
        {
            var filePath = "D:\\test.xlsx";
            var helper = ExcelDriver.getExcelHelper(filePath);
            //helper.UpdateCellValue(filePath, "Words", 2, 2, "update cell value 1" );
            //helper.LoadExcelSheetData(filePath, "Words");
            helper.InsertRow(2, 1);

            //helper.InsertColumn(2, 1);
            

            //helper.OpenExcelFileToView(filePath, "Words", 3);
            helper.Close();
        }
    }
}
