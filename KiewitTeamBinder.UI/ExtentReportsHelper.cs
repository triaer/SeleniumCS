using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI
{
    public class ExtentReportsHelper
    {
        public static ExtentReports extent;
        public static ExtentTest test;
        public static List<ExtentTest> extentTestList;
        public static List<ExtentTest> nodeList;
        public static ExtentReports CreateReport(string reportPath, string reportName)
        {
            
            System.IO.File.Create(reportPath).Dispose();
            var htmlReporter = new ExtentV3HtmlReporter(reportPath);
            //htmlReporter.LoadConfig(Utils.GetProjectPath() + "extent-config.xml");
            htmlReporter.Config.ReportName = reportName;
            htmlReporter.Config.DocumentTitle = reportName;
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extentTestList = new List<ExtentTest>();
            nodeList = new List<ExtentTest>();
            return extent;
        }

        public static ExtentTest LogTest(string testDetail, string testDescription = null)
        {
            test = extent.CreateTest(testDetail, testDescription);
            nodeList = new List<ExtentTest>();
            if (!extentTestList.Contains(test))
                extentTestList.Add(test);
            return test;
        }

        public static ExtentTest CreateStepNode([CallerMemberName]string memberName = "")
        {
            ExtentTest node;
            if (nodeList.Count == 0)
            {
                node = extentTestList.LastOrDefault().CreateNode(memberName);
                nodeList.Add(node);
            }
            else
            {
                node = nodeList.LastOrDefault().CreateNode(memberName);
                if (!nodeList.Contains(node))
                    nodeList.Add(node);
            }
            return node;
        }

        public static void EndStepNode(ExtentTest node)
        {
            if (nodeList.ElementAt(0) == node)
                nodeList = new List<ExtentTest>();
        }


        public static MediaEntityModelProvider AttachScreenshot(string imagePath)
        {
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(Utils.ImageToBase64(imagePath)).Build();
        }
    }
}
