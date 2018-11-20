using FluentAssertions;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SimpleImpersonation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.TestData.TeamBinderLoginSmoke;

namespace KiewitTeamBinder.UI.Tests.User
{
    [TestClass]
    public class SignOnTests : UITestBase
    {
        [TestMethod]
        public void NonSSO_ValidUserCanLogonAndLogOff()
        {
            // given
            var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

            test.Info("Open TeamBinder Document Web Page: " + teambinderTestAccount.Url);
            var driver = Browser.Open(teambinderTestAccount.Url, browser);
            var projectName = OtherUserLogin.ProjectName;

            // when
            //119692 - Log on via Other User Login Kiewit Account
            test = LogTest("Log on via Other User Login Kiewit Account");
            test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
            var projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
            projectsList.LogValidation<ProjectsList>(ref validations, projectsList.ValidateDataInProjectListAvailable(projectName))
                            .LogValidation<ProjectsList>(ref validations, projectsList.ValidateProjectIsHighlightedWhenHovered(projectName));
            test.Info("Navigate to DashBoard Page of Project: " + projectName);
            var projectDashBoard = projectsList.NavigateToProjectDashboardPage(projectName);
            projectDashBoard.LogValidation(ref validations, projectDashBoard.ValidateProjectIsOpened(projectName))
                .Logout();
            // then
            Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
            validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
        }
        [TestMethod]
        public void SSO_ValidUserCanLogonAndLogOff()
        {
            // given
            var teambinderTestAccount = GetTestAccount("VendorAccount1", environment, "KWUser", "SuperUserA");
            test.Info("Open TeamBinder Document Web Page: " + teambinderTestAccount.Url);
            var driver = Browser.Open(teambinderTestAccount.Url, browser);
            var projectName = SSOLogin.ProjectName;

            // when
            test.Info("Log on TeamBinder via Kiewit User Login: " + teambinderTestAccount.kiewitUserName);
            var projectsList = new SsoSignOn(driver).KiewitUserLogon(teambinderTestAccount) as ProjectsList;
            test.Info("Navigate to DashBoard Page of Project: " + projectName);
            projectsList.NavigateToProjectDashboardPage(projectName)
                        .Logout();

            // then
            //Utils.AddCollectionToCollection(validations, methodValidations);
            //Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
            //validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
        }
    }
}
