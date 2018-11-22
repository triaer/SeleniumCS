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
    public class ApplyToNRowsDialog : LoggedInLanding
    {
        #region Entities
        public string IFrameName = "RadWindowConfirm";
        public static By _message => By.XPath("//*[@id ='tdMessageBody']");
        public static By _nTextbox => By.Id("txtInput");
        public static By _oKButton => By.XPath("//a[input[@value = 'OK']]");
        public static By _cancelButton => By.XPath("//a[input[@value = 'Cancel']]");

        public IWebElement Message { get { return StableFindElement(_message); } }
        public IWebElement NTextbox { get { return StableFindElement(_nTextbox); } }
        public IWebElement OKButton { get { return StableFindElement(_oKButton); } }
        public IWebElement CancelButton { get { return StableFindElement(_cancelButton); } }
        #endregion

        #region Actions
        public ApplyToNRowsDialog(IWebDriver webDriver) : base(webDriver)
        {

        }

        public ApplyToNRowsDialog EnterNumberOfRow(string numberOfRow)
        {
            NTextbox.InputText(numberOfRow);
            return this;
        }

        public void ClickOKButton()
        {
            OKButton.HoverAndClickWithJS();
        }

        public void ClickCancelButton()
        {
            CancelButton.HoverAndClickWithJS();
        }

        private string GetMessageOnDialog()
        {
            return Message.Text;
        }

        public KeyValuePair<string, bool> ValidateMessageOnDialog(string expectedMessage, bool toNextNRows = true)
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

        private static class Validation
        {
            public static string Message_On_Dialog = "Validate that the content of message on dialog displays correctly";
            public static string Dialog_Opens = "Validate that the dialog opens";
            public static string Dialog_Closes = "Validate that the dialog Closes";
        }

        #endregion
    }
}
