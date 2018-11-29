﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using OpenQA.Selenium.Support.UI;
using KiewitTeamBinder.Common.Helper;
using System.Windows.Forms;
using KiewitTeamBinder.UI.Pages.Dialogs;


namespace KiewitTeamBinder.UI.Pages.Global
{
    public class PackagesInbox : ProjectsDashboard
    {
        #region Entities

        private static By _projectListSumary => By.Id("divProjectSummary");

        #endregion

        public PackagesInbox(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}