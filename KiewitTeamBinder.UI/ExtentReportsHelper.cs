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
        public static ExtentReports ReportCreation(string reportPath, string reportName)
        {

            System.IO.File.Create(reportPath);
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.LoadConfig(Utils.GetProjectPath() + "extent-config.xml");
            htmlReporter.Configuration().ReportName = reportName;
            htmlReporter.Configuration().DocumentTitle = reportName;
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extentTestList = new List<ExtentTest>();
            nodeList = new List<ExtentTest>();
            return extent;
        }

        public static ExtentTest LogTest(string testDetail, string testDescription = null)
        {
            test = extent.CreateTest(testDetail, testDescription);
            if (!extentTestList.Contains(test))
                extentTestList.Add(test);
            return test;
        }

        public static ExtentTest StepNode([CallerMemberName]string memberName = "")
        {
            var node = extentTestList.LastOrDefault().CreateNode(memberName);
            nodeList.Add(node);
            return node;
        }

        public static MediaEntityModelProvider AttachScreenshot(string imagePath)
        {
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(Utils.ImageToBase64(imagePath)).Build();
        }
    }
}
