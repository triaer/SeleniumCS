using System.Collections.Generic;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace Breeze.UI.DriverWrapper
{
    ///<summary>
    ///Use to control selenium web driver.
    ///</summary>
    public class WebDriver 
    {
        private static int timeOut = 60;
        ///<summary>
        ///Contain properties of web driver
        ///</summary>
        private static string defaultKey;
        private static string currentKey;
        private static ThreadLocal<DriverManager> DRIVER = new ThreadLocal<DriverManager>();

        public static void InitDriverManager(DriverProperties pro, string key = "default")
        {
            defaultKey = key;
            DRIVER.Value = new DriverManager();
        }

        ///<summary>
        ///Add webdriver by key
        ///</summary>
        public static void SetDriverByKey(DriverProperties pro, string key)
        {
            currentKey = key;
            DRIVER.Value.CreateDriverByProperties(pro, key);
        }

        ///<summary>
        ///return current webdriver
        ///</summary>
        public static IWebDriver GetDriver()
        {
            return DRIVER.Value.GetDriverByKey(currentKey);
        }

        public static DriverProperties GetProperties()
        {
            return DRIVER.Value.GetPropertiesByKey(currentKey);
        }

        public static DriverProperties GetPropertiesByKey(string key)
        {
            return DRIVER.Value.GetPropertiesByKey(key);
        }

        ///<summary>
        /// Switch To Default Web Driver.
        ///</summary>
        public static void SwitchToDefaultDriver()
        {
            currentKey = defaultKey;
        }

        ///<summary>
        ///Switch to another driver in the list, 1st driver key = defaultKey if not set
        ///</summary>
        public static void SwitchDriverTo(string driverKey) {
            currentKey = driverKey;
        }
        ///<summary>
        ///Find web element by locator.
        ///</summary>
        public static IWebElement FindElement(By locator) {
            return DRIVER.Value.GetDriverByKey(currentKey).FindElement(locator);
        }


        ///<summary>
        ///Find all web elements by locator and return to a List
        ///</summary>
        public static ReadOnlyCollection<IWebElement> FindElements(By locator) {
            return DRIVER.Value.GetDriverByKey(currentKey).FindElements(locator);
        }

        ///<summary>
        ///Navigate to URL.
        ///</summary>
        public static void GoToUrl(string url) {
            DRIVER.Value.GetDriverByKey(currentKey).Navigate().GoToUrl(url);
        }

        ///<summary>
        ///Return current URL.
        ///</summary>
        public static string CurrentUrl()
        {
            return DRIVER.Value.GetDriverByKey(currentKey).Url;
        }

        ///<summary>
        ///Close browser.
        ///</summary>
        public static void Close() {
            DRIVER.Value.GetDriverByKey(currentKey).Close();
        }

        public static void Quit()
        {
            DRIVER.Value.GetDriverByKey(currentKey).Quit();
        }

        public static void QuitAllDriver()
        {
            DRIVER.Value.QuitAllDriver();
        }

        ///<summary>
        ///Maximize web browser.
        ///</summary>
        public static void Maximize() {
            DRIVER.Value.GetDriverByKey(currentKey).Manage().Window.Maximize();
        }

        ///<summary>
        ///Minimize web browser.
        ///</summary>
        public static void Minimize()
        {
            DRIVER.Value.GetDriverByKey(currentKey).Manage().Window.Minimize();
        }

        ///<summary>
        ///Switch to Iframe IWebElement.
        ///</summary>
        public static void SwitchToIframe(IWebElement iframe)
        {
            DRIVER.Value.GetDriverByKey(currentKey).SwitchTo().Frame(iframe);
        }

        ///<summary>
        ///Switch to Previous frame 
        ///</summary>
        public static void SwitchToPrevious() {
            DRIVER.Value.GetDriverByKey(currentKey).SwitchTo().ParentFrame();
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
            return (IJavaScriptExecutor)DRIVER.Value.GetDriverByKey(currentKey);
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

        public static IWebDriver CurrentDriver()
        {
            return DRIVER.Value.GetDriverByKey(currentKey);
        }
    }
}
