﻿using System;
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
using KiewitTeamBinder.UI.Common;

namespace KiewitTeamBinder.UI
{
    /// <summary>
    /// Browser class
    /// </summary>
    public static class Browser
    {
        private static IWebDriver webDriver;
        private static bool headless = false;
        private static string browser = "";
        private static string language = Language.ENGLISH;
        private static string currency = Currency.VIETNAM_DONG;

        public static void Close()
        {
            webDriver.Close();
        }

        public static ISearchContext Driver
        {
            get { return webDriver; }
        }

        public static void Navigate(string url)
        {
            webDriver.Url = url;
        }

        internal static WebDriverWait Wait(int timeoutSecond = 30)
        {
            return new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeoutSecond));
        }

        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static bool Headless
        {
            get { return headless; }
            set { headless = value; }
        }

        public static string CurrentBrowser
        {
            get { return browser; }
        }

        public static string CurrentLanguage
        {
            get { return language; }
            set { language = value; }
        }

        public static string CurrentCurrency
        {
            get { return currency; }
            set { currency = value; }
        }

        public static IWebDriver Open(string url, string browserName, string fileDownloadLocation = null)
        {
            DesiredCapabilities capability = new DesiredCapabilities();
            capability.SetCapability("browserName", browserName);
            Uri server = new Uri(url);
            string defaultDownloadLocation = Path.GetPathRoot(Environment.SystemDirectory) + "Users\\" + Environment.UserName + "\\Downloads";
            if (browserName == "chrome")
            {
                ChromeOptions options = new ChromeOptions();
                if (headless)
                {
                    options.AddArgument("--headless");
                    options.AddArguments("--disable-gpu"); options.AddUserProfilePreference("disable-popup-blocking", "true");
                    options.AddUserProfilePreference("intl.accept_languages", "en,en_US");
                }

                if (fileDownloadLocation != null)
                    options.AddUserProfilePreference("download.default_directory", fileDownloadLocation);
                options.AddArgument("--incognito");


                webDriver = new ChromeDriver(options);
            }
            else if (browserName.ToLower() == "internetexplorer")
            {

                InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                ieOptions.EnableNativeEvents = true;
                ieOptions.UnhandledPromptBehavior = UnhandledPromptBehavior.Ignore;
                ieOptions.EnablePersistentHover = true;
                ieOptions.RequireWindowFocus = true;
                ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                ieOptions.IgnoreZoomLevel = true;
                ieOptions.EnsureCleanSession = true;
                ieOptions.AddAdditionalCapability("disable-popup-blocking", true);
                ieOptions.AddAdditionalCapability(CapabilityType.IsJavaScriptEnabled, true);
                //ieOptions.AddAdditionalCapability(CapabilityType.UnexpectedAlertBehavior, "ignore");



                if (fileDownloadLocation != null)
                {
                    RegistryKey myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main", true);
                    if (myKey != null)
                    {
                        myKey.SetValue("Default Download Directory", fileDownloadLocation);
                        myKey.Close();
                    }

                    myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\3", true);
                    if (myKey != null)
                    {
                        myKey.SetValue("1803", 0);
                        myKey.Close();
                    }
                }

                //string ieWebDriver = Environment.GetEnvironmentVariable("IEWebDriver");
                string ieWebDriver = null;
                if (string.IsNullOrEmpty(ieWebDriver))
                {
                    webDriver = new InternetExplorerDriver(ieOptions);
                }
                else
                {
                    webDriver = new InternetExplorerDriver(ieWebDriver,ieOptions);
                }
            }
            browser = browserName;

            webDriver.Navigate().GoToUrl(server);

            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            MaximizeWindow();

            return webDriver;
        }

        internal static void MaximizeWindow()
        {
            webDriver.Manage().Window.Maximize();
        }

        internal static void MinimizeWindow()
        {
            webDriver.Manage().Window.Minimize();
        }

        public static void Quit()
        {
            webDriver.Quit();
        }
        public static string GetActiveDriverInfo()
        {
            ICapabilities capabilities = ((RemoteWebDriver)webDriver).Capabilities;
            //string info = "Browser Capabilities:\n"
            //            + "Name = " + capabilities.GetCapability("browserName").ToString() + "-\n"
            //            + "Version = " + capabilities.GetCapability("browserVersion").ToString() + "-\n"
            //            + "Supports JavaScript  = " + capabilities.GetCapability(CapabilityType.IsJavaScriptEnabled).ToString() + "-\n"
            //            + "Handles Alerts = " + capabilities.GetCapability(CapabilityType.HandlesAlerts);
            return capabilities.ToString();
        }
    }
}
