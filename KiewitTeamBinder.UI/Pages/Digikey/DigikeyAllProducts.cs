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
    public class DigikeyAllProducts : DigikeyGeneral
    {
        public DigikeyAllProducts(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _lnkProductType(string section, string productType) => By.XPath($"(//h2[a[text()='{section}']]/following-sibling::ul)[1]//a[text()='{productType}']");

        #endregion

        #region Methods
        public IWebElement LnkProductType(string section, string productType) => StableFindElement(_lnkProductType(section, productType));
        #endregion

        #region Methods
        public DigikeyProductsDetail OpenSpecificProductList(string section, string type)
        {
            var node = CreateStepNode();
            node.Info("Open product list with section: " + section + " and type: " + type);
            LnkProductType(section, type).Click();
            EndStepNode(node);
            return new DigikeyProductsDetail(WebDriver);
        }
        #endregion
    }
}
