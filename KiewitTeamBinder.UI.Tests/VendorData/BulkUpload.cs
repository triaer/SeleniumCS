﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.VendorData;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.VendorData
{
    [TestClass]
    public class BulkUpload : UITestBase
    {
        [TestMethod]
        public void BulkUploadDocuments()
        {
            try
            {
                // given
                validations = new List<KeyValuePair<string, bool>>();
                List<KeyValuePair<string, bool>> methodValidations = new List<KeyValuePair<string, bool>>();
                var teambinderTestAccount = GetTestAccount("VendorAccount1", environment, "NonSSO");

                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                // and 119692 - Log on via Other User Login Kiewit Account
                test = LogTest("Log on via Other User Login Kiewit Account");
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                var projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                projectsList.LogValidation<ProjectsList>(ref validations,
                                                         projectsList.ValidateDataInProjectListAvailable(BulkUploadDocumentsSmoke.ProjectName))
                            .LogValidation<ProjectsList>(ref validations,
                                                         projectsList.ValidateProjectIsHighlightedWhenHovered(BulkUploadDocumentsSmoke.ProjectName));

                test.Info("Navigate to DashBoard Page of Project: " + BulkUploadDocumentsSmoke.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(BulkUploadDocumentsSmoke.ProjectName);
                projectDashBoard.LogValidation(ref validations,
                                               projectDashBoard.ValidateProjectIsOpened(BulkUploadDocumentsSmoke.ProjectName));

                //when - 119695 Bulk Upload Documents to Holding Area
                test = LogTest("Bulk Upload Documents to Holding Area");
                projectDashBoard.ClickVendorDataButton();
                // var a = KiewitTeamBinderENums.VendorDataMenusForVendorAccount.HoldingArea;

                projectDashBoard.LogValidation<Dashboard>(ref validations,
                                                          projectDashBoard.ValidateVendorDataMenusDisplay(BulkUploadDocumentsSmoke.VendorDataMenu));

                var holdingAreaPage = projectDashBoard.ClickHoldingAreaButton();
                holdingAreaPage.LogValidation<HoldingArea>(ref validations,
                                                           holdingAreaPage.ValidateHoldingAreaPageDisplays());
                holdingAreaPage.LogValidation<HoldingArea>(ref validations,
                                                           holdingAreaPage.ValidateDefaultFilter("New Documents"));
                holdingAreaPage.LogValidation<HoldingArea>(ref validations,
                                                           holdingAreaPage.ValidateFirstFileterBoxIsHighlighted());

                var bulkUploadDocumentsPage = holdingAreaPage.ClickBulkUploadButton();
                bulkUploadDocumentsPage.LogValidation<HoldingArea>(ref validations,
                                                                   bulkUploadDocumentsPage.ValidateWindowIsOpened("Bulk Upload Documents"));
                bulkUploadDocumentsPage.LogValidation<HoldingArea>(ref validations,
                                                                   bulkUploadDocumentsPage.ValidateFormTitle("Bulk Upload Documents"));

                bulkUploadDocumentsPage.AddFilesInBulk("C:\\BulkUpLoadInFiles", BulkUploadDocumentsSmoke.FileNames);
                bulkUploadDocumentsPage.LogValidation<HoldingArea>(ref validations,
                                                                   bulkUploadDocumentsPage.ValidateFilesDisplay(15));
                bulkUploadDocumentsPage.LogValidation<HoldingArea>(ref validations,
                                                                   bulkUploadDocumentsPage.ValidateFileNamesAreListedInColumn("Version*"));

                // then
                Utils.AddCollectionToCollection(validations, methodValidations);
                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                validations = Utils.AddCollectionToCollection(validations, methodValidations);
                throw;
            }
        }
    }
}
