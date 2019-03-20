using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models;
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
                MainPage mainPage = loginPage.SignOn(signOnData.username, signOnData.password, signOnData.repository)
                                                .ClickGlobalSettingsSubMenu(SubMenu.AddPage.ToDescription());

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
                var pageData = new PageDataSmoke();

                //When
                //2. Log in specific repository with valid account
                //3. Click on Add Page icon on Main Page
                //4. Enter Page Name field: Test
                //5. Click OK button
                test = LogTest("DA_LOGIN_TC015 - Verify user is able to add additional pages besides \"Overview\" page successfully");
                Login login = new Login(driver);
                MainPage mainPage = login.SignOn(signOnData.username, signOnData.password)
                                         .AddPage(pageData.taPage1);

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
                var pageData = new PageDataSmoke();

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
                                                .AddPage(pageData.taPage1)
                                                    .AddPage(pageData.taPage2DisplayAfter);

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
                var pageData = new PageDataSmoke();

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
                test = LogTest("DA_LOGIN_TC019 - Verify user is able to edit the \"Public\" setting of any page successfully");
                Login loginPage = new Login(driver);
                MainPage mainPage = new MainPage(driver);
                loginPage.SignOn(signOnData.username, signOnData.password, signOnData.repository)
                            .AddPage(pageData.taPage1)
                            .AddPage(pageData.taPage2)
                            .ClickPage(pageData.pageName)
                            .ClickGlobalSettingsSubMenu(SubMenu.Edit.ToDescription())
                            .LogValidation<MainPage>(ref validations, mainPage.ValidateDialogDisplayed(Dialog.EditPage.ToDescription()))
                            .FillInfoInPageDiaglog(pageData.taPage1Edited)
                            .ClickPage(pageData.anotherPageName)
                            .ClickGlobalSettingsSubMenu(SubMenu.Edit.ToDescription())
                            .LogValidation<MainPage>(ref validations, mainPage.ValidateDialogDisplayed(Dialog.EditPage.ToDescription()))
                            .FillInfoInPageDiaglog(pageData.taPage2Edited)
                            .Logout();

                loginPage.SignOn(signOnData.anotherUsername, signOnData.anotherPassword, signOnData.repository)
                            .LogValidation<MainPage>(ref validations, mainPage.ValidatePageVisible(pageData.pageName))
                            .LogValidation<MainPage>(ref validations, mainPage.ValidatePageAccessed(pageData.pageName))
                            .LogValidation<MainPage>(ref validations, mainPage.ValidatePageInvisible(pageData.anotherPageName));
                mainPage.Logout();

                //Post-condition
                //Log in  as creator page account and delete newly added page
                string[] page = { pageData.pageName, pageData.anotherPageName };
                loginPage.SignOn(signOnData.username, signOnData.password, signOnData.repository)
                            .DeletePage(page);

            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void TC020()
        {
            try
            {
                //Navigate to Dashboard login page
                //Log in specific repository with valid account
                //Add a new parent page: Test 
                //Add a children page of newly added page: Test Child 
                //Click on parent page
                //Click "Delete this page" icon
                //Check confirm message "Are you sure you want to remove this page?" appears
                //Click OK button
                //Check warning message "Can not delete page 'Test' since it has children page(s)" appears
                //Click OK button
                //Click on  children page
                //Click "Delete this page" icon
                //Check confirm message "Are you sure you want to remove this page?" appears
                //Click OK button
                //Check children page is deleted
                //Click on  parent page
                //Click "Delete this page" icon
                //Check confirm message "Are you sure you want to remove this page?" appears
                //Click OK button
                //Check parent page is deleted
                //Click on "Overview" page
                //Check "Delete this page" icon disappears

                //Given
                test.Info("Navigate to Dashboard login page");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                var signOnData = new SignOnData();
                var pageData = new PageDataSmoke();

                //Then
                Login loginPage = new Login(driver);
                MainPage mainPage = new MainPage(driver);
                loginPage.SignOn(signOnData.username, signOnData.password)
                    .AddPage(pageData.taPage1)
                    .AddPage(pageData.taChildPage)
                    .ClickPage(pageData.taPage1.PageName)
                    .ClickGlobalSettingsSubMenu(SubMenu.Delete.ToDescription())
                    .LogValidation<MainPage>(ref validations, mainPage.ValidateTextInAlertPopup(pageData.confirmDeletePageMessage));
                string[] page = { pageData.pageName, pageData.subPageName };
                mainPage.AcceptAlert<MainPage>()
                     .LogValidation<MainPage>(ref validations, mainPage.ValidateTextInAlertPopup(pageData.warningHasChildPageMessage));
                mainPage.AcceptAlert<MainPage>();
                mainPage.ClickSubPage(page)
                     .ClickGlobalSettingsSubMenu(SubMenu.Delete.ToDescription())
                     .LogValidation<MainPage>(ref validations, mainPage.ValidateTextInAlertPopup(pageData.confirmDeletePageMessage))
                     .AcceptAlert<MainPage>()
                     .LogValidation<MainPage>(ref validations, mainPage.ValidatePageNotExisted(pageData.taChildPage.PageName))
                     .ClickPage(pageData.taPage1.PageName)
                     .ClickGlobalSettingsSubMenu(SubMenu.Delete.ToDescription())
                     .LogValidation<MainPage>(ref validations, mainPage.ValidateTextInAlertPopup(pageData.confirmDeletePageMessage))
                     .AcceptAlert<MainPage>()
                     .LogValidation<MainPage>(ref validations, mainPage.ValidatePageNotExisted(pageData.taChildPage.PageName))
                     .ClickPage(pageData.OverviewPage)
                     .ClickGlobalSettingsSubMenu(SubMenu.Delete.ToDescription())
                     .LogValidation<MainPage>(ref validations, mainPage.ValidateSubMenuNotExisted(SubMenu.Delete.ToDescription()));
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }

        }


        [TestMethod]
        public void demo()
        {
            var driver = Browser.Open(Constant.HomePage, "chrome");
            var signOnData = new SignOnData();
            var pageData = new PageDataSmoke();

            
            Login loginPage = new Login(driver);
            MainPage mainPage = new MainPage(driver);
            loginPage.SignOn(signOnData.username, signOnData.password)
                .ClickPage(pageData.taPage1.PageName)
                    .ClickGlobalSettingsSubMenu(SubMenu.Delete.ToDescription())
                    .LogValidation<MainPage>(ref validations, mainPage.ValidateTextInAlertPopup(pageData.confirmDeletePageMessage));
            string[] page = { pageData.pageName, pageData.subPageName };
            mainPage.AcceptAlert<MainPage>()
                 .LogValidation<MainPage>(ref validations, mainPage.ValidateTextInAlertPopup(pageData.warningHasChildPageMessage));
            mainPage.AcceptAlert<MainPage>();
            mainPage.ClickSubPage(page);
            mainPage.ClickGlobalSettingsSubMenu(SubMenu.Delete.ToDescription())
                .LogValidation<MainPage>(ref validations, mainPage.ValidateTextInAlertPopup(pageData.confirmDeletePageMessage))
                .AcceptAlert<MainPage>()
                .LogValidation<MainPage>(ref validations, mainPage.ValidatePageNotExisted(pageData.taChildPage.PageName))
                    .ClickPage(pageData.taPage1.PageName)
                    .ClickGlobalSettingsSubMenu(SubMenu.Delete.ToDescription())
                    .LogValidation<MainPage>(ref validations, mainPage.ValidateTextInAlertPopup(pageData.confirmDeletePageMessage))
                    .AcceptAlert<MainPage>();
        }

    } 
    
}
