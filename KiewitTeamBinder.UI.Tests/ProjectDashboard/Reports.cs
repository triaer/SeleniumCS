using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using OpenQA.Selenium;
using SimpleImpersonation;
using System.Diagnostics;
using System.IO;
using KiewitTeamBinder.UI.Pages.PopupWindows;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.UI.ExtentReportsHelper;


namespace KiewitTeamBinder.UI.Tests.ProjectDashboard
{
    [TestClass]
    public class Reports : UITestBase
    {
        [TestMethod]
        public void General_RunReportAndGenerateHyperlink_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                // and log on via Other User Login Kiewit Account
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
                var reportData = new ReportSmoke();
                By currentIframe = null;
                test.Info("Navigate to DashBoard Page of Project: " + reportData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(reportData.ProjectName);

                //when User Story 123743 - 120801 Run Report
                test = LogTest("Run Report");
                StandardReports standardReports = projectDashBoard.OpenStandardReportsWindow(true);
                standardReports.SelectReportModule(ref currentIframe, reportData.ReportLeftPanel, reportData.ModuleName)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateAvailableReportsDisplay(reportData.ReportLeftPanel, reportData.ModuleName, reportData.AvailableReports))
                    .SelectReportModuleItem(ref currentIframe, reportData.ReportLeftPanel, reportData.ModuleName, reportData.ModuleItemName)
                    .SelectItemInDropdown(ref currentIframe, reportData.ContractNumberDropdownList, reportData.ContractNumberItem, ref methodValidations)
                    .ClickSearchButton(ref currentIframe, waitLoadingPanel: true)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateValueInReportDetailDisplaysCorrectly(reportData.contractNumberKey, reportData.contractNumberValueArray));

                //when User Story 123735 - 120802 Generate/Navigate to Hyperlink
                test = LogTest("Generate/Navigate to Hyperlink");
                string reportRanByUser = standardReports.GetReport(ref currentIframe);
                GenerateHyperlinkDialog generateHyperlinkDialog = standardReports.ClickGenerateHyperlink();
                string reportUrl = generateHyperlinkDialog.CopyHyperlink();
                generateHyperlinkDialog.ClickCloseButton(ref currentIframe, ref methodValidations);
                Browser.Quit();

                currentIframe = null;
                driver = Browser.Open(reportUrl, browser);
                StandardReports newStandardReports = new StandardReports(driver).Logon(teambinderTestAccount);
                newStandardReports.LogValidation<StandardReports>(ref validations, newStandardReports.ValidateReportInHyperlinkIsIdenticalToReportRanByUser(ref currentIframe, reportRanByUser));
                Browser.Quit();

                // then
                Utils.AddCollectionToCollection(validations, methodValidations);
                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                validations = Utils.AddCollectionToCollection(validations, methodValidations);
                throw;
            }
        }
        [TestMethod]
        public void General_ScheduleAndFavoriteReport_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                // and log on via Other User Login Kiewit Account
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
                var reportData = new ReportSmoke();
                By currentIframe = null;
                test.Info("Navigate to DashBoard Page of Project: " + reportData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(reportData.ProjectName);

                //when User Story 123737 - 120803 Schedule a Report
                test = LogTest("Schedule a Report");
                string scheduleStartDate;
                StandardReports standardReports = projectDashBoard.OpenStandardReportsWindow(true);
                currentIframe = null;
                standardReports.SelectReportModule(ref currentIframe, reportData.ReportLeftPanel, reportData.ModuleName)
                    .SelectReportModuleItem(ref currentIframe, reportData.ReportLeftPanel, reportData.ModuleName, reportData.ModuleItemName)
                    .SelectItemInDropdown(ref currentIframe, reportData.ContractNumberDropdownList, reportData.ContractNumberItem, ref methodValidations)
                    .ClickSearchButton(ref currentIframe, waitLoadingPanel: true);

                standardReports.ClickBackButton(ref currentIframe)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateValuePreviouslyRemainsInReport(ref currentIframe, reportData.ContractNumberDropdownList, reportData.ContractNumberItem))
                    .ClickRadioButton(ref currentIframe, reportData.radioButton)
                    .GetScheduleDate(out scheduleStartDate)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateRadioButtonIsDepressed(reportData.radioButton))
                    .EnterDataInTheToField(reportData.contractUserName)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateContactListAutoPopulated())
                    .PressEnter();
                AlertDialog messageDialog = standardReports.ClickSearchButton(ref currentIframe);
                messageDialog.LogValidation<AlertDialog>(ref validations, messageDialog.ValidateMessageDisplayCorrect(reportData.availableMessage))
                    .ClickOKOnMessageDialog<StandardReports>();
                standardReports.LogValidation<StandardReports>(ref validations, standardReports.ValidateSaveDialogStatus(closed: true));

                //when User Story 123738 - 120804 Favorite a Report
                test = LogTest("Favorite a Report");
                standardReports.ClickAddToFavorites()
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateFavoriteReportDialogIsOpen(closed: false))
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateItemListOfFavoriteFor(ref currentIframe, reportData.favoriteItems))
                    .SelectFavoriteReport(ref currentIframe, FavoriteReportFor.Myself.ToDescription())
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateFavoritedForSelectedItem(reportData.favoriteItems, FavoriteReportFor.Myself.ToDescription()));
                AlertDialog successDialog = standardReports.ClickOkFavoritePopup(ref currentIframe);
                successDialog.LogValidation<AlertDialog>(ref validations, successDialog.ValidateMessageDisplayCorrect(reportData.favSuccessfullyMsg))
                    .ClickOKOnMessageDialog<StandardReports>();

                var columnValuePairList = new List<KeyValuePair<string, string>> { reportData.Title, new KeyValuePair<string, string>("Start Date", scheduleStartDate) };

                //when User Story 123740 - 120805 Validate Scheduled/Favorited
                test = LogTest("Validate Scheduled/Favorited");
                //StandardReports standarReports = projectDashBoard.OpenStandardReportsWindow();
                standardReports.SelectStandadReportsTabs(StandardReportsTab.Scheduled)
                                //.LogValidation<StandardReports>(ref validations, standardReports.ValidateReportsAreShown(columnValuePairList))
                                .SelectStandadReportsTabs(StandardReportsTab.Favorites)
                                .LogValidation<StandardReports>(ref validations, standardReports.ValidateReportTabIsSelected(StandardReportsTab.Favorites.ToDescription()))
                                .SelectReportModule(ref currentIframe, reportData.FavLeftPanel, reportData.ModuleName)
                                .LogValidation<StandardReports>(ref validations, standardReports.ValidateFavoritedReportIsListed(reportData.ModuleItemName))
                                .SelectReportModuleItem(ref currentIframe, reportData.FavLeftPanel, reportData.ModuleName, reportData.ModuleItemName)
                                .ClickRemoveFromFavorites();
                                

                // then
                Utils.AddCollectionToCollection(validations, methodValidations);
                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                validations = Utils.AddCollectionToCollection(validations, methodValidations);
                throw;
            }
        }
    }
}
