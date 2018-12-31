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
        private static By _iframeFilter => By.XPath("//iframe[@id='ifrmFilter']");
        private static By _reportModuleButton(string moduleName) => By.XPath($"//span[contains(text(), '{moduleName}')]/ancestor::a[contains(@class, 'RootLink')]");
        private static By _reportSubMenuItemLink(string subMenuItem) => By.XPath($"./following-sibling::div//a[span = '{subMenuItem}']");

        public IWebElement IframeFilter { get { return StableFindElement(_iframeFilter); } }
        public IWebElement ReportModuleButton(string moduleName) => StableFindElement(_reportModuleButton(moduleName));
        public IWebElement ReportSubMenuItemLink(string moduleName, string subMenuItem) => ReportModuleButton(moduleName).StableFindElement(_reportSubMenuItemLink(subMenuItem));
        #endregion

        #region Actions
        public StandardReports(IWebDriver webDriver) : base(webDriver) { }

        private void ClickMenuItem(string menuItem)
        {
            var node = StepNode();
            node.Info($"Click on the root node: {menuItem}");
            ReportModuleButton(menuItem).Click();
        }

        private void ClickSubMenuItem(string menuItem, string subMenuItem)
        {
            var node = StepNode();
            node.Info($"Click on the sub node: {subMenuItem}");
            ReportSubMenuItemLink(menuItem, subMenuItem).Click();
        }

        public StandardReports SelectReportModule(string moduleName = null, bool waitForLoading = true)
        {
            if (moduleName != "")
                ClickMenuItem(moduleName);
            if (waitForLoading)
                WaitForLoadingPanel();
            return this;
        }

        public StandardReports SelectReportModuleItem(string moduleName, string moduleItemName, bool waitForLoading = true)
        {
            if (moduleItemName != "")
                ClickSubMenuItem(moduleName, moduleItemName);
            if (waitForLoading)
                WaitForLoadingPanel();

            return this;
        }

        public StandardReports SelectItemInDropdown(string fieldLabel, string selectedValue, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            WebDriver.SwitchTo().Frame(IframeFilter);
            IWebElement DropdownList = DropdownListInput(fieldLabel);
            DropdownList.Click();
            methodValidation.Add(ValidateItemDropdownIsHighlighted(selectedValue, fieldLabel));
            ItemDropdown(selectedValue).Click();
            DropdownList.SendKeys(OpenQA.Selenium.Keys.Tab);
            return this;
        }

        public KeyValuePair<string, bool> ValidateAvailableReportsDisplay(string moduleName, string[] availableReports)
        {
            var node = StepNode();
            node.Info("Validate that all available reports display");
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

        private static class Validation
        {
            public static string Available_Reports_Display = "Validate that all available reports display";
        }
        #endregion
    }
}
