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
//using KiewitTeamBinder.UI.IWebElementExtensions;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages;

namespace KiewitTeamBinder.UI.Tests.User
{
    public class DataProfilesAndPanelTable : MainPage
    {
        public DataProfilesAndPanelTable(IWebDriver driver) : base(driver)
        {
        }

        #region Elements
        static By _lnkElementBasedOnPage(string pageName, string lnkButton) => By.XPath($"//td[a[text()='{pageName}']]/following-sibling::td/a[text()='{lnkButton}']");

        #endregion

        #region Locators
        public IWebElement LnkElementBasedOnPage(string pageName, string lnkButton) => StableFindElement(_lnkElementBasedOnPage(pageName, lnkButton));

        #endregion

        #region Methods
        public DataProfilesAndPanelTable ClickTableLinkButton(string pageName, string lnkButton)
        {
            var node = CreateStepNode();
            node.Info("Click the link button: " + lnkButton + " of page " + pageName);
            LnkElementBasedOnPage(pageName, lnkButton).Click();
            EndStepNode(node);
            return this;
        }

        #endregion
    }
}
