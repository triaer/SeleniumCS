using KiewitTeamBinder.UI.Common;
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
    public class AgodaLanguageSelectionPopup : PageBase
    {

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaLanguageSelectionPopup");

        private By _popupLanguageSelection => locator.Get("_popupLanguageSelection");
        private By _linkTargetLanguage(string language) => locator.Get("_linkTargetLanguage", language);

        #endregion

        #region Elements
        public IWebElement LanguageSelectionPopup => StableFindElement(_popupLanguageSelection);
        public IWebElement TargetLanguageLink(string language) => StableFindElement(_linkTargetLanguage(language));
        #endregion

        #region Methods

        public AgodaLanguageSelectionPopup(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElementDisplay(_popupLanguageSelection);
        }

        public void SelectLanguage(string targetLanguage)
        {
            var node = CreateStepNode();
            node.Info(String.Format("Select Language: {0}", targetLanguage));
            TargetLanguageLink(targetLanguage).Click();
            Browser.CurrentLanguage = targetLanguage;
            EndStepNode(node);
        }

        #endregion
    }
}
