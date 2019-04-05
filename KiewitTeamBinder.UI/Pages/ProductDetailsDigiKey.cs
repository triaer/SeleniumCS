using KiewitTeamBinder.UI.Pages.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KiewitTeamBinder.UI.Pages
{
    class ProductDetailsDigiKey : LoggedInLanding
    {
        #region Locators
        private By _quantity => By.XPath("//input[@name='qty']");
        private By _referrence => By.XPath("//input[@id='cref']");
        private By _addtoCart => By.XPath("//input[@id='addtoorderbutton']");
        private By _searchIcon => By.XPath("//button[@id='header-search-button']");
        #endregion

        #region Elements
        public IWebElement Quantity { get { return StableFindElement(_quantity); } }
        public IWebElement Reference { get { return StableFindElement(_referrence); } }
        public IWebElement AddToCart { get { return StableFindElement(_addtoCart); } }
        #endregion

        #region Methods
        public CartDigiKey EnterQuantityAndReference(string quantity, string reference)
        {
            Quantity.SendKeys(quantity);
            Reference.SendKeys(reference);
            AddToCart.Click();
            WaitForElement(_searchIcon);
            return new CartDigiKey(WebDriver);
        }
        #endregion

        public ProductDetailsDigiKey(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}
