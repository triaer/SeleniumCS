using OpenQA.Selenium;
using System;
using static Breeze.UI.ExtentReportsHelper;

namespace Breeze.UI.Pages
{
    public class DigikeyProductCatagoryPage : DigikeyBasePage
    {

        #region Locators
        private By _eleProductIndexList => By.Id("productIndexList");
        private By _linkProductCategoryMenu(string category, string subCategory) => By.XPath($"//h2[./a[text()='{category}']]/following-sibling::*[2]//a[text()='{subCategory}']");

        #endregion

        #region Elements
        public IWebElement ProductIndexListElement => StableFindElement(_eleProductIndexList);
        public IWebElement ProductCategoryLinkMenu(string category, string subCategory) => StableFindElement(_linkProductCategoryMenu(category, subCategory));
        #endregion

        #region Methods

        public DigikeyProductCatagoryPage() : base()
        {
            WaitForElement(_eleProductIndexList);
        }

        public DigikeyProductListPage SelectTargetProductCategory(string category, string subCategory)
        {
            var node = CreateStepNode();
            node.Info(String.Format("Select product category: {0}->{1}", category, subCategory));
            ProductCategoryLinkMenu(category, subCategory).Click();
            EndStepNode(node);
            return new DigikeyProductListPage();
        }

        #endregion
    }
}