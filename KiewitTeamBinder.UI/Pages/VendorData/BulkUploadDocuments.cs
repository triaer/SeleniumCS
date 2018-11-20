using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.VendorData
{
    public class BulkUploadDocuments : HoldingArea
    {
        #region Entities
        private string documentNoHeader = "//th[./span[text()='{0}']]";
        private static By _documentNoTextBox => By.XPath("//input[@data-property-name='DocumentNo']");
        private static By _labelMandatoryFieldIcon => By.XPath("//label[@class='MandatoryFields' and contains(@style,'display: inline')][normalize-space(.)='*']");
        private string _menuButtonXpath = "//li[contains(@class,'rtbItem rtbBtn')]//span[text()='{0}']";
        private static By _validateFunctionOkButton => By.XPath("//div[contains(@id,'_content')]//span[text()='OK']");
        private static By _validateFunctionMessagePopUp => By.XPath("//div[contains(@id,'message')]");
        private static By _validateFunctionPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]");
        

        public IWebElement ValidateMessagePopUp { get { return StableFindElement(_validateFunctionMessagePopUp); } }
        public IWebElement AddFileInBulk { get { return StableFindElement(_addFileInBulk); } }
        #endregion


        #region Actions
        public BulkUploadDocuments(IWebDriver webDriver) : base(webDriver)
        {
        }

        public BulkUploadDocuments ClickValidateButton()
        {
            IWebElement FunctionButton = StableFindElement(By.XPath(string.Format(_menuButtonXpath, "Validate")));
            FunctionButton.Click();

            if (StableFindElement(_processingPopUp) != null)
                WaitForElementAttribute(StableFindElement(_processingPopUp), "display", "none");

            WebDriver.SwitchTo().ActiveElement();
            return this;
        }

        public BulkUploadDocuments ClickOkButtonOnDialogBox()
        {
            StableFindElement(_validateFunctionOkButton).Click();
            return this;
        }

        public BulkUploadDocuments EnterDocumentNoForAllRecords(string documentNo)
        {
            List<IWebElement> documentNoList = StableFindElements(_documentNoTextBox).ToList();
            int i = 1;
            foreach (IWebElement documentTextbox in documentNoList)
            {
                documentTextbox.InputText(documentNo + "_" + i);
                i++;
            }

            return this;
        }

        public KeyValuePair<string, bool> ValidateColumnIsMandatoryField(string columnName)
        {
            var node = StepNode();
            try
            {
                IWebElement mandatorySign = StableFindElement(By.XPath(string.Format(documentNoHeader, columnName)))
                                        .StableFindElement(_labelMandatoryFieldIcon);
                if (mandatorySign != null)
                    return SetPassValidation(node, Validation.Column_Marked_Mandatory_Field + columnName);
                else
                    return SetFailValidation(node, Validation.Column_Marked_Mandatory_Field + columnName);
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, Validation.Column_Marked_Mandatory_Field, e); ;
            }

        }

        public KeyValuePair<string, bool> ValidateMessageDialogBoxDisplayed()
        {
            var node = StepNode();
            string expectMessage = "Document details are successfully validated.";
            try
            {
                if (ValidateMessagePopUp.Text.Trim() == expectMessage || ValidateMessagePopUp.GetAttribute("innerHTML").Trim() == expectMessage)
                {
                    return SetPassValidation(node, Validation.Message_DialogBox_Display + expectMessage);
                }
                else
                    return SetFailValidation(node, Validation.Message_DialogBox_Display + expectMessage, expectMessage, ValidateMessagePopUp.Text);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Message_DialogBox_Display + expectMessage, e); ;
            }
        }

        public KeyValuePair<string, bool> ValidateOkButtonDialogBoxDisplayed()
        {
            var node = StepNode();
            try
            {
                if (StableFindElement(_validateFunctionOkButton) != null)
                {
                    return SetPassValidation(node, Validation.Ok_Button_DialogBox_Display);
                }
                else
                    return SetFailValidation(node, Validation.Ok_Button_DialogBox_Display);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Ok_Button_DialogBox_Display, e); ;
            }
        }

        public KeyValuePair<string, bool> ValidateYesAndNoButtonDialogBoxDisplayed()
        {
            var node = StepNode();
            try
            {
                if (StableFindElement(_validateFunctionOkButton) != null)
                {
                    return SetPassValidation(node, Validation.Ok_Button_DialogBox_Display);
                }
                else
                    return SetFailValidation(node, Validation.Ok_Button_DialogBox_Display);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Ok_Button_DialogBox_Display, e); ;
            }
        }
        public KeyValuePair<string, bool> ValidateDialogBoxClosed()
        {
            var node = StepNode();
            try
            {
                if (StableFindElement(_validateFunctionPopUp) == null)
                {
                    return SetPassValidation(node, Validation.Validate_Function_DialogBix_Closed);
                }
                else
                    return SetFailValidation(node, Validation.Validate_Function_DialogBix_Closed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Validate_Function_DialogBix_Closed, e); ;
            }
        }

        #endregion
        private static class Validation
        {
            public static string Column_Marked_Mandatory_Field = "Validate that column is marked Mandatory with red astrisk - Column Name:";
            public static string Message_DialogBox_Display = "Validate That Dialog box opens displaying ";
            public static string Ok_Button_DialogBox_Display = "Validate That The Ok Button Is Displayed.";
            public static string Validate_Function_DialogBix_Closed = "Validate That The DialogBox Is Closed.";
        }
    }
}
