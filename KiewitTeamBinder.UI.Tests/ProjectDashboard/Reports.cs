using System;
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
                var runReportData = new RunReportSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + runReportData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(runReportData.ProjectName);

                //when User Story 123743 - 120801 Run Report
                test = LogTest("Run Report");
                StandardReports standardReports = projectDashBoard.OpenStandardReportsWindow();
                standardReports.SelectReportModule(runReportData.ModuleName)
                    .LogValidation<StandardReports>(ref validations, standardReports.ValidateAvailableReportsDisplay(runReportData.ModuleName, runReportData.AvailableReports))
                    .SelectReportModuleItem(runReportData.ModuleName, runReportData.ModuleItemName)
                    .SelectItemInDropdown(runReportData.ContractNumberDropdownList, runReportData.ContractNumberItem, ref methodValidations)
                    .ClickSearchButton();

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
