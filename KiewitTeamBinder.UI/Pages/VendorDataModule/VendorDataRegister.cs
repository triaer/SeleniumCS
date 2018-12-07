using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.UI.Pages.Global;


namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class VendorDataRegister : ProjectsDashboard
    {
        #region Entities
        
        #endregion

        #region Actions
        public VendorDataRegister(IWebDriver webDriver) : base(webDriver)
        {
        }        

        private static class Validation
        {
            
        }
            #endregion
    }
}
