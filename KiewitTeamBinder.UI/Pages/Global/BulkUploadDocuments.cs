﻿using System;
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


namespace KiewitTeamBinder.UI.Pages.VendorData
{
    public class BulkUploadDocuments : Dashboard
    {
        #region Entities
        private By _addFileInBulk => By.Id("addBulkFlashWrapper");
        private static By _selectAllCheckbox => By.XPath(
            "//th//input[contains(@id, 'ClientSelectColumnSelectCheckBox')]");
        private static By _bulkUploadDocumentsTable => By.XPath(
            "//div[@id='RadGrid1_GridData']//*[contains(@class, 'rgMasterTable')]");

        public IWebElement AddFileInBulk { get { return StableFindElement(_addFileInBulk); } }
        public IWebElement SelectAllCheckbox { get { return StableFindElement(_selectAllCheckbox); } }
        public IWebElement BulkUploadDocumentsTable { get { return StableFindElement(_bulkUploadDocumentsTable); } }
        #endregion


        #region Actions
        public BulkUploadDocuments(IWebDriver webDriver) : base(webDriver) { }

        public BulkUploadDocuments ClickAddFilesInBulk()
        {
            AddFileInBulk.Click();
            return this;
        }

        public void SelectAllCheckboxes(bool selectOption)
        {
            if (selectOption)
                SelectAllCheckbox.UnCheck();

            SelectAllCheckbox.Check();
        }

        // Set 'checkSelected' equals to 'false' if desire to check all rows are deselected
        // Need to divide the totalRows because there are 2 rows for each file: 1 file name row and other is data row.
        public KeyValuePair<string, bool> ValidateAllRowsAreSelected(bool checkSelected = true)
        {
            var node = StepNode();
            List<bool> validations = new List<bool> { };
            List<int> rowNumbers = new List<int> { };

            var totalRows = GetTableRowNumber(BulkUploadDocumentsTable);

            try
            {
                if (checkSelected)
                {
                    for (int i = 0; i < totalRows / 2; i++)
                    {
                        if (FindDynamicRow(i).IsDisplayed())
                            validations.Add(true);

                        else
                        {
                            validations.Add(false);
                            rowNumbers.Add(i + 1);
                        }

                    }

                    if (validations.TrueForAll(allSelected => allSelected))
                        return SetPassValidation(node, Validation.All_Rows_Are_Selected);

                    return SetFailValidation(node, 
                        Validation.Not_All_Rows_Are_Selected 
                        + String.Format($"Rows {string.Join(",", rowNumbers)} are not selected."));
                }
                else
                {
                    for (int i = 0; i < totalRows / 2; i++)
                    {
                        if (FindDynamicRow(i, false).IsDisplayed())
                            validations.Add(true);

                        else
                        {
                            validations.Add(false);
                            rowNumbers.Add(i + 1);
                        }

                    }

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

        public static IWebElement FindDynamicRow(int rowIndex, bool isSelected = true)
        {
            if (isSelected)
                return StableFindElement(By.XPath(
                    string.Format($"//tr[contains(@class, 'rgSelectedRow') and @id='RadGrid1_ctl00__{rowIndex}']")));

            else if (rowIndex % 2 == 0)
                return StableFindElement(By.XPath(
                    string.Format($"//tr[contains(@class, 'rgRow') and @id='RadGrid1_ctl00__{rowIndex}']")));

            else
                return StableFindElement(By.XPath(
                    string.Format($"//tr[contains(@class, 'rgAltRow') and @id='RadGrid1_ctl00__{rowIndex}']")));
        }

        // checkboxType is "selectCheckbox" or "Superseded"
        public void SelectTableCheckbox(int rowIndex, string selectOption = "on", string checkboxType = "selectCheckbox")
        {
            if (checkboxType == "selectCheckbox")
            {
                if (rowIndex % 2 == 0)
                {
                    var checkboxElement = StableFindElement(By.XPath
                        (string.Format($"//tr[contains(@class, 'rgRow') and @id='RadGrid1_ctl00__{rowIndex}']//input[contains(@name, 'ClientSelectColumnSelectCheckBox')")));

                    if (String.IsNullOrWhiteSpace(selectOption) || selectOption != "on" || selectOption != "off")
                        throw new InvalidOperationException("select option should be 'on' of 'off'. Default value is 'on'");

                    else if (selectOption.ToLower() == "on")
                        checkboxElement.Check();

                    else
                        checkboxElement.UnCheck();
                }
                else
                {
                    var checkboxElement = StableFindElement(By.XPath
                          (string.Format($"//tr[contains(@class, 'rgAltRow') and @id='RadGrid1_ctl00__{rowIndex}']//input[contains(@name, 'ClientSelectColumnSelectCheckBox')")));

                    if (String.IsNullOrWhiteSpace(selectOption) || selectOption != "on" || selectOption != "off")
                        throw new InvalidOperationException("select option should be 'on' of 'off'. Default value is 'on'");

                    else if (selectOption.ToLower() == "on")
                        checkboxElement.Check();

                    else
                        checkboxElement.UnCheck();
                }
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
        }

        public void SelectTableComboBox(int rowIndex, string selectItem, TableComboBoxType comboBoxType)
        {
            switch (comboBoxType)
            {
                case TableComboBoxType.Rev:
                    var revComboBox = new SelectElement(StableFindElement(
                                                            By.XPath($"//select[@data-property-name='{TableComboBoxType.Rev.ToDescription()}'][{rowIndex}]")));
                    revComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Sts:
                    var stsComboBox = new SelectElement(StableFindElement(
                                                            By.XPath($"//select[@data-property-name='{TableComboBoxType.Sts.ToDescription()}'][{rowIndex}]")));
                    stsComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Discipline:
                    var disciplineComboBox = new SelectElement(StableFindElement(
                                                            By.XPath($"//select[@data-property-name='{TableComboBoxType.Discipline.ToDescription()}'][{rowIndex}]")));
                    disciplineComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Category:
                    var categoryComboBox = new SelectElement(StableFindElement(
                                                            By.XPath($"//select[@data-property-name='{TableComboBoxType.Category.ToDescription()}'][{rowIndex}]")));
                    categoryComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.Type:
                    var typeComboBox = new SelectElement(StableFindElement(
                                                            By.XPath($"//select[@data-property-name='{TableComboBoxType.Type.ToDescription()}'][{rowIndex}]")));
                    typeComboBox.SelectByText(selectItem);
                    break;

                case TableComboBoxType.SubType:
                    var subTypeComboBox = new SelectElement(StableFindElement(
                                                            By.XPath($"//select[@data-property-name='{TableComboBoxType.SubType.ToDescription()}'][{rowIndex}]")));
                    subTypeComboBox.SelectByText(selectItem);
                    break;
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
        }
        #endregion
    }
}
