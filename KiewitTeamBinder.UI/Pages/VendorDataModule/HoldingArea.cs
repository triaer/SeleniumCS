using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.Common.Helper;

namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class HoldingArea : ProjectsDashboard
    {
        #region Entities
        private string _functionButton = "//li[@class='rtbItem rtbBtn'][a='{0}']";
        private string _filterTextBoxXpath = "//tr[@class='rgFilterRow']/td[count(//tr/th[.='{0}']/preceding-sibling::th)+1]";
        private static By _holdingAreaLabel => By.Id("lblRegisterCaption");
        private static By _documentNoTextBox => By.XPath("//input[contains(@id,'FilterTextBox_GridColDocumentNo')]");
        private static By _holdingAreaRadGrid => By.XPath("//div[contains(@id,'_cntPhMain_GridViewHoldingArea')][contains(@class,'RadGrid')]");
        private static By _holdingAreaGridData => By.XPath("//div[contains(@id,'_GridViewHoldingArea_GridData')]");
        private static By _infoPagerInHoldingAreaGrid => By.XPath("//table[contains(@id,'GridViewHoldingArea')]//div[contains(@class,'rgInfoPart')]//span[contains(@id,'DSC')]");
        private static By _documentRowsVisiableOnGrid => By.XPath(".//tbody/tr[not(@class='rgNoRecords')][contains(@style,'visible')]");

        public IWebElement HoldingAreaLabel { get { return StableFindElement(_holdingAreaLabel); } }
        public IWebElement DocumentNoTextBox { get { return StableFindElement(_documentNoTextBox); } }
        public IWebElement HoldingAreaRadGrid { get { return StableFindElement(_holdingAreaRadGrid); } }
        public IWebElement HoldingAreaGridData { get { return StableFindElement(_holdingAreaGridData); } }
        public IWebElement InfoPagerInHoldingAreaGrid { get { return StableFindElement(_infoPagerInHoldingAreaGrid); } }        
        #endregion

        #region Actions
        public HoldingArea(IWebDriver webDriver) : base(webDriver) { }

        public BulkUploadDocuments ClickBulkUploadButton(out string currentWindow)
        {
            var node = StepNode();
            node.Info("Click Bulk Upload button in Holding Area header");
            IWebElement FunctionButton = StableFindElement(By.XPath(string.Format(_functionButton, "Bulk Upload")));
            SwitchToPopUpWindow(FunctionButton, out currentWindow, false);
            return new BulkUploadDocuments(WebDriver);
        }

        public NewTransmittal ClickCreateTransmittalsButton()
        {
            string currentWindow;
            var node = StepNode();
            node.Info("Click Create Transmittals item from Transmit dropdown list in Holding Area header");
            SwitchToPopUpWindow(HeaderDropdownItem("Create Transmittals"), out currentWindow, false);
            return new NewTransmittal(WebDriver);
        }

        public HoldingArea EnterDocumentNo(string value)
        {
            DocumentNoTextBox.InputText(value);
            return this;
        }

        public HoldingArea FilterDocumentsByGridFilterRow(string columnName, string value, bool useFilterMenu = false, FilterOptions optionItem = FilterOptions.Contains)
        {
            IWebElement FilterCell = StableFindElement(By.XPath(string.Format(_filterTextBoxXpath,columnName)));
            IWebElement FilterTextBox = FilterCell.StableFindElement(By.TagName("input"));
            FilterTextBox.InputText(value);
            if (!useFilterMenu)
                FilterTextBox.SendKeys(Keys.Enter);
            else
            {
                IWebElement FilterMenu = FilterCell.StableFindElement(By.TagName("img"));
                SelectComboboxByText(FilterMenu, _gridViewFilterListData, optionItem.ToDescription());
            }
            WaitForJQueryLoad();
            return this;
        }

        public HoldingArea ClickCheckboxOfDocumentAtRow(int indexRow)
        {
            IWebElement row = HoldingAreaGridData.StableFindElement(By.XPath(".//tbody/tr[" + indexRow + "]"));
            row.StableFindElement(By.XPath("./td[1]/input")).Check();
            return this;
        }

        public T SelectRowCheckboxesWithoutTransmittalNo<T>(string gridViewName, int numberOfCheckbox, bool check, ref string[] selectedDocuments)
        {            
            int rowIndex, colIndex = 1;
            GetTableCellValueIndex(PaneTable(gridViewName), "Document No.", out rowIndex, out colIndex, "th");
            var conditions = new List<KeyValuePair<string, string>>
            { 
                new KeyValuePair<string, string>(MainPaneTableHeaderLabel.TransmittalNo.ToDescription(), "TRN-SMOKE")
            };

            IReadOnlyCollection<IWebElement> DocumentRows = GetNonAvailableItems(new TransmitDocumentSmoke().GridViewHoldingAreaName, conditions);
            if (numberOfCheckbox > DocumentRows.Count)
                numberOfCheckbox = DocumentRows.Count;

            for (int i = 0; i < numberOfCheckbox; i++)
            {
                IWebElement RowCheckBox = DocumentRows.ElementAt(i).StableFindElement(By.XPath(".//input[@type = 'checkbox']"));
                if (check)
                {
                    RowCheckBox.Check();
                    selectedDocuments[i] = DocumentRows.ElementAt(i).StableFindElement(By.XPath("./td[" + colIndex + "]")).Text;
                }
                else
                    RowCheckBox.UnCheck();
            }
            
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }


        public List<KeyValuePair<string, bool>> ValidateHoldingAreaGridShownDataCorrect(string filterColumn, string filterValue)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                node.Info("Check the data of document lines in column: " + filterColumn);
                validation.Add(ValidateDataOfDocumentRowsInAColumn(filterColumn, filterValue));
                node.Info("Check the data of document lines in column: Hold Process Status");
                validation.Add(ValidateDataOfDocumentRowsInAColumn("Hold Process Status", "New", true));

                List<IWebElement> documentLines = HoldingAreaGridData.StableFindElements(_documentRowsVisiableOnGrid).ToList();
                int expectedTotal = documentLines.Count;
                string actualItems = InfoPagerInHoldingAreaGrid.Text;
                if (actualItems == expectedTotal.ToString())
                    validation.Add(SetPassValidation(node, Validation.Item_Count_In_Bottom_Right_Corner_Displays_Correct + expectedTotal.ToString()));
                else
                    validation.Add(SetFailValidation(node, Validation.Item_Count_In_Bottom_Right_Corner_Displays_Correct, expectedTotal.ToString(), actualItems));

                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Holding_Area_Page_Shows_Data_Correct, e));
                return validation;
            }
        }

        public KeyValuePair<string, bool> ValidateDataOfDocumentRowsInAColumn(string column, string expectedMessage, bool isEqual = false)
        {
            var node = StepNode();
            string actualContent;
            string valMsg;
            int i = 0;
            int colIndex, rowIndex;
            bool result = false;
            try
            {
                List<IWebElement> documentLines = HoldingAreaGridData.StableFindElements(_documentRowsVisiableOnGrid).ToList();
                GetTableCellValueIndex(HoldingAreaRadGrid, column, out rowIndex, out colIndex,"th");
                valMsg = string.Format(Validation.Document_Data_Displays_Correct, column, expectedMessage);
                foreach (IWebElement row in documentLines)
                {
                    i++;
                    IWebElement cell = row.FindElement(By.XPath("./td[" + colIndex + "]"));
                    actualContent = cell.Text;
                    
                    if (isEqual)
                        result = (actualContent == expectedMessage) ? true : false;
                    else
                        result = (actualContent.Contains(expectedMessage)) ? true : false;
                    if (!result)
                        return SetFailValidation(node, valMsg + " At Row: " + i, expectedMessage, actualContent);
                }

                return SetPassValidation(node, valMsg);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Document_Data_Displays_Correct, column, expectedMessage), e);
            }
        }

        public KeyValuePair<string, bool> ValidateDocumentRowIsHighlighted(int indexRow)
        {
            var node = StepNode();
            IWebElement row = HoldingAreaGridData.StableFindElement(By.XPath(".//tbody/tr[" + indexRow + "]"));
            try
            {
                if (row.GetAttribute("class").Contains("rgSelectedRow"))
                    return SetPassValidation(node, Validation.Document_Row_Is_Highlighted);
                else
                    return SetFailValidation(node, Validation.Document_Row_Is_Highlighted);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Document_Row_Is_Highlighted, e);
            }
        }
                
        private static class Validation
        {
            public static string Holding_Area_Page_Displays = "Validate that the Vendor Data Module Holding Area page displays";
            public static string Document_Data_Displays_Correct = "Validate that the document data displays correct in column '{0}' with value '{1}' ";
            public static string Item_Count_In_Bottom_Right_Corner_Displays_Correct = "Validate that item count in bottom right corner of holding area matches actual count of items in grid: ";
            public static string Holding_Area_Page_Shows_Data_Correct = "Validate that Holding Area page shows data correct ";
            public static string Document_Row_Is_Highlighted = "Validate that Document row is selected and highlighted";
        }
         

        #endregion
    }
}
