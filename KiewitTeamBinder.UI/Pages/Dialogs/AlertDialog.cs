using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Windows.Forms;
using KiewitTeamBinder.UI.Pages.Global;
using AventStack.ExtentReports;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.Dialogs
{
    public class AlertDialog : LoggedInLanding
    {
        #region Entities
        public static By _messageLabel => By.XPath("//div[contains(@id,'message')]");
        public static By _OKButton => By.XPath("");

        public IWebElement MessageLabel { get { return StableFindElement(_messageLabel); } }
        public IWebElement OKButton { get { return StableFindElement(_OKButton); } }
        #endregion

        public AlertDialog(IWebDriver webDriver) : base(webDriver)
        {
            
        }
    }
}
