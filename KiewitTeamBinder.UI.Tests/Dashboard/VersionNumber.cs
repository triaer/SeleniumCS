using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages;
using OpenQA.Selenium;
using SimpleImpersonation;
using System.Diagnostics;
using System.IO;
using KiewitTeamBinder.UI.Tests;
using KiewitTeamBinder.UI;
using static KiewitTeamBinder.UI.KiewitTeamBinderENums;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.Dashboard
{
    /// <summary>
    /// Summary description for HelpAboutTest
    /// </summary>
    [TestClass]
    public class VersionNumber : UITestBase
    {
        [TestMethod]
        public void Validate_TeamBinderVersionNumber()
        {
            try
            {
                // given
                validations = new List<KeyValuePair<string, bool>>();
                List<KeyValuePair<string, bool>> methodValidations = new List<KeyValuePair<string, bool>>();
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                // and 119692 - Log on via Other User Login Kiewit Account
                test = LogTest("Log on via Other User Login Kiewit Account");
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                var projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                projectsList.LogValidation<ProjectsList>(ref validations, projectsList.ValidateDataInProjectListAvailable(TeamBinderVersionSmoke.ProjectName))
                            .LogValidation<ProjectsList>(ref validations, projectsList.ValidateProjectIsHighlightedWhenHovered(TeamBinderVersionSmoke.ProjectName));

                test.Info("Navigate to DashBoard Page of Project: " + TeamBinderVersionSmoke.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(TeamBinderVersionSmoke.ProjectName);
                projectDashBoard.LogValidation(ref validations, projectDashBoard.ValidateProjectIsOpened(TeamBinderVersionSmoke.ProjectName));

                //when - 119693 Validate Teambinder Version No
                test = LogTest("Validate Teambinder Version No.");
                var aboutDialog = projectDashBoard.OpenHelpDialog(HelpMenuOptions.About.ToDescription());
                aboutDialog.LogValidation<HelpAboutDialog>(ref validations, aboutDialog.ValidateTeamBinderVersion(TeamBinderVersionSmoke.TeamBinderVersion))
                    .CloseHelpDialog();

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
