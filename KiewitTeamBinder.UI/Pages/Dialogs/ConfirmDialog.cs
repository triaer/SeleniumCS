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
    public class ConfirmDialog : LoggedInLanding
    {
        #region Entities
        public static By _dialog => By.XPath("//*[contains(@id,'RadWindowWrapper_confirm')]");
        public static By _messageLabel => By.XPath("//div[contains(@id,'message')]");
        public static By _yesButton => By.XPath("//a[span='Yes']");
        public static By _noButton => By.XPath("//a[span='No'] ");

        public IWebElement Dialog { get { return StableFindElement(_dialog); } }
        public IWebElement MessageLabel { get { return StableFindElement(_messageLabel); } }
        public IWebElement YesButton { get { return StableFindElement(_yesButton); } }
        public IWebElement NoButton { get { return StableFindElement(_noButton); } }
        #endregion

        #region Actions
        public ConfirmDialog(IWebDriver webDriver) : base(webDriver)
        {

        }

        public void ConfirmAction(bool confirm)
        {
            if (confirm)
                YesButton.HoverAndClickWithJS();
            else
                NoButton.HoverAndClickWithJS();
        }

        public string GetMessageOnDialog()
        {
            return MessageLabel.Text;
        }

        public KeyValuePair<string, bool> ValidateDialogOpens(bool checkOpened)
        {
            var node = StepNode();
            try
            {
                IWebElement Board = StableFindElement(By.XPath("//form[@id='form1']/div[1]"));
                if (Board.GetAttribute("id").Contains("confirm") == checkOpened)
                    return SetPassValidation(node, Validation.Dialog_Opens);
                
                return SetFailValidation(node, Validation.Dialog_Opens);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Dialog_Opens, e);
            }
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

        

        private static class Validation
        {
            public static string Message_On_Dialog = "Validate that the content of message on dialog displays correctly";
            public static string Dialog_Opens = "Validate that the dialog opens";
            public static string Dialog_Closes = "Validate that the dialog Closes";
        }
        #endregion
    }
}
