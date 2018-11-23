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

namespace KiewitTeamBinder.UI.Tests.ProjectDashboard.Mail
{
    /// <summary>
    /// Summary description for NavigateToModulesFromLeftNav
    /// </summary>
    [TestClass]
    public class NavigateToModulesFromLeftNav : UITestBase
    {
        [TestMethod]
        public void Validate_NavigateToModulesFromLeftNav()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                // and log on via Other User Login Kiewit Account
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                var projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                test.Info("Navigate to DashBoard Page of Project: " + TeamBinderVersionSmoke.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(TeamBinderVersionSmoke.ProjectName);

                //when - 119703 Validate Navigate to Modules from Left Nav
                test = LogTest("Validate Navigate to Modules from Left Nav");
                projectDashBoard.SelectModuleMenuItem(ProjectDashboardModuleName.Mail.ToDescription());


                //var aboutDialog = projectDashBoard.OpenHelpDialog(HelpMenuOptions.About.ToDescription());
                //aboutDialog.LogValidation<HelpAboutDialog>(ref validations, aboutDialog.ValidateTeamBinderVersion(TeamBinderVersionSmoke.TeamBinderVersion))
                //    .CloseHelpDialog();

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