using KiewitTeamBinder.UI.Pages.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages
{
    public class AgodaResults : LoggedInLanding
    {
        #region Locators
        private By _hotel(string name) => By.XPath($"//h3[contains(text(),'{name}')]");
        public IWebElement Hotel(string name) => StableFindElement(_hotel(name));
        #endregion

        #region Element
        #endregion

        #region Methods
        public AgodaBookingRoom SelectHotel(string name)
        {
            var node = CreateStepNode();
            node.Info("Select Hotel");
            IJavaScriptExecutor jse = (IJavaScriptExecutor)WebDriver;
            var hieght = jse.ExecuteScript("return document.body.scrollHeight");
            var scrollLocate = 600;
            //jse.ExecuteScript($"window.scrollTo(0, {scrollLocate})");
            while (FindElement(_hotel(name), 1) == null)
            {
                jse.ExecuteScript($"window.scrollTo(0, {scrollLocate})");
                scrollLocate += 200;
            }
            Hotel(name).Click();
            //Thread.Sleep(3000);
            EndStepNode(node);
            return new AgodaBookingRoom(WebDriver);
            //ScrollIntoView(Hotel(name));
        }
        #endregion
        public AgodaResults(IWebDriver webDriver) : base(webDriver)
        {

        }
    }
}
