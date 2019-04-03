﻿using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.Popup
{
    public class AgodaCurrencySelectionPopup : PageBase
    {

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaCurrencySelectionPopup");

        private By _popupCurrencySelection => locator.Get("_popupCurrencySelection");
        private By _linkTargetCurrency(string currency) => locator.Get("_linkTargetCurrency", currency);
        private By _lblTargetCurrencyIcon(string currency) => locator.Get("_lblTargetCurrencyIcon", currency);

        #endregion

        #region Elements
        public IWebElement CurrencySelectionPopup => StableFindElement(_popupCurrencySelection);
        public IWebElement TargetCurrencyLink(string currency) => StableFindElement(_linkTargetCurrency(currency));
        public IWebElement TargetCurrencyIcon(string currency) => StableFindElement(_lblTargetCurrencyIcon(currency));
        #endregion

        #region Methods

        public AgodaCurrencySelectionPopup(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElement(_popupCurrencySelection);
        }

        public void SelectCurrency(string targetCurrency)
        {
            var node = CreateStepNode();
            node.Info(String.Format("Select Currency: {0}", targetCurrency));
            Browser.CurrentCurrency = targetCurrency;
            Browser.CurrentCurrencyIcon = TargetCurrencyIcon(targetCurrency).Text;
            TargetCurrencyLink(targetCurrency).Click();
            EndStepNode(node);
        }

        #endregion
    }
}
