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


namespace KiewitTeamBinder.UI.Pages.VendorData
{
    public class BulkUploadDocuments : Dashboard
    {
        #region Entities
        private By _addFileInBulkButton
            => By.Id("addBulkFlashWrapper");
        private static By _selectAllCheckbox
            => By.XPath("//th//input[contains(@id, 'ClientSelectColumnSelectCheckBox')]");
        private static By _selectCheckboxes
            => By.XPath("//td//input[contains(@id,'ClientSelectColumnSelectCheckBox')]");
        private static By _bulkUploadDocumentsTable
            => By.XPath("//div[@id='RadGrid1_GridData']//*[contains(@class, 'rgMasterTable')]");
        private By _addFilesInBulkButton
            => By.XPath("//*[@id='flashUploadBulk']");
        private By _fileNames
            => By.XPath("//tr[contains(@id,'ViewFiles')]");
        private By _tableHearder
            => By.Id("RadGrid1_ctl00_Header");
        private static By _allDocumentRows
            => By.XPath("//tr[@valign='top']");
        private static By _allRowCheckboxes
            => By.XPath("//input[contains(@name, 'ClientSelectColumnSelectCheckBox')]");
        private static By _allSupersededCheckboxes
            => By.XPath("//input[contains(@name, 'chkSuperseded')]");
        private static By _allCopyAttributesItems
            => By.XPath("//div[@id='RadContextMenu1_detached']/div[contains(@class,'rmScrollWrap')]//li");
        
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
        #endregion

        
        #region Actions
        public BulkUploadDocuments(IWebDriver webDriver) : base(webDriver) { }

        public IReadOnlyCollection<IWebElement> GetAllDocumentRows()
        {
            IReadOnlyCollection<IWebElement> AllDocumentRows = StableFindElements(_allDocumentRows);
            return AllDocumentRows;
        }

        public IReadOnlyCollection<IWebElement> GetAllRowCheckboxes()
        {
            IReadOnlyCollection<IWebElement> AllRowCheckboxes = StableFindElements(_allRowCheckboxes);
            return AllRowCheckboxes;
        }

        public IReadOnlyCollection<IWebElement> GetAllSupersededCheckboxes()
        {
            IReadOnlyCollection<IWebElement> AllSupersededCheckboxes = StableFindElements(_allSupersededCheckboxes);
            return AllSupersededCheckboxes;
        }

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

        public IReadOnlyCollection<IWebElement> GetAllCopyAttributesItems()
        {
            IReadOnlyCollection<IWebElement> AllCopyAttributesItems = StableFindElements(_allCopyAttributesItems);
            return AllCopyAttributesItems;
        }

        // We have to use hard code waiting time cause this is Windows native control
        // In order to type in multi-file names we need to separate the filePath and fileNames
        // filePath = "C:\Working", fileNames = {\"File1.txt\" \"File2.txt\" \"File3.txt\...."}
        public BulkUploadDocuments AddFilesInBulk(string filePath, string fileNames)
        {
            AddFilesInBulkButton.Click();
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

        public bool IsRowSelected(int rowIndex)
        {
            IReadOnlyCollection<IWebElement> AllDocumentRows = GetAllDocumentRows();
            IWebElement row = AllDocumentRows.ElementAt(rowIndex);
            //IWebElement row = StableFindElement(By.XPath(string.Format($"//tr[@id='RadGrid1_ctl00__{rowIndex}']")));
            if (row.GetAttribute("class").Contains("rgSelectedRow"))
                return true;
            return false;
        }

        //public static IWebElement FindDynamicRow(int rowIndex, bool isSelected = true)
        //{
        //    if (isSelected)
        //        return StableFindElement(By.XPath(
        //            string.Format($"//tr[contains(@class, 'rgSelectedRow') and @id='RadGrid1_ctl00__{rowIndex}']")));

        //    else if (rowIndex % 2 == 0)
        //        return StableFindElement(By.XPath(
        //            string.Format($"//tr[contains(@class, 'rgRow') and @id='RadGrid1_ctl00__{rowIndex}']")));

        //    else
        //        return StableFindElement(By.XPath(
        //            string.Format($"//tr[contains(@class, 'rgAltRow') and @id='RadGrid1_ctl00__{rowIndex}']")));
        //}

        // checkboxType is "rowCheckbox" or "Superseded"
        public BulkUploadDocuments SelectTableCheckbox(int rowIndex, string selectOption = "on", 
                                                       string checkboxType = "rowCheckbox")
        {
            
            IWebElement checkbox;
            if (checkboxType == "rowCheckbox")
            {               
                IReadOnlyCollection<IWebElement> AllRowCheckboxes = GetAllRowCheckboxes();
                checkbox = AllRowCheckboxes.ElementAt(rowIndex);
            }
            else
            {
                rowIndex = Utils.RefactorIndex(rowIndex);
                IReadOnlyCollection<IWebElement> AllSupersededCheckboxes = GetAllSupersededCheckboxes();
                checkbox = AllSupersededCheckboxes.ElementAt(rowIndex);
            }
            if (String.IsNullOrWhiteSpace(selectOption) 
                || (selectOption.ToLower() != "on" && selectOption.ToLower() != "off"))
                throw new InvalidOperationException("select option should be 'on' of 'off'. Default value is 'on'");
            else if (selectOption.ToLower() == "on")
                checkbox.Check();
            else
                checkbox.UnCheck();

            return this;
        }

        public BulkUploadDocuments SelectTableComboBox(int rowIndex, string selectItem, TableComboBoxType comboBoxType)
        {
            rowIndex = Utils.RefactorIndex(rowIndex);
            switch (comboBoxType)
            {
                case TableComboBoxType.Rev:
                    //var revComboBox = new SelectElement(StableFindElement(
                    //                                    By.XPath($"//tr[@id='RadGrid1_ctl00__{rowIndex - 1}']//select[@data-property-name='{TableComboBoxType.Rev.ToDescription()}']")));
                    var revComboBox = GetAllComboBoxes(TableComboBoxType.Rev.ToDescription()).ElementAt(rowIndex);
                    revComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Sts:                    
                    var stsComboBox = GetAllComboBoxes(TableComboBoxType.Sts.ToDescription()).ElementAt(rowIndex);
                    stsComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Disc:
                    var disciplineComboBox = GetAllComboBoxes(TableComboBoxType.Disc.ToDescription()).ElementAt(rowIndex);
                    disciplineComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Cat:
                    var categoryComboBox = GetAllComboBoxes(TableComboBoxType.Cat.ToDescription()).ElementAt(rowIndex);
                    categoryComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Type:
                    var typeComboBox = GetAllComboBoxes(TableComboBoxType.Type.ToDescription()).ElementAt(rowIndex);
                    typeComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.SubType:
                    var subTypeComboBox = GetAllComboBoxes(TableComboBoxType.SubType.ToDescription()).ElementAt(rowIndex);
                    subTypeComboBox.SelectByText(selectItem);
                    break;
            }
            return this;
        }

        public BulkUploadDocuments EnterTextbox(int rowIndex, string textboxName, string content)
        {
            rowIndex = Utils.RefactorIndex(rowIndex);
            IReadOnlyCollection<IWebElement> DocumentDetailsTextbox 
                = StableFindElements(By.XPath(string.Format(_documentDetailsTextbox, textboxName)));
            DocumentDetailsTextbox.ElementAt(rowIndex).InputText(content);
            return this;
        }

        /// <summary>
        /// This method only applies for "New Rows" button, "Copy Attributes" button.
        /// </summary>
        /// <param name="buttonName"> Get from KiewitTeamBinderENums.ButtonName </param>
        /// <returns> BulkUploadDocuments object </returns>
        public T ClickButton<T>(ButtonName buttonName)
        {
            IWebElement Button = StableFindElement(By.XPath(string.Format(_headerButton, buttonName.ToDescription())));
            Button.HoverAndClickWithJS();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public AlertDialog ClickCopyAttributeButtonWithoutSelectRow()
        {
            IWebElement Button
                = StableFindElement(By.XPath(
                    string.Format(_headerButton, KiewitTeamBinderENums.ButtonName.CopyAttributes.ToDescription())));
            Button.HoverAndClickWithJS();
            return new AlertDialog(WebDriver);
        }

        public BulkUploadDocuments HoverItem(string itemName, ref int index)
        {
            IReadOnlyCollection<IWebElement> AllCopyAttributesItems = GetAllCopyAttributesItems();            
            IWebElement item;
            index = -1;
            do
            {
                index++;
                item = AllCopyAttributesItems.ElementAt(index);                
            }
            while (item.Text != itemName);
            item.HoverWithJS();
            return this;
        }

        public ConfirmDialog ClickRemoveRowsButton()
        {
            IWebElement button = StableFindElement(By.XPath(string.Format(_headerButton, ButtonName.RemoveRows.ToDescription())));
            button.HoverAndClickWithJS();
            return new ConfirmDialog(WebDriver);
        }

        public ApplyToNRowsDialog ClickToNRowsItem(ref int indexOfSubMenu, bool nextRows = true)
        {
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
            //var totalRows = GetTableRowNumber(BulkUploadDocumentsTable);
            int totalRows = GetAllDocumentRows().Count;
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
        }
        #endregion
    }
}
