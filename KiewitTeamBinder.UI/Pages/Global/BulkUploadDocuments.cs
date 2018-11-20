using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.UI.KiewitTeamBinderENums;
using OpenQA.Selenium.Support.UI;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Dialogs;


namespace KiewitTeamBinder.UI.Pages.VendorData
{
    public class BulkUploadDocuments : Dashboard
    {
        #region Entities
        private By _addFileInBulk => By.Id("addBulkFlashWrapper");
        private static By _selectAllCheckbox => By.XPath(
            "//th//input[contains(@id, 'ClientSelectColumnSelectCheckBox')]");
        private static By _selectCheckboxes => By.XPath(
            "//td//input[contains(@id,'ClientSelectColumnSelectCheckBox')]");
        private static By _bulkUploadDocumentsTable => By.XPath(
            "//div[@id='RadGrid1_GridData']//*[contains(@class, 'rgMasterTable')]");
        private static By _documentRows => By.XPath("//tr[@valign='top']");
        public string _textbox = "//tr[@id = 'RadGrid1_ctl00__{0}']//*[@data-property-name='{1}']";
        public string _button = "//a[span='{0}']";

        public IWebElement AddFileInBulk { get { return StableFindElement(_addFileInBulk); } }
        public IWebElement SelectAllCheckbox { get { return StableFindElement(_selectAllCheckbox); } }
        public IReadOnlyCollection<IWebElement> SelectCheckboxes { get { return StableFindElements(_selectCheckboxes); } }
        public IReadOnlyCollection<IWebElement> DocumentRows { get { return StableFindElements(_documentRows); } }
        public IWebElement BulkUploadDocumentsTable { get { return StableFindElement(_bulkUploadDocumentsTable); } }
        #endregion


        #region Actions
        public BulkUploadDocuments(IWebDriver webDriver) : base(webDriver) { }

        public BulkUploadDocuments ClickAddFilesInBulk()
        {
            AddFileInBulk.Click();
            return this;
        }

        public BulkUploadDocuments SelectAllCheckboxes(bool selectOption)
        {
            if (selectOption)
                SelectAllCheckbox.Check();

            SelectAllCheckbox.UnCheck();

            return this;
        }

        // Set 'checkSelected' equals to 'false' if desire to check all rows are deselected
        // Need to divide the totalRows because there are 2 rows for each file: 1 file name row and other is data row.
        public KeyValuePair<string, bool> ValidateAllRowsAreSelected(bool checkSelected = true)
        {
            var node = StepNode();
            List<bool> validations = new List<bool> { };
            List<int> rowNumbers = new List<int> { };
            //var totalRows = GetTableRowNumber(BulkUploadDocumentsTable);
            int totalRows = DocumentRows.Count;
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
            var node = StepNode();
            try
            {
                if (IsRowSelected(rowIndex -1) == checkSelected)
                    return SetPassValidation(node, string.Format(Validation.Row_Is_Selected, rowIndex - 1));
                else
                    return SetFailValidation(node, string.Format(Validation.Row_Is_Selected, rowIndex - 1));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Row_Is_Selected, rowIndex - 1), e);
            }
        }

        public bool IsRowSelected(int rowIndex)
        {
            IWebElement row = StableFindElement(By.XPath(string.Format($"//tr[@id='RadGrid1_ctl00__{rowIndex}']")));
            if (row.GetAttribute("class").Contains("rgSelectedRow"))
                return true;
            return false;
        }

        //public static IWebElement FindDynamicRow(int rowIndex, bool isSelected = true)
        //{
        //    if (isSelected)
        //        return StableFindElement(By.XPath(
        //            string.Format($"//tr[contains(@class, 'rgSelectedRow') and @id='RadGrid1_ctl00__{rowIndex}']")));
        //    else 
        //        if (rowIndex % 2 == 0)
        //            return StableFindElement(By.XPath(
        //                string.Format($"//tr[@class= 'rgRow' and @id='RadGrid1_ctl00__{rowIndex}']")));
        //        else
        //            return StableFindElement(By.XPath(
        //                string.Format($"//tr[@class= 'rgAltRow' and @id='RadGrid1_ctl00__{rowIndex}']")));
        //}

        // checkboxType is "selectCheckbox" or "Superseded"
        public BulkUploadDocuments SelectTableCheckbox(int rowIndex = 1, string selectOption = "on", string checkboxType = "selectCheckbox")
        {
            if (checkboxType == "selectCheckbox")
            {                
                var checkboxElement = StableFindElement(By.XPath
                    (string.Format($"//tr[@id='RadGrid1_ctl00__{rowIndex - 1}']//input[contains(@name, 'ClientSelectColumnSelectCheckBox')]")));

                if (String.IsNullOrWhiteSpace(selectOption) || (selectOption != "on" && selectOption != "off"))
                    throw new InvalidOperationException("select option should be 'on' or 'off'. Default value is 'on'");

                else if (selectOption.ToLower() == "on")
                    checkboxElement.Check();

                else
                    checkboxElement.UnCheck();        
            }
            else
            {
                var checkboxElement = StableFindElement(By.XPath
                         (string.Format($"//span[@class='Checkbox'][@data-property-name='Superseded'][{rowIndex}]")));

                if (String.IsNullOrWhiteSpace(selectOption) || selectOption != "on" || selectOption != "off")
                    throw new InvalidOperationException("select option should be 'on' of 'off'. Default value is 'on'");

                else if (selectOption.ToLower() == "on")
                    checkboxElement.Check();

                else
                    checkboxElement.UnCheck();
            }
            return this;
        }

        public BulkUploadDocuments SelectTableComboBox(int rowIndex, string selectItem, TableComboBoxType comboBoxType)
        {
            switch (comboBoxType)
            {
                case TableComboBoxType.Rev:
                    var revComboBox = new SelectElement(StableFindElement(
                                                        By.XPath($"//tr[@id='RadGrid1_ctl00__{rowIndex - 1}']//select[@data-property-name='{TableComboBoxType.Rev.ToDescription()}']")));
                    revComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Sts:
                    var stsComboBox = new SelectElement(StableFindElement(
                                                        By.XPath($"//tr[@id='RadGrid1_ctl00__{rowIndex - 1}']//select[@data-property-name='{TableComboBoxType.Sts.ToDescription()}']")));
                    stsComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Disc:
                    var disciplineComboBox = new SelectElement(StableFindElement(
                                                        By.XPath($"//tr[@id='RadGrid1_ctl00__{rowIndex - 1}']//select[@data-property-name='{TableComboBoxType.Disc.ToDescription()}']")));
                    disciplineComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Cat:
                    var categoryComboBox = new SelectElement(StableFindElement(
                                                        By.XPath($"//tr[@id='RadGrid1_ctl00__{rowIndex - 1}']//select[@data-property-name='{TableComboBoxType.Cat.ToDescription()}']")));
                    categoryComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Type:
                    var typeComboBox = new SelectElement(StableFindElement(
                                                        By.XPath($"//tr[@id='RadGrid1_ctl00__{rowIndex - 1}']//select[@data-property-name='{TableComboBoxType.Type.ToDescription()}']")));
                    typeComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.SubType:
                    var subTypeComboBox = new SelectElement(StableFindElement(
                                                         By.XPath($"//tr[@id='RadGrid1_ctl00__{rowIndex - 1}']//select[@data-property-name='{TableComboBoxType.SubType.ToDescription()}']")));
                    subTypeComboBox.SelectByText(selectItem);
                    break;
            }
            return this;
        }

        public BulkUploadDocuments EnterTextbox(int rowIndex, string textboxName, string content)
        {
            IWebElement TextBox = StableFindElement(By.XPath(string.Format(_textbox, rowIndex - 1, textboxName)));
            TextBox.InputText(content);
            return this;
        }

        public void ClickButton(ButtonName buttonName)
        {            
            switch (buttonName)
            {
                case ButtonName.RemoveRows:
                    ClickRemoveRowsButton();
                    break;
                default:
                    break;
            }
        }

        public ConfirmDialog ClickRemoveRowsButton()
        {
            IWebElement button = StableFindElement(By.XPath(string.Format(_button, ButtonName.RemoveRows.ToDescription())));
            button.HoverAndClickWithJS();
            return new ConfirmDialog(WebDriver);            
        }

        private static class Validation
        {
            public static string Holding_Area_Page_Displays = "Validate That The Holding Area Page Displays";
            public static string All_Rows_Are_Selected = "Validate That All Rows Are Selected";
            public static string Not_All_Rows_Are_Selected = "Validate That All Rows Are Not Selected";
            public static string All_Rows_Are_DeSelected = "Validate That All Rows Are DeSelected";
            public static string Not_All_Rows_Are_DeSelected = "Validate That All Rows Are DeSelected";
            public static string Row_Is_Selected = "Validate That row {0} is selected";
            public static string Cannot_Validate_Rows_State = "Error Cannot Validate Rows State";
        }
        #endregion
    }
}
