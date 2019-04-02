using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.Popup;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Pages
{
    public class AgodaHomePage : PageBase
    {

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaHomePage");


        private By _linkLanguageMenu => locator.Get("_linkLanguageMenu");
        private By _linkCurrencyMenu => locator.Get("_linkCurrencyMenu");

        #endregion

        #region Elements
        public IWebElement LinkLanguageMenu => StableFindElement(_linkLanguageMenu);
        public IWebElement LinkCurrencyMenu => StableFindElement(_linkCurrencyMenu);
        #endregion

        #region Methods

        public AgodaHomePage(IWebDriver webDriver) : base(webDriver)
        {
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

        #endregion
    }
}
