using SelenCS.Common.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using static SelenCS.UI.ExtentReportsHelper;

namespace SelenCS.UI.Pages
{
    public class DigikeyProductComparisonPage : DigikeyBasePage
    {

        #region Locators
        private By _formCompare => By.XPath("//form[@name='compform']");
        private By _eleManufacturerNumberBaseOnKeyNumber(string keyNumber, string manufacturerNumber) => By.XPath($"//tr[./th[normalize-space(text())='Manufacturer Part Number']]/td[position()=count(//tr[./th[normalize-space(text())='Digi-Key Part Number']]/td[./a[normalize-space(text())='{keyNumber}']]/preceding-sibling::td)+1 and normalize-space(text())='{manufacturerNumber}']");
        private By _linkBackToSearchResults => By.XPath("//a[text()='Back to Search Results']");

        #endregion

        #region Elements
        public IWebElement BackToSearchResultsLink => StableFindElement(_linkBackToSearchResults);
        #endregion

        #region Methods

        public DigikeyProductComparisonPage() : base()
        {
            WaitForElement(_formCompare);
        }

        public DigikeyProductListPage BackToSearchResultsPage()
        {
            var node = CreateStepNode();
            node.Info("Back to Search results page");
            BackToSearchResultsLink.Click();
            EndStepNode(node);
            return new DigikeyProductListPage();
        }

        public KeyValuePair<string, bool> ValidateComparedProductInfos(List<DigiProduct> productList)
        {
            var node = CreateStepNode();
            try
            {
                bool actualReSult = true;
                foreach (var product in productList)
                {
                    if (!IsElementPresent(_eleManufacturerNumberBaseOnKeyNumber(product.KeyPartNumber, product.ManufacturerPartNumber)))
                    {
                        actualReSult = false;
                        break;
                    }
                }

                if (actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateComparedProductInfo);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateComparedProductInfo);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateComparedProductInfo, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        private static class ValidationMessage
        {
            public static string ValidateComparedProductInfo = "Verify Key Number and Manufacturer Number of selected products are displayed and correct";
        }

        #endregion
    }
}