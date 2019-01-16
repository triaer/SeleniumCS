﻿using System;
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
        public void RunReport()
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
                standardReports.SelectReportModule(ref currentIframe, reportData.ReportTab, reportData.ModuleName)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateAvailableReportsDisplay(reportData.ReportTab, reportData.ModuleName, reportData.AvailableReports))
                    .SelectReportModuleItem(ref currentIframe, reportData.ReportTab, reportData.ModuleName, reportData.ModuleItemName)
                    .SelectItemInDropdown(ref currentIframe, reportData.ContractNumberDropdownList, reportData.ContractNumberItem, ref methodValidations)
                    .ClickSearchButton(ref currentIframe)
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

                //when User Story 123737 - 120803 Schedule a Report
                driver = Browser.Open(teambinderTestAccount.Url, browser);
                projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
                projectDashBoard = projectsList.NavigateToProjectDashboardPage(reportData.ProjectName);
                standardReports = projectDashBoard.OpenStandardReportsWindow(true);
                currentIframe = null;
                standardReports.SelectReportModule(ref currentIframe, reportData.ReportTab, reportData.ModuleName)
                    .SelectReportModuleItem(ref currentIframe, reportData.ReportTab, reportData.ModuleName, reportData.ModuleItemName)
                    .SelectItemInDropdown(ref currentIframe, reportData.ContractNumberDropdownList, reportData.ContractNumberItem, ref methodValidations)
                    .ClickSearchButton(ref currentIframe);

                test = LogTest("Schedule a Report");
                string contractNumbber = standardReports.GetValueFromVendorDataDetailsTable(reportData.contractNumberKey)[0];

                standardReports.ClickBackButton(ref currentIframe)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateValuePreviouslyRemainsInReport(ref currentIframe, reportData.ContractNumberDropdownList, contractNumbber))
                    .ClickRadioButton(ref currentIframe, reportData.radioButton)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateRadioButtonIsDepressed(reportData.radioButton))
                    .EnterToTextField(reportData.contractUserName)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateContactListAutoPopulated())
                    .PressEnter()
                    .ClickSearchButton(ref currentIframe, false)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateValueInMessageReportDisplaysCorrectly(reportData.availableMessage))
                    .ClickOkButtonOnPopUp<StandardReports>()
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateCloseDialog(close: true));

                currentIframe = null;

                //when User Story 123738 - 120804 Favorite a Report
                standardReports.ClickButtonReportHeader(ref currentIframe, reportData.idButtonAddToFavouriteReportHeader)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateOpenDialog(close: false))
                    //.LogValidation<StandardReports>(ref validations, standardReports.ValidateUserIsAbleToFavoriteReport(reportData.favoriteItem));
                    .SelectFavoriteReport(ref currentIframe, reportData.myselfFavReport)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateUserIsAbleToFavoriteReport(reportData.favoriteItem))
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateFavoritedForUserOnly(reportData.favoriteItem))
                    .clickOkFavoritePopup(ref currentIframe)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateOpenDialogWithMessageCorrectly(reportData.favSuccessfullyMsg, close: false))
                    .ClickOkButtonOnPopUp<StandardReports>()
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateCloseDialog(close: true))
                    .ClickButtonReportHeader(ref currentIframe, reportData.idButtonAddToFavouriteReportHeader)
                    .ClickYesFavoritePopup(ref currentIframe)
                    .ClickOkButtonOnPopUp<StandardReports>();

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
