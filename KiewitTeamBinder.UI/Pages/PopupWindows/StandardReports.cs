using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.Common;
using System.Windows.Forms;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using System.Globalization;

namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class StandardReports : PopupWindow
    {
        #region Entities        
        protected static By filterIframe => By.XPath("//iframe[@id='ifrmFilter']");
        protected static By reportViewIframe => By.XPath("//iframe[@id='ifrmReportView']");
        protected static By favoriteReportIframe => By.XPath("//iframe[@name='RadWindowFavReportAccess']");
        private static string _filterItemsXpath = "//table[contains(@id,'ReportScheduleGrid')]//tr[@valign='top'and not(contains(@style,'hidden'))]";

        private static By _reportModuleButton(string tab, string moduleName) => By.XPath($"//div[@id= '{tab}']//span[contains(text(), '{moduleName}')]/ancestor::a[contains(@class, 'RootLink')]");
        private static By _reportSubMenuItemLink(string subMenuItem) => By.XPath($"./following-sibling::div//a[span = '{subMenuItem}']");
        private static By _searchButton => By.Id("ButtonSearch");
        private static By _resultTable(string columnName) => By.XPath($"//div[text() = '{columnName}']/ancestor::table[1]");
        private static By _generateHyperlink => By.XPath("//a[contains(@id, 'generateLink')]");
        private static By _userIdTextbox => By.Id("txtUserId");
        private static By _companyIdTextbox => By.Id("txtCompanyId");
        private static By _passwordTextbox => By.Id("txtPassword");
        private static By _loginBtn => By.XPath("//a[@id='lnkLogon']/span[.='Login']");
        private static By _backButton => By.XPath("//a[contains(@id, 'back')]");
        private static By _radioButtonReportHeader(string radioButtonName) => By.XPath($"//input[@id='rdoRunReport_{radioButtonName}']");
        private static By _toInput => By.XPath("//input[@id='anonymous_element_2']");
        private static By _listContact => By.XPath("//li[contains(@id,'anonymous_element') and @class='auto-focus']");
        private static By _reportMessage => By.XPath("//div[contains(@id, 'message')]");
        private static By _saveItemPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]");
        private static By _reportHeaderButton(string buttonLabel) => By.XPath($"//input[@value='{buttonLabel}']");
        private static By _favoriteReportPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_RadWindowFavReportAccess')]");
        private static By _favReport(string nameFav) => By.XPath($"//td[@class='Checkbox']//label[text()='{nameFav}']");
        private static By _radioButtonFavReport(string nameFav) => By.XPath($"//td[@class='Checkbox']//label[text()='{nameFav}']/ancestor::td[@class='Checkbox']//input[contains(@id,'chk')]");
        private static By _okButtonInFavReport => By.XPath("//input[@id='ButtonOk']");
        private static By _yesButtonInFavReport => By.XPath("//span[text()='Yes']");
        private static By _otherUserLoginBtn => By.XPath("//a[./span[text()='OTHER USER LOGIN']]");
        private static By _standardReportsTabs(string tabName) => By.XPath($"//span[@class ='rtsTxt'][contains(text(),'{tabName}')]");
        private static By _reportTable => By.XPath("//table[contains(@id,'ReportScheduleGrid') and contains(@id,'Header')]/thead");
        private static By _selectedReportsTab(string tabName) => By.XPath($"//a[@class='rtsLink rtsSelected']//span[contains(text(),'{tabName}')]");
        private static By _favoritedReport(string favreport) => By.XPath($"//div[@id='FavReportTypePanelBar']//span[contains(@class,'rpText')][contains(text(),'{favreport}')]");
        private static By _scheduleDateInput => By.XPath("//input[@id='ScheduleSetting_radScheduleTime_dateInput_ClientState']");

        public IWebElement ReportModuleButton(string tab, string moduleName) => StableFindElement(_reportModuleButton(tab, moduleName));
        public IWebElement ReportSubMenuItemLink(string tab, string moduleName, string subMenuItem) => ReportModuleButton(tab, moduleName).StableFindElement(_reportSubMenuItemLink(subMenuItem));
        public IWebElement SearchButton { get { return StableFindElement(_searchButton); } }
        public IWebElement ResultTable(string columnName) => StableFindElement(_resultTable(columnName));
        public IWebElement GenerateHyperlink { get { return StableFindElement(_generateHyperlink); } }
        public IWebElement PasswordTextbox { get { return StableFindElement(_passwordTextbox); } }
        public IWebElement UserIdTextbox { get { return StableFindElement(_userIdTextbox); } }
        public IWebElement CompanyIdTextbox { get { return StableFindElement(_companyIdTextbox); } }
        public IWebElement LoginBtn { get { return StableFindElement(_loginBtn); } }
        public IWebElement BackButton { get { return StableFindElement(_backButton); } }
        public IWebElement RadioButtonReportHeader(string radioButtonName) => StableFindElement(_radioButtonReportHeader(radioButtonName));
        public IWebElement ToInput { get { return StableFindElement(_toInput); } }
        public IReadOnlyCollection<IWebElement> VisibleContactList => StableFindElements(_listContact);
        public IWebElement ReportMessage { get { return StableFindElement(_reportMessage); } }
        public IWebElement ReportHeaderButton(string tabName) => StableFindElement(_reportHeaderButton(tabName));
        public IWebElement FavReport(string nameFav) => StableFindElement(_favReport(nameFav));
        public IWebElement RadioButtonFavReport(string nameFav) => StableFindElement(_radioButtonFavReport(nameFav));
        public IWebElement OkButtonInFavReport { get { return StableFindElement(_okButtonInFavReport); } }
        public IWebElement YesButtonInFavReport { get { return StableFindElement(_yesButtonInFavReport); } }
        public IWebElement OtherUserLoginBtn { get { return StableFindElement(_otherUserLoginBtn); } }
        public IWebElement StandardReportsTabs(string tabName) => StableFindElement(_standardReportsTabs(tabName));
        public IWebElement ReportTable => StableFindElement(_reportTable);
        public IWebElement ScheduleDateInput { get { return FindElement(_scheduleDateInput); } }
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
        
        public string[] GetValueFromVendorDataDetailsTable(string valueKey)
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

        //public string GetContractNumber(string columnName, bool waitForLoading = true)
        //{
        //    if (waitForLoading)
        //        WaitForLoading(_contractNumber(columnName));
        //    return ContractNumber(columnName).Text;
        //}

        public GenerateHyperlinkDialog ClickGenerateHyperlink()
        {
            var node = StepNode();
            node.Info($"Click Generate hyperlink in Report Window Header");
            GenerateHyperlink.Click();
            return new GenerateHyperlinkDialog(WebDriver);
        }

        public StandardReports ClickBackButton(ref By currentIframe, bool waitLoadingPanel = true)
        {
            var node = StepNode();
            node.Info($"Click Back button");
            BackButton.Click();
            if (waitLoadingPanel)
                WaitForLoading(_loadingPanel);
            SwitchToFrame(ref currentIframe, null);
            return this;
        }

        public StandardReports Logon(TestAccount account)
        {
            //Click OtherUserLogin Button and Switch to OtherUserLogin Window
            if (OtherUserLoginBtn == null)
                Browser.MinimizeWindow();
            Browser.MaximizeWindow();
            //SwitchToNewPopUpWindow(OtherUserLoginBtn, out logonWindow, true);
            OtherUserLoginBtn.Click();
            WaitForElementDisplay(By.Id("walkme-player"));

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

        public StandardReports ClickSearchButton(ref By currentIframe, bool waitLoadingPanel = true)
        {
            var node = StepNode();
            node.Info("Click Search at bottom of Report Window");
            SwitchToFrame(ref currentIframe, filterIframe);
            SearchButton.Click();
            if (waitLoadingPanel)
                WaitForLoading(_loadingPanel);
            SwitchToFrame(ref currentIframe, null);
            SwitchToFrame(ref currentIframe, reportViewIframe);

            return this;
        }

        public AlertDialog ClickSearchButton(ref By currentIframe)
        {
            var node = StepNode();
            SwitchToFrame(ref currentIframe, filterIframe);
            SearchButton.Click();
            SwitchToFrame(ref currentIframe, null);

            return new AlertDialog(WebDriver);
        }

        public StandardReports ClickRadioButton(ref By currentIframe, string fieldLabel)
        {
            var node = StepNode();
            node.Info($"Click {fieldLabel} Radio button under Run Report in Report Header");
            SwitchToFrame(ref currentIframe, null);
            
            RadioButtonReportHeader(fieldLabel).Click();
            return this;
        }
        public StandardReports GetScheduleDate(out string date)
        {
            string selectedText = "";
            DateTime dateInput;
            var valueString = ScheduleDateInput.GetValue().Replace("{", "").Replace("}", "");
            string[] attributeValues = valueString.Split(',');
            foreach (var attributeValue in attributeValues)
            {
                if (attributeValue.Contains("lastSetTextBoxValue"))
                {
                    selectedText = attributeValue.Substring(attributeValue.IndexOf(":") + 1);
                    selectedText = selectedText.Replace("\"", "");
                    break;
                }
            }
            dateInput = DateTime.ParseExact(selectedText, "MM-dd-yyyy HH:mm", CultureInfo.InvariantCulture); //"01-21-2019 13:26"
            date = dateInput.ToString("MM-dd-y hh:mm tt");  //"01-07-19 08:29 PM"
            return this;
        }
        public StandardReports SelectReportModule(ref By currentIframe, string tab, string moduleName, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info($"Select ' {moduleName} ' on the root node from Standard Reports Left Nav.");
            ReportModuleButton(tab, moduleName).Click();
            if (waitForLoading)
                WaitForLoadingPanel();
            return this;
        }

        public StandardReports SelectReportModuleItem(ref By currentIframe, string tab, string moduleName, string moduleItemName, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info($"Click on the Report sub node: {moduleItemName}");
            SwitchToFrame(ref currentIframe, null);
            ReportSubMenuItemLink(tab, moduleName, moduleItemName).Click();
            if (waitForLoading)
                WaitForLoadingPanel();
            return this;
        }

        public StandardReports ClickButtonReportHeader(string buttonLabel)
        {
            var node = StepNode();
            node.Info($"Click '{buttonLabel}' button in Report Header");
            ReportHeaderButton(buttonLabel).Click();
            return this;
        }
        public StandardReports ClickAddToFavorites()
        {
            var node = StepNode();
            try
            {
                ClickButtonReportHeader(StandardReportsButtonHeader.AddFavorites.ToDescription());
            }
            catch
            {
                if (ReportHeaderButton(StandardReportsButtonHeader.RemoveFavorites.ToDescription()) != null)
                {
                    node.Info($"The '{StandardReportsButtonHeader.RemoveFavorites.ToDescription()}' button existent, the report currently added to favorites");
                }
            }
            return this;
        }
        public StandardReports PressEnter()
        {
            var node = StepNode();
            node.Info($"Press Enter when Users Name is Highlighted");
            WaitFor(_listContact);
            SendKeys.SendWait(@"{Enter}");
            return this;
        }

        /// <summary>
        /// Enter text to the To text field
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public StandardReports EnterDataInTheToField(string userName)
        {
            var node = StepNode();
            node.Info($"Type in Test Users Name with value: " + userName);
            ToInput.InputText(userName);
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
            node.Info($"Select '{selectedValue}' in '{fieldLabel}' dropdown list in Report filter window ");
            SwitchToFrame(ref currentIframe, filterIframe);
            IWebElement DropdownList = DropdownListInput(fieldLabel);
            DropdownList.Click();
            methodValidation.Add(ValidateItemDropdownIsHighlighted(selectedValue, fieldLabel));
            ItemDropdown(selectedValue).Click();
            DropdownList.SendKeys(OpenQA.Selenium.Keys.Tab);
            return this;
        }

        public string GetReport(ref By currentIframe)
        {
            SwitchToFrame(ref currentIframe, reportViewIframe);
            string report = StableFindElement(By.XPath("//div[contains(@id,'_oReportDiv')]")).Text;
            
            return report;
        }

        public StandardReports SelectFavoriteReport(ref By currentIframe, string nameFav, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info($"Select {nameFav} radio button");
            SwitchToFrame(ref currentIframe, favoriteReportIframe);
            FavReport(nameFav).Click();
            return this;
        }

        public AlertDialog clickOkFavoritePopup(ref By currentIframe)
        {
            var node = StepNode();
            node.Info("Click OK in Favorite Report Access Popup");
            OkButtonInFavReport.Click();
            SwitchToFrame(ref currentIframe, null);
            return new AlertDialog(WebDriver);
        }

        public StandardReports ClickYesFavoritePopup(ref By currentIframe, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info("Click Yes in Favorite Report Popup ");
            if (waitForLoading)
                WaitForLoading(_yesButtonInFavReport);
            YesButtonInFavReport.Click();
            return this;
        }

        public StandardReports SelectStandadReportsTabs(StandardReportsTab tabName)
        {
            var node = StepNode();
            node.Info($"Select {tabName.ToDescription()}");
            StandardReportsTabs(tabName.ToDescription()).Click();
            return this;
        }
        public ProjectsDashboard CloseReportWindow(ref List<KeyValuePair<string, bool>> methodValidations)
        {
            int numberOfWindowsBeforeClose = WebDriver.WindowHandles.Count;
            Browser.Close();
            methodValidations.Add(ValidateReportsWindowIsClosed(numberOfWindowsBeforeClose));
            return new ProjectsDashboard(WebDriver);
        }
        private IReadOnlyCollection<IWebElement> GetAvailableReports(List<KeyValuePair<string, string>> columnValuePairList)
        {
            int rowIndex, colIndex = 1;
            string itemsXpath = _filterItemsXpath;
            GetTableCellValueIndex(ReportTable, columnValuePairList.ElementAt(0).Key, out rowIndex, out colIndex, "th");
            if (colIndex < 2)
                return null;
            itemsXpath += $"[td[{colIndex}][contains(., '{columnValuePairList.ElementAt(0).Value}')]";

            for (int i = 1; i < columnValuePairList.Count; i++)
            {
                GetTableCellValueIndex(ReportTable, columnValuePairList.ElementAt(i).Key, out rowIndex, out colIndex, "th");
                if (colIndex < 2)
                    return null;
                itemsXpath += $" and td[{colIndex}][contains(., '{columnValuePairList.ElementAt(i).Value}')]";
            }
            itemsXpath += "]";

            return StableFindElements(By.XPath(itemsXpath));
        }
        public KeyValuePair<string, bool> ValidateContactListAutoPopulated()
        {
            var node = StepNode();
            node.Info(Validation.Contact_List_Is_Auto_Populated);
            int contactListCount = VisibleContactList.Count;
            try
            {
                if (contactListCount > 0)
                    return SetPassValidation(node, Validation.Contact_List_Is_Auto_Populated);
                return SetFailValidation(node, Validation.Contact_List_Is_Auto_Populated);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Contact_List_Is_Auto_Populated, e);
            }
        }

        public KeyValuePair<string, bool> ValidateRadioButtonIsDepressed(string radioButtonName)
        {
            var node = StepNode();
            node.Info(Validation.Radio_Button_Is_Depressed);
            try
            {
                if (RadioButtonReportHeader(radioButtonName).Selected)
                    return SetPassValidation(node, Validation.Radio_Button_Is_Depressed);
                return SetFailValidation(node, Validation.Radio_Button_Is_Depressed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Radio_Button_Is_Depressed, e);
            }
        }

        public KeyValuePair<string, bool> ValidateValuePreviouslyRemainsInReport(ref By currentIframe, string fieldLabel, string expectedValue)
        {
            var node = StepNode();
            node.Info(Validation.Value_Entered_Previously_Remains_In_Report);
            try
            {
                SwitchToFrame(ref currentIframe, filterIframe);
                string selectedText = "";
                string contractNumber = DropdownListClientState(fieldLabel).GetAttribute("value");
                string[] attributeValues = contractNumber.Split(',');
                foreach (var attributeValue in attributeValues)
                {
                    if (attributeValue.Contains("text"))
                    {
                        selectedText = attributeValue.Split(':')[1];
                        selectedText = selectedText.Replace("\"", "");

                    }
                    if (selectedText == expectedValue)
                        return SetPassValidation(node, Validation.Value_Entered_Previously_Remains_In_Report);
                }
                return SetFailValidation(node, Validation.Value_Entered_Previously_Remains_In_Report);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Value_Entered_Previously_Remains_In_Report, e);
            }
        }

        public KeyValuePair<string, bool> ValidateReportInHyperlinkIsIdenticalToReportRanByUser(ref By currentIframe, string reportRanByUser)
        {
            var node = StepNode();
            node.Info("Navigate to Report via hyperlink");
            node.Info("After user logged in and takes user to the Reports section");
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
                        return SetFailValidation(node, Validation.Available_Reports_Display + availableReport);
                }
                return SetPassValidation(node, Validation.Available_Reports_Display + availableReports);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Available_Reports_Display, e);
            }
        }

        public KeyValuePair<string, bool> ValidateValueInReportDetailDisplaysCorrectly(string valueKey, string[] expectedValueArray)
        {
            var node = StepNode();
            node.Info("The selected Contract keyed earlier is listed in the report: " + expectedValueArray);
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

        public KeyValuePair<string, bool> ValidateFavoriteReportDialogIsOpen(bool closed = false)
        {
            var node = StepNode();
            try
            {
                if (closed == true)
                {
                    if (FindElement(_favoriteReportPopUp) == null)
                        return SetPassValidation(node, Validation.Dialog_Box_Is_Closed);

                    return SetFailValidation(node, Validation.Dialog_Box_Is_Closed);
                }
                else
                {
                    //check dialog displays
                    if (StableFindElement(_favoriteReportPopUp) != null)
                        return SetPassValidation(node, Validation.Dialog_Box_Is_Opend);

                    return SetFailValidation(node, Validation.Dialog_Box_Is_Opend);
                }
            }
            catch (Exception e)
            {
                if (closed == true)
                    return SetFailValidation(node, Validation.Dialog_Box_Is_Closed);

                return SetErrorValidation(node, Validation.Dialog_Box_Is_Closed, e);
            }
        }

        public KeyValuePair<string, bool> ValidateItemListOfFavoriteFor(ref By currentIframe, string[] favoriteNames)
        {
            var node = StepNode();
            
            try
            {
                SwitchToFrame(ref currentIframe, favoriteReportIframe);
                foreach (string name in favoriteNames)
                {
                    if (FavReport(name) == null)
                        return SetFailValidation(node, Validation.User_Is_Able_To_Favorite_Report);
                }
                return SetPassValidation(node, Validation.User_Is_Able_To_Favorite_Report);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.User_Is_Able_To_Favorite_Report, e);
            }
        }

        public KeyValuePair<string, bool> ValidateFavoritedForSelectedItem(string[] favoriteName, string selectedItem)
        {
            var node = StepNode();
            node.Info(Validation.Dialog_Box_Is_Opened_With_Message_Display_Correctly);

            try
            {
                if (RadioButtonFavReport(favoriteName[0]).Selected && !RadioButtonFavReport(favoriteName[1]).Selected && !RadioButtonFavReport(favoriteName[2]).Selected)
                {
                    return SetPassValidation(node, Validation.Report_Is_Favorited_For_User_Only);
                }
                return SetFailValidation(node, Validation.Report_Is_Favorited_For_User_Only);

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Report_Is_Favorited_For_User_Only, e);
            }
        }
        public KeyValuePair<string, bool> ValidateReportsAreShown(List<KeyValuePair<string, string>> columnValuePairList)
        {
            var node = StepNode();
            try
            {

                var ReportsList = GetAvailableReports(columnValuePairList);
                if (ReportsList.Count > 0)
                    return SetPassValidation(node, Validation.Reports_Are_Shown);
                return SetFailValidation(node, Validation.Reports_Are_Shown);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Reports_Are_Shown, e);
            }
        }
        public KeyValuePair<string, bool> ValidateReportTabIsSelected(string tabname)
        {
            var node = StepNode();
            try
            {
                if (FindElement(_selectedReportsTab(tabname)) != null)
                {
                    return SetPassValidation(node, Validation.Report_Tab_Is_Selected);
                }
                return SetFailValidation(node, Validation.Report_Tab_Is_Selected);

            }
            catch (Exception e)
            {

                return SetErrorValidation(node, Validation.Report_Tab_Is_Selected, e);
            }

        }
        public KeyValuePair<string, bool> ValidateFavoritedReportIsListed(string favreport)
        {
            var node = StepNode();
            try
            {
                if (FindElement(_favoritedReport(favreport)) != null)
                {
                    return SetPassValidation(node, Validation.Favorited_Report_Is_Listed);
                }
                return SetFailValidation(node, Validation.Favorited_Report_Is_Listed);

            }
            catch (Exception e)
            {

                return SetErrorValidation(node, Validation.Favorited_Report_Is_Listed, e);
            }

        }

        public KeyValuePair<string, bool> ValidateReportsWindowIsClosed(int numberOfWindowsBeforeClose)
        {
            var node = StepNode();
            int numberOfWindowsAfterClose = WebDriver.WindowHandles.Count;

            try
            {
                if (numberOfWindowsBeforeClose == numberOfWindowsAfterClose + 1)
                {
                    return SetPassValidation(node, Validation.Reports_Window_Is_Close);
                }
                return SetFailValidation(node, Validation.Reports_Window_Is_Close);

            }
            catch (Exception e)
            {

                return SetErrorValidation(node, Validation.Reports_Window_Is_Close, e);
            }

        }

        private static class Validation
        {
            public static string Available_Reports_Display = "Validate that all available reports display correctly: ";
            public static string Value_In_Report_Detail_Displays_Correctly = "Validate that value in report detail displays correctly";
            public static string Report_In_Hyperlink_Is_Identical_To_Report_Ran_By_User = "Validate that the report in hyperlink is identical to report ran by user";            
            public static string Value_Entered_Previously_Remains_In_Report = "Validate that Value entered previously remains in report";
            public static string Message_Reports_Display_Correctly = "Validate that message displays correctly";
            public static string Radio_Button_Is_Depressed = "Validate that radio button is depressed";
            public static string Contact_List_Is_Auto_Populated = "Vatlidate that contact list auto populated";
            public static string Dialog_Box_Is_Closed = "Validate that dialog Box is closed";
            public static string Dialog_Box_Is_Opend = "Validate that dialog box is opend";
            public static string User_Is_Able_To_Favorite_Report = "Validate that User is able to favorite report ";
            public static string Dialog_Box_Is_Opened_With_Message_Display_Correctly = "Validate that dialog dialog box is opend with displays message correctly";
            public static string Report_Is_Favorited_For_User_Only = "Validate that Report is favorited for user only";
            public static string Reports_Are_Shown = "Validate that reports are shown";
            public static string Report_Tab_Is_Selected = "Validate report tab is selected";
            public static string Favorited_Report_Is_Listed = "Favorited Report is listed ";
            public static string Reports_Window_Is_Close = "Validate reports window is close";
        }
        #endregion
    }
}
