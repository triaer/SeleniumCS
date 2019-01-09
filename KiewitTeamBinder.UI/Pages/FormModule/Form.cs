using System;
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
using KiewitTeamBinder.Common.TestData;

namespace KiewitTeamBinder.UI.Pages.FormModule
{
    public class Form : ProjectsDashboard
    {
        #region Entities
        
        #endregion

        #region Actions
        public Form(IWebDriver webDriver) : base(webDriver)
        {
        }        

        private static class Validation
        {
            
        }
        #endregion
    }
}
