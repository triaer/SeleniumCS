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
    public class DigikeyBasePage : PageBase
    {

        #region Locators
        private By _linkProductMenu => By.XPath("//a[contains(@class,'header__resource') and ./span[text()='PRODUCTS']]");

        #endregion

        #region Elements
        public IWebElement ProductLinkMenu => StableFindElement(_linkProductMenu);
        #endregion

        #region Methods

        public DigikeyBasePage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public DigikeyProductCatagoryPage SelectProductMenu()
        {
            var node = CreateStepNode();
            node.Info("Select PRODUCTS top menu");
            ProductLinkMenu.Click();
            EndStepNode(node);
            return new DigikeyProductCatagoryPage(WebDriver);
        }
        #endregion
    }
}
