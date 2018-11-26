﻿using System;
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

        private string _allComboBoxes = "//select[@data-property-name='{0}']";
        private string _documentDetailsTextbox = "//td//*[@data-property-name='{0}']";
        private string _headerButton = "//a[span='{0}']";
        private string _toNRows = "//*[@id='RadContextMenu1_detached']/div[{0}]//a[span= '{1}']";
        
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
        
        #endregion


        #region Actions
        public BulkUploadDocuments(IWebDriver webDriver) : base(webDriver) { }
        
        public IReadOnlyCollection<SelectElement> GetAllComboBoxes(string comboBoxName)
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
            node.Info("Files name: " + fileNames);
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

        public BulkUploadDocuments ClickACheckboxInDocumentRow(int rowIndex, bool checkedState = true, string checkboxType = "rowCheckbox")
        {
            var node = StepNode();
            IWebElement checkbox;
            if (checkboxType == "rowCheckbox")
            {
                checkbox = AllRowCheckboxes.ElementAt(rowIndex);
            }
            else
            {
                documentRow = Utils.RefactorIndex(documentRow);
                checkbox = AllSupersededCheckboxes.ElementAt(documentRow);
            }
            if (checkedState)
            {
                node.Info($"Check The Checkbox At The Row {documentRow} With Type: " + checkboxType);
                checkbox.Check();
            }
            else
            {
                node.Info($"UnCheck The Checkbox At The Row {documentRow} With Type" + checkboxType);
                checkbox.UnCheck();
            }
            return this;
        }

        public BulkUploadDocuments SelectDataOfDocumentPropertyDropdown(string selectItem, DocBulkUploadDropdownType comboBoxType, int documentRow)
        {
            documentRow = Utils.RefactorIndex(documentRow);
            var comboBox = GetAllComboBoxes(comboBoxType.ToDescription()).ElementAt(documentRow);
            var node = StepNode();
            node.Info($"Select {comboBoxType.ToDescription()} Dropdown With Item '{selectItem}' On The Document Row {documentRow + 1} " );
            comboBox.SelectByText(selectItem);
            return this;
        }

        public BulkUploadDocuments EnterDataOfDocumentPropertyTextbox(string content, string textboxName, int documentRow)
        {
            var node = StepNode();
            node.Info($"Enter {textboxName} With Data '{content}' On The Document Row {documentRow} ");
            documentRow = Utils.RefactorIndex(documentRow);
            IReadOnlyCollection<IWebElement> DocumentDetailsTextbox 
                = StableFindElements(By.XPath(string.Format(_documentDetailsTextbox, textboxName)));
            DocumentDetailsTextbox.ElementAt(documentRow).InputText(content);
            return this;
        }
                
        public BulkUploadDocuments EnterTextboxes(string content, string textboxName, bool unique = true)
        {
            var node = StepNode();
            node.Info($"Enter {textboxName} With Data '{content}' ");
            IReadOnlyCollection<IWebElement> DocumentDetailsTextbox
                = StableFindElements(By.XPath(string.Format(_documentDetailsTextbox, textboxName)));
            string[] data = GenerateDataForTextbox(content, DocumentDetailsTextbox.Count, unique);

            for (int i = 0; i < DocumentDetailsTextbox.Count; i++)            
                DocumentDetailsTextbox.ElementAt(i).InputText(data[i]);            

            return this;
        }

        private string[] GenerateDataForTextbox(string content, int numberOfTextbox, bool unique)
        {
            string[] data = new string[numberOfTextbox];
            if (unique)
            {
                for (int i = 0; i < numberOfTextbox; i++)
                {
                    data[i] = content + " " + (i + 1).ToString();
                }
            }
            else
            {
                for (int i = 0; i < numberOfTextbox; i++)
                {
                    data[i] = content;
                }
            }
            return data;

        }

        public T ClickHeaderButton<T>(DocBulkUploadHeaderButton buttonName)
        {
            IWebElement Button = StableFindElement(By.XPath(string.Format(_headerButton, buttonName.ToDescription())));
            var node = StepNode();            
            node.Info("Click the button: " + buttonName.ToDescription());
            Button.HoverAndClickWithJS();
            //if (StableFindElement(_processingPopUp) != null)
            //    WaitForElementAttribute(StableFindElement(_processingPopUp), "display", "none");

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
            node.Info("Hover On Item:" + itemName);
            item.HoverWithJS();
            return this;
        }

        public ApplyToNRowsDialog ClickToNRowsItem(ref int indexOfSubMenu, bool nextRows = true)
        {
            var node = StepNode();
            node.Info("Click 'to next N rows' item");
            IWebElement ToNRows;
            if (nextRows)            
                ToNRows = StableFindElement(By.XPath(string.Format(_toNRows, indexOfSubMenu + 2, "to next N rows...")));
            else
                ToNRows = StableFindElement(By.XPath(string.Format(_toNRows, indexOfSubMenu + 2, "to previous N rows...")));
            ToNRows.HoverAndClickWithJS();

            var applyToNRowsDialog = new ApplyToNRowsDialog(WebDriver);
            WebDriver.SwitchTo().Frame(applyToNRowsDialog.IFrameName);
            WaitUntil(driver => applyToNRowsDialog.OKButton != null);
            return applyToNRowsDialog;
        }

        private Dictionary<string, string> GetDataFromDocumentRow(int rowIndex)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            string value;
            string[] textboxName = new string[7];
            textboxName[0] = DocBulkUploadInputText.DocumentNo.ToDescription();
            textboxName[1] = DocBulkUploadInputText.Title.ToDescription();
            textboxName[2] = DocBulkUploadInputText.Due.ToDescription();
            textboxName[3] = DocBulkUploadInputText.Actual.ToDescription();
            textboxName[4] = DocBulkUploadInputText.Forecast.ToDescription();
            textboxName[5] = DocBulkUploadInputText.AltDocumentNo.ToDescription();
            textboxName[6] = DocBulkUploadInputText.IncTrnNo.ToDescription();

            IReadOnlyCollection<IWebElement> DocumentDetailsTextbox;
            for (int i = 0; i < textboxName.Length; i++)
            {
                DocumentDetailsTextbox = StableFindElements(By.XPath(string.Format(_documentDetailsTextbox, textboxName[i])));
                if (DocumentDetailsTextbox.ElementAt(rowIndex).Text != null)
                    value = DocumentDetailsTextbox.ElementAt(rowIndex).Text;
                else
                    value = "";
                data.Add(textboxName[i], value);
            }

            string[] comboboxName = new string[8];
            comboboxName[0] = DocBulkUploadDropdownType.Rev.ToDescription();
            comboboxName[1] = DocBulkUploadDropdownType.Sts.ToDescription();
            comboboxName[2] = DocBulkUploadDropdownType.Disc.ToDescription();
            comboboxName[3] = DocBulkUploadDropdownType.Cat.ToDescription();
            comboboxName[4] = DocBulkUploadDropdownType.Type.ToDescription();
            comboboxName[5] = DocBulkUploadDropdownType.Location.ToDescription();
            comboboxName[6] = DocBulkUploadDropdownType.SpecReference.ToDescription();
            comboboxName[7] = DocBulkUploadDropdownType.SubType.ToDescription();

            IReadOnlyCollection<SelectElement> DocumentDetailsCombobox;
            for (int i = 0; i < comboboxName.Length; i++)
            {
                DocumentDetailsCombobox = GetAllComboBoxes(comboboxName[i]);
                data.Add(comboboxName[i], DocumentDetailsCombobox.ElementAt(rowIndex).SelectedOption.Text);
            }
            
            data.Add("SupersededCheckbox",AllSupersededCheckboxes.ElementAt(rowIndex).Selected.ToString());

            return data;
        }


        public KeyValuePair<string, bool> ValidateFilesDisplay(int numberOfFiles)
        {
            var node = StepNode();

            try
            {
                if (FileNames.Count == numberOfFiles)
                    return SetPassValidation(node, Validation.Validate_Files_Display + numberOfFiles);

                return SetFailValidation(node, Validation.Validate_Files_Display + numberOfFiles);
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

        public KeyValuePair<string, bool> ValidateDocumentRowIsSelected(int documentRow, bool checkSelected = true)
        {
            documentRow = Utils.RefactorIndex(documentRow);
            var node = StepNode();
            try
            {
                if (IsRowSelected(documentRow) == checkSelected)
                    return SetPassValidation(node, string.Format(Validation.Row_Is_Selected, documentRow + 1));
                else
                    return SetFailValidation(node, string.Format(Validation.Row_Is_Selected, documentRow + 1));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Row_Is_Selected, documentRow + 1), e);
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
        //TO-DO: Currently, there is an abnormal issue relating to the Copy Properties function. Sometime, the properties are not copied to n rows as expected
        //
        public KeyValuePair<string, bool> ValidateDocumentPropertiesAreCopiedToAllRows(int rowIndexOfStandardRow)
        {
            var node = StepNode();
            rowIndexOfStandardRow = Utils.RefactorIndex(rowIndexOfStandardRow);
            try
            {
                Dictionary<string, string> dataOfStandardRow = GetDataFromDocumentRow(rowIndexOfStandardRow);
                int i = 0;
                do
                {
                    if (i != rowIndexOfStandardRow)
                    {
                        var dataFromDocumentRow = GetDataFromDocumentRow(i);
                        if (!Utils.DeepEquals(dataFromDocumentRow,dataOfStandardRow))
                            return SetFailValidation(node, Validation.Document_Properties_Are_Copied_To_All_Rows);
                    }
                    i++;
                }
                while (i < AllDocumentRows.Count);

                return SetPassValidation(node, Validation.Document_Properties_Are_Copied_To_All_Rows); 
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Document_Properties_Are_Copied_To_All_Rows, e);
            }
        }

        private static class Validation
        {
            public static string Holding_Area_Page_Displays = "Validate That The Holding Area Page Displays";
            public static string All_Rows_Are_Selected = "Validate That All Rows Are Selected";
            public static string Not_All_Rows_Are_Selected = "Validate That All Rows Are Not Selected";
            public static string All_Rows_Are_DeSelected = "Validate That All Rows Are DeSelected";
            public static string Not_All_Rows_Are_DeSelected = "Validate That All Rows Are Not DeSelected";
            public static string Cannot_Validate_Rows_State = "Error Cannot Validate Rows State";
            public static string Validate_Files_Display = "Validate That Files Are Added And Displayed Success:";
            public static string Validat_File_Names_Are_Listed_In_Column = "Validate That File Names Are Listed In {0} Column";
            public static string Row_Is_Selected = "Validate That Document Row {0} Is Selected";
            public static string Submenu_Displays = "Validate That Submenu Displays After Hovering";
            public static string Document_Properties_Are_Copied_To_All_Rows = "Validate That The Document Properties Are Copied To All Rows";
        }
        #endregion
    }
}
