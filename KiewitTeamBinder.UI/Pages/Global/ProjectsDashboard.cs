﻿using OpenQA.Selenium;
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
using System.Diagnostics;
using KiewitTeamBinder.UI.Pages.PopupWindows;
using System.IO;

namespace KiewitTeamBinder.UI.Pages.Global
{
    public class ProjectsDashboard : LoggedInLanding
    {
        #region Entities     
        private static string _filterItemsXpath = "//tr[@valign='top' and not(contains(@style, 'hidden'))]";
        private static string _headerButtonXpath = "//a[span='{0}']";
        private static string _filterTextBoxXpath = "//table[contains(@id,'{0}')]//tr[@class='rgFilterRow']/td[{1}]";
        private static string _panelXpath = "//div[@id='ctl00_cntPhMain_GridUpdatePanel']/div[@class='GridViewPlaceHolder' and not(contains(@style,'display: none;'))]";

        public By _dashBoardLabel => By.XPath("//span[.='Dashboard']");
        public By _gridViewFilterListData => By.XPath("//div[substring(@id, string-length(@id) - string-length('_rfltMenu_detached') +1) = '_rfltMenu_detached'][contains(@style,'block')]/ul/li");
        public By _columnLabel(string titleColumn) => By.XPath(_panelXpath + $"//a[text() ='{titleColumn}']");
        private static By _sortButton(string titleColumn) => By.XPath(_panelXpath + $"//a[text() ='{titleColumn}']/following::input[1]");
        private static By _nameProjectLabel => By.Id("projectInput");
        private static By _projectListDropdown => By.Id("btnShowProjectList");
        private static By _projectListSumary => By.Id("divProjectSummary");
        private static By _projectListTable => By.XPath("//table[@id='ctl00_MaterToolbar_ProjectGrid_GridViewProjList_ctl00']");
        private static By _helpButtonDropdown => By.XPath("//div[@id='divHelpButton']");
        private static By _helpButtonDropDownData => By.XPath("//div[@id='HelpDropDown_detached']/ul/li");
        private static By _vendorButton => By.Id("divVendorData");
        private static By _viewFilter => By.Id("lblView");
        private static By _formTitle => By.Id("formTitle");
        private static By _subMenuItemLink(string value) => By.XPath($"//span[(text()='{value}')]");
        private static By _moduleButton(string value) => By.XPath($"//div[@id = 'div{value}']");
        private static By _subPageTable(string value) => By.XPath($"//table[@id='ctl00_cntPhMain_{value}_ctl00']");
        private static By _itemsNumberLabel(string gridViewName) => By.XPath($"//span[contains(@id, '{gridViewName}_ctl00DSC')]");
        private static By _divSubMenu => By.XPath("//div[@id='divSubMenu']");
        private static By _subPageHeader => By.Id("lblRegisterCaption");
        private static By _paneTable(string gridViewName) => By.XPath($"//table[contains(@id, '{gridViewName}_ctl00_Header')]/thead");
        private static By _visibleRows(string gridViewName) => By.XPath($"//div[contains(@id, '{gridViewName}_GridData')]//tr[@class != 'rgNoRecords' and not(contains(@style, 'hidden'))]");
        private static By _documentsTable(string gridViewName) => By.XPath($"//div[contains(@id, '{gridViewName}_GridData')]");
        private static By _headerDropdownItem(string itemName) => By.XPath($"//li[a = '{itemName}']");
        private static By _clearHyperlink => By.Id("lblClear");
        private static By _imageOfFilterOptionByIndex(string selected, string index) => By.XPath($"//img[contains(@id,'Link{selected}{index}')]");
        private static By _imageOfFilterOptionByName(string selected, string name) => By.XPath($"//img[contains(@id,'Link{selected}') and contains(@title, '{name}')]");
        private static By _userNameLabel => By.Id("divUserName");
        private static By _rowCountInInfoPartOfGridPager(string gridViewName) => By.XPath($"//table[contains(@id,'{gridViewName}')]//div[contains(@class,'rgInfoPart')]//span[contains(@id,'DSC')]");
        private static By _numberPagesInfoOfGridPager(string gridViewName) => By.XPath($"//table[contains(@id,'{gridViewName}')]//div[contains(@class,'rgInfoPart')]//span[contains(@id,'PCN')]");
        private static By _pageCountInNumPartOfGridPager(string gridViewName) => By.XPath($"//table[contains(@id,'{gridViewName}')]//div[contains(@class,'rgNumPart')]//a");
        private static By _arrowFirstPageInGridPager(string gridViewName) => By.XPath($"//table[contains(@id,'{gridViewName}')]//img[@title='First Page']");
        private static By _arrowLastPageInGridPager(string gridViewName) => By.XPath($"//table[contains(@id,'{gridViewName}')]//img[@title='Last Page']");
        private static By _arrowNextPageInGridPager(string gridViewName) => By.XPath($"//table[contains(@id,'{gridViewName}')]//img[@title='Next Page']");
        private static By _reportsButton => By.Id("btnReports");
        private static By _registerViewCheckbox(string view) => By.XPath($"//a[span = '{view}']/img");
        private static By _currentPageSize(string gridViewName) => By.XPath($"//input[contains(@id, '{gridViewName}') and contains(@id,'PageSizeComboBox_Input')]");
        private static By _rowDataTable(string gridViewName) => By.XPath($"//tr[contains(@id,'{gridViewName}')]");
        private static By _totalItem(string gridView) => By.XPath($"//span[contains(@id,'ctl00_cntPhMain_{gridView}_ctl00DSC')]");
        private static By _gridViewLoading(string gridViewName) => By.XPath($"//div[contains(@id,'{gridViewName}')]//div[@class='raDiv']");
        private static By _gridViewData(string gridViewName) => By.XPath($"//div[contains(@id,'{gridViewName}')]");

        public IWebElement FormTitle { get { return StableFindElement(_formTitle); } }
        public IWebElement ViewFilter { get { return StableFindElement(_viewFilter); } }
        public IWebElement VendorButton { get { return StableFindElement(_vendorButton); } }
        public IWebElement SubPageHeader { get { return StableFindElement(_subPageHeader); } }
        public IWebElement ProjectListDropdown { get { return StableFindElement(_projectListDropdown); } }
        public IWebElement ProjectListSumary { get { return StableFindElement(_projectListSumary); } }
        public IWebElement ProjectListTable { get { return StableFindElement(_projectListTable); } }
        public IWebElement HelpButtonDropDown { get { return StableFindElement(_helpButtonDropdown); } }
        public IWebElement NameProjectLabel { get { return StableFindElement(_nameProjectLabel); } }
        public IWebElement DivSubMenu { get { return StableFindElement(_divSubMenu); } }
        public IWebElement ModuleButton(string value) => StableFindElement(_moduleButton(value));
        public IWebElement SubMenuItemLink(string value) => StableFindElement(_subMenuItemLink(value));
        public IWebElement ItemsNumberLabel(string gridViewName) => StableFindElement(_itemsNumberLabel(gridViewName));
        public IWebElement SubPageTable(string value) => StableFindElement(_subPageTable(value));
        public IWebElement PaneTable(string gridViewName) => StableFindElement(_paneTable(gridViewName));
        public IReadOnlyCollection<IWebElement> VisibleRows(string gridViewName) => StableFindElements(_visibleRows(gridViewName));
        public IWebElement HeaderDropdownItem(string itemName) => StableFindElement(_headerDropdownItem(itemName));
        public IWebElement DocumentsTable(string gridViewName) => StableFindElement(_documentsTable(gridViewName));
        public IWebElement ColumnLabel(string titleColumn) => StableFindElement(_columnLabel(titleColumn));
        public IWebElement SortButton(string titleColumn) => FindElement(_sortButton(titleColumn));
        public IWebElement ClearHyperlink { get { return StableFindElement(_clearHyperlink); } }
        public IWebElement ImageOfFilterOptionByIndex(string selected, string index) => StableFindElement(_imageOfFilterOptionByIndex(selected, index));
        public IWebElement ImageOfFilterOptionByName(string selected, string name) => StableFindElement(_imageOfFilterOptionByName(selected, name));
        public IWebElement UserNameLabel { get { return StableFindElement(_userNameLabel); } }
        public IWebElement RowCountInInfoPartOfGridPager(string gridViewName) => StableFindElement(_rowCountInInfoPartOfGridPager(gridViewName));
        public IReadOnlyCollection<IWebElement> PageCountInNumPartOfGridPager(string gridViewName) => StableFindElements(_pageCountInNumPartOfGridPager(gridViewName));
        public IWebElement NumberPagesInfoOfGridPager(string gridViewName) => StableFindElement(_numberPagesInfoOfGridPager(gridViewName));
        public IWebElement ArrowFirstPageInGridPager(string gridViewName) => StableFindElement(_arrowFirstPageInGridPager(gridViewName));
        public IWebElement ArrowLastPageInGridPager(string gridViewName) => StableFindElement(_arrowLastPageInGridPager(gridViewName));
        public IWebElement ArowNextPageInGridPager(string gridViewName) => StableFindElement(_arrowNextPageInGridPager(gridViewName));
        public IWebElement ReportsButton { get { return StableFindElement(_reportsButton); } }
        public IWebElement RegisterViewCheckbox(string gridViewName) => StableFindElement(_registerViewCheckbox(gridViewName));
        public IWebElement CurrentPageSize(string gridViewName) => StableFindElement(_currentPageSize(gridViewName));
        public IReadOnlyCollection<IWebElement> RowDataTable(string gridViewName) => StableFindElements(_rowDataTable(gridViewName));
        public IWebElement TotalItem(string gridViewName) => StableFindElement(_totalItem(gridViewName));
        public IWebElement GridViewLoading(string gridViewName) => StableFindElement(_gridViewLoading(gridViewName));
        #endregion

        #region Actions
        public ProjectsDashboard(IWebDriver webDriver) : base(webDriver)
        { }
        public int GetCountWindow()
        {
            var node = StepNode();
            test.Info("Get count window");
            return WebDriver.WindowHandles.Count;
        }
        public int GetPageSize(string gridViewName) {
            return int.Parse(CurrentPageSize(gridViewName).Text);
        }
        public int GetTotalItems(string gridView)
        {
            return int.Parse(TotalItem(gridView).Text);
        }

        public void WaitForLoadPage(string gridViewName)
        {
            while (true)
            {
                if (GridViewLoading(gridViewName) != null)
                    WaitForElement(_gridViewData(gridViewName));
                else
                    break;
            }
        }
        public List<string> GetListRowsInGridView(string gridViewName)
        {
            List<string> listRows = new List<string>();
            int numberOfRows = RowDataTable(gridViewName).Count;
            int countRow = 0;
            while (countRow < GetTotalItems(gridViewName))
            {
                for (int i = 0; i < numberOfRows; i++)
                {
                    if (RowDataTable(gridViewName).ElementAt(i).IsDisplayed())
                    {
                        ScrollIntoView(RowDataTable(gridViewName).ElementAt(i));
                        listRows.Add(RowDataTable(gridViewName).ElementAt(i).Text);
                        countRow += 1;
                    }
                }
                if (countRow < GetTotalItems(gridViewName))
                {
                    ArowNextPageInGridPager(gridViewName).HoverAndClickWithJS();
                    WaitForLoadPage(gridViewName);
                    numberOfRows = RowDataTable(gridViewName).Count;
                }                   
            }
            return listRows;
        }

        public ProjectsDashboard ShowProjectList()
        {
            ProjectListDropdown.Click();
            WaitForElementAttribute(ProjectListSumary, "display", "block");
            return this;
        }

        private void ClickMenuItem(string menuItem)
        {
            var node = StepNode();
            node.Info($"Click on the root node: {menuItem}");
            ModuleButton(menuItem).Click();
        }

        private void ClickSubMenuItem(string subMenuItem)
        {
            var node = StepNode();
            node.Info($"Click on the sub node: {subMenuItem}");
            SubMenuItemLink(subMenuItem).Click();
        }

        public T SelectFilterOption<T>(string nameOrIndex, bool byName = true, bool waitForLoading = true)
        {
            var node = StepNode();
            if (byName)
                ImageOfFilterOptionByName("NotSelected", nameOrIndex).Click();
            else
                ImageOfFilterOptionByIndex("NotSelected", nameOrIndex).Click();

            if (waitForLoading)
                WaitForLoadingPanel();

            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T SelectModuleMenuItemOnLeftNav<T>(string menuItem = "", string subMenuItem = "", bool waitForLoading = true)
        {
            if (menuItem != "")
                ClickMenuItem(menuItem);

            if (subMenuItem != "")
                ClickSubMenuItem(subMenuItem);

            if (waitForLoading)
            {
                WaitForLoadingPanel();
                if (menuItem != ModuleNameInLeftNav.DASHBOARD.ToDescription())
                    WaitForElement(_subPageHeader);
            }

            return (T)Activator.CreateInstance(typeof(T), WebDriver);            
        }

        public HelpAboutDialog OpenHelpDialog(string option)
        {
            WaitForElementClickable(_helpButtonDropdown);
            SelectComboboxByText(HelpButtonDropDown, _helpButtonDropDownData, option);
            var helpAboutDialog = new HelpAboutDialog(WebDriver);
            WebDriver.SwitchTo().Frame(helpAboutDialog.IFrameName);
            WaitUntil(driver => helpAboutDialog.OkButton != null);

            return helpAboutDialog;
        }

        public StandardReports OpenStandardReportsWindow(bool closePreviousWindow = false)
        {
            var node = StepNode();
            string currentWindow;
            SwitchToNewPopUpWindow(ReportsButton, out currentWindow, closePreviousWindow);
            node.Info("Click Reports in Top Right Corner of Dashboard, then Report window opens");
            return new StandardReports(WebDriver);
        }

        public T ClickClearHyperlink<T>(bool waitForLoading = true)
        {
            ClearHyperlink.Click();
            if (waitForLoading)
                WaitForLoadingPanel();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T ClickHeaderButton<T>(MainPaneTableHeaderButton buttonName, bool waitForLoading = true, string tableName = null)
        {
            var node = StepNode();
            IWebElement Button = StableFindElement(By.XPath(string.Format(_headerButtonXpath, buttonName.ToDescription())));
            node.Info("Click the button: " + buttonName.ToDescription());
            Button.HoverAndClickWithJS();
            if (waitForLoading && tableName != null)
                WaitForElement(_subPageTable(tableName));
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T SelectItemOnHeaderDropdown<T>(MainPaneHeaderDropdownItem item)
        {
            var node = StepNode();
            node.Info("Selected item: " + item.ToDescription());
            HeaderDropdownItem(item.ToDescription()).Click();
            WaitForAngularJSLoad();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }
        public T SelectDropdownItemWithSwitchWindow<T>(MainPaneHeaderDropdownItem item)
        {
            var node = StepNode();
            node.Info($"Click the item: {item.ToDescription()}, and switch to new window popup");
           
            string currentWindow;
            SwitchToNewPopUpWindow(HeaderDropdownItem(item.ToDescription()), out currentWindow, false);
            try
            {
                WaitForJQueryLoad();
                WaitForElementDisplay(_walkMe);
            }
            catch { }
            
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T SelectDropdownItemWithSwitchDialog<T>(MainPaneHeaderDropdownItem item)
        {
            var node = StepNode();
            node.Info($"Click the item: {item.ToDescription()}, and switch to dialog popup");

            SelectItemOnHeaderDropdown<T>(item);
            WebDriver.SwitchTo().ActiveElement();
            
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T ClickHeaderDropdownItem<T>(MainPaneHeaderDropdownItem item, bool switchWindow, bool switchPopUp = false, bool walkMe = true)
        {
            var node = StepNode();
            node.Info("Click the item: " + item.ToDescription());

            if (switchWindow)
            {
                string currentWindow;
                SwitchToNewPopUpWindow(HeaderDropdownItem(item.ToDescription()), out currentWindow, false);
            }
            else
                HeaderDropdownItem(item.ToDescription()).Click();

            if (switchPopUp)
                WebDriver.SwitchTo().ActiveElement();

            if (walkMe)
                WaitForElementDisplay(_walkMe);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T HoverHeaderDropdownItem<T>(MainPaneHeaderDropdownItem item)
        {
            var node = StepNode();
            node.Info("Hover the item: " + item.ToDescription());
            HeaderDropdownItem(item.ToDescription()).HoverElement();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T ClickHeaderLabelToSort<T>(MainPaneTableHeaderLabel label)
        {
            var node = StepNode();
            node.Info("Click the label of column: " + label.ToDescription());
            ColumnLabel(label.ToDescription()).Click();
            WaitForAngularJSLoad();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public string GetUserNameLogon()
        {
            return UserNameLabel.Text;
        }

        public T FilterDocumentsByGridFilterRow<T>(string gridViewName, string columnName, string value, bool useFilterMenu = false, FilterOptions optionItem = FilterOptions.Contains, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info($"Filter the '{columnName}' column with value '{value}'");
            int rowIndex, colIndex = 1;
            GetTableCellValueIndex(PaneTable(gridViewName), columnName, out rowIndex, out colIndex, "th");

            IWebElement FilterCell = StableFindElement(By.XPath(string.Format(_filterTextBoxXpath, gridViewName, colIndex)));
            IWebElement FilterTextBox = FilterCell.StableFindElement(By.TagName("input"));
            FilterTextBox.InputText(value);
            if (!useFilterMenu)
            {
                try
                {
                    FilterTextBox.SendKeys(Keys.Enter);
                    Wait(shortTimeout / 5);
                    FilterTextBox.SendKeys(Keys.Enter);
                }
                catch { }
            }
            else
            {
                IWebElement FilterMenu = FilterCell.StableFindElement(By.TagName("img"));
                SelectComboboxByText(FilterMenu, _gridViewFilterListData, optionItem.ToDescription());
            }
            WaitForJQueryLoad();
            if (waitForLoading)
                WaitForLoadingPanel();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        protected IReadOnlyCollection<IWebElement> GetAvailableItemsOnCurrentPage(string gridViewName, List<KeyValuePair<string, string>> columnValuePairList, bool contains = true)
        {
            int rowIndex, colIndex = 1;
            string itemsXpath = _filterItemsXpath;
            GetTableCellValueIndex(PaneTable(gridViewName), columnValuePairList.ElementAt(0).Key, out rowIndex, out colIndex, "th");
            if (colIndex < 2)
                return null;
            itemsXpath += (contains) ? $"[td[{colIndex}][contains(., '{columnValuePairList.ElementAt(0).Value}')]"
                                     : $"[td[{colIndex}][not(contains(., '{columnValuePairList.ElementAt(0).Value}'))]";

            for (int i = 1; i < columnValuePairList.Count; i++)
            {
                GetTableCellValueIndex(PaneTable(gridViewName), columnValuePairList.ElementAt(i).Key, out rowIndex, out colIndex, "th");
                if (colIndex < 2)
                    return null;
                itemsXpath += (contains) ? $" and td[{colIndex}][contains(., '{columnValuePairList.ElementAt(i).Value}')]"
                                         : $" and td[{colIndex}][not(contains(., '{columnValuePairList.ElementAt(i).Value}'))]";
            }
            itemsXpath += "]";

            return StableFindElements(By.XPath(itemsXpath));
        }

        protected T SelectRowCheckbox<T>(IWebElement Checkbox, bool check = true)
        {
            if (check)
                Checkbox.Check();
            else
                Checkbox.UnCheck();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public int GetTableItemNumberWithConditions(string gridViewName, List<KeyValuePair<string, string>> columnValuePairList)
        {
            List<IWebElement> AvailableItems = new List<IWebElement> { };
            try
            {
                //get pages count
                List<IWebElement> GridPages = PageCountInNumPartOfGridPager(gridViewName).ToList();
                for (int i = 0; i < GridPages.Count; i++)
                {
                    IReadOnlyCollection<IWebElement> matchedRows = GetAvailableItemsOnCurrentPage(gridViewName, columnValuePairList);
                    if (matchedRows != null)
                        AvailableItems.AddRange(matchedRows.ToList());
                    if ((i + 1) != GridPages.Count)
                    {
                        GridPages[i + 1].Click();
                        WaitForLoadingPanel();
                        GridPages = PageCountInNumPartOfGridPager(gridViewName).ToList();
                    }
                }
                if (GridPages.Count > 1)
                {
                    ArrowFirstPageInGridPager(gridViewName).Click();
                    WaitForLoadingPanel();
                }

                return AvailableItems.Count;
            }
            catch
            {
                return 0;
            }
        }

        public int GetTotalRowsVisibleInGrid(string gridViewName)
        {
            var node = StepNode();
            int pagesCount, rowsCount;
            try
            {
                //get count of pages
                pagesCount = int.Parse(NumberPagesInfoOfGridPager(gridViewName).Text);
                if (pagesCount > 1)
                {
                    ArrowLastPageInGridPager(gridViewName).Click();
                    //WaitForLoadingPanel();
                    WaitForLoadPage(gridViewName);
                    rowsCount = (pagesCount - 1) * int.Parse(CurrentPageSize(gridViewName).GetAttribute("value")) + VisibleRows(gridViewName).Count;
                    //return back 1st page
                    ArrowFirstPageInGridPager(gridViewName).Click();
                    //WaitForLoadingPanel(longTimeout);
                    WaitForLoadPage(gridViewName);
                }
                else
                    rowsCount = VisibleRows(gridViewName).Count;
                node.Info("Get total number of items in table: " + rowsCount);
                return rowsCount;
            }
            catch
            {
                return 0;
            }
        }

        private int GetOptionFilterIndex(string OptionFilterName)
        {
            try
            {
                string id = ImageOfFilterOptionByName("Selected", OptionFilterName).GetAttribute("id");
                return int.Parse(id[id.Length - 1].ToString()) + 1;
            }
            catch
            {
                return -1;
            }
        }

        public KeyValuePair<string, bool> ValidateColumnIsSorted(string columnName)
        {
            var node = StepNode();
            node.Info("Validate that the column is sorted");
            try
            {
                if (SortButton(columnName).IsDisplayed())
                    return SetPassValidation(node, Validation.Column_Is_Sorted);
                else
                    return SetFailValidation(node, Validation.Column_Is_Sorted);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Column_Is_Sorted, e);
            }
        }

        protected KeyValuePair<string, bool> ValidateProgressContentMessage(string message)
        {
            var node = StepNode();
            IWebElement DialogMessage = FindElement(_progressMessage);
            var actual = DialogMessage.GetAttribute("innerHTML");
            if (actual.Contains(message))
                return SetPassValidation(node, Validation.Progress_Message_Is_Displayed + message);
            else
                return SetFailValidation(node, Validation.Progress_Message_Is_Displayed, message, actual);
        }
        public KeyValuePair<string, bool> ValidateExcelItemsCount(string gridViewName, string excelFilePath, string sheetName = "")
        {
            Wait(longTimeout);
            var node = StepNode();
            try
            {
                DirectoryInfo downloadedFolder = new DirectoryInfo(Utils.GetDownloadFilesLocalPath());
                FileInfo[] Files = downloadedFolder.GetFiles("*.*");
                string filesName = "";
                List<string> listValueExpected = GetListRowsInGridView(gridViewName);

                foreach (var file in Files)
                    filesName += file.Name + ", ";
                node.Info("Files in DownloadedFiles folder: " + filesName);
                node.Info("Expected path: " + excelFilePath);

                int expected = int.Parse(ItemsNumberLabel(gridViewName).Text);
                
                if (listValueExpected.Last().Trim().Contains("N/A"))
                    expected = expected - 1;

                int actual = ExcelUtils.GetNumberOfRows(excelFilePath, sheetName);

                //TO-DO: the number of items on web (includes N/A item) does not match with number of item in Excel file.
                //we temporarily use (expected - 1) to compare
                if (actual == expected)
                    return SetPassValidation(node, Validation.Excel_Items_Count);
                else
                    return SetFailValidation(node, Validation.Excel_Items_Count, expected.ToString(), actual.ToString());
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Excel_Items_Count, e);
            }
        }

        public KeyValuePair<string, bool> ValidateRegisterViewIsCorrect(string expectedView)
        {
            var node = StepNode();
            try
            {
                if (RegisterViewCheckbox(expectedView).Displayed)
                    return SetPassValidation(node, Validation.Register_View_Is_Correct);
                else
                    return SetFailValidation(node, Validation.Register_View_Is_Correct);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Register_View_Is_Correct, e);
            }
        }

        public KeyValuePair<string, bool> ValidateRecordsMatchingFilterAreReturned(string gridViewName, List<KeyValuePair<string, string>> ValueInColumn, int expectedNumberOfRecord)
        {
            var node = StepNode();
            try
            {
                int totalRecords = GetTotalRowsVisibleInGrid(gridViewName);

                if (totalRecords == expectedNumberOfRecord)
                    return SetPassValidation(node, Validation.Records_Matching_Filter_Are_Returned + totalRecords);
                else
                    return SetFailValidation(node, Validation.Records_Matching_Filter_Are_Returned, expectedNumberOfRecord.ToString(), totalRecords.ToString());
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Records_Matching_Filter_Are_Returned, e);
            }
        }

        public KeyValuePair<string, bool> ValidateValueInColumnIsCorrect(string gridViewName, string columnName, string value)
        {
            var node = StepNode();
            try
            {
                int totalRecords = GetTotalRowsVisibleInGrid(gridViewName);
                var columnValuePairList = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(columnName, value) };
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
                int numberOfCorrectRow = GetTableItemNumberWithConditions(gridViewName, columnValuePairList);

                if (totalRecords == numberOfCorrectRow)
                    return SetPassValidation(node, Validation.Value_In_Column_Is_Correct + numberOfCorrectRow);
                else
                    return SetFailValidation(node, Validation.Value_In_Column_Is_Correct, totalRecords.ToString(), numberOfCorrectRow.ToString());
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Value_In_Column_Is_Correct, e);
            }
        }

        public KeyValuePair<string, bool> ValidateFilteredRecordsAreCleared(string gridViewName, int expectedNumberOfClearRecord)
        {
            var node = StepNode();
            try
            {
                int totalRecords = GetTotalRowsVisibleInGrid(gridViewName);

                if (totalRecords == expectedNumberOfClearRecord)
                    return SetPassValidation(node, Validation.Filtered_Records_Are_Cleared + totalRecords.ToString());
                else
                    return SetFailValidation(node, Validation.Filtered_Records_Are_Cleared, expectedNumberOfClearRecord.ToString(), totalRecords.ToString());
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Filtered_Records_Are_Cleared, e);
            }
        }

        public KeyValuePair<string, bool> ValidateProjectIsOpened(string nameProject)
        {
            var node = StepNode();
            try
            {
                if (NameProjectLabel.GetAttribute("title").Equals(nameProject))
                    return SetPassValidation(node, Validation.Project_Is_Opened);
                else
                    return SetFailValidation(node, Validation.Project_Is_Opened, nameProject, NameProjectLabel.GetAttribute("title"));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Project_Is_Opened, e);
            }
        }

        public KeyValuePair<string, bool> ValidateDisplayedSubItemLinks(string[] subItemLinks)
        {
            var node = StepNode();
            node.Info("Validate all sub item links are displayed");
            try
            {
                foreach (var item in subItemLinks)
                {
                    if (SubMenuItemLink(item).IsDisplayed() == false)
                        return SetFailValidation(node, Validation.Sub_Item_links_Are_Displayed);
                }
                return SetPassValidation(node, Validation.Sub_Item_links_Are_Displayed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Sub_Item_links_Are_Displayed, e);
            }
        }

        public KeyValuePair<string, bool> ValidateDisplayedViewFilterOption(string filterOption)
        {
            var node = StepNode();
            try
            {
                WaitForElementDisplay(_viewFilter);
                if (ViewFilter.Text == filterOption)
                    return SetPassValidation(node, string.Format(Validation.Default_Filter_Display, filterOption));

                return SetFailValidation(node, string.Format(Validation.Default_Filter_Display, filterOption));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Default_Filter_Display, filterOption), e);
            }
        }

        // moduleName: Mail/Transmittals/Vendor Data
        public KeyValuePair<string, bool> ValidateRecordItemsCount(string gridViewName)
        {
            int itemsNumber = GetTotalRowsVisibleInGrid(gridViewName);
            var node = StepNode();
            node.Info($"Validate number of record items is equals to: {itemsNumber}");
            try
            {
                var actualQuantity = ItemsNumberLabel(gridViewName).Text;
                if (Int32.Parse(actualQuantity) == itemsNumber)
                    return SetPassValidation(node, Validation.Number_Of_Items_Counted_Is_Valid + actualQuantity);

                return SetFailValidation(node, Validation.Number_Of_Items_Counted_Is_Valid, itemsNumber.ToString(), actualQuantity);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Number_Of_Items_Counted_Is_Valid, e);
            }
        }

        public KeyValuePair<string, bool> ValidateRecordItemsCount(string gridViewName, int expectedCount)
        {
            var node = StepNode();
            node.Info($"Validate number of record items is equals to: {expectedCount}");
            try
            {
                var actualQuantity = ItemsNumberLabel(gridViewName).Text;
                if (Int32.Parse(actualQuantity) == expectedCount)
                    return SetPassValidation(node, Validation.Number_Of_Items_Counted_Is_Valid + actualQuantity);

                return SetFailValidation(node, Validation.Number_Of_Items_Counted_Is_Valid, expectedCount.ToString(), actualQuantity);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Number_Of_Items_Counted_Is_Valid, e);
            }
        }

        public KeyValuePair<string, bool> ValidateFilterBoxIsHighlighted(int filterBoxIndex, bool isHighlighted = true)
        {
            var node = StepNode();
            node.Info("Validate the view filter checkbox is highlighted.");
            try
            {
                filterBoxIndex = Utils.RefactorIndex(filterBoxIndex);
                string highlight = "Selected";
                if (!isHighlighted)
                    highlight = "NotSelected";

                if (ImageOfFilterOptionByIndex(highlight, filterBoxIndex.ToString()).IsDisplayed())
                    return SetPassValidation(node, string.Format(Validation.Filter_Box_Is_Highlighted, filterBoxIndex + 1));

                return SetFailValidation(node, string.Format(Validation.Filter_Box_Is_Highlighted, filterBoxIndex + 1));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Filter_Box_Is_Highlighted, filterBoxIndex + 1), e);
            }
        }

        public KeyValuePair<string, bool> ValidateFilterBoxIsHighlighted(string filterBoxName, bool isHighlighted = true)
        {
            var node = StepNode();
            node.Info("Validate the view filter checkbox is highlighted.");
            try
            {
                string highlight = "Selected";
                if (!isHighlighted)
                    highlight = "NotSelected";

                if (ImageOfFilterOptionByName(highlight, filterBoxName).IsDisplayed())
                    return SetPassValidation(node, string.Format(Validation.Filter_Box_Is_Highlighted, filterBoxName));

                return SetFailValidation(node, string.Format(Validation.Filter_Box_Is_Highlighted, filterBoxName));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Filter_Box_Is_Highlighted, filterBoxName), e);
            }
        }

        public KeyValuePair<string, bool> ValidateWindowIsOpened(string windowName)
        {
            var node = StepNode();
            try
            {
                if (WebDriver.Title == windowName)
                    return SetPassValidation(node, string.Format(Validation.Window_Is_Opened, windowName));
                else
                    return SetFailValidation(node, string.Format(Validation.Window_Is_Opened, windowName, WebDriver.Title));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Window_Is_Opened, windowName), e);
            }
        }

        public KeyValuePair<string, bool> ValidateFormTitle(string formTitle)
        {
            var node = StepNode();
            try
            {
                if (FormTitle.Text == formTitle)
                    return SetPassValidation(node, string.Format(Validation.Form_Title_Is_Correct, formTitle));
                else
                    return SetFailValidation(node, string.Format(Validation.Form_Title_Is_Correct, formTitle));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Form_Title_Is_Correct, formTitle), e);
            }
        }

        public KeyValuePair<string, bool> ValidateSubPageIsDislayed(string subPage)
        {
            var node = StepNode();
            node.Info($"Validate sub page '{subPage}' is displayed.");
            try
            {
                if (SubPageHeader.Text == subPage)
                    return SetPassValidation(node, Validation.Sub_Page_Is_Displayed);
                else
                    return SetFailValidation(node, Validation.Sub_Page_Is_Displayed, $"Actutal Sub Page Is: {SubPageHeader.Text}");
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Sub_Page_Is_Displayed, e);
            }
        }

        public KeyValuePair<string, bool> ValidateItemsAreNotShown(string columnName, string value, string gridViewName)
        {
            var node = StepNode();
            FilterDocumentsByGridFilterRow<ProjectsDashboard>(gridViewName, columnName, value);
            int itemsNumber = GetTotalRowsVisibleInGrid(gridViewName);
            try
            {
                if (itemsNumber > 0)
                    return SetFailValidation(node, Validation.Item_Is_Not_Displayed);
                return SetPassValidation(node, Validation.Item_Is_Not_Displayed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Item_Is_Not_Displayed, e);
            }
        }

        public KeyValuePair<string, bool> ValidateItemsAreShown(List<KeyValuePair<string, string>> columnValuePairList, string gridViewName)
        {
            var node = StepNode();
            try
            {
                if (GetTableItemNumberWithConditions(gridViewName, columnValuePairList) > 0)
                    return SetPassValidation(node, Validation.Items_Are_Shown);
                return SetFailValidation(node, Validation.Items_Are_Shown);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Items_Are_Shown, e);
            }
        }

        private static class Validation
        {
            public static string Item_Is_Not_Displayed = "Validate that item on main pane is not displayed";
            public static string Project_Is_Opened = "Validate that the project is opened";
            public static string Vendor_Data_Menus_Display = "Validate that the vendor data sub-menus display correct";
            public static string Default_Filter_Display = "Validate that the view filter in upper right corner is defaulted to the {0}";
            public static string Filter_Box_Is_Highlighted = "Validate that the {0} filter box is highlighted";
            public static string Window_Is_Opened = "Validate that {0} window is opened";
            public static string Form_Title_Is_Correct = "Validate that form title is {0}";
            public static string Sub_Item_links_Are_Displayed = "Validate that sub item links are displayed";
            public static string Number_Of_Items_Counted_Is_Valid = "Validate that item count in bottom right corner of the grid matches actual count of items shown in grid content: ";
            public static string Sub_Page_Is_Displayed = "Validate that the sub page is displayed";
            public static string Items_Are_Shown = "Validate that items on main pane are shown";
            public static string Progress_Message_Is_Displayed = "Validate that the progress message display: ";
            public static string Records_Matching_Filter_Are_Returned = "Validate that the records matching filter are returned: ";
            public static string Value_In_Column_Is_Correct = "Validate that the value in column is correct: ";
            public static string Filtered_Records_Are_Cleared = "Validate that the filtered records are cleared, and return the total records before filtering: ";
            public static string Register_View_Is_Correct = "Validate that the register view is correct";
            public static string Excel_Items_Count = "Validate that the excel items count matches the items count on the Web";
            public static string Column_Is_Sorted = "Validate that the column is sorted";
        }
        #endregion
    }
}
