using Digikey.DataObjects;
using OpenQA.Selenium;
using System;
using static Digikey.Constants.Constants;

namespace Digikey.Pages
{
    public class ProductDetailPage : PageBase
    {
        private IWebDriver _driver;
        //private Product product;

        #region Locators
        static readonly By _itemDigiKey = By.XPath("//td[@id='reportPartNumber']");
        static readonly By _itemMfgNumber = By.XPath("//h1[@itemprop='model']");
        static readonly By _itemManufacturer = By.XPath("//span[@itemprop='name']");
        static readonly By _itemDescription = By.XPath("//td[@itemprop='description']");
        
        private By _quantityTxt = By.XPath("//input[@id='qty']");
        private By _customerRefTxt = By.XPath("//input[@id='cref']");
        private By _addToCartBtn = By.XPath("//input[@id='addtoorderbutton']");


        #endregion

        #region Elements
        public IWebElement LabelItemDigiKey
        {
            get {
                return FindElement(_itemDigiKey);
            }
        }

        public IWebElement LabelMfgNumber
        {
            get { return StableFindElement(_itemMfgNumber); }
        }

        public IWebElement LabelManufacturer
        {
            get { return StableFindElement(_itemManufacturer);}
        }

        public IWebElement LabelDescription
        {
            get { return StableFindElement(_itemDescription); }
        }

        public IWebElement TextQuantity
        {
            get { return StableFindElement(_quantityTxt); }
        }

        public IWebElement TextCustomerReference
        {
            get { return StableFindElement(_customerRefTxt); }
        }

        public IWebElement ButtonAddToCart
        {
            get { return StableFindElement(_addToCartBtn); }
        }

        #endregion

        #region Methods

        public ProductDetailPage(IWebDriver driver) : base (driver)
        {
            this._driver = driver;
        }

        
        public ProductDetailPage(IWebDriver driver, Product product) : this(driver)
        {
            this._driver = driver;
            //this.product = product;
        }

        public CartPage AddProductToCart(int quantity, string customerRef)
        {

            //_driver.Navigate().Refresh();
            //Wait(5);

            // Get product info
            var product = new Product();
            product._digiKey = LabelItemDigiKey.Text.Trim();
            product._mfgPartNumber = LabelMfgNumber.Text;
            product._manufacturer = LabelManufacturer.Text;
            product._description = LabelDescription.Text;

            // Update cart item
            var cartItem = new CartItem();
            cartItem._product = product;
            cartItem._quantity = quantity;
            cartItem._customerRef = customerRef;

            // Action
            TextQuantity.Clear();
            TextQuantity.SendKeys(quantity.ToString());
            TextCustomerReference.Clear();
            TextCustomerReference.SendKeys(customerRef);

            // Add Cart Item to Cart
            _cart.Add(cartItem);
           
            Console.WriteLine(_cart[0].ToString());

            // Go to Cart page
            Wait(2);
            ButtonAddToCart.Click();

            return new CartPage(_driver);
        }

        #endregion

    }
}