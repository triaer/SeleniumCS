using KiewitTeamBinder.Common.Models.VendorData;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.PopupWindows;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class ProcessDocuments : PopupWindow
    {
        #region Entities
        private static By _selectedDropdown(string nameHeader, string value) => By.XPath($"//select[@data-property-name = '{nameHeader}']/option[starts-with(text(),'{value}')]");
        private static By _documentNoTextBox => By.XPath("//input[@data-property-name = 'DocumentNo']");
        private static By _titleTextBox => By.XPath("//textarea[@data-property-name = 'Title']");

        public IWebElement SelectedDropdown(string nameHeader, string value) => StableFindElement(_selectedDropdown(nameHeader, value));
        public IWebElement TitleTextBox { get { return StableFindElement(_titleTextBox); } }
        public IWebElement DocumentNoTextBox { get { return StableFindElement(_documentNoTextBox); } }
        #endregion

        #region Actions
        public ProcessDocuments(IWebDriver webDriver) : base(webDriver) { }

        public DocumentReceivedDateDialog ClickProcessDocumentDetails(ToolbarButton buttonName)
        {
            var dialog = ClickToolbarButtonOnWinPopup<DocumentReceivedDateDialog>(buttonName, true);
            WebDriver.SwitchTo().Frame(dialog.Iframe);
            return dialog;
        }

        public List<KeyValuePair<string, bool>> ValidateDocumentDetailDisplayCorrect(SingleDocumentInfo singleDocInformation, string[] ListHeader)
        {
            var node = StepNode();
            var validations = new List<KeyValuePair<string, bool>>();
            string[] optionDropdown = { singleDocInformation.RevStatus, singleDocInformation.Status, singleDocInformation.Discipline, singleDocInformation.Category };

            try
            {
                node.Info("Document No. : " + singleDocInformation.DocumentNo);
                if (DocumentNoTextBox.GetValue() == singleDocInformation.DocumentNo)
                    validations.Add(SetPassValidation(node, Validation.Document_Detail_Display_Correct));
                else
                    validations.Add(SetFailValidation(node, Validation.Document_Detail_Display_Correct, singleDocInformation.DocumentNo, DocumentNoTextBox.GetValue()));

                node.Info("Title : " + singleDocInformation.Title);
                if (TitleTextBox.Text == singleDocInformation.Title)
                    validations.Add(SetPassValidation(node, Validation.Document_Detail_Display_Correct));
                else
                    validations.Add(SetFailValidation(node, Validation.Document_Detail_Display_Correct, singleDocInformation.Title, TitleTextBox.Text));

                for (int item = 0; item < optionDropdown.Length; item++)
                {
                    node.Info("Dropdown : " + ListHeader[item] + " shows value: " + optionDropdown[item]);
                    if (SelectedDropdown(ListHeader[item], optionDropdown[item]) != null)
                        validations.Add(SetPassValidation(node, Validation.Document_Detail_Display_Correct));
                    else
                        validations.Add(SetFailValidation(node, Validation.Document_Detail_Display_Correct));
                }

                return validations;
            }
            catch (Exception e)
            {
                validations.Add(SetErrorValidation(node, Validation.Document_Detail_Display_Correct, e));
                return validations;
            }
        }

        public int GetCountWindow()
        {
            var node = StepNode();
            node.Info("count window: " + WebDriver.WindowHandles.Count);
            return WebDriver.WindowHandles.Count;
        }

        public KeyValuePair<string, bool> ValidateProcessDocumentlWindowIsClosed(int countWindow)
        {
            var node = StepNode();
            try
            {
                if (WebDriver.WindowHandles.Count == countWindow - 1)
                    return SetPassValidation(node, Validation.Process_Document_Window_Is_Closed);
                else
                    return SetFailValidation(node, Validation.Process_Document_Window_Is_Closed);
            } 
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Process_Document_Window_Is_Closed, e);
            }

        }
        #endregion

        private static class Validation
        {
            public static string Document_Detail_Display_Correct = "Validate that the document detail is diplayed correctly";
            public static string Process_Document_Window_Is_Closed = "Validate that the process document window is closed";
        }
    }
}
