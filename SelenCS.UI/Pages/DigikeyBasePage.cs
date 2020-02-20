using SelenCS.UI.Pages.Global;
using OpenQA.Selenium;
using static SelenCS.UI.ExtentReportsHelper;

namespace SelenCS.UI.Pages
{
    public class DigikeyBasePage : BasePage
    {

        #region Locators
        private By _linkProductMenu => By.XPath("//a[contains(@class,'section-title') and text()='Products']");

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
