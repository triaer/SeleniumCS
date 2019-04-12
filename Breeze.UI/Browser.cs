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

namespace Breeze.UI
{
    /// <summary>
    /// Browser class
    /// </summary>
    public static class Browser
    {
        public static IWebDriver Open(string url, string plaftform, bool headless = false, string fileDownloadLocation = null, string arguments = null, string key = null)
        {
            DriverProperties prop = new DriverProperties(plaftform, headless, fileDownloadLocation, arguments);
            if (key == null)
            {
                key = plaftform;
            }
            WebDriver.SetDriverByKey(prop, key);
            WebDriver.GoToUrl(url);
            MaximizeWindow();

            return WebDriver.CurrentDriver();
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
            get { return WebDriver.CurrentDriver(); }
        }

        public static void Navigate(string url)
        {
            WebDriver.CurrentDriver().Url = url;
        }

        internal static WebDriverWait Wait(int timeoutSecond = 30)
        {
            return new WebDriverWait(WebDriver.CurrentDriver(), TimeSpan.FromSeconds(timeoutSecond));
        }

        public static string Title
        {
            get { return WebDriver.CurrentDriver().Title; }
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
