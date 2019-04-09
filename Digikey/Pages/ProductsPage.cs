using Digikey.DataObjects;
using OpenQA.Selenium;
using System;
using System.Collections;
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
        static readonly By _cartMenu = By.XPath("//a[contains(@class,'cart')]");
        static readonly By _ViewCartBtn = By.XPath("//div[contains(@class,'dropdown--cart')]/a[@class='dropdown--button']");

        static readonly By _LinkBatteryProdCat = By.XPath("//a[@href='#cat-6' and text()='Battery Products']");
        static readonly By _linkAccessories = By.XPath("//a[@href='/products/en/battery-products/accessories/87']");
        //static readonly By _linkAccessories = By.XPath("//h2[.//a[text()='Battery Products']]/following-sibling::ul[1]//a[text()='Accessories']");

        string _checkBoxProduct = "//tbody/tr[{0}]//input";
        string _linkDigikeyPart = "//tbody/tr[{0}]//td[contains(@class,'dkPartNumber')]/a";
        string _linkMfgPartNumber = "//tbody/tr[{0}]//td[contains(@class,'mfgPartNumber')]//span";
        string _linkManufacturer = "//tbody/tr[{0}]//td[contains(@class,'tr-vendor')]//span[@itemprop='name']";

        string _itemByKey = "//td[contains(@class,'dkPartNumber')]/a[contains(text(),'{0}')]";
        string _itemMfgByKey = "//tr[./td[./a[contains(text(),'{0}')]]]/td[contains(@class,'mfgPartNumber')]//span[@itemprop='name']";
        string _itemManufacturerByKey = "//tr[./td[./a[contains(text(),'{0}')]]]/td[contains(@class,'vendor')]//span[@itemprop='name']";
        string _itemDescriptionByKey = "//tr[./td[./a[contains(text(),'{0}')]]]/td[contains(@class,'description')]";

        static readonly By _buttonCompare = By.XPath("//input[@id='compare-button']");

        #endregion

        //======================================

        #region Elements

        public IWebElement CartMenu
        {
            get { return StableFindElement(_cartMenu); }
        }


        public IWebElement ButtonViewCart
        {
            get { return StableFindElement(_ViewCartBtn); }
        }

        public IWebElement LinkBatteryProductsCategory
        {
            get { return StableFindElement(_LinkBatteryProdCat); }
        }
        
        public IWebElement LinkAccessories
        {
            get { return StableFindElement(_linkAccessories); }
        }

        public IWebElement CheckboxProduct(int productNumber)
        {
            return StableFindElement(By.XPath(string.Format(_checkBoxProduct, productNumber.ToString())));
        }

        public IWebElement LinkDigikeyPart(int productNumber)
        {
            return StableFindElement(By.XPath(string.Format(_linkDigikeyPart, productNumber.ToString()))); 
        }

        public IWebElement LinkMfgPartNumber(int productNumber)
        {
            return StableFindElement(By.XPath(string.Format(_linkMfgPartNumber, productNumber.ToString()))); 
        }

        public IWebElement LinkManufacturer(int productNumber)
        {
            return StableFindElement(By.XPath(string.Format(_linkManufacturer, productNumber.ToString())));
        }

        public IWebElement ItemLinkByKey(string digiKey)
        {
            WaitForElementDisplay(By.XPath(string.Format(_itemByKey, digiKey)));
            return StableFindElement(By.XPath(string.Format(_itemByKey, digiKey)));
        }

        public IWebElement ItemMfgByKey (string digikey)
        {
            //WaitForElementDisplay(By.XPath(string.Format(_itemMfgByKey, digikey)));
            return StableFindElement(By.XPath(string.Format(_itemMfgByKey, digikey)));
        }

        public IWebElement ItemManufacturerByKey(string digikey)
        {
            return StableFindElement(By.XPath(string.Format(_itemManufacturerByKey, digikey)));
        }

        public IWebElement ItemDescriptionByKey (string digikey)
        {
            return StableFindElement(By.XPath(string.Format(_itemDescriptionByKey, digikey)));
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

        public ProductsPage GetProductList()
        {
            this.LinkBatteryProductsCategory.Click();
            this.LinkAccessories.Click();
            return this;
        }

        public ProductComparePage SelectItemsToCompare(int quantity)
        {
            ArrayList alist = new ArrayList();
            for (int i = 1; i <= quantity; i++)
            {
                CheckboxProduct(i).Check();
                var product = new Product();
                product._digiKey = LinkDigikeyPart(i).Text;
                product._mfgPartNumber = LinkMfgPartNumber(i).Text;
                product._manufacturer = LinkManufacturer(i).Text;
                //Console.WriteLine(product._digiKey + "|" + product._mfgPartNumber + "|" + product._manufacturer);
                alist.Add(product);
                
            }
            ButtonCompare.Click();
            return new ProductComparePage(_driver, alist);
        }

        public ProductDetailPage SelectItemByDigikey(int order)
        {
            LinkDigikeyPart(order).Click();
            return new ProductDetailPage(_driver);
        }

        public CartPage goToCartPage()
        {
            this.CartMenu.Click();
            this.ButtonViewCart.Click();
            return new CartPage(_driver);
        }

        #endregion
    }
}
