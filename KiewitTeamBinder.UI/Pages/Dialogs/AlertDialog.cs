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

        public List<KeyValuePair<string, bool>> ValidateMessageDialogAsExpected(string expectedMessage)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                string actualContent = GetDialogMessage();
                if (expectedMessage == actualContent)
                    validation.Add(SetPassValidation(node, Validation.Message_On_Dialog + actualContent));
                else
                    validation.Add(SetFailValidation(node, Validation.Message_On_Dialog, expectedMessage, actualContent));
                if (StableFindElement(_oKButton) != null)
                    validation.Add(SetPassValidation(node, Validation.OK_Button_Displays));
                else
                    validation.Add(SetFailValidation(node, Validation.OK_Button_Displays));

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
            public static string OK_Button_Displays = "Validate That The OK Button Displays On Dialog";
            public static string Dialog_Opens = "Validate That The Dialog Opens";
        }
        #endregion
    }
}
