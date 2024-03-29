﻿using AventStack.ExtentReports;
using Breeze.Common;
using Breeze.Common.DriverWrapper;
using Breeze.Common.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Breeze.UI.Tests
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
        [ThreadStatic]
        public static ExtentTest test;

        [TestInitialize]
        public void TestInitialize()
        {
            DriverProperties driverProperties = new DriverProperties();
            if (TestContext.Properties.Contains("browser"))
            {
                browser = TestContext.Properties["browser"].ToString();
                driverProperties.setDriverType(browser);
            }

            if (TestContext.Properties.Contains("arguments"))
            {
                driverProperties.setArguments(TestContext.Properties["arguments"].ToString());
            }

            if (TestContext.Properties.Contains("headless"))
            {
                driverProperties.setHeadless(bool.Parse(TestContext.Properties["headless"].ToString()));
            }

            if (TestContext.Properties.Contains("downloadlocation"))
            {
                driverProperties.setDownloadLocation(TestContext.Properties["downloadlocation"].ToString());
            }

            if (TestContext.Properties.Contains("environment"))
            {
                environment = TestContext.Properties["environment"].ToString();
            }

            if (!System.IO.Directory.Exists(captureLocation))
            {
                Directory.CreateDirectory(captureLocation);
            }

            WebDriver.InitDriverManager(driverProperties);

            string report = Utils.GetRandomValue(TestContext.TestName);
            reportPath = captureLocation + report + ".html";
            extent = ExtentReportsHelper.CreateReport(reportPath, TestContext.TestName);
            extent.AddSystemInfo("Environment", environment);
            extent.AddSystemInfo("Browser", browser);
            test = ExtentReportsHelper.LogTest("Pre-condition");
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
                    Screenshot screenshot = WebDriver.GetScreenshot();
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
                            {
                                ExtentReportsHelper.nodeList.LastOrDefault().Error(lastException.ToString(), ExtentReportsHelper.AttachScreenshot(filePath));
                            }
                            else
                            {
                                ExtentReportsHelper.test.Error(lastException.ToString(), ExtentReportsHelper.AttachScreenshot(filePath));
                            }

                            test.Error(TestContext.TestName + " Got Exception During Execution - " + lastException.Message + " " + lastException.StackTrace, ExtentReportsHelper.AttachScreenshot(filePath));
                            for (int i = 0; i < validations.Count; i++)
                            {
                                test.Info(string.Join(Environment.NewLine, validations[i]));
                            }
                        }
                    }
                    catch (Exception)
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
                Browser.QuitAll();
                return;
            }

            else if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                ReportResult(Status.Fail, reportPath);
                Browser.QuitAll();
                return;
            }
        }
    }
}
