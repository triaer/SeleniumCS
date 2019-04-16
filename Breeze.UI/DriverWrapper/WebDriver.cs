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
        private static ThreadLocal<DriverManager> DRIVER = new ThreadLocal<DriverManager>();

        public static void InitDriverManager(DriverProperties pro, string key = "default")
        {
            DRIVER.Value = new DriverManager(key);
        }

        ///<summary>
        ///Add webdriver by key
        ///</summary>
        public static void SetDriverByKey(DriverProperties pro, string key)
        {
            DRIVER.Value.CreateDriverByProperties(pro, key);
        }

        ///<summary>
        ///return current webdriver
        ///</summary>
        public static IWebDriver GetDriver()
        {
            return DRIVER.Value.GetCurrentDriver();
        }

        public static DriverProperties GetProperties()
        {
            return DRIVER.Value.GetCurrentProperties();
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
            DRIVER.Value.SwitchToDefaultDriver();
        }

        ///<summary>
        ///Switch to another driver in the list, 1st driver key = defaultKey if not set
        ///</summary>
        public static void SwitchDriverTo(string driverKey) {
            DRIVER.Value.SwitchToTargetDriver(driverKey);
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
            DRIVER.Value.QuitAllDriver();
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
