using Breeze.Common.DriverWrapper;
using Breeze.Common.Helper;
using OpenQA.Selenium;

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
            DriverProperties prop = WebDriver.GetDefaultProperties();

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

        public static IWebDriver Driver
        {
            get { return WebDriver.GetDriver(); }
        }

        public static void Navigate(string url)
        {
            WebDriver.GetDriver().Url = url;
        }

        public static string Title
        {
            get { return WebDriver.Title; }
        }

        internal static void MaximizeWindow()
        {
            WebDriver.Maximize();
        }

        internal static void MinimizeWindow()
        {
            WebDriver.Minimize();
        }

        public static void SwitchToDefaultBrowser()
        {
            WebDriver.SwitchToDefaultDriver();
        }

        public static void SwitchToTargetBrowser(string browserName, int browserIndex = 1)
        {
            WebDriver.SwitchToTargetDriver(browserName, browserIndex);
        }
    }
}
