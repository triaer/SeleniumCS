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
using KiewitTeamBinder.UI.Pages.PopupWindows;


namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class VendorDataRegister : ProjectsDashboard
    {
        #region Entities
        private static By _deliverableLineItem => By.XPath("//span[(text()='Deliverable Line Item')]");

        public IWebElement DeliverableLineItem => StableFindElement(_deliverableLineItem);
        #endregion

        #region Actions
        public VendorDataRegister(IWebDriver webDriver) : base(webDriver)
        {
        }        

        public DeliverableItemDetail OpenDeliverableLineItemTemplate(out string parrentWindow)
        {
            SwitchToNewPopUpWindow(DeliverableLineItem, out parrentWindow, true);
            return new DeliverableItemDetail(WebDriver);
        }

        private static class Validation
        {
            
        }
            #endregion
    }
}
