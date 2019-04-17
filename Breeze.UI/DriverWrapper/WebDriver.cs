using System.Collections.Generic;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using Microsoft.Win32;
using OpenQA.Selenium.Remote;
using System.IO;

namespace Breeze.UI.DriverWrapper
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

        public static void InitDriverManager(DriverProperties pro, string plaform)
        {
            listDriver = new Dictionary<string, IWebDriver>() ;
            listProperties = new Dictionary<string, DriverProperties>();
            defaultKey = plaform + "-1";
            timeOut = 60;
            defaultDriverProperties = pro;
        }

        public static void CreateDriverByProperties(DriverProperties properties, string plaform)
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
                    options.AddArguments("--disable-gpu");
                    options.AddUserProfilePreference("disable-popup-blocking", "true");
                    options.AddUserProfilePreference("intl.accept_languages", "en,en_US");
                }

                if (properties.getDownloadLocation() != null)
                    options.AddUserProfilePreference("download.default_directory", properties.getDownloadLocation());
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

                options.AddArgument("--private");

                webDriver = new FirefoxDriver(options);
            }
            else
            {

            }

            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

            key = plaform + "-" + getNextPlatformNumber(plaform);
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

        public static DriverProperties GetPDefaultProperties()
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
        public static void Wait(int second) {
            System.Threading.Thread.Sleep(second * 1000);

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
    }
}
