using Breeze.Common.Models;
using Breeze.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Breeze.UI.ExtentReportsHelper;

namespace Breeze.UI.Pages
{
    public class DigikeyBasePage : BasePage
    {

        #region Locators
        private By _linkProductMenu => By.XPath("//a[contains(@class,'header__resource') and ./span[text()='PRODUCTS']]");

        #endregion

        #region Elements
        public IWebElement ProductLinkMenu => StableFindElement(_linkProductMenu);
        #endregion

        #region Methods

        public DigikeyBasePage() : base()
        {
        }

        public DigikeyProductCatagoryPage SelectProductMenu()
        {
            var node = CreateStepNode();
            node.Info("Select PRODUCTS top menu");
            ProductLinkMenu.Click();
            EndStepNode(node);
            return new DigikeyProductCatagoryPage();
        }
        #endregion
    }
}
