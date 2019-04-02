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
    public class AgodaLanguageSelectionPopup : PageBase
    {

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaLanguageSelectionPopup");

        private By _popupLanguageSelection => locator.Get("_popupLanguageSelection");
        private By _linkTargetLanguage(string language) => locator.Get("_linkTargetLanguage", language);

        #endregion

        #region Elements
        public IWebElement PopupLanguageSelection => StableFindElement(_popupLanguageSelection);
        public IWebElement LinkTargetLanguage(string language) => StableFindElement(_linkTargetLanguage(language));
        #endregion

        #region Methods

        public AgodaLanguageSelectionPopup(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElementDisplay(_popupLanguageSelection);
        }

        public void SelectLanguage(string targetLanguage)
        {
            LinkTargetLanguage(targetLanguage).Click();
            Browser.CurrentLanguage = targetLanguage;
        }

        #endregion
    }
}
