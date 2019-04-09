using Digikey.DataObjects;
using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using static Digikey.ExtentReportsHelper;

namespace Digikey.Pages
{
    public class ProductComparePage : PageBase
    {
        private IWebDriver _driver;
        private ArrayList _list;

        # region Locators
        private string _linkDigiKeyPN = "//tr[.//th[contains(text(),'Digi-Key Part Number')]]/td[{0}]/a";
        private string _mfgPN = "//tr[.//th[contains(text(),'Manufacturer Part Number')]]/td[{0}]";
        private string _manufacturer = "//tr[.//th[contains(text(),'Manufacturer')]]/td[{0}]//span[@itemprop='name']";

        private By _LinkBackToProduct = By.XPath("./descendant::a[text()='Back to Search Results'][position()=1]");

        #endregion

        #region Elements
        public IWebElement LinkDigikeyPart(int productNumber)
        {
            return StableFindElement(By.XPath(string.Format(_linkDigiKeyPN, productNumber.ToString())));
        }

        public IWebElement LinkMfgPartNumber(int productNumber)
        {
            return StableFindElement(By.XPath(string.Format(_mfgPN, productNumber.ToString())));
        }

        public IWebElement LinkManufacturer(int productNumber)
        {
            return StableFindElement(By.XPath(string.Format(_manufacturer, productNumber.ToString())));
        }

        public IWebElement LinkBackToProduct
        {
            get { return StableFindElement(_LinkBackToProduct); }
        }

        # endregion
        
        # region Methods
        public ProductComparePage(IWebDriver driver, ArrayList list) : base (driver)
        {
            this._driver = driver;
            this._list = list;
        }

        public ProductComparePage(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
        }

        public ProductsPage BackToProductPage()
        {
            this.LinkBackToProduct.Click();
            return new ProductsPage(_driver);
        }

        public bool CompareData()
        {
            bool result = true;
            for (int i = 1; i <= _list.Count; i++)
            {
                var prod = (Product)_list[i - 1];
                // Console.WriteLine(prod._digiKey + "|" + prod._mfgPartNumber + "|" + prod._manufacturer);
                // Console.WriteLine(prod._digiKey + " | " + LinkDigikeyPart(i).Text);
                bool a = prod._digiKey.Equals(LinkDigikeyPart(i).Text);
                // Console.WriteLine(prod._mfgPartNumber + " | " + LinkMfgPartNumber(i).Text);
                bool b = prod._mfgPartNumber.Equals(LinkMfgPartNumber(i).Text);
                // Console.WriteLine(prod._manufacturer + " | " + LinkManufacturer(i).Text);
                bool c = prod._manufacturer.Equals(LinkManufacturer(i).Text);

                if (prod._digiKey.Equals(LinkDigikeyPart(i).Text) && prod._mfgPartNumber.Equals(LinkMfgPartNumber(i).Text) && prod._manufacturer.Equals(LinkManufacturer(i).Text))
                    result = true;
                else
                {
                    result = false;
                    break;
                }
            }      
            return result;
        }

        public KeyValuePair<string, bool> ValidateSelectedItemsInfo()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool totalCheck = this.CompareData();

                if (totalCheck == true)
                    validation = SetPassValidation(node, ValidationMessage.ValidateSelectedItemsInfo);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateSelectedItemsInfo);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateSelectedItemsInfo, e);
            }
            EndStepNode(node);
            return validation;
        }

        private static class ValidationMessage
        {
            public static string ValidateSelectedItemsInfo = "Validate That All Information Of Selected Items Correct.";
        }
        
        # endregion

    }
}