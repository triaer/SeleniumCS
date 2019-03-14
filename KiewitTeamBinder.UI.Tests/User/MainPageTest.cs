using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static KiewitTeamBinder.Common.DashBoardENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.User
{
    [TestClass]
    public class MainPageTest : UITestBase
    {
        [TestMethod]
        public void TC014()
        {
            try
            {
                //Given:
                //1. Navigate to TA Dashboard
                test.Info("Navigate to TA Dashboard");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                var signOnData = new SignOnData();

                //When
                //2 Login with valid account: test / test
                //3. Click Add Page icon
                //4. Try to click other controls on Main page when New Page dialog is opening
                Login loginPage = new Login(driver);
                MainPage mainPage = loginPage.SignOn(signOnData.username, signOnData.password, false, signOnData.repository)
                                                .ClickSubSymbolMenu(SymbolMenu.GlobalSettings.ToDescription(), SubMenu.AddPage.ToDescription());

                //Then
                //VP: Observe the current page: All other controls within Dashboard are locked and disabled
                validations.Add(mainPage.ValidateAllControlInPageAreLocked());
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
                //1. Navigate to Dashboard login page
                test.Info("Navigate to Dashboard login page");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                var signOnData = new SignOnData();
                var pageData = new PageData();

                //When
                //2. Log in specific repository with valid account
                //3. Click on Add Page icon on Main Page
                //4. Enter Page Name field: Test
                //5. Click OK button
                test = LogTest("DA_LOGIN_TC015 - Verify user is able to add additional pages besides \"Overview\" page successfully");
                Login login = new Login(driver);
                MainPage mainPage = login.SignOn(signOnData.username, signOnData.password)
                                         .AddPage(pageData.pageName, true);

                //Then
                //VP: Check "Test" page is displayed besides "Overview" page
                string[] pageName = { pageData.OverviewPage, pageData.pageName };
                validations.Add(mainPage.ValidatePageNextToAnotherPage(pageName));
                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);

                //Post-Condition: Delete newly added page
                mainPage.DeleteOnePage(pageData.pageName);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void TC016()
        {
            try
            {
                //Given
                //1. Navigate to Dashboard login page
                test.Info("Navigate to Dashboard login page");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                var signOnData = new SignOnData();
                var pageData = new PageData();

                //When
                //2. Log in specific repository with valid account
                //3. Click on Add Page icon on Main Page
                //4. Enter Page Name field: Test
                //5. Click OK button
                //6. Click on Add Page icon on Main Page
                //7. Enter Page Name field: Another Test
                //8. Click on Displayed After dropdown list
                //9. Select specific page: Test
                //10. Click OK button
                test = LogTest("DA_LOGIN_TC016 - Verify the newly added main parent page is positioned at the location specified as set with \"Displayed After\" field of \"New Page\" form on the main page bar \"Parent Page\" dropped down menu");
                Login loginPage = new Login(driver);
                MainPage mainPage = loginPage.SignOn(signOnData.username, signOnData.password)
                                                .AddPage(pageData.pageName, true)
                                                    .AddPage(pageData.anotherPageName, true, null, 2, pageData.pageName);

                //Then
                //VP: Check "Another Test" page is positioned besides the "Test" page
                string[] pageName = { pageData.pageName, pageData.anotherPageName };
                validations.Add(mainPage.ValidatePageNextToAnotherPage(pageName));
                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);

                //Post-Condition: Delete newly added main child page and its parent page
                mainPage.DeletePage(pageName);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void TC019()
        {
            try
            {
                //Given:
                //1. Navigate to Dashboard login page
                test.Info("Navigate to Dashboard login page");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                var signOnData = new SignOnData();
                var pageData = new PageData();

                //When
                //2. Log in specific repository with valid account
                //3. Click on Add Page icon
                //4. Enter Page Name: Test
                //5. Click OK button
                //6. Click on Add Page icon
                //7. Enter Page Name: Another Test
                //8. Check Public checkbox
                //9. Click OK button
                //10. Click on "Test" page
                //11. Click on "Edit this page" icon
                Login loginPage = new Login(driver);
                MainPage mainPage = loginPage.SignOn(signOnData.username, signOnData.password, false, signOnData.repository)
                                             .AddPage(pageData.pageName, true)
                                             .AddPage(pageData.anotherPageName, true, null, 2, null, CheckValue.Yes.ToDescription())
                                             .ClickPage(pageData.pageName)
                                             .ClickSubSymbolMenu(SymbolMenu.GlobalSettings.ToDescription(), SubMenu.Edit.ToDescription())
                                             .LogValidation<MainPage>(ref validations, mainPage.ValidateDialogDisplayed(Dialog.EditPage.ToDescription()))
                                             .FillInfoInPageDiaglog(null, true, null, 2, null, CheckValue.Yes.ToDescription())
                                             .ClickPage(pageData.anotherPageName)
                                             .ClickSubSymbolMenu(SymbolMenu.GlobalSettings.ToDescription(), SubMenu.Edit.ToDescription())
                                             .LogValidation<MainPage>(ref validations, mainPage.ValidateDialogDisplayed(Dialog.EditPage.ToDescription()))
                                             .FillInfoInPageDiaglog(null, true, null, 2, null, CheckValue.No.ToDescription());

                mainPage.Logout();                           
                                             
                //Then
                //VP1: Check "Edit Page" pop up window is displayed

                //When
                //12. Check Public checkbox
                //13. Click OK button
                //14. Click on "Another Test" page
                //15. Click on "Edit this page" icon

                //Then
                //VP2: Check "Edit Page" pop up window is displayed

                //When
                //16. Uncheck Public checkbox
                //17. Click OK button
                //18. Click Log out link
                //19. Log in with another valid account

                //Then
                //VP3: Check "Test" Page is visibled and can be accessed
                //VP4: Check "Another Test" page is invisible

                //Post-condition
                //Log in  as creator page account and delete newly added page


            }
            catch (Exception e)
            {

            }
        }

        [TestMethod]
        public void demo()
        {
            var driver = Browser.Open(Constant.HomePage, "chrome");
            var signOnData = new SignOnData();
            var pageData = new PageData();

            Login loginPage = new Login(driver);
            MainPage mainPage = loginPage.SignOn(signOnData.username, signOnData.password);
            string[] pageName = { pageData.pageName, pageData.anotherPageName };
            mainPage.DeletePage(pageName);
        }


    }
}
