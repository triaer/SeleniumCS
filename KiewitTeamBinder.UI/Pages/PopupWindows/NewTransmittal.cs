using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.Common;
using OpenQA.Selenium.Support.UI;
using KiewitTeamBinder.Common.Helper;
using System.Windows.Forms;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.UI.Pages.VendorDataModule;


namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class NewTransmittal : PopupWindow
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
        private static By _reasonForIssueDropdown => By.XPath("//a[contains(@id, 'cboReason_Arrow')]");
        private static By _reasonForIssueData => By.XPath("//div[contains(@id,'Reason_DropDown')]//ul/li");
        private static By _respondByMessageDropdown => By.XPath("//a[contains(@id, 'cboRespondMessage_Arrow')]");
        private static By _respondByMessageData => By.XPath("//div[contains(@id,'RespondMessage_DropDown')]//ul/li");
        private static By _respondByDateIcon => By.XPath("//a[contains(@id, 'RespondByDate_popupButton')]");
        private static By _respondByDateData => By.XPath("//div[contains(@id, 'RespondByDate_calendar_wrapper')]");

        private static string _filterItemsXpath = "//tr[@valign='top' and not(contains(@style, 'hidden'))]";

        public IWebElement IframeMessage { get { return StableFindElement(_iframeMessage); } }
        public IWebElement RecipientsButton(string buttonName) => StableFindElement(_recipientsButton(buttonName));
        public new IWebElement PaneTable(string gridViewName) => StableFindElement(_paneTable(gridViewName));
        public IReadOnlyCollection<IWebElement> SelectedUsersInToField { get { return StableFindElements(_selectedUsersInToField); } }
        public IReadOnlyCollection<IWebElement> SelectedUsersInCcField { get { return StableFindElements(_selectedUsersInCcField); } }
        public IWebElement SubjectTextField { get { return StableFindElement(_subjectTextField); } }
        public IWebElement MessageTextField { get { return StableFindElement(_messageTextField); } }
        public IWebElement LinkDocumentExpandIcon { get { return StableFindElement(_linkDocumentExpandIcon); } }
        public IWebElement ReasonForIssueDropdown { get { return StableFindElement(_reasonForIssueDropdown); } }
        public IWebElement RespondByMessageDropdown { get { return StableFindElement(_respondByMessageDropdown); } }
        public IWebElement RespondByDateIcon { get { return StableFindElement(_respondByDateIcon); } }
        #endregion

        #region Actions
        public NewTransmittal(IWebDriver webDriver) : base(webDriver) { }

        public SelectRecipientsDialog ClickRecipientsButton(string buttonName, bool switchToFrame = true)
        {
            RecipientsButton(buttonName).Click();

            var selectRecipientsDialog = new SelectRecipientsDialog(WebDriver);
            if (switchToFrame)
            {
                WebDriver.SwitchTo().Frame(selectRecipientsDialog.IFrameName);
                WaitUntil(driver => selectRecipientsDialog.DropdownFilterButon != null);
            }
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
            MessageTextField.SendKeys(OpenQA.Selenium.Keys.Tab);
            WebDriver.SwitchTo().DefaultContent();
            return this;
        }

        public NewTransmittal SelectNewTransmitDetailInforByText(string reasonForIssue, string respondByMessage, string respondByDate)
        {
            if (reasonForIssue != "")
                SelectComboboxByText(ReasonForIssueDropdown, _reasonForIssueData, reasonForIssue);

            if (respondByMessage != "")
                SelectComboboxByText(RespondByMessageDropdown, _respondByMessageData, respondByMessage);

            if (respondByDate != "")
                SelectDateOnCalendar(respondByDate, RespondByDateIcon, _respondByDateData);

            return this;
        }

        public TransmittalDetail ClickSendButton(ref List<KeyValuePair<string, bool>> methodValidation)
        {            
            ClickToolbarButton<TransmittalDetail>(KiewitTeamBinderENums.ToolbarButton.Send);
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

                if (GetTotalRowsVisibleInGrid("GridViewDocuments") != selectedDocuments.Length)
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

        public List<KeyValuePair<string, bool>> ValidateSelectedUsersPopulateInTheToField(string[] selectedUsers, string companyName = "")
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                bool flag;
                if (companyName != "")
                    for (int itemUser = 0; itemUser < selectedUsers.Length; itemUser++)
                        selectedUsers[itemUser] = selectedUsers[itemUser] + " (" + companyName + ")";
                 
                foreach (var selectedUser in SelectedUsersInToField)
                {
                    flag = false;
                    for (int i = 0; i < selectedUsers.Length; i++)
                    {
                        if (selectedUsers[i] == selectedUser.Text)
                        {
                            flag = true;
                            break;
                        }                           
                    }
                    if (flag == false)
                        validation.Add(SetFailValidation(node, Validation.Selected_Users_Populate_In_The_To_Field));
                    else
                        validation.Add(SetPassValidation(node, Validation.Selected_Users_Populate_In_The_To_Field));
                }                
                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Selected_Users_Populate_In_The_To_Field, e));
                return validation;
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
