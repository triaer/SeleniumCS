using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.PopupWindows;


namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class VendorDataRegister : ProjectsDashboard
    {
        #region Entities
        private static string _filterItemsXpath = "//table[contains(@id,'GridViewItemsVendor')]//tr[@valign='top']";

        private static By _deliverableLineItem => By.XPath("//span[(text()='Deliverable Line Item')]");
        private static By _purchaseTable => By.XPath("//table[contains(@id,'GridViewItemsVendor')]/thead");
        private static By _deliverableTable => By.XPath("//table[contains(@id,'GridViewItemsVendor') and contains(@id, 'ViewDeliverableVendor')]/thead");
        private static By _expandButton => By.XPath("//td[@class = 'rgExpandCol']/input");
        private static By _descriptionColInGridView(string selectedValue, string gridView, string contractDescription) => By.XPath($"//span[contains(text(),'{selectedValue}')]/ancestor::tr[contains(@id,'{gridView}_ctl00')]//nobr[contains(text(),'{contractDescription}')]");
        private static By _checkBoxItemContract(string gridView, string description) => By.XPath($"//*[contains(text(),'{description}')]/ancestor::tr[contains(@id,'{gridView}_ctl00')]//input[contains(@id,'{gridView}_ctl00')]");
        private static By _selectedRecordCount => By.XPath("//span[@id='lblRegisterSelectedRecordCount']");
        private static By _blueHeader(string blueHeader) => By.XPath($"//div[@id='lblRegisterCaption']/span[text()='{blueHeader}']");
        private static By _blueItem(string gridView, string itemName) => By.XPath($"//span[@class='HyperLinkWithNoLine']/ancestor::tr[contains(@id,'ctl00_cntPhMain_{gridView}_ctl00')]//span[text()='{itemName}']");
        private static By _itemsList(string gridView) => By.XPath($"//tr[contains(@id,'ctl00_cntPhMain_{gridView}_ctl00')]");
        private static By _selectedRow(string gridView, string Description) => By.XPath($"//*[contains(text(),'{Description}')]/ancestor::tr[contains(@id,'{gridView}_ctl00')]");
        private static By _deliverableLineItemNmberItem(string value) => By.XPath($"//span[@class='HyperLinkWithNoLine' and text() = '{value}']");
        private static By _itemSubRegisterCaption => By.XPath("//div[@id='lblRegisterCaption']/span");
        private static By _registerCaptionLabel => By.Id("lblRegisterCaption");
        private static By _pageSizeBox => By.XPath("//input[contains(@id,'PageSizeComboBox_ClientState') and contains(@id,'RadGridExpeditingContracts')]");
        private static By _documentRow(int index) => By.XPath($"//div[contains(@id, 'GridViewDocumentsGrid_GridData')]/table/tbody/tr[{index}]/td");
        private static By _rowColumnName(string gridViewName) => By.XPath($"//a[contains(@onclick,'{gridViewName}')]/ancestor::th[contains(@class,'rgHeader')]");

        public IWebElement BlueItem(string gridViewName, string itemName) => StableFindElement(_blueItem(gridViewName, itemName));
        public IWebElement DeliverableTable { get { return StableFindElement(_deliverableTable); } }
        public IWebElement DeliverableLineItem => StableFindElement(_deliverableLineItem);
        public IReadOnlyCollection<IWebElement> ExpandButton { get { return StableFindElements(_expandButton); } }
        public IWebElement PurchaseTable { get { return StableFindElement(_purchaseTable); } }
        public IWebElement DescriptionColInGridView(string contractNumber, string gridView, string description) => StableFindElement(_descriptionColInGridView(contractNumber, gridView, description));
        public IWebElement CheckBoxItemContract(string gridView, string description) => StableFindElement(_checkBoxItemContract(gridView, description));
        public IWebElement SelectedRecordCount { get { return StableFindElement(_selectedRecordCount); } }
        public IWebElement BlueHeader(string blueHeader) => StableFindElement(_blueHeader(blueHeader));
        public IWebElement PageSizeBox { get { return FindElement(_pageSizeBox); } }
        public IReadOnlyCollection<IWebElement> ItemsList(string gridView) => StableFindElements(_itemsList(gridView));
        public IWebElement SelectedRow(string gridView, string description) => StableFindElement(_selectedRow(gridView, description));
        public IWebElement RegisterCaptionLabel { get { return StableFindElement(_registerCaptionLabel); } }
        public IReadOnlyCollection<IWebElement> RowColumnName(string gridViewName) => StableFindElements(_rowColumnName(gridViewName));
        #endregion

        #region Actions
        public VendorDataRegister(IWebDriver webDriver) : base(webDriver)
        {
        }

        public List<string> GetDocumentInfoByIndex(int index)
        {
            var listInfo = new List<string>();
            List<IWebElement> ListElement = StableFindElements(_documentRow(index)).ToList();
            foreach (var item in ListElement)
                listInfo.Add(item.Text.Trim());
            return listInfo;
        }

        public string GetDocumentDetailWindowByItemIndex(int index)
        {
            string documentDetailWindow = "AUTO1";
            string[] listInfoDocument = GetDocumentInfoByIndex(index).ToArray();
            for (int i = 1; i < 4; i++)
            {
                documentDetailWindow = documentDetailWindow + " - ";
                documentDetailWindow = documentDetailWindow + listInfoDocument[i];     
            }
            return documentDetailWindow.Trim();
        }

        public VendorItemDetail OpenItem(List<KeyValuePair<string, string>> columnValuePairList)
        {
            var PurchaseItemsList = GetAvailablePurchaseItems(columnValuePairList);
            var item = PurchaseItemsList.ElementAt(0).StableFindElement(By.XPath("./td[3]"));
            ScrollIntoView(item);
            string currentWindow;
            SwitchToNewPopUpWindow(item, out currentWindow, doubleClick: true);
            return new VendorItemDetail(WebDriver);
        }

        public VendorDataRegister ClickExpandButton(int index, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info("Click the expand button");
            index = Utils.RefactorIndex(index);
            ExpandButton.ElementAt(index).Click();
            if (waitForLoading)
            {
                WaitForLoadingPanel(shortTimeout * 2);
            }
            return this;
        }

        private IReadOnlyCollection<IWebElement> GetAvailablePurchaseItems(List<KeyValuePair<string, string>> columnValuePairList)
        {
            int rowIndex, colIndex = 1;
            string itemsXpath = _filterItemsXpath;
            GetTableCellValueIndex(PurchaseTable, columnValuePairList.ElementAt(0).Key, out rowIndex, out colIndex, "th");
            if (colIndex < 2)
                return null;
            itemsXpath += $"[td[{colIndex}][contains(., '{columnValuePairList.ElementAt(0).Value}')]";                                    

            for (int i = 1; i < columnValuePairList.Count; i++)
            {
                GetTableCellValueIndex(PurchaseTable, columnValuePairList.ElementAt(i).Key, out rowIndex, out colIndex, "th");
                if (colIndex < 2)
                    return null;
                itemsXpath += $" and td[{colIndex}][contains(., '{columnValuePairList.ElementAt(i).Value}')]";                                        
            }
            itemsXpath += "]";

            return StableFindElements(By.XPath(itemsXpath));
        }
        
        private IReadOnlyCollection<IWebElement> GetAvalibleDeliverable(List<KeyValuePair<string, string>> columnValuePairList)
        {
            int rowIndex, colIndex = 1;
            string itemsXpath = _filterItemsXpath;
            GetTableCellValueIndex(DeliverableTable, columnValuePairList.ElementAt(0).Key, out rowIndex, out colIndex, "th");
            if (colIndex < 2)
                return null;
            itemsXpath += $"[td[{colIndex}][contains(., '{columnValuePairList.ElementAt(0).Value}')]";

            for (int i = 1; i < columnValuePairList.Count; i++)
            {
                GetTableCellValueIndex(DeliverableTable, columnValuePairList.ElementAt(i).Key, out rowIndex, out colIndex, "th");
                if (colIndex < 2)
                    return null;
                itemsXpath += $" and td[{colIndex}][contains(., '{columnValuePairList.ElementAt(i).Value}')]";
            }
            itemsXpath += "]";

            return StableFindElements(By.XPath(itemsXpath));

        }

        public VendorDeliverableDetail OpenDeliverableLineItemTemplate(out string parrentWindow)
        {
            SwitchToNewPopUpWindow(DeliverableLineItem, out parrentWindow, true);
            return new VendorDeliverableDetail(WebDriver);
        }

        public static int GetWindowHandle() {
            return WebDriver.WindowHandles.Count;
        }

        public int GetSelectedRecordCount(bool waitForLoading = true)
        {
            string selectedText = "";
            string selectedCount = SelectedRecordCount.Text;

            selectedText = selectedCount.Split(':')[1];
            selectedText = selectedText.Replace(" ", "");
            return int.Parse(selectedText);
        }

        public VendorDataRegister ClickOnBlueHeader(string[] blueHeader)
        {
            var node = StepNode();
            node.Info($"Click on the {blueHeader} BlueHeader");
            for (int i = 0; i < blueHeader.Length; i++)
            {
                ScrollIntoView(BlueHeader(blueHeader[i]));
                BlueHeader(blueHeader[i]).Click();
            }                
            return this;
        }

        public VendorDataRegister ClickOnCheckBox(string gridView, string contractDescription, bool uncheck = false)
        {
            var node = StepNode();
            node.Info("Check box a line item");
            if (uncheck)
                CheckBoxItemContract(gridView, contractDescription.Trim()).UnCheck();
            else
                CheckBoxItemContract(gridView, contractDescription.Trim()).Check();
            return this;
        }

        public VendorDataRegister DoubleClickItem(string selectedValue, string gridView, string description)
        {
            var node = StepNode(); 
            node.Info("Double click on a item");
            while (DescriptionColInGridView(selectedValue, gridView, description) == null)
                ArowNextPageInGridPager(gridView).Click();
            ScrollIntoView(DescriptionColInGridView(selectedValue, gridView, description));
            DescriptionColInGridView(selectedValue, gridView, description).DoubleClick();
            WaitForAngularJSLoad();
            return this;
        }

        public T ClickOnBlueItem<T>(string gridViewName, string value)
        {
            WaitForElementClickable(_blueItem(gridViewName, value.Trim()));
            BlueItem(gridViewName, value.Trim()).Click();
            SwitchToWindow(WebDriver.WindowHandles.Last());
            return(T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        private void ReplaceValueOfStatusColumn(ref List<KeyValuePair<string, string>> columnValuePairList)
        {
            foreach (var columnValuePair in columnValuePairList)
            {
                if (columnValuePair.Key == "Status")
                {
                    string status = columnValuePair.Value.Split('-')[0].Trim();
                    columnValuePairList.Remove(columnValuePair);
                    columnValuePairList.Add(new KeyValuePair<string, string>("Status", status));
                    break;
                }
            }
        }

        public KeyValuePair<string, bool> ValidatePurchaseItemsAreShown(List<KeyValuePair<string, string>> columnValuePairList)
        {
            var node = StepNode();
            try
            {
                ReplaceValueOfStatusColumn(ref columnValuePairList);
                var PurchaseItemsList = GetAvailablePurchaseItems(columnValuePairList);
                if (PurchaseItemsList.Count > 0)
                    return SetPassValidation(node, Validation.Purchase_Items_Are_Shown);
                return SetFailValidation(node, Validation.Purchase_Items_Are_Shown);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Purchase_Items_Are_Shown, e);
            }
        }

		public KeyValuePair<string, bool> ValidateDeliverablesAreShown(List<KeyValuePair<string, string>> columnValuePairList)
        {
            var node = StepNode();
            try
            {
                ReplaceValueOfStatusColumn(ref columnValuePairList);

                var DeliverableList = GetAvalibleDeliverable(columnValuePairList);
                if (DeliverableList.Count > 0)
                {
                    return SetPassValidation(node, Validation.Deliverables_Are_Shown);
                }
                return SetFailValidation(node, Validation.Deliverables_Are_Shown);
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, Validation.Deliverables_Are_Shown, e);
            }
        }

        public KeyValuePair<string, bool> ValidateItemsCountedAreMatches(string gridViewName)
        {
            var node = StepNode();
            node.Info("Validate that counted items matches");
            int itemListSize = 0;
            int expectedValue = GetTotalItems(gridViewName);
            try
            {
                foreach (IWebElement item in ItemsList(gridViewName))
                    if (item.IsDisplayed())
                        itemListSize += 1;

                while (itemListSize < expectedValue)
                {
                    ArowNextPageInGridPager(gridViewName).HoverAndClickWithJS();
                    foreach (IWebElement item in ItemsList(gridViewName))
                        if (item.IsDisplayed())
                            itemListSize += 1;
                }
                if (itemListSize == expectedValue)
                    return SetPassValidation(node, Validation.Items_Counted_Are_Matches);
                return SetFailValidation(node, Validation.Items_Counted_Are_Matches, expectedValue.ToString(), itemListSize.ToString());
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Items_Counted_Are_Matches, e);
            }
        }

        public KeyValuePair<string, bool> ValidateLineItemIsHighlighted(string gridView, string description)
        {
            var node = StepNode();
            node.Info("Validate that the line item is highlighted");
            try
            {
                SelectedRow(gridView, description.Trim()).HoverElement();
                if (SelectedRow(gridView, description.Trim()).GetAttribute("Class").Contains("HoveredRow"))
                    return SetPassValidation(node, Validation.Line_Items_Is_Highlighted);
                return SetFailValidation(node, Validation.Line_Items_Is_Highlighted);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Line_Items_Is_Highlighted, e);
            }
        }

        public KeyValuePair<string, bool> ValidateSelectedCountStatus(int rowSelectedCount, bool isDeCreased = false)
        {
            var node = StepNode();
            try
            {
                int actualCount = GetSelectedRecordCount();
                if (isDeCreased)
                {
                    if (actualCount == rowSelectedCount)
                        return SetPassValidation(node, Validation.Selected_Count_Is_Decreased);
                    return SetFailValidation(node, Validation.Selected_Count_Is_Decreased, rowSelectedCount.ToString(), actualCount.ToString());
                }
                else
                {
                    if (actualCount == rowSelectedCount)
                        return SetPassValidation(node, Validation.Selected_Count_Is_Increased);
                    return SetFailValidation(node, Validation.Selected_Count_Is_Increased);
                }
            }
            catch (Exception e)
            {
                if (isDeCreased)
                    return SetErrorValidation(node, Validation.Selected_Count_Is_Decreased, e);
                else
                    return SetErrorValidation(node, Validation.Selected_Count_Is_Increased, e);
            }
        }

        public KeyValuePair<string, bool> ValidateBredCrumbTrailDisplayCorrect(string type, bool isContracts = false, string contractNumber = "", string itemID = "", string deliverableLinkItem = "")
        {
            var node = StepNode();
            string bredCrumbTrail = "Vendor Data - ";
            string[] path = { contractNumber, itemID, deliverableLinkItem };
            try
            {
                string expected = bredCrumbTrail;
                for (int item = 0; item < path.Length; item++)
                {
                    if (path[item] != "")
                        expected += (path[item] + " > ");                    
                }
                expected += type;

                string actual = RegisterCaptionLabel.Text;               
                
                if (actual == expected)
                    return SetPassValidation(node, Validation.Bred_Crumb_Trail_Is_Displayed_Correct);
                return SetFailValidation(node, Validation.Bred_Crumb_Trail_Is_Displayed_Correct, expected + type, actual);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Bred_Crumb_Trail_Is_Displayed_Correct, e);
            }
        }

        public KeyValuePair<string, bool> ValidatePageSizeDefaultIsCorrectly(string gridViewName, int expectedSize)
        {
            var node = StepNode();
            try
            {
                string selectedText = "";
                string pageSize = PageSizeBox.GetAttribute("value");
                string[] attributeValues = pageSize.Split(',');

                foreach (var attributeValue in attributeValues)
                {
                    if (attributeValue.Contains("value"))
                    {
                        selectedText = attributeValue.Split(':')[1];
                        selectedText = selectedText.Replace("\"", "");
                    }
                }
                if (int.Parse(selectedText) == expectedSize)
                    return SetPassValidation(node, Validation.Page_Size_Is_Default);
                return SetFailValidation(node, Validation.Page_Size_Is_Default, expectedSize.ToString(), selectedText);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Page_Size_Is_Default, e);
            }
        }

        public List<KeyValuePair<string, bool>> ValidateDataExcelCorrectly(string gridViewName, string excelFilePath, string sheetName = "")
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                ArrowFirstPageInGridPager(gridViewName).ClickWithHandleTimeout();

                int numberOfColumns = RowColumnName(gridViewName).Count;
                int numberOfRows = RowDataTable(gridViewName).Count;
                string expectedHeader = "";
                for (int i = 0; i < numberOfColumns; i++)
                {
                    ScrollIntoView(RowColumnName(gridViewName).ElementAt(i));
                    expectedHeader = expectedHeader + RowColumnName(gridViewName).ElementAt(i).Text + " ";
                }

                List<string> expectedData = GetListRowsInGridView(gridViewName);
                List<string> actualData = new List<string>();
                for (int i = 0; i <= ExcelUtils.GetNumberOfRows(excelFilePath, sheetName); i++)
                    actualData.Add(ExcelUtils.GetExcelRowValue(excelFilePath, sheetName, i + 1));

                if (expectedHeader.Trim() == actualData[0].Trim())
                    validation.Add(SetPassValidation(node, Validation.Data_Excel_are_correctly));
                else
                    validation.Add(SetFailValidation(node, Validation.Data_Excel_are_correctly, expectedHeader.Trim(), actualData[0].Trim()));

                for (int i = 1; i <= actualData.ToArray().Length; i++)
                {
                    string c = actualData[i].Trim();
                    string d = expectedData[i - 1].Trim();
                    if (actualData[i].Trim() != expectedData[i - 1].Trim())
                    {
                        validation.Add(SetFailValidation(node, Validation.Data_Excel_are_correctly, expectedData[i - 1].Trim(), actualData[i].Trim()));
                        break;
                    }
                    else
                        validation.Add(SetPassValidation(node, Validation.Data_Excel_are_correctly));
                }
                return validation;   
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Data_Excel_are_correctly, e));
                return validation;
            }
        }

        private static class Validation
        {
            public static string Purchase_Items_Are_Shown = "Validate that purchase items are shown";
            public static string Items_Counted_Are_Matches = "Validate that number of line items matches the count";
            public static string Line_Items_Is_Highlighted = "Validate that the line item is highlighted.";
            public static string Selected_Count_Is_Increased = "Validate that selected count increased on bottom right corner";
            public static string Selected_Count_Is_Decreased = "Validate that selected count decreased on bottom right corner";
            public static string Deliverables_Are_Shown = "Validate that deliverables are shown";
            public static string Bred_Crumb_Trail_Is_Displayed_Correct = "Validate that bred crumb is displayed correctly";
            public static string Title_Page_Are_Shown_Correctly = "Validate that linking page are shown";
            public static string Return_To_Vendor_Data_Contracts = "Validate that page return Vendor Data - Contracts when clicked the Blue Header";
            public static string Window_Is_Opened = "Validate that window is opened";
            public static string Window_Is_Closed = "Validate that window is closed";
            public static string Page_Size_Is_Default = "Validate that page size default correctly";
            public static string Drop_Down_Items_Are_Shown = "Validate that drop down items are shown";
            public static string Data_Excel_are_correctly = "Validate that data excel are correctly";
        }
        #endregion
    }
}
