using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.Popup;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages
{
    public class DigikeyProductDetailsPage : DigikeyBasePage
    {

        #region Locators
        private By _eleProductDetails => By.Id("pdp_content");
        private By _txtProductQuantity => By.Id("qty");
        private By _txtCustomerReference => By.Id("cref");
        private By _btnAddToCart => By.Id("addtoorderbutton");
        #endregion

        #region Elements
        public IWebElement ProductQuantityTextbox => StableFindElement(_txtProductQuantity);
        public IWebElement CustomerReferenceTextbox => StableFindElement(_txtCustomerReference);
        public IWebElement AddToCartButton => StableFindElement(_btnAddToCart);
        #endregion

        #region Methods

        public DigikeyProductDetailsPage(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElement(_eleProductDetails);
        }

        public DigikeyShoppingCartPage EnterShoppingInfoAndAddToCart(DigiProduct productInfo)
        {
            var node = CreateStepNode();
            node.Info($"Enter Quantity={productInfo.Quantity}, Customer Reference={productInfo.CustomerReference}");
            ProductQuantityTextbox.InputText(productInfo.Quantity.ToString());
            CustomerReferenceTextbox.InputText(productInfo.CustomerReference);
            node.Info("Select 'Add to Cart' button");
            AddToCartButton.Click();

            EndStepNode(node);
            return new DigikeyShoppingCartPage(WebDriver);
        }

        #endregion
    }
}