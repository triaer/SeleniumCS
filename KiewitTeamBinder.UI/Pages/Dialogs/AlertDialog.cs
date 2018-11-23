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
    public class AlertDialog : LoggedInLanding
    {
        #region Entities

        public static By _oKButton => By.XPath("//div[@class='rwDialogPopup radalert']//a");
        public static By _messageDialog => By.XPath("//div[@class='rwDialogPopup radalert']//div[@class='rwDialogText']");
        public IWebElement OKButton { get { return StableFindElement(_oKButton); } }
        public IWebElement MessageDialog { get { return StableFindElement(_messageDialog); } }
        
        #endregion

        #region Actions
        public AlertDialog(IWebDriver webDriver) : base(webDriver)
        {
            webDriver.SwitchTo().ActiveElement();
        }

        public T ClickOKButton<T>()
        {
            OKButton.Click();
            WebDriver.SwitchTo().DefaultContent();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        private string GetDialogMessage()
        {
            return MessageDialog.Text;
        }

        public KeyValuePair<string, bool> ValidateMessageDialogAsExpected(string expectedMessage)
        {
            var node = StepNode();
            try
            {
                if (expectedMessage == GetDialogMessage())
                    return SetPassValidation(node, Validation.Message_On_Dialog);
                else
                    return SetFailValidation(node, Validation.Message_On_Dialog);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Message_On_Dialog, e);
            }
        }



        private static class Validation
        {
            public static string Message_On_Dialog = "Validate that the content of message on dialog displays correctly";
            public static string Dialog_Opens = "Validate that the dialog opens";
            public static string Dialog_Closes = "Validate that the dialog Closes";
        }
        #endregion
    }
}
