using System;
using System.Collections.Generic;
using Digikey.DataObjects;
using OpenQA.Selenium;
using static Digikey.Constants.Constants;
using static Digikey.ExtentReportsHelper;

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

        private string _linkItemByKey = "//table[@id='cartDetails']//a[contains(@href,'{0}')]";
        private string _checkBoxByKey = "//tr[.//a[contains(@href,'{0}')]]//input[@class='deleteCheckbox']";
        private string _txtQuantityByKey = "//tr[.//a[contains(@href,'{0}')]]//div[@class='cart-qtyInput']/input";
        private string _lblAvailability = "//tr[.//a[contains(@href,'{0}')]]//div[@class='cart-availability']";

        static readonly By _buttonDeleteUpper = By.XPath("//div[@id='cartViewButtons']/span[contains(@class,'trash-button')]");
        static readonly By _buttonDeleteLower = By.XPath("//div[@class='table-buttons_under']");

        static readonly By _buttonConfirmDelete = By.XPath("//div[@role='dialog']//button[text()='OK']");

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

        public IWebElement LinkItemByKey(string digiKey)
        {
            return StableFindElement(By.XPath(string.Format(_linkItemByKey, digiKey)));
        }

        public IWebElement TextQuantityByKey(string digiKey)
        {
            return StableFindElement(By.XPath(string.Format(_txtQuantityByKey, digiKey)));
        }
                
        public IWebElement LabelAvailabilityByKey(string digiKey)
        {
            return StableFindElement(By.XPath(string.Format(_lblAvailability, digiKey)));
        }

        public IWebElement CheckboxByKey(string digiKey)
        {
            return StableFindElement(By.XPath(string.Format(_checkBoxByKey, digiKey)));
        }

        public IWebElement ButtonDeleteUpper
        {
            get { return StableFindElement(_buttonDeleteUpper); }
        }

        public IWebElement ButtonDeleteLower
        {
            get { return StableFindElement(_buttonDeleteLower); }
        }

        public IWebElement ButtonConfirmDelete
        {
            get { return StableFindElement(_buttonConfirmDelete); }
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

        //public bool IsSelectedCartItemDisplayed(string digiKey)
        //{
        //    return LinkItemByKey(digiKey).IsDisplayed();
        //}

        public bool CheckSelectedCartItems()
        {
            for (int i = 0; i < _cart.Count; i++)
            {
                var cartItem = (CartItem)_cart[i];
                // Console.WriteLine(cartItem._product.getDigiKey());
                // if (IsSelectedCartItemDisplayed(cartItem._product.getDigiKey()) == false)
                //     return false;
                if (LinkItemByKey(cartItem._product.getDigiKey()).Displayed == false)
                    return false;
            }
            return true;
        }

        public void modifyQuantityForAllItems(int newQuantity)
        {
            for (int i = 0; i < _cart.Count; i++)
            {
                // Update new quantity
                var cartItem = (CartItem)_cart[i];
                cartItem._quantity = newQuantity;

                // Send new value
                var key = cartItem._product._digiKey;
                Wait(1);
                TextQuantityByKey(key).Click();
                Wait(2);
                TextQuantityByKey(key).DoubleClick();
                Wait(1);
                TextQuantityByKey(key).SendKeys(newQuantity.ToString());
                Wait(2);
                LabelAvailabilityByKey(key).Click();
                Wait(2);
            }
        }

        public bool CheckNewQuantityOfCartItems(int quantity = 3) //specific test.
        {
            for (int i = 0; i < _cart.Count; i++)
            {
                var cartItem = (CartItem)_cart[i];
                var key = cartItem._product._digiKey;
                var expectedQty = cartItem._quantity.ToString();
                var observedQty = TextQuantityByKey(key).GetValue();
                if (!observedQty.Equals(expectedQty))
                {
                    Console.WriteLine(key + " | " + expectedQty + " | " + observedQty);
                    return false;
                }
            }
            return true;
        }

        private void DeleteCartItemByKey(string key)
        {
            Wait(2);
            CheckboxByKey(key).Check();
            ButtonDeleteUpper.Click();
            ButtonConfirmDelete.Click();
        }

        public CartPage DeleteItemFromCart(int deleteQty)
        {
            for (int i = 0; i < deleteQty; i++)
            {
                var cartItem = (CartItem)_cart[i];
                this.DeleteCartItemByKey(cartItem._product._digiKey);
            }
            return this;
        }

        public bool CheckDeletedCartItemsDisappeared(int deleteQty)
        {
            for (int i = 0; i < deleteQty; i++)
            {
                var cartItem = (CartItem)_cart[i];
                if (LinkItemByKey(cartItem._product._digiKey).IsDisplayed() == true)
                {
                    return false;
                }
            }
            return true;
        }
        
        internal KeyValuePair<string, bool> ValidateCartItems()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool totalCheck = this.CheckSelectedCartItems();

                if (totalCheck == true)
                    validation = SetPassValidation(node, ValidationMessage.ValidateCartItems);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateCartItems);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateCartItems, e);
            }
            EndStepNode(node);
            return validation;
        }
        
        internal KeyValuePair<string, bool> ValidateNewQuantityOfCartItems()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool totalCheck = this.CheckNewQuantityOfCartItems();

                if (totalCheck == true)
                    validation = SetPassValidation(node, ValidationMessage.ValidateNewQuantityOfAllCartItems);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateNewQuantityOfAllCartItems);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateNewQuantityOfAllCartItems, e);
            }
            EndStepNode(node);
            return validation;
        }

        internal KeyValuePair<string, bool> ValidateExistenceOfDeletedCartItems()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool totalCheck = this.CheckDeletedCartItemsDisappeared(2);

                if (totalCheck == true)
                    validation = SetPassValidation(node, ValidationMessage.ValidateExistenceOfDeletedCartItems);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateExistenceOfDeletedCartItems);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateExistenceOfDeletedCartItems, e);
            }
            EndStepNode(node);
            return validation;
        }

        private static class ValidationMessage
        {
            public static string ValidateCartItems = "Validate That All Selected Cart Items Displayed.";
            public static string ValidateNewQuantityOfAllCartItems = "Validate That New Quantity Of All Selected Cart Items Displayed Correctly.";
            public static string ValidateExistenceOfDeletedCartItems = "Validate That Deleted Cart Items Disappeared.";
        }

        #endregion
    }
}