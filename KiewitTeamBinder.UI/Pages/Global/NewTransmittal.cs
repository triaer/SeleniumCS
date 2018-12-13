using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.Common;
using OpenQA.Selenium.Support.UI;
using KiewitTeamBinder.Common.Helper;
using System.Windows.Forms;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.UI.Pages.VendorDataModule;


namespace KiewitTeamBinder.UI.Pages.Global
{
    public class NewTransmittal : ProjectsDashboard
    {
        #region Entities
        private static By _iframeMessage => By.XPath("//iframe[contains(@id,'RadEditorMessage_contentIframe')]");
        private static By _recipientsButton(string buttonName) => By.XPath($"//a[input[contains(@id, '{buttonName}')]]");
        private static By _paneTable(string gridViewName) => By.XPath($"//table[contains(@id, '{gridViewName}_ctl00')]/thead");
        private static By _selectedUsersInToField => By.XPath("//tr[@id = 'trTo']//li[contains(@id, 'bit')]");
        private static By _selectedUsersInCcField => By.XPath("//tr[@id = 'trCc']//li[contains(@id, 'bit')]");
        private static By _subjectTextField => By.Id("txtSubject");
        private static By _messageTextField => By.XPath("//body[@spellcheck]");
        private static By _linkDocumentExpandIcon => By.Id("LinkDocumentExpandIcon");

        private static string _filterItemsXpath = "//tr[@valign='top']";

        public IWebElement IframeMessage { get { return StableFindElement(_iframeMessage); } }
        public IWebElement RecipientsButton(string buttonName) => StableFindElement(_recipientsButton(buttonName));
        public new IWebElement PaneTable(string gridViewName) => StableFindElement(_paneTable(gridViewName));
        public IReadOnlyCollection<IWebElement> SelectedUsersInToField { get { return StableFindElements(_selectedUsersInToField); } }
        public IReadOnlyCollection<IWebElement> SelectedUsersInCcField { get { return StableFindElements(_selectedUsersInCcField); } }
        public IWebElement SubjectTextField { get { return StableFindElement(_subjectTextField); } }
        public IWebElement MessageTextField { get { return StableFindElement(_messageTextField); } }
        public IWebElement LinkDocumentExpandIcon { get { return StableFindElement(_linkDocumentExpandIcon); } }
        #endregion

        #region Actions
        public NewTransmittal(IWebDriver webDriver) : base(webDriver) { }

        public SelectRecipientsDialog ClickRecipientsButton(string buttonName)
        {
            RecipientsButton(buttonName).Click();

            var selectRecipientsDialog = new SelectRecipientsDialog(WebDriver);
            WebDriver.SwitchTo().Frame(selectRecipientsDialog.IFrameName);
            WaitUntil(driver => selectRecipientsDialog.DropdownFilterButon != null);

            return selectRecipientsDialog;
        }

        public int GetTableItemNumberWithConditions(string gridViewName, string selectedDocuments)
        {
            IReadOnlyCollection<IWebElement> AvailableItems = GetAvailableItems(gridViewName, selectedDocuments);
            try
            {
                return AvailableItems.Count;
            }
            catch
            {
                return 0;
            }
        }

        public NewTransmittal EnterSubject(string subject)
        {
            SubjectTextField.InputText(subject);
            
            return this;
        }

        public NewTransmittal EnterMessage(string message)
        {            
            WebDriver.SwitchTo().Frame(IframeMessage);
            WaitUntil(driver => this.MessageTextField != null);
            MessageTextField.InputText(message);
            WebDriver.SwitchTo().DefaultContent();
            return this;
        }

        public TransmittalDetail ClickSendButton(ref List<KeyValuePair<string, bool>> methodValidation)
        {
            //string window;
            //SwitchToPopUpWindow(ToolbarButton(KiewitTeamBinderENums.ToolbarButton.Send.ToDescription()), out window, true);
            ToolBarButton(KiewitTeamBinderENums.ToolbarButton.Send.ToDescription()).Click();
            methodValidation.Add(ValidateProgressContentMessage("Please wait while transmittal is being sent"));
            return new TransmittalDetail(WebDriver);
        }

        private IReadOnlyCollection<IWebElement> GetAvailableItems(string gridViewName, string selectedDocument)
        {
            int rowIndex, colIndex = 1;
            
            GetTableCellValueIndex(PaneTable(gridViewName), "Document No.", out rowIndex, out colIndex, "th");
            if (colIndex < 2)
                return null;

            string itemsXpath = _filterItemsXpath + $"[td[{colIndex}][contains(., '{selectedDocument}')]]";

            return StableFindElements(By.XPath(itemsXpath));
        }

        public KeyValuePair<string, bool> ValidateAllSelectedDocumentsAreListed(ref string[] selectedDocuments)
        {
            var node = StepNode();
            try
            {
                WaitForElementAttribute(LinkDocumentExpandIcon, "class", "ImageCollapse");

                if (GetTableItemNumber("GridViewDocuments") != selectedDocuments.Length)
                    return SetFailValidation(node, Validation.All_Selected_Documents_Are_Listed);
                
                for (int i = 0; i < selectedDocuments.Length; i++)
                {
                    if (GetTableItemNumberWithConditions("GridViewDocuments", selectedDocuments[i].Trim()) < 1)
                        return SetFailValidation(node, Validation.All_Selected_Documents_Are_Listed);
                }
                return SetPassValidation(node, Validation.All_Selected_Documents_Are_Listed);
               
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.All_Selected_Documents_Are_Listed, e);
            }
        }

        public KeyValuePair<string, bool> ValidateSelectedUsersPopulateInTheToField(string[] selectedUsers)
        {
            var node = StepNode();
            try
            {
                bool flag;
                foreach (var selectedUser in SelectedUsersInToField)
                {
                    flag = false;
                    for (int i = 0; i < selectedUsers.Length; i++)
                    {
                        if (selectedUsers[i] == selectedUser.Text)
                            flag = true;
                    }
                    if (flag == false)                    
                        return SetFailValidation(node, Validation.Selected_Users_Populate_In_The_To_Field);
                    
                }
                return SetPassValidation(node, Validation.Selected_Users_Populate_In_The_To_Field);

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Selected_Users_Populate_In_The_To_Field, e);
            }
        }

        private static class Validation
        {
            public static string All_Selected_Documents_Are_Listed = "Validate that all selected documents are listed";
            public static string Selected_Users_Populate_In_The_To_Field = "Validate that Selected users populate in the To field";
        }
        #endregion
    }
}
