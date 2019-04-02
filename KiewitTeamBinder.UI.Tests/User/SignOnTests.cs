//using FluentAssertions;
//using KiewitTeamBinder.Common;
//using KiewitTeamBinder.Common.Helper;
//using KiewitTeamBinder.Common.TestData;
//using KiewitTeamBinder.UI.Pages;
//using KiewitTeamBinder.UI.Pages.Global;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using OpenQA.Selenium;
//using SimpleImpersonation;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
//using static KiewitTeamBinder.UI.ExtentReportsHelper;

//namespace KiewitTeamBinder.UI.Tests.User
//{
//    [TestClass]
//    public class SignOnTests : UITestBase
//    {

//        [TestMethod]
//        public void TC001()
//        {

//            try
//            {
//                //Given
//                //1. Navigate to Dashboard login page.
//                test.Info("Navigate to Dashboard login page.");
//                var driver = Browser.Open(Constant.HomePage, "chrome");
//                var testdemoAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
//                //When
//                //2. Enter valid username and password.
//                //3. Click on "Login" button

//                test = LogTest("DA_LOGIN_TC001 - Verify that user can login specific repository successfully via Dashboard login page with correct credentials.");
//                Login loginPage = new Login(driver);
//                MainPage mainPage = loginPage.SignOn(testdemoAccount);


//                //Then
//                //VP: Verify that Dashboard Mainpage appears
//                validations.Add(mainPage.ValidateDashboardMainPageDisplayed());
//                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
//                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
//            }
//            catch (Exception e)
//            {
//                lastException = e;
//                throw;
//            }
//        }
//        [TestMethod]
//        public void DA_MP_TO021()
//        {
//            try
//            {
//                test.Info("Navigate to Dashboard login page.");
//                var driver = Browser.Open(Constant.HomePage, "chrome");
//                var testdemoAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

//                SignOnTestsSmoke singonData = new SignOnTestsSmoke();

//                //When 

//                test = LogTest("Verify user is able to add additional sibbling pages to the parent page successfully");
//                Login loginPage = new Login(driver);
//                string PageName = singonData.PageName;
//                MainPage mainPage = loginPage.SignOn(testdemoAccount);
//                mainPage.SelectSubMenu(SubMenuItems.AddPage.ToDescription())
//                    .InputPageName(PageName)
//                    .SelectParentPage(singonData.NonParentPage)
//                    .SelectNumberCoumn(singonData.NumberCoumn)
//                    .ClickOKButton()
//                    .SelectSubMenu(SubMenuItems.AddPage.ToDescription())
//                    .InputPageName(singonData.PageName)
//                    .SelectParentPage(PageName)
//                    .SelectNumberCoumn(singonData.NumberCoumn)
//                    .ClickOKButton()
//                    .ClickOnParentPage(PageName)
//                    .SelectSubMenu(SubMenuItems.Delete.ToDescription(), false).LogValidation<MainPage>(ref validations, mainPage.ValidateFristAlert())
//                    .ClickOKButtonOnAlert().LogValidation<MainPage>(ref validations, mainPage.ValidateSecondAlert(PageName))
//                    .ClickOKButtonOnAlert();


//            }
//            catch (Exception e)
//            {
//                lastException = e;
//                throw;
//            }
//        }

//        [TestMethod]
//        public void DA_MP_TO022()
//        {
//            try
//            {
//                var driver = Browser.Open(Constant.GGPage, "chrome");
//                MainPage mainPage = new MainPage(driver);
//                test.Info("Navigate to Dashboard login page.");
//                var testdemoAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
//                string us = testdemoAccount.Username;
//                string pw = testdemoAccount.Password;
//                //When 

//                test = LogTest("Verify user is able to add additional sibbling pages to the parent page successfully");
//                //Login loginPage = new Login(driver);
//                validations.Add(mainPage.VlidateTest());

//                //test = LogTest("Verify user is able to add additional sibbling pages to the parent page successfully");
                

//                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
//                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);

//            }
//            catch (Exception e)
//            {
//                lastException = e;
//                throw;
//            }
//        }


//        [TestMethod]
//        public void DA_DP_TC100()
//        {
//            try
//            {
//                //given
//                var driver = Browser.Open(Constant.HomePage, "chrome");
//                var testdemoAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
//                //Login loginPage = new Login(driver);
//                SignOnTestsSmoke signOnData = new SignOnTestsSmoke();
//                MainPage mainPage = new Login(driver).SignOn(testdemoAccount);
//                DataProfiles dataProfile = mainPage.NavigateToDataProfilesPage();

//                //when
//                test = LogTest("Verify all settings are recorded and updated correctly when user click on 'Finish' buttons");
//                //dataProfile.IpuntName(signOnData.InputName)
//                //           .SelectItemType(signOnData.ItemType)
//                //           .SelectRelated(signOnData.RelatedDataItem)
//                //           .ClickNextButton(true, dataProfile._displayFileds)
//                //           .CheckNameCheckBox()
//                //           .ClickNextButton(true, dataProfile._displayFileds)
//                //           .SelectField(signOnData.Field)
//                //           .CLickAddLevelButton()
//                //           .ClickNextButton(true, dataProfile._displayFileds)
//                //           .AddCriteria(signOnData.ValueDescription, signOnData.FitllterField)
//                //           .ClickNextButton(true, dataProfile._displayFileds)
//                //           .CheckNameCheckBox()
//                //           .ClickFinishBUtton(true, dataProfile._dataProfilesColumn)
//                dataProfile.AddNewDataProfile(signOnData.InputName, signOnData.ItemType, signOnData.RelatedDataItem, signOnData.Field, signOnData.ValueDescription,signOnData.FitllterField)
//                           .SelectCreatedDataProfiles(signOnData.InputName).LogValidation<DataProfiles>(ref validations, dataProfile.ValidateDataProfilename(signOnData.InputName))
//                                                                           .LogValidation<DataProfiles>(ref validations, dataProfile.ValidateItemType(signOnData.ItemType))
//                                                                           .LogValidation<DataProfiles>(ref validations, dataProfile.ValidateRelatedData(signOnData.RelatedDataItem))
//                           .ClickNextButton(dataProfile._displayFileds).LogValidation<DataProfiles>(ref validations, dataProfile.ValidateNameisCheked())
//                           .ClickNextButton(dataProfile._displayFileds).LogValidation<DataProfiles>(ref validations, dataProfile.ValidateAddedLevelIsExit(signOnData.Field))
//                           .ClickNextButton(dataProfile._displayFileds).LogValidation<DataProfiles>(ref validations, dataProfile.ValidateFilterValue(signOnData.FitllterField, signOnData.ValueDescription, signOnData.AndFillter))
//                           .ClickNextButton(dataProfile._displayFileds).LogValidation<DataProfiles>(ref validations, dataProfile.ValidateNameisCheked());

//                //then
//                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
//                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);


//            }
//            catch (Exception e)
//            {
//                lastException = e;
//                throw;
//            }
//        }

//        [TestMethod]
//        public void DA_DP_TC99()
//        {
//            try
//            {
//                //given
//                var driver = Browser.Open(Constant.HomePage, "chrome");
//                var testdemoAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
//                SignOnTestsSmoke signOnData = new SignOnTestsSmoke();
//                MainPage mainPage = new Login(driver).SignOn(testdemoAccount);
//                DataProfiles dataProfile = mainPage.NavigateToDataProfilesPage();

//                //when
//                dataProfile.IpuntName(signOnData.InputName)
//                           .SelectItemType(signOnData.ItemType)
//                           .NavigateToStaticPage()
//                           //.CheckAllFields()
//                           //.UnCheckAllField()
//                           .CheckAllFields().LogValidation<DataProfiles>(ref validations, dataProfile.ValidateAllCheckBoxesAreChecked())
//                           .UnCheckAllField().LogValidation<DataProfiles>(ref validations, dataProfile.ValidationAllCheckBoxesAreUncheked());
//                //then
//                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
//                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);

//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }

//        [TestMethod]
//        public void DA_DP_TC98()
//        {
//            try
//            {
//                //given
//                var driver = Browser.Open(Constant.HomePage, "chrome");
//                var testdemoAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
//                SignOnTestsSmoke singOnData = new SignOnTestsSmoke();
//                MainPage mainpage = new Login(driver).SignOn(testdemoAccount);

//                //when
//                DataProfiles dataProfile = mainpage.NavigateToDataProfilesPage();
//                dataProfile.IpuntName(singOnData.InputName)
//                           .SelectItemType(singOnData.ItemType)
//                           .NavigateToStaticPage().LogValidation<DataProfiles>(ref validations, dataProfile.ValidateAllFieldsArePopulated());

//                //then
//                Console.WriteLine((string.Join(System.Environment.NewLine, validations.ToArray())));
//                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
//            }
//            catch (Exception)
//            {

//                throw;
//            }

//        }

       
//        //[TestMethod]
//        //public void General_NonSSOValidUserSignon_UI()
//        //{
//        //    try
//        //    {
//        //        // given
//        //        var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

//        //        test.Info("Open TeamBinder Document Web Page: " + teambinderTestAccount.Url);
//        //        var driver = Browser.Open(teambinderTestAccount.Url, browser);
//        //        var projectName = OtherUserLogin.ProjectName;

//        //        // when
//        //        //119692 - Log on via Other User Login Kiewit Account
//        //        test = LogTest("Log on via Other User Login Kiewit Account");
//        //        test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
//        //        ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
//        //        projectsList.LogValidation<ProjectsList>(ref validations, projectsList.ValidateDataInProjectListAvailable(projectName))
//        //                        .LogValidation<ProjectsList>(ref validations, projectsList.ValidateProjectIsHighlightedWhenHovered(projectName));
//        //        test.Info("Navigate to DashBoard Page of Project: " + projectName);
//        //        ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(projectName);
//        //        projectDashBoard.LogValidation(ref validations, projectDashBoard.ValidateProjectIsOpened(projectName))
//        //            .Logout();
//        //        // then
//        //        Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
//        //        validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        lastException = e;
//        //        throw;
//        //    }
//        //}

//        //[TestMethod]
//        //public void General_SSOValidUserSignon_UI()
//        //{
//        //    // given
//        //    var teambinderTestAccount = GetTestAccount("VendorAccount1", environment, "KWUser", "SuperUserA");
//        //    test.Info("Open TeamBinder Document Web Page: " + teambinderTestAccount.Url);
//        //    var driver = Browser.Open(teambinderTestAccount.Url, browser);
//        //    var projectName = SSOLogin.ProjectName;

//        //    // when
//        //    test.Info("Log on TeamBinder via Kiewit User Login: " + teambinderTestAccount.kiewitUserName);
//        //    ProjectsList projectsList = new SsoSignOn(driver).KiewitUserLogon(teambinderTestAccount) as ProjectsList;
//        //    test.Info("Navigate to DashBoard Page of Project: " + projectName);
//        //    ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(projectName);
//        //    projectDashBoard
//        //        .LogValidation(ref validations, projectDashBoard.ValidateProjectIsOpened(projectName))
//        //        .Logout();

//        //    // then
//        //    Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));

//        //    validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
//        //}
//    }
//}
