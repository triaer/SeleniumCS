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
using KiewitTeamBinder.Common.Helper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;

namespace KiewitTeamBinder.UI.Pages.Dialogs
{
    public class ConfirmDialog : LoggedInLanding
    {
        #region Entities

        public string _button => "//div[@class='rwDialogPopup radconfirm']//a[.='{0}']";
        public static By _messageDialog => By.XPath("//div[@class='rwDialogPopup radconfirm']//div[@class='rwDialogText']");

        public IWebElement MessageDialog { get { return StableFindElement(_messageDialog); } }
        
        #endregion

        #region Actions
        public ConfirmDialog(IWebDriver webDriver) : base(webDriver)
        {
            //webDriver.SwitchTo().ActiveElement();
            WebDriver.SwitchTo().DefaultContent();
        }

        public T ClickPopupButton<T>(DialogPopupButton name, bool closeBrowser = false, string parentWindowTitle = "Automation Project")
        {
            IWebElement Button = StableFindElement(By.XPath(string.Format(_button, name.ToDescription())));
            if (closeBrowser)
            {
                SwitchToPopUpWindowByTitle(Button, parentWindowTitle);
            }
            else
            {
                Button.ClickOnElement();
                WebDriver.SwitchTo().DefaultContent();
            }
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        private string GetDialogMessage()
        {
            return MessageDialog.Text;
        }

        public List<KeyValuePair<string, bool>> ValidateMessageDialogAsExpected(string expectedMessage)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                string actualContent = GetDialogMessage().Replace(System.Environment.NewLine, string.Empty);
                if (expectedMessage == actualContent)
                    validation.Add(SetPassValidation(node, Validation.Message_On_Dialog + actualContent));
                else
                    validation.Add(SetFailValidation(node, Validation.Message_On_Dialog, expectedMessage, actualContent));

                if (StableFindElement(By.XPath(string.Format(_button, DialogPopupButton.Yes.ToDescription()))) != null)
                    validation.Add(SetPassValidation(node, Validation.Yes_Button_Displays));
                else
                    validation.Add(SetFailValidation(node, Validation.Yes_Button_Displays));
                if (StableFindElement(By.XPath(string.Format(_button, DialogPopupButton.No.ToDescription()))) != null)
                    validation.Add(SetPassValidation(node, Validation.No_Button_Displays));
                else
                    validation.Add(SetFailValidation(node, Validation.No_Button_Displays));


                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Message_On_Dialog, e));
                return validation;
            }
        }



        private static class Validation
        {
            public static string Message_On_Dialog = "Validate The Content Of Message On Dialog Displays Correctly: ";
            public static string Dialog_Opens = "Validate That The Dialog Opens";
            public static string Yes_Button_Displays = "Validate That The Yes Button Displays On Dialog";
            public static string No_Button_Displays = "Validate That The No Button Displays On Dialog";
        }
        #endregion
    }
}
