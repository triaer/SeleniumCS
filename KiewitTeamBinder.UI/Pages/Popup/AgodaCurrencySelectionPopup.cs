using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Pages.Popup
{
    public class AgodaCurrencySelectionPopup : PageBase
    {

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaCurrencySelectionPopup");

        private By _popupCurrencySelection => locator.Get("_popupCurrencySelection");
        private By _linkTargetCurrency(string currency) => locator.Get("_linkTargetCurrency", currency);

        #endregion

        #region Elements
        public IWebElement PopupCurrencySelection => StableFindElement(_popupCurrencySelection);
        public IWebElement LinkTargetCurrency(string currency) => StableFindElement(_linkTargetCurrency(currency));
        #endregion

        #region Methods

        public AgodaCurrencySelectionPopup(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElementDisplay(_popupCurrencySelection);
        }

        public void SelectCurrency(string targetCurrency)
        {
            LinkTargetCurrency(targetCurrency).Click();
            Browser.CurrentCurrency = targetCurrency;
        }

        #endregion
    }
}
