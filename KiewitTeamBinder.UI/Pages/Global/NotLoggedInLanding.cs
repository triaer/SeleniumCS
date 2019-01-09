using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace KiewitTeamBinder.UI.Pages.Global
{
    public class NotLoggedInLanding : PageBase
    {
        #region Entities 

        private static By _otherUserLoginBtn => By.XPath("//a[text()='Other User Login']");
        private static By _kiewitUserLoginBtn => By.XPath("//a[contains(text(),'Kiewit User Login')]");

        public IWebElement OtherUserLoginBtn { get { return StableFindElement(_otherUserLoginBtn); } }
        public IWebElement KiewitUserLoginBtn { get { return StableFindElement(_kiewitUserLoginBtn); } }


        #endregion

        #region Actions

        public NotLoggedInLanding(IWebDriver webDriver) : base(webDriver)
        {

        }

        //public NonSsoSignOn ClickOtherUserLoginBtn(out string logonLink)
        //{
        //    OtherUserLoginBtn.Click();
        //    logonLink = WebDriver.WindowHandles.Last();

        //    return new NonSsoSignOn(WebDriver); ;
        //}
        #endregion
    }
}
