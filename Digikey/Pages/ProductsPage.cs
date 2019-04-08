using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digikey.Pages
{
    public class ProductsPage : PageBase
    {
        private IWebDriver _driver;

        # region Locators

        static readonly By _LinkBatteryProdCat = By.XPath("//a[@href='#cat-6' and text()='Battery Products']");
        static readonly By _linkAccessories = By.XPath("//a[@href='/products/en/battery-products/accessories/87']");
        //static readonly By _linkAccessories = By.XPath("//h2[.//a[text()='Battery Products']]/following-sibling::ul[1]//a[text()='Accessories']");

        static readonly By _checkBoxProduct = By.XPath("//tbody/tr[{0}]//input");
        static readonly By _linkDigikeyPart = By.XPath("//tbody/tr[{0}]//td[contains(@class,'dkPartNumber')]/a");
        static readonly By _linkMfgPartNumber = By.XPath("//tbody/tr[{0}]//td[contains(@class,'mfgPartNumber')]//span");
        static readonly By _linkManufacturer = By.XPath("//tbody/tr[1]//td[contains(@class,'tr-vendor')]//span[@itemprop='name']");

        static readonly By _buttonCompare = By.XPath("//input[@id='compare-button']");
        #endregion

        //======================================

        #region Elements
        public IWebElement LinkBatteryProductsCategory
        {
            get { return StableFindElement(_LinkBatteryProdCat); }
        }
        
        public IWebElement LinkAccessories
        {
            get { return StableFindElement(_linkAccessories); }
        }

        public IWebElement CheckboxProduct
        {
            get { return StableFindElement(_checkBoxProduct); }
        }

        public IWebElement LinkDigikeyPart
        {
            get { return StableFindElement(_linkDigikeyPart); }
        }

        public IWebElement LinkMfgPartNumber
        {
            get { return StableFindElement(_linkMfgPartNumber); }
        }

        public IWebElement LinkManufacturer
        {
            get { return StableFindElement(_linkManufacturer); }
        }

        public IWebElement ButtonCompare
        {
            get { return StableFindElement(_buttonCompare); }
        }

        #endregion

        //======================================

        #region Methods

        public ProductsPage(IWebDriver driver) : base (driver)
        {
            this._driver = driver;
        }

        public ProductsPage jumpToBatteryProductsCategory()
        {
            this.LinkBatteryProductsCategory.Click();
            return this;
        }

        public ProductsPage getAccessories()
        {
            this.LinkAccessories.Click();
            return this;
        }



        #endregion
    }
}
