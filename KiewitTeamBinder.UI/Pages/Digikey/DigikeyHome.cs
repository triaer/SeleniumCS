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
using OpenQA.Selenium.Support.UI;
using KiewitTeamBinder.UI.Pages.Digikey;

namespace KiewitTeamBinder.UI.Pages.Home
{
    public class DigikeyHome : DigikeyGeneral
    {
        public DigikeyHome(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _imgLocation(string location) => By.XPath($"//a[text()='{location}']/ancestor::div/preceding-sibling::a[contains(@id,'Flag')]/div");
        #endregion

        #region Methods
        public IWebElement ImgLocation(string location) => StableFindElement(_imgLocation(location));
        #endregion

        #region Methods
        public DigikeyHome SelectLocation(string location)
        {
            var node = CreateStepNode();
            node.Info("Select location: " + location);
            ImgLocation(location).Click();
            EndStepNode(node);
            return this;
        }
        #endregion
    }
}
