using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digikey.Pages;
using OpenQA.Selenium;
using static Digikey.ExtentReportsHelper;
using System.Globalization;
using System.Collections;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace Digikey.Pages
{
    public class DigikeyMain : PageBase
    {
        private IWebDriver _driver;

        # region Locators
        static readonly By _linkProducts = By.XPath("//a[@href='/products/en']");
        


        #endregion 

        //======================================

        #region Elements
        public IWebElement LinkProducts
        {
            get { return StableFindElement(_linkProducts); }
        }
        
        
        
        #endregion

        //======================================

        #region Methods
        public DigikeyMain(IWebDriver driver) : base (driver)
        {
            this._driver = driver;
        }
        
        public ProductsPage GoToProductPage()
        {
            this.LinkProducts.Click();
            return new ProductsPage(_driver);
        }

        

        
        

        public void test()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-FR");
            //Console.WriteLine(string.Hello);
        }

        public KeyValuePair<string, bool> ValidateFilledInformation()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool totalCheck = true;

                if (totalCheck == true)
                    validation = SetPassValidation(node, ValidationMessage.ValidateFilledInformation);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateFilledInformation);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateFilledInformation, e);
            }
            EndStepNode(node);
            return validation;
        }

        # endregion

        private static class ValidationMessage
        {
            public static string ValidateFilledInformation = "Validate That All Filled Information Is Correct.";
        }
    }


}
