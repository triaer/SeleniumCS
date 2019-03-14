using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.Users
{
    [TestClass]
    public class MainPageTests : UITestBase
    {
        [TestMethod]
        public void TC014()
        {
            try
            {
                //Given
                //1. Navigate to Dashboard login page.
                test.Info("1. Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                test.Info("2. Login with valid account.");
                //2. Login with valid account
                Login loginPage = new Login(driver);
                MainPage mainPage = loginPage.SignOn("administrator", "", "Dashboard");

                test.Info("3. Click Add Page button");
                //3. Click Add Page icon
                mainPage.OpenAddPageDialog();

                test = LogTest("DA_LOGIN_TC014 - Verify when 'New Page' control/form is brought up to focus all other control within Dashboard page are locked and disabled ");
                //Then
                //VP: Try to click other controls on Main page when New Page dialog is opening

                validations.Add(mainPage.ValidateControlsAreLockedAndDisabled());
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
        public void TC015()
        {
            try
            {
                //Given
                //1. Navigate to Dashboard login page.
                test.Info("1. Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                test.Info("2. Login with valid account.");
                //2. Login with valid account
                Login loginPage = new Login(driver);
                MainPage mainPage = loginPage.SignOn("administrator", "", "Dashboard");

                test.Info("3. Click Add Page button");
                //3. Click Add Page icon
                mainPage.OpenAddPageDialog();

                test = LogTest("DA_LOGIN_TC015 - Verify user is able to add additional pages besides 'Overview' page successfully");
                //Then
                //VP: Try to click other controls on Main page when New Page dialog is opening

                validations.Add(mainPage.ValidateControlsAreLockedAndDisabled());
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
