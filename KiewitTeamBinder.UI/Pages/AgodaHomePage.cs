using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.Popup;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages
{
    public class AgodaHomePage : PageBase
    {

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaHomePage");


        private By _linkLanguageMenu => locator.Get("_linkLanguageMenu");
        private By _linkCurrencyMenu => locator.Get("_linkCurrencyMenu");
        private By _txtSearch => locator.Get("_txtSearch");
        private By _eleCheckInDate => locator.Get("_eleCheckInDate");
        private By _eleCheckOutDate => locator.Get("_eleCheckOutDate");
        private By _eleOccupancy => locator.Get("_eleOccupancy");
        private By _btnSearch => locator.Get("_btnSearch");

        #endregion

        #region Elements
        public IWebElement LinkLanguageMenu => StableFindElement(_linkLanguageMenu);
        public IWebElement LinkCurrencyMenu => StableFindElement(_linkCurrencyMenu);
        public IWebElement SearchTextBox => StableFindElement(_txtSearch);
        public IWebElement CheckInDateElement => StableFindElement(_eleCheckInDate);
        public IWebElement CheckOutDateElement => StableFindElement(_eleCheckOutDate);
        public IWebElement OccupancyElement => StableFindElement(_eleOccupancy);
        public IWebElement SearchButton => StableFindElement(_btnSearch);
        #endregion

        #region Methods

        public AgodaHomePage(IWebDriver webDriver) : base(webDriver)
        {
            LinkLanguageMenu.Click();
            (new AgodaLanguageSelectionPopup(WebDriver)).SelectLanguage(Browser.CurrentLanguage);

            LinkCurrencyMenu.Click();
            (new AgodaCurrencySelectionPopup(WebDriver)).SelectCurrency(Browser.CurrentCurrency);
        }

        public void SelectLanguage(string targetLanguage)
        {
            LinkLanguageMenu.Click();
            AgodaLanguageSelectionPopup popup = new AgodaLanguageSelectionPopup(WebDriver);
            popup.SelectLanguage(targetLanguage);
        }

        public void SelectCurrency(string targetCurrency)
        {
            LinkCurrencyMenu.Click();
            AgodaCurrencySelectionPopup popup = new AgodaCurrencySelectionPopup(WebDriver);
            popup.SelectCurrency(targetCurrency);
        }

        public void EnterBookingInfo(BookingInfo info)
        {
            var node = CreateStepNode();
            node.Info(String.Format("Input Destination: {0}", info.Destination));
            SearchTextBox.SendKeys(info.Destination);
            Wait(1);
            SearchTextBox.SendKeys(Keys.Enter);
            AgodaDayPicker dayPicker = new AgodaDayPicker(WebDriver);
            dayPicker.SelectDateRange(info.CheckInDate, info.CheckOutDate);
            AgodaTravelerSelection travelerPicker = new AgodaTravelerSelection(WebDriver);
            travelerPicker.EnterTravelerInfo(info);
            SearchButton.Click();
            EndStepNode(node);
        }

        #endregion
    }
}
