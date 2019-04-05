using KiewitTeamBinder.UI.Pages.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KiewitTeamBinder.UI.Pages
{
    public class MainDigiKey : LoggedInLanding
    {
        #region Locators
        private By _topMenu(string menu) => By.XPath($"//div[@class='header__links']//span[contains(text(),'{menu}')]");
        private By _subCategory(string category, string subCategory) => By.XPath($"//h2[a[contains(text(),'{category}')]]/following-sibling::ul[1]//a[contains(text(),'{subCategory}')]");
        private By _searchIcon => By.XPath("//button[@id='header-search-button']");
        #endregion

        #region Elements
        public IWebElement TopMenu(string menu) => StableFindElement(_topMenu(menu));
        public IWebElement SubCategory(string category, string subCategory) => StableFindElement(_subCategory(category, subCategory));
        #endregion

        #region Methods
        public T SelectMenu<T> (string menu)
        {
            TopMenu(menu).Click();
            WaitForElement(_searchIcon);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        //public T SelectSubCategory<T>(string category, string subCategory)
        //{
        //    SubCategory(category, subCategory).Click();
        //    WaitForElement(_searchIcon);
        //    return (T)Activator.CreateInstance(typeof(T), WebDriver);
        //}
        #endregion

        public MainDigiKey(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}
