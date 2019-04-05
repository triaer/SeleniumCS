using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.DashBoardENums;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.Common.AgodaEnums;
using KiewitTeamBinder.Common.Models.Agoda;
using OpenQA.Selenium.Support.UI;

namespace KiewitTeamBinder.UI.Pages.Digikey
{
    public class DigikeyProductList : DigikeyGeneral
    {
        public DigikeyProductList(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _imgProduct(int position) => By.XPath($"//tr[@itemscope][position()<={position}]/td[@class='tr-image']");
        static By _imgSingleProduct(int position) => By.XPath($"//tr[@itemscope][{position}]/td[@class='tr-image']");
        static By _chkProduct(int position) => By.XPath($"//tr[@itemscope][position()<={position}]//input");
        static By _lnkDigikeyPartNumber(int position) => By.XPath($"//tr[@itemscope][position()<={position}]/td[contains(@class,'tr-dkPartNumber')]/a");
        static By _lnkMrfPartNumber(int position) => By.XPath($"//tr[@itemscope][position()<={position}]/td[contains(@class,'tr-mfgPartNumber')]//span");
        static By _btnCompareSelected => By.XPath("//input[@id='compare-button']");
        #endregion

        #region Elements
        public IReadOnlyCollection<IWebElement> ImgProduct(int position) => StableFindElements(_imgProduct(position));
        public IWebElement ImgSingleProduct(int position) => StableFindElement(_imgSingleProduct(position));
        public IReadOnlyCollection<IWebElement> ChkProduct(int position) => StableFindElements(_chkProduct(position));
        public IReadOnlyCollection<IWebElement> LnkDigikeyPartNumber(int position) => StableFindElements(_lnkDigikeyPartNumber(position));
        public IReadOnlyCollection<IWebElement> LnkMrfPartNumber(int position) => StableFindElements(_lnkMrfPartNumber(position));
        public IWebElement BtnCompareSelected => StableFindElement(_btnCompareSelected);

        #endregion

        #region Methods
        public DigikeyProductList SelectProduct(int productPosition)
        {
            var node = CreateStepNode();
            node.Info("Select the products");
            foreach (var item in ChkProduct(productPosition))
            {
                ScrollToElement(_chkProduct(productPosition));
                //WaitForElementClickable(_chkProduct(productPosition));
                item.Check();
            }
            GetDigikeyPartNumber(productPosition);
            GetMrfPartNumber(productPosition);
            EndStepNode(node);
            return this;
        }

        public string[] GetDigikeyPartNumber(int position)
        {
            var node = CreateStepNode();
            List<string> tempDigikey = new List<string>();
            foreach (var item in LnkDigikeyPartNumber(position))
            {
                tempDigikey.Add(item.Text);
            }
            string[] digikey = tempDigikey.ToArray();
            node.Info("Get Mouser information of product in position: " + position + ", the value is: " + tempDigikey);
            EndStepNode(node);
            return digikey = Constant.digikeyPartNumber;
        }

        public string[] GetMrfPartNumber(int position)
        {
            var node = CreateStepNode();
            List<string> tempMrf = new List<string>();
            foreach (var item in LnkMrfPartNumber(position))
            {
                tempMrf.Add(item.Text);
            }
            string[] mrf = tempMrf.ToArray();
            node.Info("Get Mouser information of product in position: " + position + ", the value is: " + mrf);
            EndStepNode(node);
            return mrf = Constant.digikeyMrfNumber;
        }

        public DigikeyCompare OpenDigikeyComparePage()
        {
            var node = CreateStepNode();
            node.Info("Open the Digikey Compare page.");
            WaitForElementClickable(_btnCompareSelected);
            BtnCompareSelected.Click();
            EndStepNode(node);
            return new DigikeyCompare(WebDriver);
        }

        public DigikeyProductList AddProductsToCart(string productSubSection, int position, int quantity, string customerReference)
        {
            var node = CreateStepNode();
            node.Info("Add some products to cart.");
            DigikeyProductDetail digikeyProductDetail = new DigikeyProductDetail(WebDriver);
            //foreach (var item in ImgProduct(position))
            //{
            //    item.Click();
            //    digikeyProductDetail.AddAProductToCartFromProductDetailPage(quantity, customerReference);
            //    BackToPreviousPage<DigikeyProductDetail>();
            //    digikeyProductDetail.LnkProduct(productSubSection);
            //}
            int i = 1;
            while (i <= position)
            {
                ScrollToElement(_imgProduct(i));
                ImgSingleProduct(i).Click();
                digikeyProductDetail.AddAProductToCartFromProductDetailPage(quantity, customerReference);
                i++;
                if(i <= position)
                {
                    BackToPreviousPage<DigikeyProductDetail>();
                    digikeyProductDetail.LnkProduct(productSubSection).Click();
                }
            }
            EndStepNode(node);
            return new DigikeyProductList(WebDriver);
        }

        #endregion
    }
}
