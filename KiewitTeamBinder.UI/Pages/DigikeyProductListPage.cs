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
    public class DigikeyProductListPage : DigikeyBasePage
    {

        #region Locators
        private By _eleProductIndexList => By.Id("productTable");
        private By _btnCompareSelected => By.Id("compare-button");
        private By _eleTargetProductDigiKeyPartNumber(int index) => By.XPath($"//table[@id='productTable']/tbody/tr[{index}]/td[contains(@class,'tr-dkPartNumber')]/a");
        private By _eleTargetProductManufacturerPartNumber(int index) => By.XPath($"//table[@id='productTable']/tbody/tr[{index}]/td[contains(@class,'tr-mfgPartNumber')]//span");
        private By _chkComparePart(string number) => By.XPath($"//tr[./td[contains(@class,'tr-dkPartNumber') and ./a[normalize-space(text())='{number}']]]/td[contains(@class,'tr-compareParts')]/input");
        private By _linkDigiKeyPartNumber(string number) => By.XPath($"//td[contains(@class,'tr-dkPartNumber')]/a[normalize-space(text())='{number}']");

        #endregion

        #region Elements
        public IWebElement ProductIndexListElement => StableFindElement(_eleProductIndexList);
        public IWebElement CompareSelectedButton => StableFindElement(_btnCompareSelected);
        public IWebElement TargetProductDigiKeyPartNumber(int index) => StableFindElement(_eleTargetProductDigiKeyPartNumber(index));
        public IWebElement TargetProductManufacturerPartNumber(int index) => StableFindElement(_eleTargetProductManufacturerPartNumber(index));
        public IWebElement ComparePartCheckbox(string number) => StableFindElement(_chkComparePart(number));
        public IWebElement DigiKeyPartNumberLink(string number) => StableFindElement(_linkDigiKeyPartNumber(number));
        #endregion

        #region Methods

        public DigikeyProductListPage(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElement(_eleProductIndexList);
        }

        public DigiProduct GetProductInfoAtIndex(int index)
        {
            var node = CreateStepNode();
            string keyNumber = TargetProductDigiKeyPartNumber(index).Text.Trim();
            string manufacturerNumber = TargetProductManufacturerPartNumber(index).Text.Trim();
            node.Info($"Get info of product item {index}: Key Number={keyNumber}, Manufacturer Number={manufacturerNumber}");
            EndStepNode(node);
            return new DigiProduct()
            {
                KeyPartNumber = keyNumber,
                ManufacturerPartNumber = manufacturerNumber
            };
        }

        public DigikeyProductComparisonPage SelectProductsAndCompare(List<DigiProduct> productList)
        {
            var node = CreateStepNode();
            foreach (var product in productList)
            {
                node.Info($"Select product with Key Number = {product.KeyPartNumber}");
                ScrollIntoViewBottom(ComparePartCheckbox(product.KeyPartNumber));
                ComparePartCheckbox(product.KeyPartNumber).Check();
            }
            node.Info("Click on 'Compare Selected' button");
            CompareSelectedButton.Click();

            EndStepNode(node);
            return new DigikeyProductComparisonPage(WebDriver);
        }

        public DigikeyProductDetailsPage SelectTargetProduct(DigiProduct productInfo)
        {
            var node = CreateStepNode();
            node.Info($"Select product with Key Number = {productInfo.KeyPartNumber}");
            ScrollIntoViewBottom(DigiKeyPartNumberLink(productInfo.KeyPartNumber));
            DigiKeyPartNumberLink(productInfo.KeyPartNumber).ClickWithJS();

            EndStepNode(node);
            return new DigikeyProductDetailsPage(WebDriver);
        }

        #endregion
    }
}