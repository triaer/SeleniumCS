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
        private static By _deliverableLineItem => By.XPath("//span[(text()='Deliverable Line Item')]");        
        private static By _purchaseTable => By.XPath("//table[contains(@id,'GridViewItemsVendor')]/thead");
        private static By _deliverableTable => By.XPath("//table[contains(@id,'GridViewItemsVendor') and contains(@id, 'ViewDeliverableVendor')]/thead");        
        private static By _expandButton => By.XPath("//td[@class = 'rgExpandCol']/input");           
        private static By _contractNumber(string contractNumber,string gridView, string contractDescription) => By.XPath($"//span[contains(text(),'{contractNumber}')]/ancestor::tr[contains(@id,'{gridView}_ctl00')]//nobr[contains(text(),'{contractDescription}')]");
        private static By _checkBoxItemContract(string gridView, string description) => By.XPath($"//*[contains(text(),'{description}')]/ancestor::tr[contains(@id,'{gridView}_ctl00')]//input[contains(@id,'{gridView}_ctl00')]");
        private static By _selectedRecordCount => By.XPath("//span[@id='lblRegisterSelectedRecordCount']");
        private static By _blueHeader(string blueHeader) => By.XPath($"//div[@id='lblRegisterCaption']/span[text()='{blueHeader}']");
        private static By _blueItem(string gridView, string itemName) => By.XPath($"//span[@class='HyperLinkWithNoLine']/ancestor::tr[contains(@id,'ctl00_cntPhMain_{gridView}_ctl00')]//span[text()='{itemName}']");
        private static By _itemsList(string gridView) => By.XPath($"//tr[contains(@id,'ctl00_cntPhMain_{gridView}_ctl00')]");
        private static By _totalItem(string gridView) => By.XPath($"//span[contains(@id,'ctl00_cntPhMain_{gridView}_ctl00DSC')]");
        private static By _selectedRow(string gridView, string Description) => By.XPath($"//*[contains(text(),'{Description}')]/ancestor::tr[contains(@id,'{gridView}_ctl00')]");
        private static By _headerTitlePage => By.XPath("//div[@id='lblRegisterCaption']");
        private static By _pageSizeBox => By.XPath("//input[contains(@id,'PageSizeComboBox_ClientState') and contains(@id,'RadGridExpeditingContracts')]");
        private static By _rowData(string gridViewName) => By.XPath($"//a[contains(@onclick,'{gridViewName}')]/ancestor::th[contains(@class,'rgHeader')]");

        private static string _filterItemsXpath = "//table[contains(@id,'GridViewItemsVendor')]//tr[@valign='top']";

        public IWebElement DeliverableTable { get { return StableFindElement(_deliverableTable); } }
        public IWebElement DeliverableLineItem => StableFindElement(_deliverableLineItem);
        public IReadOnlyCollection<IWebElement> ExpandButton { get { return StableFindElements(_expandButton); } }
        public IWebElement PurchaseTable { get { return StableFindElement(_purchaseTable); } }
        public IWebElement ContractNumber(string contractNumber, string gridView, string description) => StableFindElement(_contractNumber(contractNumber, gridView, description));
        public IWebElement CheckBoxItemContract(string gridView, string description) => StableFindElement(_checkBoxItemContract(gridView, description));
        public IWebElement SelectedRecordCount { get { return StableFindElement(_selectedRecordCount); } }
        public IWebElement BlueHeader(string blueHeader) => StableFindElement(_blueHeader(blueHeader));
        public IWebElement BlueItem(string gridViewName, string itemName) => StableFindElement(_blueItem(gridViewName, itemName));
        public IWebElement TotalItem(string gridViewName) => StableFindElement(_totalItem(gridViewName));
        public IWebElement HeaderTitlePage { get { return StableFindElement(_headerTitlePage); } }
        public IWebElement PageSizeBox { get { return FindElement(_pageSizeBox); } }
        public IReadOnlyCollection<IWebElement> RowData(string gridViewName) => StableFindElements(_rowData(gridViewName));
        public IReadOnlyCollection<IWebElement> ItemsList(string gridView) => StableFindElements(_itemsList(gridView));
        public IReadOnlyCollection<IWebElement> SelectedRow(string gridView, string description) => StableFindElements(_selectedRow(gridView, description));

        #endregion

        #region Actions
        public VendorDataRegister(IWebDriver webDriver) : base(webDriver)
        {
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

        public int GetTotalItems(string gridView, bool waitForLoading = true) {
            return int.Parse(TotalItem(gridView).Text);
        }

        public int GetSelectedRecordCount(bool waitForLoading = true) {
            string selectedText = "";
            string selectedCount = SelectedRecordCount.Text;

            selectedText = selectedCount.Split(':')[1];
            selectedText = selectedText.Replace(" ", "");
            return int.Parse(selectedText);
        }
        public string GetHeaderTitle() {
            return  HeaderTitlePage.Text;
        }

        public T ClickOnBlueItem<T>(string gridView, string blueItem)
        {
            var node = StepNode();
            node.Info($"Click on the {blueItem}");

            BlueItem(gridView, blueItem).Click();
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles.Last());
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public VendorDataRegister ClickOnBlueHeader(string blueHeader) {
            var node = StepNode();
            node.Info($"Click on the {blueHeader}");

            BlueHeader(blueHeader).Click();
            return this;
        }

        public VendorDataRegister ClickOnCheckBox(string gridView, string contractDescription, bool uncheck = false, bool waitForLoading = true) {
            if (uncheck)
            {
                var node = StepNode();
                node.Info("Check box a line item");
                CheckBoxItemContract(gridView, contractDescription).Click();
                return this;
            }
            else {
                var node = StepNode();
                node.Info("Uncheck box a line item");
                if (waitForLoading)
                    WaitForLoadingPanel();
                CheckBoxItemContract(gridView, contractDescription).Click();
                return this;
            }
        }

        public VendorDataRegister DoubleClickItem(string contractNumber, string gridViewName, string description, bool waitForLoading = true) {
            var node = StepNode();
            node.Info("Double click on a item");
            if (ContractNumber(contractNumber, gridViewName, description) == null)
            {
                ArowNextPageInGridPager(gridViewName).Click();
                ScrollIntoView(ContractNumber(contractNumber, gridViewName, description));
                ContractNumber(contractNumber, gridViewName, description).DoubleClick();
                return this;
            }

            ScrollIntoView(ContractNumber(contractNumber, gridViewName, description));
            ContractNumber(contractNumber, gridViewName, description).DoubleClick();
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

        public KeyValuePair<string, bool> ValidateItemsCountedAreMatches(string gridViewName, int expectedValue=0)
        {
            var node = StepNode();
            try
            {
                expectedValue = GetTotalItems(gridViewName);
                int itemListSize = 0;
                //int itemListSize = ItemsList(gridView).Count;
                foreach (IWebElement item in ItemsList(gridViewName)) {
                    if (item.IsDisplayed()) {
                        itemListSize += 1;
                    }
                    
                }
                if (itemListSize < expectedValue)
                {
                    ArowNextPageInGridPager(gridViewName).Click();
                    foreach (IWebElement item in ItemsList(gridViewName))
                    {
                        if (item.IsDisplayed())
                        {
                            itemListSize += 1;
                        }
                    }
                }
                    if (itemListSize == expectedValue)
                    return SetPassValidation(node, Validation.Items_Counted_Are_Matches);
                return SetFailValidation(node, Validation.Items_Counted_Are_Matches);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Items_Counted_Are_Matches, e);
            }
        }

        public KeyValuePair<string, bool> ValidateLineItemsIsHighlighted(string gridView, string description, int expectedValue) {
            var node = StepNode();
            try
            {
                expectedValue = GetSelectedRecordCount();
                string actual;
                int count = 0;

                foreach (IWebElement item in SelectedRow(gridView, description)) {
                    actual = item.GetAttribute("class");
                    if ((actual.Contains("Selected"))) {
                        count += 1;
                    }
                }
                if (count == expectedValue)
                    return SetPassValidation(node, Validation.Line_Items_Is_Highlighted);
                return SetFailValidation(node, Validation.Line_Items_Is_Highlighted);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Line_Items_Is_Highlighted, e);
            }
        }

        public KeyValuePair<string, bool> ValidateSelectedCountInCreased(string gridView, string description, int expectedValue) {
            var node = StepNode();
            try
            {
                int count = 0;
                int selectedRow = GetSelectedRecordCount();
                string actual;

                foreach (IWebElement item in SelectedRow(gridView, description))
                {
                    actual = item.GetAttribute("class");
                    if ((actual.Contains("Selected")))
                    {
                       count += 1;
                    }
                }

                if ((expectedValue + count) == selectedRow)
                {
                    return SetPassValidation(node, Validation.Selected_Count_Is_Increased);
                }
                return SetFailValidation(node, Validation.Selected_Count_Is_Increased);

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Selected_Count_Is_Increased, e);
            }
        }

        public KeyValuePair<string, bool> ValidateSelectedCountDeCreased(string gridView, string description, int expectedValue)
        {
            var node = StepNode();
            try
            {

                int selectedRow = GetSelectedRecordCount();



                if (expectedValue  > selectedRow)
                {
                    return SetPassValidation(node, Validation.Selected_Count_Is_Decreased);
                }
                return SetFailValidation(node, Validation.Selected_Count_Is_Decreased);

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Selected_Count_Is_Decreased, e);
            }
        }

        public KeyValuePair<string, bool> ValidateTitlePageAreShownCorrectly(string contractNumber = "", string itemIDNumber = "", string deliverableNumber = "")
        {
            var node = StepNode();
            try
            {
                string title = GetHeaderTitle();
                if (title.Contains("Item Purchased")) {
                    if (title.Equals($"Vendor Data - {contractNumber} > Item Purchased"))
                        return SetPassValidation(node, Validation.Title_Page_Are_Shown_Correctly);
                    return SetFailValidation(node, Validation.Title_Page_Are_Shown_Correctly);
                }
                if (title.Contains("Deliverables")) {
                    if (title.Equals($"Vendor Data - {contractNumber} > {itemIDNumber} > Deliverables"))
                        return SetPassValidation(node, Validation.Title_Page_Are_Shown_Correctly);
                    return SetFailValidation(node, Validation.Title_Page_Are_Shown_Correctly);
                }
                if (title.Contains("Documents")) {
                    if (title.Equals($"Vendor Data - {contractNumber} > {itemIDNumber} > {deliverableNumber} > Documents"))
                        return SetPassValidation(node, Validation.Title_Page_Are_Shown_Correctly);
                    return SetFailValidation(node, Validation.Title_Page_Are_Shown_Correctly);
                }
                if (title.Contains("Contracts")) {
                    if(title.Equals("Vendor Data - Contracts"))
                        return SetPassValidation(node, Validation.Title_Page_Are_Shown_Correctly);
                    return SetFailValidation(node, Validation.Title_Page_Are_Shown_Correctly);
                }
                return SetFailValidation(node, Validation.Title_Page_Are_Shown_Correctly);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Title_Page_Are_Shown_Correctly, e);
            }
        }

        public KeyValuePair<string, bool> ValidateWindowIsOpen(int expectedValue, bool open = true) {
            var node = StepNode();
            try
            {
                if (open) {
                    if (expectedValue < GetWindowHandle()) 
                        return SetPassValidation(node, Validation.Window_Is_Opened);
                    return SetFailValidation(node, Validation.Window_Is_Opened);
                }
                else
                {
                    if (expectedValue == 1)
                        return SetPassValidation(node, Validation.Window_Is_Closed);
                    return SetFailValidation(node, Validation.Window_Is_Closed);
                }
            }
            catch (Exception e)
            {
                if(open)
                    return SetErrorValidation(node, Validation.Window_Is_Opened, e);
                return SetErrorValidation(node, Validation.Window_Is_Closed, e);
            }
        }

        public KeyValuePair<string, bool> ValidatePageSizeDefaultIsCorrectly(string gridViewName, int expectedSize) {
                var node = StepNode();
            try
            {
                //int pageSize = GetPageSize(gridViewName);
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
                return SetFailValidation(node, Validation.Page_Size_Is_Default);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Page_Size_Is_Default, e);
            }
        }

        public KeyValuePair<string, bool> ValidateDataExcelCorrectly(string gridViewName, int rowIndex, string excelFilePath, string sheetName = "")
        {
            var node = StepNode();
            try
            {
                //string expected = RowData(gridViewName).Text;
                string expected = "";
                int numberOfColumns = RowData(gridViewName).Count;
                for (int i = 0; i < numberOfColumns; i++)
                {
                    ScrollIntoView(RowData(gridViewName).ElementAt(i));
                    expected += RowData(gridViewName).ElementAt(i).Text + ",";
                }
                expected = expected.Substring(expected.Length - 1);
                //foreach (IWebElement data in RowData(gridViewName))
                //{
                //    if (data.Displayed == false)
                //        ScrollIntoView(data);
                //    expected += data.Text + ",";
                //}

                //int expected = int.Parse(ItemsNumberLabel(gridViewName).Text);
                //int actual = ExcelUtils.GetNumberkOfRows(excelFilePath, sheetName) - 1;
                string actual = ExcelUtils.GetExcelRowValue( excelFilePath, sheetName, rowIndex);
                if (actual == expected)
                    return SetPassValidation(node, Validation.Data_Excel_are_correctly);
                else
                    return SetFailValidation(node, Validation.Data_Excel_are_correctly, expected.ToString(), actual.ToString());
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Data_Excel_are_correctly, e);
            }
        }

        private static class Validation
        {
            public static string Purchase_Items_Are_Shown = "Validate that purchase items are shown";
            public static string Items_Counted_Are_Matches = "Validate that number of line items matches the count";
            public static string Line_Items_Is_Highlighted = "Validate that the line item is highlighted when clicked checkbox";
            public static string Selected_Count_Is_Increased = "Validate that selected count increased on bottom right corner";
            public static string Selected_Count_Is_Decreased = "Validate that selected count decreased on bottom right corner";
            public static string Deliverables_Are_Shown = "Validate that deliverables are shown";
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
