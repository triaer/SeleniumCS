using KiewitTeamBinder.UI.Pages.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KiewitTeamBinder.UI.Pages
{
    public class CompareDigiKey : LoggedInLanding
    {
        #region Locators
        private By _backButton => By.XPath("(//a[contains(text(),'Back')])[1]");
        private By _searchIcon => By.XPath("//button[@id='header-search-button']");
        #endregion

        #region Elements
        public IWebElement BackButton { get { return StableFindElement(_backButton); } }
        #endregion

        #region Methods
        public T ClickBackButton<T>()
        {
            BackButton.Click();
            WaitForElement(_searchIcon);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);

        }
        #endregion

        public CompareDigiKey(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}
