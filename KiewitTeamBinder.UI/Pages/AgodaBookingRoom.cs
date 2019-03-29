using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
namespace KiewitTeamBinder.UI.Pages
{

    public class AgodaBookingRoom : LoggedInLanding
    {
        #region Locators
        private By _bookingLabel => By.XPath("(//button[@data-selenium='ChildRoomsList-bookButtonInput'])[1]");
        private By _informationLabel => By.XPath("//span[@data-bind='text: progressTracker.customerInformationText']");
        #endregion

        #region Element
        public IWebElement BookingLabel { get { return StableFindElement(_bookingLabel); } } 
        #endregion

        #region Methods
        public BookingForm SelectSpecificRoomType()
        {
            //IJavaScriptExecutor jse = (IJavaScriptExecutor)WebDriver;
            //var hieght = jse.ExecuteScript("return document.body.scrollHeight");
            //var scrollLocate = 600;
            ////jse.ExecuteScript($"window.scrollTo(0, {scrollLocate})");
            //while (FindElement(_bookingLabel, 1) == null)
            //{
            //    jse.ExecuteScript($"window.scrollTo(0, {scrollLocate})");
            //    scrollLocate += 200;
            //}
            var node = CreateStepNode();
            node.Info("Select a specific room type");
            string currentWindow = WebDriver.WindowHandles.Last();
            SwitchToWindow(currentWindow);
            BookingLabel.Click();
            WaitForElement(_informationLabel);
            EndStepNode(node);
            return new BookingForm(WebDriver);
        }


        #endregion
        public AgodaBookingRoom(IWebDriver webDriver) : base(webDriver)
        {

        }
    }
}
