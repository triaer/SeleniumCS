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

namespace KiewitTeamBinder.UI.Pages.Global
{
    public class ProjectsDashboard : LoggedInLanding
    {
        #region Entities              
        public By _dashBoardLabel => By.XPath("//span[.='Dashboard']");
        public By _gridViewFilterListData => By.XPath("//div[substring(@id, string-length(@id) - string-length('_rfltMenu_detached') +1) = '_rfltMenu_detached'][contains(@style,'block')]/ul/li");
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
        private static By _itemsNumberLabel(string value) => By.XPath($"//span[contains(@id, '{value}_ctl00DSC')]");
        private static By _divSubMenu => By.XPath("//div[@id='divSubMenu']");        
        private static By _subPageHeader => By.Id("lblRegisterCaption");              
        private static By _paneTable(string gridViewName) => By.XPath($"//table[contains(@id, '{gridViewName}_ctl00_Header')]/thead");
        private static By _visibleRows(string gridViewName) => By.XPath($"//div[contains(@id, '{gridViewName}_GridData')]//tr[@class != 'rgNoRecords' and not(contains(@style, 'hidden'))]");
        private static By _documentsTable(string gridViewName) => By.XPath($"//div[contains(@id, '{gridViewName}_GridData')]");
        private static By _headerDropdownItem(string itemName) => By.XPath($"//li[a = '{itemName}']");
        private static By _sortButton(string titleColumn) => By.XPath($"//a[text() ='{titleColumn}']");
        private static By _loadingPanel => By.XPath("//div[contains(@id, 'LoadingPanel')]");
        private static By _clearHyperlink => By.Id("lblClear");
        private static By _imageOfFilterOptionByIndex(string selected, string index) => By.XPath($"//img[contains(@id,'Link{selected}{index}')]");
        private static By _imageOfFilterOptionByName(string selected, string name) => By.XPath($"//img[contains(@id,'Link{selected}') and contains(@title, '{name}')]");
        private static By _userNameLabel => By.Id("divUserName");
        private static By _rowCountInInfoPartOfGridPager(string gridViewName) => By.XPath($"//table[contains(@id,'{gridViewName}')]//div[contains(@class,'rgInfoPart')]//span[contains(@id,'DSC')]");
        private static By _pageCountInNumPartOfGridPager(string gridViewName) => By.XPath($"//table[contains(@id,'{gridViewName}')]//div[contains(@class,'rgNumPart')]//a");

        private static string _filterItemsXpath = "//tr[@valign='top' and not(contains(@style, 'hidden'))]";        
        private string _headerButtonXpath = "//a[span='{0}']";

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
        public IWebElement ItemsNumberLabel(string value) => StableFindElement(_itemsNumberLabel(value));
        public IWebElement SubPageTable(string value) => StableFindElement(_subPageTable(value));
        public IWebElement PaneTable(string gridViewName) => StableFindElement(_paneTable(gridViewName));
        public IReadOnlyCollection<IWebElement> VisibleRows(string gridViewName) => StableFindElements(_visibleRows(gridViewName));
        public IWebElement HeaderDropdownItem(string itemName) => StableFindElement(_headerDropdownItem(itemName));
        public IWebElement DocumentsTable(string gridViewName) => StableFindElement(_documentsTable(gridViewName));
        public IWebElement SortButton(string titleColumn) => StableFindElement(_sortButton(titleColumn));
        public IWebElement LoadingPanel { get { return StableFindElement(_loadingPanel); } }
        public IWebElement ClearHyperlink { get { return StableFindElement(_clearHyperlink); } }
        public IWebElement ImageOfFilterOptionByIndex(string selected, string index) => StableFindElement(_imageOfFilterOptionByIndex(selected, index));
        public IWebElement ImageOfFilterOptionByName(string selected, string name) => StableFindElement(_imageOfFilterOptionByName(selected, name));
        public IWebElement UserNameLabel { get { return StableFindElement(_userNameLabel); } }
        public IWebElement RowCountInInfoPartOfGridPager(string gridViewName) => StableFindElement(_rowCountInInfoPartOfGridPager(gridViewName));
        public IReadOnlyCollection<IWebElement> PageCountInNumPartOfGridPager(string gridViewName) => StableFindElements(_pageCountInNumPartOfGridPager(gridViewName));
        #endregion

        #region Actions
        public ProjectsDashboard(IWebDriver webDriver) : base(webDriver)
        {  }

        public ProjectsDashboard ShowProjectList()
        {
            ProjectListDropdown.Click();
            WaitForElementAttribute(ProjectListSumary, "display", "block");
            return this;
        }       
        
        public void WaitForLoadingPanel()
        {
            WaitForLoading(_loadingPanel);
        }
            
        private void ClickMenuItem(string menuItem)
        {
            var node = StepNode();
            node.Info($"Click on the root node: {menuItem}");
            ModuleButton(menuItem).Click();
            WaitForElement(_divSubMenu);
        }

        private void ClickSubMenuItem(string subMenuItem)
        {
            var node = StepNode();
            node.Info($"Click on the sub node: {subMenuItem}");
            SubMenuItemLink(subMenuItem).Click();
            WaitForElement(_subPageHeader);
        }

        public T SelectFilterOption<T>(string nameOrIndex, bool byName = true, bool waitForLoading = true)
        {
            if (byName)            
                ImageOfFilterOptionByName("NotSelected", nameOrIndex).Click();            
            else            
                ImageOfFilterOptionByIndex("NotSelected", nameOrIndex).Click();

            if (waitForLoading)
                WaitForLoadingPanel();

            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        private ProjectsDashboard SelectModuleMenuItem(string menuItem, string subMenuItem)
        {
            if (menuItem != "")
                ClickMenuItem(menuItem);
            
            if (subMenuItem != "")
                ClickSubMenuItem(subMenuItem);

            return this;
        }

        public T SelectModuleMenuItem<T>(string menuItem = "", string subMenuItem = "", bool waitForLoading = true)
        {
            SelectModuleMenuItem(menuItem, subMenuItem);
            if (waitForLoading)
                WaitForLoadingPanel();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);            
        }

        public HelpAboutDialog OpenHelpDialog(string option)
        {
            SelectComboboxByText(HelpButtonDropDown, _helpButtonDropDownData, option);
            var helpAboutDialog = new HelpAboutDialog(WebDriver);
            WebDriver.SwitchTo().Frame(helpAboutDialog.IFrameName);
            WaitUntil(driver => helpAboutDialog.OkButton != null);
            
            return helpAboutDialog;
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
            IWebElement Button = StableFindElement(By.XPath(string.Format(_headerButtonXpath, buttonName.ToDescription())));
            var node = StepNode();
            node.Info("Click the button: " + buttonName.ToDescription());
            Button.HoverAndClickWithJS();

            if (waitForLoading && tableName != null)
                WaitForElement(_subPageTable(tableName));
            
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }
                
        public T ClickHeaderDropdownItem<T>(MainPaneHeaderDropdownItem item)
        {
            var node = StepNode();
            node.Info("Click the item: " + item.ToDescription());
            HeaderDropdownItem(item.ToDescription()).Click();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public string GetUserNameLogon()
        {
            return UserNameLabel.Text;
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


        protected IReadOnlyCollection<IWebElement> GetAvailableItemsOnCurrentPage(string gridViewName, List<KeyValuePair<string, string>> columnValuePairList, bool contains=true)
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
                    }
                }
                if (GridPages.Count > 1)
                {
                    PageCountInNumPartOfGridPager(gridViewName).First().ClickOnElement();
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
                List<IWebElement> GridPages = PageCountInNumPartOfGridPager(gridViewName).ToList();
                pagesCount = GridPages.Count;
                if (pagesCount > 1)
                {
                    GridPages.Last().ClickOnElement();
                    WaitForLoadingPanel();
                    rowsCount = (pagesCount - 1) * 100 + VisibleRows(gridViewName).Count;
                    //return back 1st page
                    PageCountInNumPartOfGridPager(gridViewName).First().ClickOnElement();
                    WaitForLoadingPanel();
                }
                else
                    rowsCount =  VisibleRows(gridViewName).Count;
                node.Info("Get number of items in table: " + rowsCount);
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

        public KeyValuePair<string, bool> ValidateRecordsMatchingFilterAreReturned(string gridViewName, List<KeyValuePair<string, string>> ValueInColumn, int expectedNumberOfRecord)
        {
            var node = StepNode();
            try
            {
                int totalRecords = GetTotalRowsVisibleInGrid(gridViewName);
                int fileredRecords = GetTableItemNumberWithConditions(gridViewName, ValueInColumn);

                if (totalRecords == fileredRecords && totalRecords == expectedNumberOfRecord)
                    return SetPassValidation(node, Validation.Records_Matching_Filter_Are_Returned);
                else
                    return SetFailValidation(node, Validation.Records_Matching_Filter_Are_Returned, expectedNumberOfRecord.ToString(), fileredRecords.ToString());
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
                var valueInColumn = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(columnName, value) };
                int numberOfCorrectRow = GetTableItemNumberWithConditions(gridViewName, valueInColumn);

                if (totalRecords == numberOfCorrectRow)
                    return SetPassValidation(node, Validation.Value_In_Column_Is_Correct);
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
                    return SetPassValidation(node, Validation.Filtered_Records_Are_Cleared);
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
        public virtual KeyValuePair<string, bool> ValidateRecordItemsCount(string gridViewName)
        {
            int itemsNumber = GetTotalRowsVisibleInGrid(gridViewName);
            var node = StepNode();
            node.Info($"Validate number of record items is equals to: {itemsNumber}");
            try
            {
                var actualQuantity = ItemsNumberLabel(gridViewName).Text;
                if (Int32.Parse(actualQuantity) == itemsNumber)
                    return SetPassValidation(node, Validation.Number_Of_Items_Counted_Is_Valid);

                return SetFailValidation(node, Validation.Number_Of_Items_Counted_Is_Valid, itemsNumber.ToString(), actualQuantity);
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
                    return SetFailValidation(node, string.Format(Validation.Window_Is_Opened, windowName));
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
			public static string Project_Is_Opened = "Validate that the project is opened";
            public static string Vendor_Data_Menus_Display = "Validate that the vendor data sub-menus display correct";
            public static string Default_Filter_Display = "Validate that the view filter in upper right corner is defaulted to the {0}";
            public static string Filter_Box_Is_Highlighted = "Validate that the {0} filter box is highlighted";
            public static string Window_Is_Opened = "Validate that {0} window is opened";
            public static string Form_Title_Is_Correct = "Validate that form title is {0}";
            public static string Sub_Item_links_Are_Displayed = "Validate that sub item links are displayed";
            public static string Number_Of_Items_Counted_Is_Valid = "Validate that number of items counted is valid";
            public static string Sub_Page_Is_Displayed = "Validate that the sub page is displayed";
            public static string Items_Are_Shown = "Validate that items on main pane are shown";
            public static string Progress_Message_Is_Displayed = "Validate that the progress message display: ";
            public static string Records_Matching_Filter_Are_Returned = "Validate that the records matching filter are returned";
            public static string Value_In_Column_Is_Correct = "Validate that the value in column is correct";
            public static string Filtered_Records_Are_Cleared = "Validate that the filtered records are cleared";
        }
        #endregion
    }
}
