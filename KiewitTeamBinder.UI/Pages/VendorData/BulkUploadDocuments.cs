using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.UI.ExtentReportsHelper;


namespace KiewitTeamBinder.UI.Pages.VendorData
{
    public class BulkUploadDocuments : HoldingArea
    {
        #region Entities
        private By _addFileInBulk => By.Id("addBulkFlashWrapper");

        private string documentNoHeader = "//th[./span[text()='{0}']]";
        private string _functionButtonPopUp = "//div[contains(@id,'_content')]//span[text()='{0}']";

        private static By _documentNoTextBox => By.XPath("//input[@data-property-name='DocumentNo']");
        private static By _labelMandatoryFieldIcon => By.XPath("//label[@class='MandatoryFields' and contains(@style,'display: inline')][normalize-space(.)='*']");
        private string _menuButtonXpath = "//li[contains(@class,'rtbItem rtbBtn')]//span[text()='{0}']";
        private static By _validateFunctionMessagePopUp => By.XPath("//div[contains(@id,'message')]");
        private static By _validateFunctionPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]");
        private static By _saveFunctionMessagePop => By.XPath("//div[@id='divProgressMessage']//span");

        public IWebElement ValidateMessagePopUp { get { return StableFindElement(_validateFunctionMessagePopUp); } }
        public IWebElement AddFileInBulk { get { return StableFindElement(_addFileInBulk); } }
        public IWebElement SaveFunctionMessagePop { get { return StableFindElement(_saveFunctionMessagePop); } }
        #endregion


        #region Actions
        public BulkUploadDocuments(IWebDriver webDriver) : base(webDriver) { }

        public BulkUploadDocuments ClickAddFilesInBulk()
        {
            AddFileInBulk.Click();
            return this;
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
            StableFindElement(By.XPath(string.Format(_functionButtonPopUp, "OK")))
                .Click();
            return this;
        }

        public BulkUploadDocuments ClickSaveButton()
        {
            IWebElement FunctionButton = StableFindElement(By.XPath(string.Format(_menuButtonXpath, "Save")));
            FunctionButton.Click();

            if (StableFindElement(_processingPopUp) != null)
                WaitForElementAttribute(StableFindElement(_processingPopUp), "display", "none");

            WebDriver.SwitchTo().ActiveElement();
            return this;
        }

        public BulkUploadDocuments ClickNoButtonOnDialogBox()
        {
            StableFindElement(By.XPath(string.Format(_functionButtonPopUp, "No")))
                .Click();
            return this;
        }

        public BulkUploadDocuments ClickCancelButton()
        {
            IWebElement FunctionButton = StableFindElement(By.XPath(string.Format(_menuButtonXpath, "Cancel")));
            FunctionButton.Click();

            WebDriver.SwitchTo().ActiveElement();
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

        public KeyValuePair<string, bool> ValidateButtonIsHighlightedWhenHovered(string valueBtn)
        {
            var node = StepNode();

            try
            {
                IWebElement Button = StableFindElement(By.XPath(string.Format(_menuButtonXpath, valueBtn)));
                ScrollToElement(Button);

                string actualAttribute = Button.GetCssValue("background-color");
                if (actualAttribute.Equals("#e5e5e5"))
                    return SetPassValidation(node, Validation.Button_Is_Highlighted_When_Hovered);

                else
                    return SetFailValidation(node, Validation.Button_Is_Highlighted_When_Hovered, "Color is #e5e5e5 ", actualAttribute);

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Button_Is_Highlighted_When_Hovered, e);
            }
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

        public KeyValuePair<string, bool> ValidateMessageDialogBoxDisplayedForValidateFunction()
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

        public KeyValuePair<string, bool> ValidateMessageDialogBoxDisplayedForSaveAndCancelFunction()
        {
            var node = StepNode();
            string expectMessage = "Document details saved successfully. Do you want to upload more documents?";
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
        public List<KeyValuePair<string, bool>> ValidateButtonDialogBoxDisplayed(string[] valueButton)
        {
            var node = StepNode();
            var validations = new List<KeyValuePair<string, bool>>();

            try
            {
                for (int i = 0; i < valueButton.Length; i++)
                {
                    if (StableFindElement(By.XPath(string.Format(_functionButtonPopUp, valueButton[i]))) != null)
                    {
                        validations.Add(SetPassValidation(node, Validation.Button_DialogBox_Displayed));
                    }
                    else
                        validations.Add(SetFailValidation(node, Validation.Button_DialogBox_Displayed));
                }
            }
            catch (Exception e)
            {
                validations.Add(SetErrorValidation(node, Validation.Button_DialogBox_Displayed, e));
            }
            return validations;
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
            public static string Button_DialogBox_Displayed = "Validate That The Button Is Displayed.";
            public static string Validate_Function_DialogBix_Closed = "Validate That The DialogBox Is Closed.";
            public static string Button_Is_Highlighted_When_Hovered = "Validate That Button Is Highlighted When Hovered";
        }
    }
}
