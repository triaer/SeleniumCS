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

namespace KiewitTeamBinder.UI
{
    /// <summary>
    /// Browser class
    /// </summary>
    public static class Browser
    {
        private static IWebDriver webDriver;
        private static bool headless = false;

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
                ieOptions.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
                ieOptions.EnablePersistentHover = true;
                ieOptions.RequireWindowFocus = true;
                ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                ieOptions.IgnoreZoomLevel = true;
                ieOptions.EnsureCleanSession = true;
                ieOptions.AddAdditionalCapability("disable-popup-blocking", "true");

                string ieWebDriver = Environment.GetEnvironmentVariable("IEWebDriver");
                if (string.IsNullOrEmpty(ieWebDriver))
                {
                    webDriver = new InternetExplorerDriver(ieOptions);
                }
                else
                {
                    webDriver = new InternetExplorerDriver(ieWebDriver,ieOptions);
                }
            }
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
    }
}
