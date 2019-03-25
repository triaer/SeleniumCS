using System;
using System.Diagnostics;
using System.IO;
using Agoda.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using System.Linq;
using Agoda.Common.Helper;
using System.Collections.Generic;

namespace Agoda.UI.Tests
{
    [TestClass]
    public abstract class UITestBase
    {
        protected string browser = string.Empty;
        protected string environment = string.Empty;
        private string excelUserSourcePath = string.Empty;
        private string localTempExcelUserTargetPath = string.Empty;
        private string reportPath = string.Empty;
        protected string captureLocation = "c:\\temp\\testresults\\";
        protected List<KeyValuePair<string, bool>> validations = new List<KeyValuePair<string, bool>>();
        protected List<KeyValuePair<string, bool>> methodValidations = new List<KeyValuePair<string, bool>>();
        public Exception lastException;
        public ExtentReports extent;
        public TestContext TestContext { get; set; }
        public static ExtentTest test;
        protected string teamBinderVersion;

        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.Properties.Contains("browser"))
            {
                browser = TestContext.Properties["browser"].ToString();
            }

            if (TestContext.Properties.Contains("environment"))
            {
                environment = TestContext.Properties["environment"].ToString();
            }

            if (TestContext.Properties.Contains("teamBinderVersion"))
            {
                teamBinderVersion = TestContext.Properties["teamBinderVersion"].ToString();
            }

            if (TestContext.Properties.Contains("excelUserSourcePath"))
            {
                excelUserSourcePath = TestContext.Properties["excelUserSourcePath"].ToString();
            }

            if (TestContext.Properties.Contains("localTempExcelUserTargetPath"))
            {
                localTempExcelUserTargetPath = TestContext.Properties["localTempExcelUserTargetPath"].ToString();
            }

            if (TestContext.Properties.Contains("headless"))
            {
                Browser.Headless = bool.Parse(TestContext.Properties["headless"].ToString());
            }

            if (!System.IO.Directory.Exists(captureLocation))
            {
                Directory.CreateDirectory(captureLocation);
            }

            string report = Utils.GetRandomValue(TestContext.TestName);
            reportPath = captureLocation + report + ".html";
            extent = ExtentReportsHelper.CreateReport(reportPath, TestContext.TestName);
            extent.AddSystemInfo("Environment", TestContext.Properties["environment"].ToString());
            extent.AddSystemInfo("Browser", TestContext.Properties["browser"].ToString());
            test = ExtentReportsHelper.LogTest("Pre-condition");
        }



        protected TestAccount GetTestAccount(string role, string environment, string type, string tbUserRole = "AdminAccount1")
        {
            string sourceFilePath = excelUserSourcePath; // @"C:\working\DT_CommonDataKiewitTeamBinder.xls";
            var user = TestAccountAccess.GetTestAccount(role, environment, type, tbUserRole, excelUserSourcePath, localTempExcelUserTargetPath);

            return user;
        }

        protected void ReportResult(Status status, string reportFilePath)
        {
            test = extent.CreateTest("Test Summary");

            if (status == Status.Pass)
            {
                test.Pass(TestContext.TestName + " Passed");
                for (int i = 0; i < validations.Count; i++)
                {
                    test.Info(string.Join(Environment.NewLine, validations[i]));
                }
            }

            else
            {
                //ExtentReportsHelper.test.Error(lastException);
                //string callingMethodName = new StackFrame(1, true).GetMethod().Name;
                //string callingClassName = GetType().Name;
                string timeStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");
                string filePath = captureLocation + "ErrorCapture" + timeStamp + ".png";
                try
                {
                    Screenshot screenshot = ((ITakesScreenshot)Browser.Driver).GetScreenshot();
                    screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
                }
                catch (Exception e)
                {
                    Console.WriteLine("TakeScreenshot encountered an error. " + e.Message);
                }
                finally
                {
                    try
                    {
                        if (lastException == null || lastException.ToString().Contains("Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException"))
                        {
                            test.Fail(TestContext.TestName + " Failed - " + lastException.Message);
                            for (int i = 0; i < validations.Count; i++)
                            {
                                test.Info(string.Join(Environment.NewLine, validations[i]));
                            }
                        }

                        else
                        {
                            if (ExtentReportsHelper.nodeList.LastOrDefault() != null)
                                ExtentReportsHelper.nodeList.LastOrDefault().Error(lastException.ToString(), ExtentReportsHelper.AttachScreenshot(filePath));
                            {
                                test.Error(TestContext.TestName + " Got Exception During Execution - " + lastException.Message + " " + lastException.StackTrace, ExtentReportsHelper.AttachScreenshot(filePath));
                                for (int i = 0; i < validations.Count; i++)
                                {
                                    test.Info(string.Join(Environment.NewLine, validations[i]));
                                }
                            }

                        }
                    }
                    catch(Exception)
                    {
                        // do nothing
                    }
                }
                TestContext.AddResultFile(filePath);
            }

            extent.AnalysisStrategy = AnalysisStrategy.Test;
            extent.Flush();
            TestContext.AddResultFile(reportPath);
        }


        [TestCleanup]
        public void TestCleanup()
        {

            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
            {
                ReportResult(Status.Pass, reportPath);
                Browser.Quit();
                return;
            }

            else if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                ReportResult(Status.Fail, reportPath);
                Browser.Quit();
                return;
            }
        }
    }
}
