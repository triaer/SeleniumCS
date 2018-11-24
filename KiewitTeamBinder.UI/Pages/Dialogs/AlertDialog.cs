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
                string actualContent = GetDialogMessage();
                if (expectedMessage == actualContent)
                    return SetPassValidation(node, Validation.Message_On_Dialog + actualContent);
                else
                    return SetFailValidation(node, Validation.Message_On_Dialog, expectedMessage, actualContent);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Message_On_Dialog, e);
            }
        }



        private static class Validation
        {
            public static string Message_On_Dialog = "Validate The Content Of Message On Dialog Displays Correctly: ";
            public static string Dialog_Opens = "Validate That The Dialog Opens";
            public static string Dialog_Closes = "Validate That The Dialog Closes";
        }
        #endregion
    }
}
