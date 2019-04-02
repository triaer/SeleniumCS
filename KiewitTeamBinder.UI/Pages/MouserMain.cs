using KiewitTeamBinder.UI.Pages.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KiewitTeamBinder.UI.Pages
{
    public class MouserMain : LoggedInLanding
    {
        #region Locators
        private By _productsMenu(string product) => By.XPath($"//li[@class='col2']//a[contains(text(),'{product}')]");
        private By _thermalManagementTabs(string tab) => By.XPath($"//ul[@id='categoryFlyoutRight_6']//div[@data-bind='foreach: Subcategories']//li//a[contains(text(),'{tab}')]");

        #endregion

        #region Elements
        public IWebElement ProductsMenu(string product) => StableFindElement(_productsMenu(product));
        public IWebElement ThermalManagementTabs(string tab) => StableFindElement(_thermalManagementTabs(tab)); 
        #endregion

        #region Methods
        public T SelectSubMenuProducts<T>(string product, string tab)
        {
            HoverElement(_productsMenu(product));
            ThermalManagementTabs(tab).Click();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }
        #endregion
        public MouserMain(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}
