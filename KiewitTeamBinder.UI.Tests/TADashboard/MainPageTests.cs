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

                validations.Add(mainPage.CheckPageDisplayed("Test"));
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
        public void TC016()
        {
            try
            {
                test = LogTest("DA_LOGIN_TC016 - Verify the newly added main parent page is positioned at the location specified as set with 'Displayed After' field of 'New Page' form on the main page");
                //Given
                test.Info("1. Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                test.Info("2. Log in specific repository with valid account.");
                Login loginPage = new Login(driver);
                MainPage mainPage = loginPage.SignOn("administrator", "", "SampleRepository");

                test.Info("3. Click on Add Page icon on Main Page");
                test.Info("4. Enter Page Name field"); // Test
                test.Info("5. Click OK button");
                mainPage.AddNewPage("Test");

                test.Info("6. Click on Add Page icon on Main Page");
                test.Info("7. Enter Page Name field"); // Another Test
                test.Info("8. Click on  Displayed After dropdown list");
                test.Info("9. Select specific page"); // Test
                test.Info("10. Click OK button");

                mainPage.AddNewPage(pageName:"AnotherTest", displayAfterPage: "Test");
                

                //Then
                //VP: Try to click other controls on Main page when New Page dialog is opening
                validations.Add(mainPage.CheckPagesOrder("Test", "AnotherTest"));

                mainPage.selectPage("Test").deletePage().confirmDeletePage();
                mainPage.selectPage("AnotherTest").deletePage().confirmDeletePage();

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
        public void TC017()
        {
            try
            {
                test = LogTest("DA_LOGIN_TC017 - Verify 'Public' pages can be visible and accessed by all users of working repository");
                //Given
                test.Info("1. Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                test.Info("2. Log in specific repository with valid account.");
                MainPage mainPage = new Login(driver).SignOn("administrator", "", "SampleRepository"); ;
                
                test.Info("3. Click on Add Page icon on Main Page");
                test.Info("4. Enter Page Name field"); // Test
                test.Info("5. Check Public checkbox");
                test.Info("6. Click OK button");
                mainPage.AddNewPage(pageName: "Test", publicChbx: true);

                test.Info("7. Log out");
                test.Info("8. login with another account");
                //Then
                //VP: Try to click other controls on Main page when New Page dialog is opening

                validations.Add(mainPage.CheckPageExisted("Test"));

                mainPage.selectPage("Test").deletePage().confirmDeletePage();

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
        public void TC020()
        {
            try
            {
                test = LogTest("DA_LOGIN_TC020 - Verify user can remove any main parent page except 'Overview' page successfully and the order of pages stays persistent as long as there is not children page ");
                //Given
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                MainPage mainPage = new Login(driver).SignOn("administrator", "", "SampleRepository");

                string parent = "parent";
                string child = "child";

                mainPage.AddNewPage(pageName: parent);
                mainPage.AddNewPage(pageName: child, parentPage: parent);

                mainPage.selectPage(parent).deletePage();

                string msg1= "Are you sure you want to delete this page?";
                string msg2 = String.Format("Can not delete page '{0}' since it has children page(s)", parent);

                // VP1: 
                validations.Add(mainPage.CheckAlertMessage(msg1)); // "Are you sure you want to delete this page?"
                
                // VP2:
                validations.Add(mainPage.CheckAlertMessage(msg2)); // Can not delete page 'HP' since it has children page(s)
                
                mainPage.selectChildPage(parent, child).deletePage();

                // VP3:
                validations.Add(mainPage.CheckAlertMessage(msg1)); // "Are you sure you want to delete this page?"
                

                // VP4: 
                validations.Add(mainPage.CheckPageDeleted(parent, child));

                mainPage.selectPage(parent).deletePage();
                // VP5:
                validations.Add(mainPage.CheckAlertMessage(msg1)); // "Are you sure you want to delete this page?"                

                // VP6: 
                validations.Add(mainPage.CheckPageDeleted(parent));

                mainPage.selectPage("Overview");
                // VP7 
                validations.Add(mainPage.CheckDeleteButtonDisappeared());
                
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
        public void TC021()
        {
            try
            {
                test = LogTest("DA_LOGIN_TC021 - Verify user is able to add additional sibbling pages to the parent page successfully");
                //Given
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                MainPage mainPage = new Login(driver).SignOn("administrator", "", "SampleRepository");

                string parent = "parent";
                string child1 = "child1";
                string child2 = "child2";

                mainPage.AddNewPage(pageName: parent);
                mainPage.AddNewPage(pageName: child1, parentPage: parent);
                mainPage.AddNewPage(pageName: child2, parentPage: parent);

                // VP1
                validations.Add(mainPage.CheckChildPageExisted(child2, parent));

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
        public void TC024()
        {
            try
            {
                test = LogTest("DA_LOGIN_TC024 - Verify user is able to edit the name of the page (Parent/Sibbling) successfully");
                //Given
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                MainPage mainPage = new Login(driver).SignOn("administrator", "", "SampleRepository");

                string parent = "parent";
                string child = "child";
                // string parentedit = "parentedit";
                // string childedit = "childedit";

                mainPage.AddNewPage(pageName: parent);
                mainPage.AddNewPage(pageName: child, parentPage: parent);
                


                // VP1
                validations.Add(mainPage.CheckChildPageExisted(child, parent));

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
