﻿using KiewitTeamBinder.UI.Pages.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.Diagnostics;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.Common.TestData;

namespace KiewitTeamBinder.UI.Pages
{
    public class CartDigiKey : LoggedInLanding
    {
        #region Lcators
        private By _customerRefs(int elements) => By.XPath($"(//input[@class='customer-reference'])[{elements}]");
        private By _quantityLable => By.XPath("//th[@class='detailRow_quantity_th']");
        //private By _detailRows => By.XPath("//table[@class='DetailTable']//tr[contains(@class,'detailRow') and not(contains(@class,'sub'))]");
        private By _deleteCheckbox(int checkbox) => By.XPath($"(//input[@class ='deleteCheckbox'])[{checkbox}]");
        private By _deleteButton => By.XPath("//div[@id='cartViewButtons']//span[@class='trash-button icon-trash']");
        private By _okButton => By.XPath("//button[contains(text(),'OK')]");
        private By _quantityTextbox(int elements) => By.XPath($"(//input[@class='part-quantity text-box single-line'])[{elements}]");
        private By _loadingPanel => By.Id("spinner");
        private By _digiKeyNumbers => By.XPath("//div[@class='cart-partNumber']");
        #endregion

        #region Elements
        public IWebElement CustomerRef(int elements) => StableFindElement(_customerRefs(elements));
        public IWebElement DeleteCheckbox(int checkbox) => StableFindElement(_deleteCheckbox(checkbox));
        public IWebElement DeleteButton { get { return StableFindElement(_deleteButton); } }
        public IWebElement OkBUtton { get { return StableFindElement(_okButton); } }
        public IWebElement QuantityTextbox(int elements) => StableFindElement(_quantityTextbox(elements));
        public IWebElement QuantityLable { get { return StableFindElement(_quantityLable); } }
        public IWebElement LoadingPanel { get { return StableFindElement(_loadingPanel); } }
        public List<IWebElement> DigiKeyNumbers => StableFindElements(_digiKeyNumbers).ToList();
        #endregion

        #region Methods
        List<string> ExpDigiKeyList;
        public CartDigiKey ModifyCustomerRef(string [] modifiedRef, string [] modifiedQuantity, int elements)
        {
            for (int i = 1; i <= elements; i++)
            {
                CustomerRef(i).Clear();
                WaitForLoadingPanel();
                //WaitForLoadingPanel(_loadingPanel);
                CustomerRef(i).SendKeys(modifiedRef[i-1]);
                if (i==elements)
                {
                    CustomerRef(elements).SendKeys(Keys.Enter);
                    WaitForLoadingPanel();
                    //Thread.Sleep(2000);
                }
                 
            }
            for (int i = 1; i <= elements; i++)
            {

                QuantityTextbox(i).SendKeys(Keys.Control + "a");
                QuantityTextbox(i).SendKeys(modifiedQuantity[i-1]);
                QuantityTextbox(i).SendKeys(Keys.Enter);
                WaitForLoadingPanel();
                //Thread.Sleep(2000);
                //WaitForElement
                //    System.Windows.Forms.SendKeys.SendWait(@"{Enter}");

            }

            return this;
        }

        //public void WaitForLoadingPanel(By pannel, long timeout = 2)
        //{
        //    //WaitForElementDisplay(pannel);
        //    WaitForElement(pannel);
        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();
        //    do
        //    {
        //        WaitForElementDisappear(pannel);

        //    } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);
        //}

     

        public CartDigiKey DeleteProducts(int products)
        {
            for (int i = 1; i <= products; i++)
            {
                DeleteCheckbox(i).Check();
            }
            DeleteButton.Click();
            WaitForElement(_okButton);
            OkBUtton.Click();
            return this;
        }

        public bool CompareDigiKeyNumbers(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                if (ExpDigiKeyList[i] != DigiKeyNumbers[i].Text)
                {
                    return false;
                }
            }
            return true;
        }

        public KeyValuePair<string, bool> ValidateProductInCart()
        {
            var node = CreateStepNode();
            DigiKeyTestsSmoke digiKeyData = new DigiKeyTestsSmoke();
            try
            {
                if (CompareDigiKeyNumbers(digiKeyData.Quantity))
                {
                    return SetPassValidation(node, ValidationMessage.ValidateNumber);
                }
                return SetFailValidation(node, ValidationMessage.ValidateNumber);
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateNumber, e);
            }
        }
        #endregion

        public CartDigiKey(IWebDriver webDriver) : base(webDriver)
        {
        }
        public CartDigiKey(IWebDriver webDriver, List<string> digiKeyNumberList) : base(webDriver)
        {
            ExpDigiKeyList = digiKeyNumberList;
        }
        private static class ValidationMessage
        {
            public static string ValidateNumber = "verify Digi-Key Part Number & Manufacturer Part Number is correct";

        }
    }
}
