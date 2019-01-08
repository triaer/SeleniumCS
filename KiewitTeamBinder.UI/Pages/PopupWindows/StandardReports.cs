using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.Common;

namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class StandardReports : PopupWindow
    {
        #region Entities        
        protected static By filterIframe => By.XPath("//iframe[@id='ifrmFilter']");
        protected static By reportViewIframe => By.XPath("//iframe[@id='ifrmReportView']");

        private static By _reportModuleButton(string tab, string moduleName) => By.XPath($"//div[@id= '{tab}']//span[contains(text(), '{moduleName}')]/ancestor::a[contains(@class, 'RootLink')]");
        private static By _reportSubMenuItemLink(string subMenuItem) => By.XPath($"./following-sibling::div//a[span = '{subMenuItem}']");
        private static By _searchButton => By.Id("ButtonSearch");
        private static By _resultTable(string columnName) => By.XPath($"//div[text() = '{columnName}']/ancestor::table[1]");
        private static By _generateHyperlink => By.XPath("//a[contains(@id, 'generateLink')]");
        private static By _userIdTextbox => By.Id("txtUserId");
        private static By _companyIdTextbox => By.Id("txtCompanyId");
        private static By _passwordTextbox => By.Id("txtPassword");
        private static By _loginBtn => By.XPath("//a[@id='lnkLogon']/span[.='Login']");

        public IWebElement ReportModuleButton(string tab, string moduleName) => StableFindElement(_reportModuleButton(tab, moduleName));
        public IWebElement ReportSubMenuItemLink(string tab, string moduleName, string subMenuItem) => ReportModuleButton(tab, moduleName).StableFindElement(_reportSubMenuItemLink(subMenuItem));
        public IWebElement SearchButton { get { return StableFindElement(_searchButton); } }
        public IWebElement ResultTable(string columnName) => StableFindElement(_resultTable(columnName));
        public IWebElement GenerateHyperlink { get { return StableFindElement(_generateHyperlink); } }
        public IWebElement PasswordTextbox { get { return StableFindElement(_passwordTextbox); } }
        public IWebElement UserIdTextbox { get { return StableFindElement(_userIdTextbox); } }
        public IWebElement CompanyIdTextbox { get { return StableFindElement(_companyIdTextbox); } }
        public IWebElement LoginBtn { get { return StableFindElement(_loginBtn); } }
        #endregion

        #region Actions
        public StandardReports(IWebDriver webDriver) : base(webDriver)
        {            
        }

        protected void SwitchToFrame(ref By currentIFrame, By switchIFrame)
        {
            if (switchIFrame == null && currentIFrame != null)
            {
                WebDriver.SwitchOutOfIFrame();
            }
            else if (switchIFrame != null && currentIFrame != switchIFrame)
            {
                WebDriver.SwitchTo().Frame(StableFindElement(switchIFrame));
            }
            currentIFrame = switchIFrame;
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
        
        public GenerateHyperlinkDialog ClickGenerateHyperlink()
        {
            GenerateHyperlink.Click();
            return new GenerateHyperlinkDialog(WebDriver);
        }

        public StandardReports Logon(TestAccount account)
        {           
            //Fill account fields
            UserIdTextbox.InputText(account.Username, true);
            WaitForElementEnable(_companyIdTextbox);
            CompanyIdTextbox.InputText(account.Company, true);
            WaitForElementEnable(_passwordTextbox);
            PasswordTextbox.InputText(account.Password, true);

            //Click LogIn button
            string parentWindow;
            SwitchToNewPopUpWindow(LoginBtn, out parentWindow, true);
            return this;
        }

        public StandardReports ClickSearchButton(ref By currentIframe, bool reportNow = true, bool waitLoadingPanel = true)
        {
            if (reportNow)
            {
                SwitchToFrame(ref currentIframe, filterIframe);
                SearchButton.Click();
                if (waitLoadingPanel)
                    WaitForLoading(_loadingPanel);
                SwitchToFrame(ref currentIframe, null);
                SwitchToFrame(ref currentIframe, reportViewIframe);
            }
            else
            {

            }
            return this;
        }

        public StandardReports SelectReportModule(ref By currentIframe, string tab, string moduleName, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info($"Click on the root node: {moduleName}");
            //(ref currentIframe, null);
            ReportModuleButton(tab, moduleName).Click();
            if (waitForLoading)
                WaitForLoadingPanel();
            return this;
        }

        public StandardReports SelectReportModuleItem(ref By currentIframe, string tab, string moduleName, string moduleItemName, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info($"Click on the sub node: {moduleItemName}");
            SwitchToFrame(ref currentIframe, null);
            ReportSubMenuItemLink(tab, moduleName, moduleItemName).Click();
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
        public StandardReports SelectItemInDropdown(ref By currentIframe, string fieldLabel, string selectedValue, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            var node = StepNode();
            node.Info($"Select {selectedValue} in dropdown list in Filter iframe");
            SwitchToFrame(ref currentIframe, filterIframe);
            IWebElement DropdownList = DropdownListInput(fieldLabel);
            DropdownList.Click();
            methodValidation.Add(ValidateItemDropdownIsHighlighted(selectedValue, fieldLabel));
            ItemDropdown(selectedValue).Click();
            DropdownList.SendKeys(Keys.Tab);
            return this;
        }

        public string GetReport(ref By currentIframe)
        {
            SwitchToFrame(ref currentIframe, reportViewIframe);
            var reportHeader = StableFindElement(By.XPath("//td[contains(@id,'1_oReportCell')]/table/tbody/tr[1]"));
            string report = reportHeader.Text;
            var reportTable = StableFindElement(By.XPath("//td[contains(@id,'1_oReportCell')]/table/tbody/tr[2]"));
            report += reportTable.Text;
            var totalItems = StableFindElement(By.XPath("//td[contains(@id,'1_oReportCell')]/table/tbody/tr[3]"));
            report += totalItems.Text;
            return report;
        }

        public KeyValuePair<string, bool> ValidateReportInHyperlinkIsIdenticalToReportRanByUser(ref By currentIframe, string reportRanByUser)
        {
            var node = StepNode();
            node.Info(Validation.Report_In_Hyperlink_Is_Identical_To_Report_Ran_By_User);
            try
            {
                string reportInHyperlink = GetReport(ref currentIframe);
                if (reportInHyperlink == reportRanByUser)
                    return SetPassValidation(node, Validation.Report_In_Hyperlink_Is_Identical_To_Report_Ran_By_User);
                else                
                    return SetFailValidation(node, Validation.Report_In_Hyperlink_Is_Identical_To_Report_Ran_By_User, reportRanByUser, reportInHyperlink);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Report_In_Hyperlink_Is_Identical_To_Report_Ran_By_User, e);
            }
        }

        public KeyValuePair<string, bool> ValidateAvailableReportsDisplay(string tab, string moduleName, string[] availableReports)
        {
            var node = StepNode();
            node.Info(Validation.Available_Reports_Display);
            try
            {
                foreach (var availableReport in availableReports)
                {
                    if (ReportSubMenuItemLink(tab, moduleName, availableReport).IsDisplayed() == false)
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
            public static string Report_In_Hyperlink_Is_Identical_To_Report_Ran_By_User = "Validate that the report in hyperlink is identical to report ran by user";
        }
        #endregion
    }
}
