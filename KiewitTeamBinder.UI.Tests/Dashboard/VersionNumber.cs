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
            // given
            validations = new List<KeyValuePair<string, bool>>();
            List<KeyValuePair<string, bool>> methodValidations = new List<KeyValuePair<string, bool>>();
            var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

            var driver = Browser.Open(teambinderTestAccount.Url, browser);
            NonSsoSignOn otherUserSignOnPage = new NonSsoSignOn(driver);

            // and
            var dashboardPage = otherUserSignOnPage.Logon(teambinderTestAccount);

            //when - 119693 Validate Teambinder Version No
            var aboutDialog = dashboardPage.OpenHelpDialog(HelpMenuOptions.About.ToDescription());
            aboutDialog.LogValidation<HelpAboutDialog>(ref validations, aboutDialog.ValidateTeamBinderVersion(TeamBinderVersionSmoke.TeamBinderVersion))
                .CloseHelpDialog();

            // then
            Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
            validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
        }
    }
}
