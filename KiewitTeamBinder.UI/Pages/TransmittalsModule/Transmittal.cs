using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using OpenQA.Selenium;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.PopupWindows;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using OpenQA.Selenium.Support.UI;
using KiewitTeamBinder.Common.Helper;


namespace KiewitTeamBinder.UI.Pages.TransmittalsModule
{
    public class Transmittal : ProjectsDashboard
    {
        #region Entities
        private static By _transmittalRegisterGridCol(string value) => By.XPath($"//table[@id='ctl00_cntPhMain_TransmittalRegisterGrid_ctl00']//tbody/tr//td[contains(.,'{value}')]/..");
        private static By _transmittalRegisterGridRows = By.XPath("//table[@id='ctl00_cntPhMain_TransmittalRegisterGrid_ctl00']//tr");
        private static By _transmittalRegisterGrid = By.XPath("//table[@id='ctl00_cntPhMain_TransmittalRegisterGrid_ctl00']//tbody");

        public IWebElement TransmittalRegisterGridCol(string value) => StableFindElement(_transmittalRegisterGridCol(value));
        public IWebElement TransmittalRegisterGrid { get { return StableFindElement(_transmittalRegisterGrid); } }
        #endregion

        #region Actions
        public Transmittal(IWebDriver webDriver) : base(webDriver)
        {
        }

        public IWebElement FindTransmittalMail(string transmittalNo)
        {
            var node = StepNode();
            node.Info("Transmisttal No.: " + transmittalNo);
            return TransmittalRegisterGridCol(transmittalNo); 
        }

        public TransmittalDetail OpenTransmittalMail(string transmittalNo, out string parrentWindow)
        {
            var node = StepNode();
            IWebElement ItemTransmittalMail = FindTransmittalMail(transmittalNo);
            SwitchToNewPopUpWindow(ItemTransmittalMail, out parrentWindow, false, true);

            return new TransmittalDetail(WebDriver);
        }

        public KeyValuePair<string, bool> ValidateNewTransmittalFromVendorInbox()
        {
            var node = StepNode();
            
            try
            {
                List<IWebElement> listElement = StableFindElements(_transmittalRegisterGridRows).ToList();
                foreach (var item in listElement)
                {
                    string atribute = item.GetCssValue("font-weight");
                    if (int.Parse(atribute) >= 700)
                    {
                        node.Info("TransmittalNo: " + item.Text);
                        atribute = "bold";
                    }

                    if (atribute == "bold")
                        return SetPassValidation(node, Validation.New_Transmittal_From_Vendor_Inbox);
                }

                return SetFailValidation(node, Validation.New_Transmittal_From_Vendor_Inbox);

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.New_Transmittal_From_Vendor_Inbox, e);
            }
        }

        
        #endregion

        private static class Validation
        {
            public static string New_Transmittal_From_Vendor_Inbox = "Validate that the new transmittal from vendor inbox.";
            
        }
    }
}
