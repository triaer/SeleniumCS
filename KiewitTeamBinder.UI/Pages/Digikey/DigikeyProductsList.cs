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
    public class DigikeyProductsList : DigikeyGeneral
    {
        public DigikeyProductsList(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _imgProduct(int position) => By.XPath($"//tr[@itemscope][{position}]/td[@class='tr-image']");
        static By _chkProduct(int position) => By.XPath($"//tr[@itemscope][{position}]//input");
        static By _lnkDigikeyPartNumber(int position) => By.XPath($"//tr[@itemscope][{position}]/td[contains(@class,'tr-dkPartNumber')]/a");
        static By _lnkMrfPartNumber(int position) => By.XPath($"//tr[@itemscope][{position}]/td[contains(@class,'tr-mfgPartNumber')]//span");
        static By _btnCompareSelected => By.XPath("//input[@id='compare-button']");
        #endregion

        #region Elements
        public IWebElement ImgProduct(int position) => StableFindElement(_imgProduct(position));
        public IWebElement ChkProduct(int position) => StableFindElement(_chkProduct(position));
        public IWebElement LnkDigikeyPartNumber(int position) => StableFindElement(_lnkDigikeyPartNumber(position));
        public IWebElement LnkMrfPartNumber(int position) => StableFindElement(_lnkMrfPartNumber(position));
        public IWebElement BtnCompareSelected => StableFindElement(_btnCompareSelected);

        #endregion

        #region Methods
        public DigikeyProductsList SelectProduct(int[] productPosition)
        {
            var node = CreateStepNode();
            node.Info("Select the products");
            foreach (var item in productPosition)
            {
                ScrollIntoView(ImgProduct(item));
                ChkProduct(item).Check();
            }
            GetDigikeyPartNumber(productPosition);
            GetMrfPartNumber(productPosition);
            EndStepNode(node);
            return this;
        }

        public string[] GetDigikeyPartNumber(int[] position)
        {
            var node = CreateStepNode();
            List<string> tempDigikey = new List<string>();
            foreach (var item in position)
            {
                tempDigikey.Add(LnkDigikeyPartNumber(item).Text);
            }
            string[] digikey = tempDigikey.ToArray();
            node.Info("Get Mouser information of product in position: " + position + ", the value is: " + tempDigikey);
            EndStepNode(node);
            return digikey = Constant.digikeyPartNumber;
        }

        public string[] GetMrfPartNumber(int[] position)
        {
            var node = CreateStepNode();
            List<string> tempMrf = new List<string>();
            foreach (var item in position)
            {
                tempMrf.Add(LnkMrfPartNumber(item).Text);
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

        #endregion
    }
}
