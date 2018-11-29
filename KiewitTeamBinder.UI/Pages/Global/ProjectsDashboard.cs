using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.UI.Pages.VendorData;
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
        public static By _subMenuItemLink(string value) => By.XPath($"//span[(text()='{value}')]");

        public static By _moduleButton(string value) => By.XPath($"//div[@id = 'div{value}']");
        public static By _subPageTable(string value) => By.XPath($"//table[@id='ctl00_cntPhMain_{value}_ctl00']");

        private static By _itemsNumberLabel(string value) => By.XPath($"//span[contains(@id, '{value}_ctl00DSC')]");
        private static By _divSubMenu => By.XPath("//div[@id='divSubMenu']");
        private static string _menuButton = "//li[a='{0}']";
        private static string _imageOfFilterBox = "//img[contains(@id,'Link{1}{0}')]";
        private static By _subPageHeader => By.Id("lblRegisterCaption");
        private string _headerButton = "//a[span='{0}']";
        private static string _filterItems = "//tr[@valign='top']";
        private static By _paneTable(string gridViewName) => By.XPath($"//table[contains(@id, '{gridViewName}_ctl00_Header')]/thead");

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

        public ProjectsDashboard ClickVendorDataButton()
        {
            var node = StepNode();
            node.Info("Click Vendor Data Module Button in Left Nav");
            VendorButton.Click();            
            WaitForElementDisplay(By.XPath(string.Format(_menuButton, "Holding Area")));
            return this;
        }

        public IWebElement MenuButton(string menu)
        {
            return StableFindElement(By.XPath(string.Format(_menuButton, menu)));
        }

        public HoldingArea ClickHoldingAreaButton()
        {
            var node = StepNode();
            node.Info("Click Holding Area under Vendor Data Module in Left Nav");
            MenuButton("Holding Area").Click();
            return new HoldingArea(WebDriver);
        }

        public int GetTableItemNumber()
        {
            var node = StepNode();
            node.Info("Get number of items in table");
            const string rowXpath = "//tr[contains(@style, 'visibility: visible')]";

            try
            {
                var rows = StableFindElements(By.XPath(rowXpath)).Count;
                return rows;
            }
            catch
            {
                return 0;
            }
        }

        public ProjectsDashboard SelectModuleMenuItem(string menuPath)
        {
            var node = StepNode();

            var separator = '/';
            var nodes = menuPath.Split(separator);
            if (nodes.Count() == 1)
            {
                node.Info($"Click on the root node: {nodes[0]}");
                ModuleButton(nodes[0]).Click();
                WaitForElement(_divSubMenu);
            }
            else
            {
                node.Info($"Click on the root node: {nodes[0]}");
                ModuleButton(nodes[0]).Click();
                WaitForElement(_divSubMenu);
                node.Info($"Click on the sub node: {nodes[1]}");
                SubMenuItemLink(nodes[1]).Click();
                WaitForElement(_subPageHeader);
            }

            return this;
        }

        public T SelectModuleMenuItem<T>(string menuPath)
        {
            SelectModuleMenuItem(menuPath);
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

        public T ClickHeaderButton<T>(MainPaneTableHeaderButton buttonName, bool waitForLoading, string tableName = null)
        {
            IWebElement Button = StableFindElement(By.XPath(string.Format(_headerButton, buttonName.ToDescription())));
            var node = StepNode();
            node.Info("Click the button: " + buttonName.ToDescription());
            Button.HoverAndClickWithJS();

            if (!waitForLoading && tableName != null)
                WaitForElement(_subPageTable(tableName));
            
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
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
        public KeyValuePair<string, bool> ValidateRecordItemsCount(int itemsNumber, string moduleName)
        {
            var node = StepNode();
            node.Info($"Validate number of record items is equals to: {itemsNumber}");

            try
            {
                var actualQuantity = ItemsNumberLabel(moduleName).Text;
                if (Int32.Parse(actualQuantity) == itemsNumber)
                    return SetPassValidation(node, Validation.Number_Of_Items_Counted_Is_Valid);

                return SetFailValidation(node, Validation.Number_Of_Items_Counted_Is_Valid);
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

                IWebElement ImageOfFilterBox = StableFindElement(By.XPath(string.Format(_imageOfFilterBox, filterBoxIndex, highlight)));
                if (ImageOfFilterBox.IsDisplayed())                
                    return SetPassValidation(node, Validation.First_Filter_Box_Is_Highlighted);
                
                return SetFailValidation(node, Validation.First_Filter_Box_Is_Highlighted);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.First_Filter_Box_Is_Highlighted, e);
            }
        }

        public KeyValuePair<string, bool> ValidateWindowIsOpened(string windowName)
        {
            var node = StepNode();
            try
            {
                if (WebDriver.Title == windowName)
                    return SetPassValidation(node, string.Format(Validation.Validate_Window_Is_Opened, windowName));
                else
                    return SetFailValidation(node, string.Format(Validation.Validate_Window_Is_Opened, windowName));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Validate_Window_Is_Opened, windowName), e);
            }
        }

        public KeyValuePair<string, bool> ValidateFormTitle(string formTitle)
        {
            var node = StepNode();
            try
            {
                if (FormTitle.Text == formTitle)
                    return SetPassValidation(node, string.Format(Validation.Validate_Form_Title_Is_Correct, formTitle));
                else
                    return SetFailValidation(node, string.Format(Validation.Validate_Form_Title_Is_Correct, formTitle));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Validate_Form_Title_Is_Correct, formTitle), e);
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
                if (IsItemShown(gridViewName, columnValuePairList))
                    return SetPassValidation(node, Validation.Validate_Items_Are_Shown);
                return SetFailValidation(node, Validation.Validate_Items_Are_Shown);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Validate_Items_Are_Shown, e);
            }
        }

        private bool IsItemShown(string gridViewName, List<KeyValuePair<string, string>> columnValuePairList)
        {
            IReadOnlyCollection<IWebElement> AvailableItems = GetAvailableItems(gridViewName, columnValuePairList);
            if (AvailableItems != null)
                return true;
            return false;
        }

        private IReadOnlyCollection<IWebElement> GetAvailableItems(string gridViewName, List<KeyValuePair<string, string>> columnValuePairList)
        {
            int rowIndex, colIndex = 1;
            string itemsXpath = _filterItems;
            GetTableCellValueIndex(PaneTable(gridViewName), columnValuePairList.ElementAt(0).Key, out rowIndex, out colIndex, "th");
            if (colIndex < 2)
                return null;
            itemsXpath += $"[td[{colIndex}][contains(., '{columnValuePairList.ElementAt(0).Value}')]";

            for (int i = 1; i < columnValuePairList.Count; i++)
            {
                GetTableCellValueIndex(PaneTable(gridViewName), columnValuePairList.ElementAt(i).Key, out rowIndex, out colIndex, "th");
                if (colIndex < 2)
                    return null;
                itemsXpath += $" and td[{colIndex}][contains(., '{columnValuePairList.ElementAt(i).Value}')]";
            }                       
            itemsXpath += "]";

            return StableFindElements(By.XPath(itemsXpath));
        }

        private static class Validation
        {
			public static string Project_Is_Opened = "Validate that the project is opened";
            public static string Vendor_Data_Menus_Display = "Validate that the vendor data sub-menus display correct";
            public static string Default_Filter_Display = "Validate that the view filter in upper right corner is defaulted to the {0}";
            public static string First_Filter_Box_Is_Highlighted = "Validate that the first filter box is highlighted";
            public static string Validate_Window_Is_Opened = "Validate that {0} window is opened";
            public static string Validate_Form_Title_Is_Correct = "Validate that form title is {0}";
            public static string Sub_Item_links_Are_Displayed = "Validate that sub item links are displayed";
            public static string Number_Of_Items_Counted_Is_Valid = "Validate that number of items counted is valid";
            public static string Sub_Page_Is_Displayed = "Validate that the sub page is displayed";
            public static string Validate_Items_Are_Shown = "Validate that items on main pane are shown";

        }
        #endregion
    }
}
