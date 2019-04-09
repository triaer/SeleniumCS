using Digikey.DataObjects;
using OpenQA.Selenium;
using static Digikey.Constants.Constants;

namespace Digikey.Pages
{
    public class ProductDetailPage : PageBase
    {
        private IWebDriver _driver;
        private Product product;

        #region Locators
        
        private By _quantityTxt = By.XPath("//input[@id='qty']");
        private By _customerRefTxt = By.XPath("//input[@id='cref']");
        private By _addToCartBtn = By.XPath("//input[@id='addtoorderbutton']");
        

        #endregion

        #region Elements
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
            this.product = product;
        }

        public CartPage AddProductToCart(int quantity, string customerRef)
        {
            // Update cart item
            var cartItem = new CartItem();
            cartItem._product = this.product;
            cartItem._quantity = quantity;
            cartItem._customerRef = customerRef;

            // Action
            TextQuantity.Clear();
            TextQuantity.SendKeys(quantity.ToString());
            TextCustomerReference.Clear();
            TextCustomerReference.SendKeys(customerRef);

            // Add Cart Item to Cart
            _cart.Add(cartItem);

            // Go to Cart page
            ButtonAddToCart.Click();

            return new CartPage(_driver);
        }

        #endregion

    }
}