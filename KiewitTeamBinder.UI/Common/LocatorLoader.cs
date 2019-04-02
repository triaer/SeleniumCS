using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Common
{
    public class LocatorLoader
    {
        private JObject locators;

        public LocatorLoader(string className)
        {
            string workingDirectory = Environment.CurrentDirectory;
            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string path = String.Format("{0}\\Locator\\{1}.json", projectDirectory, className);
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                JObject jsonObj = JObject.Parse(json);
                locators = (JObject)jsonObj["default"];
                if (jsonObj[Browser.CurrentBrowser] != null)
                {
                    foreach (var item in (JObject)jsonObj[Browser.CurrentBrowser])
                    {
                        locators[item.Key] = item.Value;
                    }
                }
            }
        }

        public By Get(string locatorName, object param = null)
        {
            if (locators[locatorName] != null)
            {
                string type = (String)locators[locatorName]["type"];
                string value = (String)locators[locatorName]["value"];
                if (param != null)
                {
                    value = String.Format(value, param);
                }
                return GetByLocator(type, value);
            }
            else
            {
                Console.WriteLine("Locator '{0}' does not exist", locatorName);
                return null;
            }
        }

        public By Get(string locatorName, object[] param)
        {
            if (locators[locatorName] != null)
            {
                string type = (String)locators[locatorName]["type"];
                string value = (String)locators[locatorName]["value"];

                return GetByLocator(type, String.Format(value, param));
            }
            else
            {
                Console.WriteLine("Locator '{0}' does not exist", locatorName);
                return null;
            }
        }

        private By GetByLocator(String type, String value)
        {
            switch (type)
            {
                case "css":
                    return By.CssSelector(value);
                case "id":
                    return By.Id(value);
                case "link":
                    return By.LinkText(value);
                case "tagName":
                    return By.TagName(value);
                case "name":
                    return By.Name(value);
                default:
                    return By.XPath(value);
            }
        }
    }
}
