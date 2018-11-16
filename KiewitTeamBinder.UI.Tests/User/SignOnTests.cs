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

            var driver = Browser.Open(teambinderTestAccount.Url, browser);

            // when
            var projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
            projectsList.NavigateToProjectDashboardPage("Automation Project 1")
                        .Logout();

            // then
            //Utils.AddCollectionToCollection(validations, methodValidations);
            //Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
            //validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
        }
        [TestMethod]
        public void SSO_ValidUserCanLogonAndLogOff()
        {
            // given
            var teambinderTestAccount = GetTestAccount("VendorAccount1", environment, "KWUser", "SuperUserA");

            var driver = Browser.Open(teambinderTestAccount.Url, browser);

            // when
            var projectsList = new SsoSignOn(driver).KiewitUserLogon(teambinderTestAccount) as ProjectsList;
            projectsList.NavigateToProjectDashboardPage("Automation Project 1")
                        .Logout();

            // then
            //Utils.AddCollectionToCollection(validations, methodValidations);
            //Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
            //validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
        }
    }
}
