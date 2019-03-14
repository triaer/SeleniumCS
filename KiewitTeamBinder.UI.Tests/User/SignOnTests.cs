using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
                var driver = Browser.Open(Constant.HomePage, "chrome");
                //When
                //2. Enter valid username and password.
                //3. Click on "Login" button

                test = LogTest("DA_LOGIN_TC001 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials.");
                Login loginPage = new Login(driver);
                MainPage mainPage = loginPage.SignOn("administrator", "", "SampleRepository");

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

        [TestMethod]
        public void TC002()
        {
            try
            {
                //Given
                //1. Navigate to Dashboard login page.
                test.Info("Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                //When
                //2. Enter valid username and password.
                //3. Click on "Login" button

                test = LogTest("DA_LOGIN_TC002 - Verify user fails to log in specific repository via Dashboard login page with incorrect credentials");
                Login loginPage = new Login(driver).SignOnFailed("test", "123", "SampleRepository");
                
                //Then
                //VP: Verify that Dashboard Mainpage appears
                validations.Add(loginPage.ValidateDashboardDashboardErrorMessageAppeared());
                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void TC003()
        {
            try
            {
                //Given
                //1. Navigate to Dashboard login page.
                test.Info("Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                //When
                //2. Enter valid username and password.
                //3. Click on "Login" button

                test = LogTest("DA_LOGIN_TC003 - Verify user fails to log in specific repository via Dashboard login page with correct username and incorrect password");
                Login loginPage = new Login(driver).SignOnFailed("administrator", "123", "SampleRepository");

                //Then
                //VP: Verify that Dashboard Mainpage appears
                validations.Add(loginPage.ValidateDashboardDashboardErrorMessageAppeared());
                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void TC004()
        {
            try
            {
                //Given
                //1. Navigate to Dashboard login page.

                test.Info("Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                //2. Select a Repository
                //3. Enter valid username and password.
                //4. Click on "Login" button
                //5. Click LogOut
                test = LogTest("DA_LOGIN_TC004 - Verify user is able to log in different repositories successfully after logging out current repository");
                Login loginPage = new Login(driver).SignOn("administrator", "", "SampleRepository").LogOut();

                //6. Select another repo
                MainPage mainPage = loginPage.SignOn("administrator", "", "Dashboard");

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


        [TestMethod]
        public void TC010()
        {
            try
            {
                //Given
                //1. Navigate to Dashboard login page.

                test.Info("Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                //2. Select a Repository
                //3. Enter valid username and password.
                //4. Click on "Login" button
                //5. Click LogOut
                test = LogTest("DA_LOGIN_TC010 - Verify 'Username' is not case sensitive");
                Login loginPage = new Login(driver).SignOn("administrator", "", "SampleRepository").LogOut();

                //6. Select another repo
                MainPage mainPage = loginPage.SignOn("ADMINISTRATOR", "", "SampleRepository");

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


    }
}
