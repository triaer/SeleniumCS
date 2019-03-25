using FluentAssertions;
using Agoda.Common.Helper;
using Agoda.UI.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Agoda.UI.ExtentReportsHelper;

namespace Agoda.UI.Tests.Users
{
    [TestClass]
    public class PanelTests : UITestBase
    {
        [TestMethod]
        public void TC030()
        {
            try
            {
                test = LogTest("Verify when 'Choose panels' form is expanded all pre-set panels are populated and sorted correctly ");

                //Given
                test.Info("Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                test.Info("Login with valid account.");
                MainPage mainPage = new Login(driver).SignOn("administrator", "", "SampleRepository");

                //When
                test.Info("1. Click Add Page button");
                test.Info("2. Enter pagename");
                test.Info("3. Click OK");

                mainPage.AddNewPage("TC030").expandChoosePanels();

                test.Info("4. Try to click other controls on Main page when New Page dialog is opening");
                //Then

                mainPage.selectPage("TC030").deletePage().confirmDeletePage();
                /*VP: All pre-set panels:
                Chart:
                + Action Implementation By Status
                + Test Module Implementation By Priority
                + Test Module Implementation By Status
                + Test Module Implementation Progress
                + Test Module Status per Assigned Users

                Indicator:
                + Test Case Execution
                + Test Module Execution
                + Test Objective Execution

                are populated and sorted correctly
                */

                //validations.Add(mainPage.ValidateControlsAreLockedAndDisabled());
                //Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                //validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;  
            }
        }
        
        [TestMethod]
        public void TC032()
        {
            try
            {
                test = LogTest("Verify user is unable to create new panel when (*) required field is not filled");

                //Given
                test.Info("Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                test.Info("Login");
                MainPage mainPage = new Login(driver).SignOn("administrator", "", "SampleRepository");

                //When
                test.Info("Go Administer > Panel");
                test.Info("Click Add New link");
                Panel panelPage = mainPage.openPanelPage().AddNewPanel(displayName: "");
                
                //Then
                
                //VP: 

                validations.Add(panelPage.ValidateErrorMessage("Display Name is a required field."));
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
        public void TC033()
        {
            try
            {
                test = LogTest("No special character except '@' character is allowed to be inputted into 'Display Name' field");

                //Given
                test.Info("Navigate to Dashboard login page.");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                test.Info("Login");
                MainPage mainPage = new Login(driver).SignOn("administrator", "", "SampleRepository");

                //When
                test.Info("Go Administer > Panel");
                test.Info("Click Add New link");
                Panel panelPage = mainPage.openPanelPage().AddNewPanel(displayName: "Logigear#$%");

                //Then
                // VP1: 
                string expectedMsg1 = "Invalid display name.The name can't contain high ASCII characters or any of following characters: /:*?<>|\"#{[]{};";
                validations.Add(panelPage.ValidateErrorMessage(expectedMsg1));

                panelPage.DismissPanelDialog();

                // VP2: 
                panelPage.AddNewPanel(displayName: "Logigear@");

                validations.Add(panelPage.ValidatePanelExisted("Logigear@"));
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
