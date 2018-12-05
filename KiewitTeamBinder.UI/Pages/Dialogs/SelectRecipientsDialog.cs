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
using SeleniumExtras.WaitHelpers;

namespace KiewitTeamBinder.UI.Pages.Dialogs
{
    public class SelectRecipientsDialog : LoggedInLanding
    {
        #region Entities


        #endregion

        #region Actions
        public SelectRecipientsDialog(IWebDriver webDriver) : base(webDriver)
        {
            webDriver.SwitchTo().ActiveElement();
        }
        #endregion
    }
}
