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

                test.Info("4. Try to click other controls on Main page when New Page dialog is opening");
                //Then
                //VP: Try to click other controls on Main page when New Page dialog is opening
                test = LogTest("DA_MP_TC014 - Verify when 'New Page' control/form is brought up to focus all other control within Dashboard page are locked and disabled ");
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
                test = LogTest("DA_LOGIN_TC015 - Verify user is able to add additional pages besides 'Overview' page successfully");
                //Given
                test.Info("1. Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                test.Info("2. Login with valid account.");
                Login loginPage = new Login(driver);
                MainPage mainPage = loginPage.SignOn("administrator", "", "Dashboard");

                test.Info("3. Click Add Page button");
                test.Info("4. Enter new page name: Test");
                test.Info("5. Click OK button");

                mainPage.AddNewPage("Test");
                
                //Then
                //VP: Try to click other controls on Main page when New Page dialog is opening

                validations.Add(mainPage.CheckNewPageDisplayed());
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
