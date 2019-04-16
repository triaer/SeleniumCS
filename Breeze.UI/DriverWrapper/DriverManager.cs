using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;

namespace Breeze.UI.DriverWrapper
{
    /// <summary>
    /// Create Web Driver 
    /// </summary>
     class DriverManager
    {
        private string defaultKey;
        private string currentKey;
        private Dictionary<string, IWebDriver> listDriver = new Dictionary<string, IWebDriver>();
        private Dictionary<string, DriverProperties> listProperties = new Dictionary<string, DriverProperties>();

        public DriverManager(string key)
        {
            defaultKey = key;
        }

        public DriverManager(DriverProperties properties, string key)
        {
            defaultKey = key;
            CreateDriverByProperties(properties, key);
        }
        /// <summary>
        /// Create Web Driver By properties
        /// </summary>
        public void CreateDriverByProperties(DriverProperties properties, string key)
        {
            if (listDriver.Count == 0 || listDriver.ContainsKey(key) == false)
            {
                IWebDriver webDriver = null;
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

                listDriver.Add(key, webDriver);
                listProperties.Add(key, properties);
                currentKey = key;
            }
        }

        /// <summary>
        /// Get Driver By key
        /// </summary>
        public IWebDriver GetDriverByKey(string key)
        {
            return listDriver[key];
        }

        public IWebDriver GetCurrentDriver()
        {
            return listDriver[currentKey];
        }

        public DriverProperties GetPropertiesByKey(string key)
        {
            return listProperties[key];
        }

        public DriverProperties GetCurrentProperties()
        {
            return listProperties[currentKey];
        }

        public void SwitchToDefaultDriver()
        {
            currentKey = defaultKey;
        }

        public void SwitchToTargetDriver(string key)
        {
            currentKey = key;
        }

        public void QuitAllDriver()
        {
            foreach (var driver in listDriver.Values)
            {
                driver.Quit();
            }
        }
    }
}
