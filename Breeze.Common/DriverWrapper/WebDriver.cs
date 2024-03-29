﻿using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Breeze.Common.Helper;

namespace Breeze.Common.DriverWrapper
{
    ///<summary>
    ///Use to control selenium web driver.
    ///</summary>
    public class WebDriver 
    {
        [ThreadStatic]
        private static int timeOut;
        [ThreadStatic]
        private static DriverProperties defaultDriverProperties;
        [ThreadStatic]
        private static string defaultKey;
        [ThreadStatic]
        private static string currentKey;
        [ThreadStatic]
        private static Dictionary<string, IWebDriver> listDriver;
        [ThreadStatic]
        private static Dictionary<string, DriverProperties> listProperties;

        public static void InitDriverManager(DriverProperties pro)
        {
            listDriver = new Dictionary<string, IWebDriver>() ;
            listProperties = new Dictionary<string, DriverProperties>();
            defaultKey = pro.getDriverType().ToDescription() + "-1";
            timeOut = 60;
            defaultDriverProperties = pro;
        }

        public static void CreateDriverByProperties(DriverProperties properties)
        {
            IWebDriver webDriver = null;
            string key;
            string defaultDownloadLocation = Path.GetPathRoot(Environment.SystemDirectory) + "Users\\" + Environment.UserName + "\\Downloads";
            if (properties.getDriverType() == DriverType.Chrome)
            {
                ChromeOptions options = new ChromeOptions();
                if (properties.isHeadless())
                {
                    options.AddArgument("--headless");
                    options.AddArgument("--disable-gpu");
                    options.AddUserProfilePreference("disable-popup-blocking", "true");
                    options.AddUserProfilePreference("intl.accept_languages", "en,en_US");
                }

                if (properties.getDownloadLocation() != null)
                {
                    options.AddUserProfilePreference("download.default_directory", properties.getDownloadLocation());
                }

                if (properties.getArguments() != null)
                {
                    options.AddArguments(properties.getArguments());
                }

                // run in private mode
                options.AddArgument("--incognito");

                webDriver = new ChromeDriver(options);
            }
            else if (properties.getDriverType() == DriverType.IE)
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

                if (properties.getDownloadLocation() != null)
                {
                    RegistryKey myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main", true);
                    if (myKey != null)
                    {
                        myKey.SetValue("Default Download Directory", properties.getDownloadLocation());
                        myKey.Close();
                    }

                    myKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Zones\\3", true);
                    if (myKey != null)
                    {
                        myKey.SetValue("1803", 0);
                        myKey.Close();
                    }
                }
                //ieOptions.ForceCreateProcessApi = true;
                //// run in private mode
                //string arguments = "-private";

                //if (properties.getArguments() != null)
                //{
                //    arguments += " " + properties.getArgumentsAsString();
                //}

                //ieOptions.BrowserCommandLineArguments = arguments;

                webDriver = new InternetExplorerDriver(ieOptions);
            }
            else if (properties.getDriverType() == DriverType.Firefox)
            {
                FirefoxOptions options = new FirefoxOptions();
                if (properties.isHeadless())
                {
                    options.AddArgument("--headless");
                    options.SetPreference("intl.accept_languages", "en,en_US");
                }

                if (properties.getDownloadLocation() != null)
                {
                    options.SetPreference("browser.download.folderList", 2);
                    options.SetPreference("browser.download.dir", properties.getDownloadLocation());
                }

                if (properties.getArguments() != null)
                {
                    options.AddArguments(properties.getArguments());
                }

                // run in private mode
                options.AddArgument("--private");


                webDriver = new FirefoxDriver(options);
            }
            else
            {
                // Handle more platforms here
            }

            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            key = properties.getDriverType().ToDescription() + "-" + getNextPlatformNumber(properties.getDriverType().ToDescription());
            listDriver.Add(key, webDriver);
            listProperties.Add(key, properties);
            currentKey = key;
        }

        private static int getNextPlatformNumber(string plaform)
        {
            int number = 1;
            foreach (var item in listDriver.Keys)
            {
                int lastIndex = item.LastIndexOf("-");
                if (item.Substring(0, lastIndex) == plaform)
                {
                    number++;
                }
            }
            return number;
        }

        ///<summary>
        ///Get Title
        ///</summary>
        public static string Title
        {
            get { return GetDriver().Title; }
        }

        ///<summary>
        ///Get Actions
        ///</summary>
        public static Actions Actions
        {
            get { return new Actions(GetDriver()); }
        }

        ///<summary>
        ///Get Manage()
        ///</summary>
        public static IOptions Manage
        {
            get { return GetDriver().Manage(); }
        }

        ///<summary>
        ///Get Navigate()
        ///</summary>
        public static INavigation Navigate
        {
            get { return GetDriver().Navigate(); }
        }

        ///<summary>
        ///Get SwitchTo()
        ///</summary>
        public static ITargetLocator SwitchTo
        {
            get { return GetDriver().SwitchTo(); }
        }

        ///<summary>
        ///return current webdriver
        ///</summary>
        public static IWebDriver GetDriver()
        {
            return listDriver[currentKey];
        }

        ///<summary>
        ///return current DriverProperties
        ///</summary>
        public static DriverProperties GetProperties()
        {
            return listProperties[currentKey];
        }

        public static DriverProperties GetTargetProperties(string platform, int index = 1)
        {
            try
            {
                return listProperties[platform + "-" + index];
            }
            catch
            {
                return null;
            }
        }

        public static DriverProperties GetDefaultProperties()
        {
            return defaultDriverProperties;
        }

        public static void SwitchToDefaultDriver()
        {
            currentKey = defaultKey;
        }

        public static void SwitchToTargetDriver(string platform, int index = 1)
        {
            currentKey = platform + "-" + index;
        }

        ///<summary>
        ///Find web element by locator.
        ///</summary>
        public static IWebElement FindElement(By locator) {
            return GetDriver().FindElement(locator);
        }


        ///<summary>
        ///Find all web elements by locator and return to a List
        ///</summary>
        public static ReadOnlyCollection<IWebElement> FindElements(By locator) {
            return GetDriver().FindElements(locator);
        }

        ///<summary>
        ///Navigate to URL.
        ///</summary>
        public static void GoToUrl(string url) {
            GetDriver().Navigate().GoToUrl(url);
        }

        ///<summary>
        ///Return current URL.
        ///</summary>
        public static string CurrentUrl()
        {
            return GetDriver().Url;
        }

        ///<summary>
        ///Close browser.
        ///</summary>
        public static void Close() {
            GetDriver().Close();
        }

        public static void Quit()
        {
            GetDriver().Quit();
        }

        public static void QuitAllDriver()
        {
            foreach (var driver in listDriver.Values)
            {
                driver.Quit();
            }
        }

        ///<summary>
        ///Maximize web browser.
        ///</summary>
        public static void Maximize() {
            GetDriver().Manage().Window.Maximize();
        }

        ///<summary>
        ///Minimize web browser.
        ///</summary>
        public static void Minimize()
        {
            GetDriver().Manage().Window.Minimize();
        }

        ///<summary>
        ///Switch to Iframe IWebElement.
        ///</summary>
        public static void SwitchToIframe(IWebElement iframe)
        {
            GetDriver().SwitchTo().Frame(iframe);
        }

        ///<summary>
        ///Switch to Previous frame 
        ///</summary>
        public static void SwitchToPrevious() {
            GetDriver().SwitchTo().ParentFrame();
        }

        ///<summary>
        ///wait by second
        ///</summary>
        public static void Sleep(int second) {
            System.Threading.Thread.Sleep(second * 1000);
        }

        ///<summary>
        ///Create WebDriverWait
        ///</summary>
        public static WebDriverWait Wait(int second = 30)
        {
            return new WebDriverWait(GetDriver(), TimeSpan.FromSeconds(second));
        }

        ///<summary>
        ///Create javascript executor.
        ///</summary>
        public static IJavaScriptExecutor JsExecutor() {
            return (IJavaScriptExecutor)GetDriver();
        }

        ///<summary>
        ///Execute javascript .
        ///</summary>
        public static object ExecuteScript(string code, params object[] args)
        {
            return JsExecutor().ExecuteScript(code, args);
        }

        ///<summary>
        ///set implicit waits time out for web-driver , default is 60 seconds
        ///</summary>
        public static void SetImplicitWaits(int second) {
            timeOut = second;
        }

        ///<summary>
        ///Scroll the web page till end.
        ///</summary>
        public static void ScrollTillEnd()
        {
            string js = string.Format("window.scrollTo(0, document.body.scrollHeight)");
            JsExecutor().ExecuteScript(js);
        }

        ///<summary>
        ///Get Screenshot
        ///</summary>
        public static Screenshot GetScreenshot()
        {
            return ((ITakesScreenshot)GetDriver()).GetScreenshot();
        }
    }
}
