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

        #region Actions
        public AlertDialog(IWebDriver webDriver) : base(webDriver)
        {
            
        }

        public void ClickOKButton()
        {
            OKButton.HoverAndClickWithJS();
        }

        private string GetMessageOnDialog()
        {
            return MessageLabel.Text;
        }

        public KeyValuePair<string, bool> ValidateMessageOnDialog(string expectedMessage)
        {
            var node = StepNode();
            try
            {
                if (expectedMessage == GetMessageOnDialog())
                    return SetPassValidation(node, Validation.Message_On_Dialog);
                else
                    return SetFailValidation(node, Validation.Message_On_Dialog);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Message_On_Dialog, e);
            }
        }

        public KeyValuePair<string, bool> ValidateDialogOpens(bool checkOpened)
        {
            var node = StepNode();
            try
            {
                IWebElement Board = StableFindElement(By.XPath("//form[@id='form1']/div[1][table]"));
                if (Board.GetAttribute("id").ToLower().Contains("alert") == checkOpened)
                    return SetPassValidation(node, Validation.Dialog_Opens);

                return SetFailValidation(node, Validation.Dialog_Opens);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Dialog_Opens, e);
            }
        }

        private static class Validation
        {
            public static string Message_On_Dialog = "Validate that the content of message on dialog displays correctly";
            public static string Dialog_Opens = "Validate that the dialog opens";
            public static string Dialog_Closes = "Validate that the dialog Closes";
            public static string OK_Button_Displays = "Validate that the OK button displays";
        }
        #endregion
    }
}
