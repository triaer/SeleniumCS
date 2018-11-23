using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.UI.KiewitTeamBinderENums;
using OpenQA.Selenium.Support.UI;
using KiewitTeamBinder.Common.Helper;
using System.Windows.Forms;
using KiewitTeamBinder.UI.Pages.Dialogs;


namespace KiewitTeamBinder.UI.Pages.Global
{
    public class BulkUploadDocuments : ProjectsDashboard
    {
        #region Entities
        private static By _addFileInBulkButton => By.Id("addBulkFlashWrapper");
        private static By _selectAllCheckbox => By.XPath("//th//input[contains(@id, 'ClientSelectColumnSelectCheckBox')]");
        private static By _selectCheckboxes => By.XPath("//td//input[contains(@id,'ClientSelectColumnSelectCheckBox')]");
        private static By _bulkUploadDocumentsTable => By.XPath("//div[@id='RadGrid1_GridData']//*[contains(@class, 'rgMasterTable')]");
        private static By _addFilesInBulkButton => By.XPath("//*[@id='flashUploadBulk']");
        private static By _fileNames => By.XPath("//tr[contains(@id,'ViewFiles')]");
        private static By _tableHearder => By.Id("RadGrid1_ctl00_Header");
        private static By _allDocumentRows => By.XPath("//tr[@valign='top']");
        private static By _allRowCheckboxes => By.XPath("//input[contains(@name, 'ClientSelectColumnSelectCheckBox')]");
        private static By _allSupersededCheckboxes => By.XPath("//input[contains(@name, 'chkSuperseded')]");
        private static By _allCopyAttributesItems => By.XPath("//div[@id='RadContextMenu1_detached']/div[contains(@class,'rmScrollWrap')]//li");
        private static By _documentNoTextBox => By.XPath("//input[@data-property-name='DocumentNo']");
        private static By _labelMandatoryFieldIcon => By.XPath("//label[@class='MandatoryFields' and contains(@style,'display: inline')][normalize-space(.)='*']");
        private static By _validateFunctionMessagePopUp => By.XPath("//div[contains(@id,'message')]");
        private static By _validateFunctionPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]");
        private static By _saveFunctionMessagePop => By.XPath("//div[@id='divProgressMessage']//span");


        public string _allComboBoxes = "//select[@data-property-name='{0}']";
        public string _documentDetailsTextbox = "//td//*[@data-property-name='{0}']";
        public string _headerButton = "//a[span='{0}']";
        public string _toNRows = "//*[@id='RadContextMenu1_detached']/div[{0}]//a[span= '{1}']";
        
        public IWebElement AddFileInBulkButton { get { return StableFindElement(_addFileInBulkButton); } }
        public IWebElement SelectAllCheckbox { get { return StableFindElement(_selectAllCheckbox); } }
        public IReadOnlyCollection<IWebElement> SelectCheckboxes { get { return StableFindElements(_selectCheckboxes); } }
        public IWebElement BulkUploadDocumentsTable { get { return StableFindElement(_bulkUploadDocumentsTable); } }
        public IWebElement TableHeader { get { return StableFindElement(_tableHearder); } }
        public IReadOnlyCollection<IWebElement> FileNames { get { return StableFindElements(_fileNames); } }
        public IWebElement AddFilesInBulkButton { get { return StableFindElement(_addFilesInBulkButton); } }
        public IReadOnlyCollection<IWebElement> AllItemsInCopyAttributesDropdown { get { return StableFindElements(_allCopyAttributesItems); } }
        public IReadOnlyCollection<IWebElement> AllDocumentRows { get { return StableFindElements(_allDocumentRows); } }
        public IReadOnlyCollection<IWebElement> AllRowCheckboxes { get { return StableFindElements(_allRowCheckboxes); } }
        public IReadOnlyCollection<IWebElement> AllSupersededCheckboxes { get { return StableFindElements(_allSupersededCheckboxes); } }
        public IWebElement ValidateMessagePopUp { get { return StableFindElement(_validateFunctionMessagePopUp); } }
        public IWebElement SaveFunctionMessagePop { get { return StableFindElement(_saveFunctionMessagePop); } }
		#endregion


        #region Actions
        public BulkUploadDocuments(IWebDriver webDriver) : base(webDriver) { }

        private IReadOnlyCollection<SelectElement> GetAllComboBoxes(string comboBoxName)
        {
            IReadOnlyCollection<IWebElement> AllComboBoxes = StableFindElements(By.XPath(string.Format(_allComboBoxes, 
                                                                                                       comboBoxName)));
            Collection<SelectElement> AllSelectComboBoxes = new Collection<SelectElement>();
            for (int i = 0; i < AllComboBoxes.Count; i++)
            {
                var t = new SelectElement(AllComboBoxes.ElementAt(i));
                AllSelectComboBoxes.Add(t);
            }

            return AllSelectComboBoxes;
        }


        // We have to use hard code waiting time cause this is Windows native control
        // In order to type in multi-file names we need to separate the filePath and fileNames
        // filePath = "C:\Working", fileNames = {\"File1.txt\" \"File2.txt\" \"File3.txt\...."}
        public BulkUploadDocuments AddFilesInBulk(string filePath, string fileNames)
        {
            var node = StepNode();
            node.Info("Click Add Files In Bulk button");
            AddFilesInBulkButton.Click();
            node.Info("Choose files from window explorer form");
            Wait(shortTimeout/2);
            SendKeys.SendWait(@filePath);
            SendKeys.SendWait(@"{Enter}");
            Wait(shortTimeout/2);
            SendKeys.SendWait(@fileNames);
            Wait(shortTimeout/3);
            SendKeys.SendWait(@"{Enter}");
            Wait(shortTimeout/3);
            
            return this;
        }

        public BulkUploadDocuments SelectAllCheckboxes(bool selectOption)
        {
            if (selectOption)
                SelectAllCheckbox.Check();

            SelectAllCheckbox.UnCheck();

            return this;
        }

        private bool IsRowSelected(int rowIndex)
        {
            IWebElement row = AllDocumentRows.ElementAt(rowIndex);
            if (row.GetAttribute("class").Contains("rgSelectedRow"))
                return true;
            return false;
        }


        // checkboxType is "rowCheckbox" or "Superseded"
        public BulkUploadDocuments SelectTableCheckbox(int rowIndex, string selectOption = "on", string checkboxType = "rowCheckbox")
        {
            var node = StepNode();
            IWebElement checkbox;
            if (checkboxType == "rowCheckbox")
            {               
                checkbox = AllRowCheckboxes.ElementAt(rowIndex);
            }
            else
            {
                rowIndex = Utils.RefactorIndex(rowIndex);
                checkbox = AllSupersededCheckboxes.ElementAt(rowIndex);
            }
            if (String.IsNullOrWhiteSpace(selectOption)
                || (selectOption.ToLower() != "on" && selectOption.ToLower() != "off"))
                throw new InvalidOperationException("select option should be 'on' of 'off'. Default value is 'on'");
            else if (selectOption.ToLower() == "on")
            {
                node.Info("Check the checkbox: " + checkboxType);
                checkbox.Check();
            }
            else
            {
                node.Info("Uncheck the checkbox: " + checkboxType);
                checkbox.UnCheck();
            }
            return this;
        }

        public BulkUploadDocuments SelectDataOfDocumentPropertyDropdown(int rowIndex, string selectItem, DocBulkUploadDropdownType comboBoxType)
        {
            rowIndex = Utils.RefactorIndex(rowIndex);
            var comboBox = GetAllComboBoxes(comboBoxType.ToDescription()).ElementAt(rowIndex);
            var node = StepNode();
            node.Info("Select " + selectItem);
            comboBox.SelectByText(selectItem);
            return this;
        }

        public BulkUploadDocuments EnterDataOfDocumentPropertyTextbox(int rowIndex, string content, string textboxName)
        {
            rowIndex = Utils.RefactorIndex(rowIndex);
            IReadOnlyCollection<IWebElement> DocumentDetailsTextbox 
                = StableFindElements(By.XPath(string.Format(_documentDetailsTextbox, textboxName)));
            DocumentDetailsTextbox.ElementAt(rowIndex).InputText(content);
            return this;
        }
                
        public T ClickHeaderButton<T>(DocBulkUploadHeaderButton buttonName)
        {
            IWebElement Button = StableFindElement(By.XPath(string.Format(_headerButton, buttonName.ToDescription())));
            var node = StepNode();            
            node.Info("Click the button: " + buttonName.ToDescription());
            Button.HoverAndClickWithJS();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }            

        public BulkUploadDocuments HoverOnCopyAttributesMainItem(string itemName, ref int index)
        {
            IWebElement item;
            index = -1;
            do
            {
                index++;
                item = AllItemsInCopyAttributesDropdown.ElementAt(index);                
            }
            while (item.Text != itemName);
            var node = StepNode();
            node.Info("Hover " + itemName);
            item.HoverWithJS();
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
        public KeyValuePair<string, bool> ValidateFilesDisplay(int numberOfFiles)
        {
            var node = StepNode();

            try
            {
                if (FileNames.Count == numberOfFiles)
                    return SetPassValidation(node, Validation.Validate_Files_Display);

                return SetFailValidation(node, Validation.Validate_Files_Display);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Validate_Files_Display, e);
            }
        }
        //TO-DO: Currently, the document file names are shown under the "Document No." instead of the "Version" column.
        // We need confirmation from the test design team.
        public KeyValuePair<string, bool> ValidateFileNamesAreListedInColumn(string columnName)
        {
            var node = StepNode();
            int rowIndex;
            int colIndex;
            try
            {
                GetTableCellValueIndex(TableHeader, columnName, out rowIndex, out colIndex, "th");
                if (colIndex == 5)
                    return SetPassValidation(node, string.Format(Validation.Validat_File_Names_Are_Listed_In_Column, columnName));

                return SetFailValidation(node, string.Format(Validation.Validat_File_Names_Are_Listed_In_Column, columnName));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Validat_File_Names_Are_Listed_In_Column, columnName), e);
            }
        }

        // Set 'checkSelected' equals to 'false' if desire to check all rows are deselected
        // Need to divide the totalRows because there are 2 rows for each file: 1 file name row and other is data row.
        public KeyValuePair<string, bool> ValidateAllRowsAreSelected(bool checkSelected = true)
        {
            var node = StepNode();
            List<bool> validations = new List<bool> { };
            List<int> rowNumbers = new List<int> { };
            int totalRows = AllDocumentRows.Count;
            try
            {
                for (int i = 0; i < totalRows; i++)
                {
                    if (IsRowSelected(i) == checkSelected)
                        validations.Add(true);
                    else
                    {
                        validations.Add(false);
                        rowNumbers.Add(i + 1);
                    }
                }
                if (checkSelected)
                {
                    if (validations.TrueForAll(allSelected => allSelected))
                        return SetPassValidation(node, Validation.All_Rows_Are_Selected);

                    return SetFailValidation(node,
                        Validation.Not_All_Rows_Are_Selected
                        + String.Format($"Rows {string.Join(",", rowNumbers)} are not selected."));
                }
                else
                {
                    if (validations.TrueForAll(allDeselected => allDeselected))
                        return SetPassValidation(node, Validation.All_Rows_Are_DeSelected);

                    return SetFailValidation(node,
                        Validation.Not_All_Rows_Are_DeSelected
                        + String.Format($"Rows {string.Join(",", rowNumbers)} are not Deselected."));
                }
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Cannot_Validate_Rows_State, e);
            }
        }

        public KeyValuePair<string, bool> ValidateRowIsSelected(int rowIndex, bool checkSelected = true)
        {
            rowIndex = Utils.RefactorIndex(rowIndex);
            var node = StepNode();
            try
            {
                if (IsRowSelected(rowIndex - 1) == checkSelected)
                    return SetPassValidation(node, string.Format(Validation.Row_Is_Selected, rowIndex - 1));
                else
                    return SetFailValidation(node, string.Format(Validation.Row_Is_Selected, rowIndex - 1));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Row_Is_Selected, rowIndex - 1), e);
            }
        }

        public KeyValuePair<string, bool> ValidateSubMenuDisplaysAfterHovering(ref int indexOfSubmenu)
        {
            var node = StepNode();
            IWebElement submenu = StableFindElement(By.XPath($"//*[@id='RadContextMenu1_detached']/div[{indexOfSubmenu + 2}]"));
            try
            {
                if (submenu.GetAttribute("style").Contains("display: block"))
                    return SetPassValidation(node, Validation.Submenu_Displays);
                else
                    return SetFailValidation(node, Validation.Submenu_Displays);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Submenu_Displays, e);
            }
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
        private static class Validation
        {
            public static string Holding_Area_Page_Displays = "Validate That The Holding Area Page Displays";
            public static string All_Rows_Are_Selected = "Validate That All Rows Are Selected";
            public static string Not_All_Rows_Are_Selected = "Validate That All Rows Are Not Selected";
            public static string All_Rows_Are_DeSelected = "Validate That All Rows Are DeSelected";
            public static string Not_All_Rows_Are_DeSelected = "Validate That All Rows Are DeSelected";
            public static string Cannot_Validate_Rows_State = "Error Cannot Validate Rows State";
            public static string Validate_Files_Display = "Validate that 15 files display";
            public static string Validat_File_Names_Are_Listed_In_Column = "Validat File names are listed in {0} column";
            public static string Row_Is_Selected = "Validate that row {0} is selected";
            public static string Submenu_Displays = "Validate that Submenu displays after hovering";
			
			public static string Column_Marked_Mandatory_Field = "Validate that column is marked Mandatory with red astrisk - Column Name:";
            public static string Message_DialogBox_Display = "Validate That Dialog box opens displaying ";
            public static string Button_DialogBox_Displayed = "Validate That The Button Is Displayed.";
            public static string Validate_Function_DialogBix_Closed = "Validate That The DialogBox Is Closed.";
            public static string Button_Is_Highlighted_When_Hovered = "Validate That Button Is Highlighted When Hovered";

        }
        #endregion
    }
}
