using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Reflection;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using SimpleImpersonation;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using Breeze.Common;
using System.Globalization;
using OpenQA.Selenium.Firefox;
using Breeze.UI.DriverWrapper;
using Breeze.Common.Helper;

namespace Breeze.UI
{
    /// <summary>
    /// Browser class
    /// </summary>
    public static class Browser
    {
        public static IWebDriver Open(string url, string platform, bool headless, string fileDownloadLocation, string arguments = null)
        {
            DriverProperties prop = new DriverProperties(platform, headless, fileDownloadLocation, arguments);
            WebDriver.CreateDriverByProperties(prop, platform);
            WebDriver.GoToUrl(url);
            MaximizeWindow();

            return WebDriver.GetDriver();
        }

        public static IWebDriver Open(string url, string platform)
        {
            DriverProperties prop = WebDriver.GetPDefaultProperties();

            if (prop.getDriverType().ToDescription() != platform.ToLower())
            {
                prop = new DriverProperties(platform);
            }

            WebDriver.CreateDriverByProperties(prop, platform);
            WebDriver.GoToUrl(url);
            MaximizeWindow();

            return WebDriver.GetDriver();
        }

        public static void Close()
        {
            WebDriver.Close();
        }

        public static void Quit()
        {
            WebDriver.Quit();
        }

        public static void QuitAll()
        {
            WebDriver.QuitAllDriver();
        }

        public static ISearchContext Driver
        {
            get { return WebDriver.GetDriver(); }
        }

        public static void Navigate(string url)
        {
            WebDriver.GetDriver().Url = url;
        }

        internal static WebDriverWait Wait(int timeoutSecond = 30)
        {
            return new WebDriverWait(WebDriver.GetDriver(), TimeSpan.FromSeconds(timeoutSecond));
        }

        public static string Title
        {
            get { return WebDriver.GetDriver().Title; }
        }

        internal static void MaximizeWindow()
        {
            WebDriver.Maximize();
        }

        internal static void MinimizeWindow()
        {
            WebDriver.Minimize();
        }
    }
}
