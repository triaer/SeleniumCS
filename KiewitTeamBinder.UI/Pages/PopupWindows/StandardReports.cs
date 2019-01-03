using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class StandardReports : PopupWindow
    {
        #region Entities           
        private static By _filterIframe => By.XPath("//iframe[@id='ifrmFilter']");
        private static By _reportViewIframe => By.XPath("//iframe[@id='ifrmReportView']");
        private static By _reportModuleButton(string moduleName) => By.XPath($"//span[contains(text(), '{moduleName}')]/ancestor::a[contains(@class, 'RootLink')]");
        private static By _reportSubMenuItemLink(string subMenuItem) => By.XPath($"./following-sibling::div//a[span = '{subMenuItem}']");
        private static By _searchButton => By.Id("ButtonSearch");
        private static By _resultTable(string columnName) => By.XPath($"//div[text() = '{columnName}']/ancestor::table[1]");

        private By _currentIframe;

        public IWebElement ReportModuleButton(string moduleName) => StableFindElement(_reportModuleButton(moduleName));
        public IWebElement ReportSubMenuItemLink(string moduleName, string subMenuItem) => ReportModuleButton(moduleName).StableFindElement(_reportSubMenuItemLink(subMenuItem));
        public IWebElement SearchButton { get { return StableFindElement(_searchButton); } }
        public IWebElement ResultTable(string columnName) => StableFindElement(_resultTable(columnName));
        #endregion

        #region Actions
        public StandardReports(IWebDriver webDriver) : base(webDriver)
        {
            _currentIframe = null;
        }

        private void SwitchToFrame(ref By currentFrame, By switchFrame)
        {
            if (switchFrame == null)
            {
                WebDriver.SwitchTo().DefaultContent();
            }
            else if (currentFrame == null || currentFrame != switchFrame)
            {
                WebDriver.SwitchTo().Frame(StableFindElement(switchFrame));
            }
            currentFrame = switchFrame;
        }

        private void ClickMenuItem(string moduleName)
        {   
            ReportModuleButton(moduleName).Click();
        }

        private void ClickSubMenuItem(string moduleName, string subModuleItem)
        {   
            ReportSubMenuItemLink(moduleName, subModuleItem).Click();
        }

        private string[] GetValueFromVendorDataDetailsTable(string valueKey)
        {
            int rowIndex, colIndex = -1;
            GetTableCellValueIndex(ResultTable(valueKey), valueKey, out rowIndex, out colIndex);
            IReadOnlyCollection<IWebElement> tableRows = ResultTable(valueKey).StableFindElements(By.XPath(".//tr"));
            string[] valueArray = new string[tableRows.Count - rowIndex];
            for (int i = 0; i < valueArray.Length; i++)
            {
                valueArray[i] = tableRows.ElementAt(i + rowIndex).StableFindElement(By.XPath("./td[" + colIndex + "]")).Text;
            }
            return valueArray;
        }

        public StandardReports ClickSearchButton(bool waitLoadingPanel = true)
        {
            SearchButton.Click();
            if (waitLoadingPanel)
                WaitForLoading(_loadingPanel);
            SwitchToFrame(ref _currentIframe, null);
            SwitchToFrame(ref _currentIframe, _reportViewIframe);
            return this;
        }

        public StandardReports SelectReportModule(string moduleName, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info($"Click on the root node: {moduleName}");
            SwitchToFrame(ref _currentIframe, null);
            ClickMenuItem(moduleName);
            if (waitForLoading)
                WaitForLoadingPanel();
            return this;
        }

        public StandardReports SelectReportModuleItem(string moduleName, string moduleItemName, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info($"Click on the sub node: {moduleItemName}");
            SwitchToFrame(ref _currentIframe, null);
            ClickSubMenuItem(moduleName, moduleItemName);
            if (waitForLoading)
                WaitForLoadingPanel();
            return this;
        }

        /// <summary>
        /// Select item in dropdown list in Filter iframe
        /// </summary>
        /// <param name="fieldLabel"></param>
        /// <param name="selectedValue"></param>
        /// <param name="methodValidation"></param>
        /// <returns>StandardReports object</returns>
        public StandardReports SelectItemInDropdown(string fieldLabel, string selectedValue, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            var node = StepNode();
            node.Info($"Select {selectedValue} in dropdown list in Filter iframe");
            SwitchToFrame(ref _currentIframe, _filterIframe);
            IWebElement DropdownList = DropdownListInput(fieldLabel);
            DropdownList.Click();
            methodValidation.Add(ValidateItemDropdownIsHighlighted(selectedValue, fieldLabel));
            ItemDropdown(selectedValue).Click();
            DropdownList.SendKeys(Keys.Tab);
            return this;
        }

        public KeyValuePair<string, bool> ValidateAvailableReportsDisplay(string moduleName, string[] availableReports)
        {
            var node = StepNode();
            node.Info(Validation.Available_Reports_Display);
            try
            {
                foreach (var availableReport in availableReports)
                {
                    if (ReportSubMenuItemLink(moduleName, availableReport).IsDisplayed() == false)
                        return SetFailValidation(node, Validation.Available_Reports_Display);
                }
                return SetPassValidation(node, Validation.Available_Reports_Display);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Available_Reports_Display, e);
            }
        }

        public KeyValuePair<string, bool> ValidateValueInReportDetailDisplaysCorrectly(string valueKey, string[] expectedValueArray)
        {
            var node = StepNode();
            node.Info(Validation.Value_In_Report_Detail_Displays_Correctly);
            try
            {
                var actualValue = GetValueFromVendorDataDetailsTable(valueKey);
                foreach (var value in actualValue)
                {
                    bool exist = false;
                    for (int i = 0; i < expectedValueArray.Length; i++)
                    {
                        if (value == expectedValueArray[i])
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (!exist)
                        return SetFailValidation(node, Validation.Value_In_Report_Detail_Displays_Correctly);
                }
                return SetPassValidation(node, Validation.Value_In_Report_Detail_Displays_Correctly);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Value_In_Report_Detail_Displays_Correctly, e);
            }
        }

        private static class Validation
        {
            public static string Available_Reports_Display = "Validate that all available reports display";
            public static string Value_In_Report_Detail_Displays_Correctly = "Validate that value in report detail displays correctly";
        }
        #endregion
    }
}
