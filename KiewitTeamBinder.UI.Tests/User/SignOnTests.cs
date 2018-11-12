using FluentAssertions;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.UI.Pages;
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
            validations = new List<KeyValuePair<string, bool>>();
            List<KeyValuePair<string, bool>> methodValidations = new List<KeyValuePair<string, bool>>();
            var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

            var driver = Browser.Open(teambinderTestAccount.Url, browser);
            NonSsoSignOn otherUserSignOnPage = new NonSsoSignOn(driver);

            // when
            otherUserSignOnPage.Logon(teambinderTestAccount);

            // then
            //otherUserSignOnPage.Title.Should().Equals(Browser.Title);
            //Utils.AddCollectionToCollection(validations, methodValidations);
            //Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
            //validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
        }
        [TestMethod]
        public void SSO_ValidUserCanLogonAndLogOff()
        {
            // given
            validations = new List<KeyValuePair<string, bool>>();
            List<KeyValuePair<string, bool>> methodValidations = new List<KeyValuePair<string, bool>>();
            var teambinderTestAccount = GetTestAccount("SuperUserA", environment, "KWUser", "VendorAccount1");

            var driver = Browser.Open(teambinderTestAccount.Url, browser);
            SsoSignOn kwSignOnPage = new SsoSignOn(driver);

            // when
            kwSignOnPage.KiewitUserLogon(teambinderTestAccount);

            // then
            //otherUserSignOnPage.Title.Should().Equals(Browser.Title);
            //Utils.AddCollectionToCollection(validations, methodValidations);
            //Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
            //validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
        }
    }
}
