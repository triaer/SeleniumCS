using Digikey.DataObjects;
using OpenQA.Selenium;

namespace Digikey.Pages
{
    public class CartPage : PageBase
    {
        private CartItem cartItem;
        private IWebDriver _driver;

        #region Locators
        static readonly By _linkProducts = By.XPath("//a[@href='/products/en']");

        private string _itemMfgByKey = "//tr[td[div[./a[contains(text(),'‎{0}‎')]]]]//div[contains(@class,'mfgPartNumber')]";
        private string _itemManufacturerByKey = "//tr[td[div[./a[contains(text(),'‎{0}‎')]]]]//div[contains(@class,'mfgName')]";
        private string _itemDescriptionByKey = "//tr[td[div[./a[contains(text(),'‎{0}‎')]]]]//div[contains(@class,'description')]";
        private string _itemCusRefByKey = "//tr[td[div[./a[contains(text(),'‎{0}')]]]]//div[contains(@class,'customerReference')]/input";
        private string _itemQuantityByKey = "//tr[td[div[./a[contains(text(),'‎{0}‎')]]]]/td[contains(@class,'qtyInput')]//input";


        #endregion

        #region Elements
        public IWebElement LinkProducts
        {
            get { return StableFindElement(_linkProducts); }
        }
        
        public IWebElement LabelMfgByKey(string digiKey)
        {
            return StableFindElement(By.XPath(string.Format(_itemMfgByKey, digiKey)));
        }

        public IWebElement LabelManufacturerByKey(string digiKey)
        {
            return StableFindElement(By.XPath(string.Format(_itemManufacturerByKey, digiKey)));
        }

        public IWebElement LabelDescriptionByKey(string digiKey)
        {
            return StableFindElement(By.XPath(string.Format(_itemDescriptionByKey, digiKey)));
        }

        public IWebElement FieldCustomerReferenceByKey(string digiKey)
        {
            return StableFindElement(By.XPath(string.Format(_itemCusRefByKey, digiKey)));
        }

        public IWebElement FieldQuantity(string digiKey)
        {
            return StableFindElement(By.XPath(string.Format(_itemQuantityByKey, digiKey)));
        }
        #endregion

        #region Methods
        public CartPage(IWebDriver driver) : base (driver)
        {
            this._driver = driver;
        }

        public ProductsPage BackToProductsPage()
        {
            LinkProducts.Click();
            return new ProductsPage(_driver);
        }

        public bool IsSelectedCartItemDisplayed(string digiKey)
        {
            bool isDisplayed = true;




            return isDisplayed;
        }

        #endregion
    }
}