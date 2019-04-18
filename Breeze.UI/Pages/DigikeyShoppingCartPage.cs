using Breeze.Common.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using static Breeze.UI.ExtentReportsHelper;

namespace Breeze.UI.Pages
{
    public class DigikeyShoppingCartPage : DigikeyBasePage
    {

        #region Locators
        private By _eleCartTitle => By.Id("divCartTitle");
        private By _eleTargetProduct(string number) => By.XPath($"//table[@id='cartDetails']//tr[.//div[@class='cart-partNumber']/a[contains(@href,'{number}')]]");
        private By _chkDeleteTargetProduct(string number) => By.XPath($"//table[@id='cartDetails']//tr[.//div[@class='cart-partNumber']/a[contains(@href,'{number}')]]//input[@type='checkbox']");
        private By _txtTargetProductQuantity(string number) => By.XPath($"//table[@id='cartDetails']//tr[.//div[@class='cart-partNumber']/a[contains(@href,'{number}')]]//div[@class='cart-qtyInput']/input");
        private By _txtTargetProductCustomerReference(string number) => By.XPath($"//table[@id='cartDetails']//tr[.//div[@class='cart-partNumber']/a[contains(@href,'{number}')]]//div[@class='cart-customerReference']/input");
        private By _btnDeleteProduct => By.XPath("//div[@id='cartViewButtons']/span[@class='trash-button icon-trash']");
        private By _btnConfirmDeleteProductOnPopup => By.XPath("//div[@role='dialog']//button[text()='OK']");
        #endregion

        #region Elements
        public IWebElement DeleteTargetProductCheckbox(string number) => StableFindElement(_chkDeleteTargetProduct(number));
        public IWebElement TargetProductQuantityTextbox(string number) => StableFindElement(_txtTargetProductQuantity(number));
        public IWebElement TargetProductCustomerReferenceTextbox(string number) => StableFindElement(_txtTargetProductCustomerReference(number));
        public IWebElement DeleteProductButton => StableFindElement(_btnDeleteProduct);
        public IWebElement ConfirmDeleteProductOnPopupButton => StableFindElement(_btnConfirmDeleteProductOnPopup);
        #endregion

        #region Methods

        public DigikeyShoppingCartPage() : base()
        {
            WaitForElement(_eleCartTitle);
        }

        public DigikeyShoppingCartPage EditShoppingInfos(List<DigiProduct> productList)
        {
            var node = CreateStepNode();
            foreach (var product in productList)
            {
                node.Info($"Editting shopping info of product with Key Number = {product.KeyPartNumber}");
                node.Info($"Update quantity = {product.Quantity}");
                TargetProductQuantityTextbox(product.KeyPartNumber).Click();
                WaitForLoadingPanel();
                TargetProductQuantityTextbox(product.KeyPartNumber).SendKeys(Keys.Control + "a");
                TargetProductQuantityTextbox(product.KeyPartNumber).SendKeys(product.Quantity.ToString());
                TargetProductQuantityTextbox(product.KeyPartNumber).SendKeys(Keys.Enter);
                WaitForLoadingPanel();
                node.Info($"Update customer reference = {product.CustomerReference}");
                TargetProductCustomerReferenceTextbox(product.KeyPartNumber).Clear();
                WaitForLoadingPanel();
                TargetProductCustomerReferenceTextbox(product.KeyPartNumber).SendKeys(product.CustomerReference);
                TargetProductCustomerReferenceTextbox(product.KeyPartNumber).SendKeys(Keys.Enter);
                WaitForLoadingPanel();
            }

            EndStepNode(node);
            return this;
        }

        public DigikeyShoppingCartPage DeleteProducts(List<DigiProduct> productList)
        {
            var node = CreateStepNode();
            foreach (var product in productList)
            {
                node.Info($"Delete product with Key Number = {product.KeyPartNumber}");
                DeleteTargetProductCheckbox(product.KeyPartNumber).Check();
            }
            DeleteProductButton.Click();
            ConfirmDeleteProductOnPopupButton.Click();
            WaitForLoadingPanel();

            EndStepNode(node);
            return this;
        }

        public KeyValuePair<string, bool> ValidateProductsExistInCart(List<DigiProduct> productList)
        {
            var node = CreateStepNode();
            try
            {
                bool actualReSult = true;
                foreach (var product in productList)
                {
                    if (!IsElementPresent(_eleTargetProduct(product.KeyPartNumber)))
                    {
                        actualReSult = false;
                        break;
                    }
                }

                if (actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateProductsExistInCart);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateProductsExistInCart);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateProductsExistInCart, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidateModifiedCustomerNameAndQuantityAreCorrect(List<DigiProduct> productList)
        {
            var node = CreateStepNode();
            try
            {
                bool actualReSult = true;
                foreach (var product in productList)
                {
                    if (!TargetProductCustomerReferenceTextbox(product.KeyPartNumber).GetValue().Equals(product.CustomerReference) ||
                        !TargetProductQuantityTextbox(product.KeyPartNumber).GetValue().Equals(product.Quantity.ToString()))
                    {
                        actualReSult = false;
                        break;
                    }
                }

                if (actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateModifiedCustomerNameAndQuantityAreCorrect);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateModifiedCustomerNameAndQuantityAreCorrect);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateModifiedCustomerNameAndQuantityAreCorrect, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidateDeletedProductsNotExistInCart(List<DigiProduct> productList)
        {
            var node = CreateStepNode();
            try
            {
                bool actualReSult = true;
                foreach (var product in productList)
                {
                    if (IsElementPresent(_eleTargetProduct(product.KeyPartNumber)))
                    {
                        actualReSult = false;
                        break;
                    }
                }

                if (actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateDeletedProductsNotExistInCart);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateDeletedProductsNotExistInCart);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateDeletedProductsNotExistInCart, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        private static class ValidationMessage
        {
            public static string ValidateProductsExistInCart = "Verify selected products are in cart";
            public static string ValidateModifiedCustomerNameAndQuantityAreCorrect = "Verify just modified customer name and quantity are correct";
            public static string ValidateDeletedProductsNotExistInCart = "Verify deleted products are not in cart anymore";
        }

        #endregion
    }
}