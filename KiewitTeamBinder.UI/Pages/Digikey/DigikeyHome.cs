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

namespace KiewitTeamBinder.UI.Pages.Mouser
{
    public class MouserHome : MouserGeneral
    {
        public MouserHome(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _imgLocation(string location) => By.XPath($"//a[text()='{location}']/ancestor::div/preceding-sibling::a[contains(@id,'Flag')]/div");
        #endregion

        #region Methods
        public IWebElement ImgLocation(string location) => StableFindElement(_imgLocation(location));
        #endregion

        #region Methods
        public MouserHome SelectLocation(string location)
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
