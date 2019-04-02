﻿using FluentAssertions;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SimpleImpersonation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.User
{
    [TestClass]
    public class SignOnTests : UITestBase
    {

        [TestMethod]
        public void TC001()
        {

            try
            {
                
                //Given
                //1. Navigate to Dashboard login page.
                test.Info("Navigate to Dashboard login page.");
                CultureInfo b = LanguageHelper.GetCulture(Language.VIETNAM);
                string c = b.DateTimeFormat.LongDatePattern;
                //DateTime r = DateTime.ParseExact("1 Tháng Tư 2019", "dd MMMM yyyy", b);
                string d = DateTime.Today.ToString("MMMM yyyy", b);
                DateTime r = DateTime.ParseExact(d, "MMMM yyyy", b);
                DateTime e = DateTime.ParseExact("Tháng 4 2019", "'Tháng' M yyyy", b);
                
                var driver = Browser.Open(Constant.HomePage, "chrome");
                AgodaHomePage a = new AgodaHomePage(driver);

                //When
                //2. Enter valid username and password.
                //3. Click on "Login" button

                test = LogTest("DA_LOGIN_TC001 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials.");
                Login loginPage = new Login(driver);
                MainPage mainPage = loginPage.SignOn("administrators", "");

                //Then
                //VP: Verify that Dashboard Mainpage appears
                validations.Add(mainPage.ValidateDashboardMainPageDisplayed());
                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }


        }

        //[TestMethod]
        //public void General_NonSSOValidUserSignon_UI()
        //{
        //    try
        //    {
        //        // given
        //        var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

        //        test.Info("Open TeamBinder Document Web Page: " + teambinderTestAccount.Url);
        //        var driver = Browser.Open(teambinderTestAccount.Url, browser);
        //        var projectName = OtherUserLogin.ProjectName;

        //        // when
        //        //119692 - Log on via Other User Login Kiewit Account
        //        test = LogTest("Log on via Other User Login Kiewit Account");
        //        test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
        //        ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
        //        projectsList.LogValidation<ProjectsList>(ref validations, projectsList.ValidateDataInProjectListAvailable(projectName))
        //                        .LogValidation<ProjectsList>(ref validations, projectsList.ValidateProjectIsHighlightedWhenHovered(projectName));
        //        test.Info("Navigate to DashBoard Page of Project: " + projectName);
        //        ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(projectName);
        //        projectDashBoard.LogValidation(ref validations, projectDashBoard.ValidateProjectIsOpened(projectName))
        //            .Logout();
        //        // then
        //        Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
        //        validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
        //    }
        //    catch (Exception e)
        //    {
        //        lastException = e;
        //        throw;
        //    }
        //}

        //[TestMethod]
        //public void General_SSOValidUserSignon_UI()
        //{
        //    // given
        //    var teambinderTestAccount = GetTestAccount("VendorAccount1", environment, "KWUser", "SuperUserA");
        //    test.Info("Open TeamBinder Document Web Page: " + teambinderTestAccount.Url);
        //    var driver = Browser.Open(teambinderTestAccount.Url, browser);
        //    var projectName = SSOLogin.ProjectName;

        //    // when
        //    test.Info("Log on TeamBinder via Kiewit User Login: " + teambinderTestAccount.kiewitUserName);
        //    ProjectsList projectsList = new SsoSignOn(driver).KiewitUserLogon(teambinderTestAccount) as ProjectsList;
        //    test.Info("Navigate to DashBoard Page of Project: " + projectName);
        //    ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(projectName);
        //    projectDashBoard
        //        .LogValidation(ref validations, projectDashBoard.ValidateProjectIsOpened(projectName))
        //        .Logout();

        //    // then
        //    Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));

        //    validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
        //}
    }
}
