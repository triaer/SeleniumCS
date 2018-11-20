using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.VendorData;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;


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
                bulkUploadDocumentsPage.ValidateWindowIsOpened("Bulk Upload Documents");
                bulkUploadDocumentsPage.ValidateFormTitle("Bulk Upload Documents");
                //bulkUploadDocumentspage.ClickAddFilesInBulk();    

                bulkUploadDocumentsPage.SelectAllCheckboxes(false)
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocumentsPage.ValidateAllRowsAreSelected(false));
                bulkUploadDocumentsPage.SelectTableCheckbox(1)
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocumentsPage.ValidateRowIsSelected(1));

                bulkUploadDocumentsPage.SelectTableComboBox(1, "00 - Rev 00", KiewitTeamBinderENums.TableComboBoxType.Rev)
                    .SelectTableComboBox(1, "VSUB - Vendor Submission", KiewitTeamBinderENums.TableComboBoxType.Sts)
                    .EnterTextbox(1, KiewitTeamBinderENums.TextboxName.Title.ToDescription(), "Vendor Submitted Document")
                    .SelectTableComboBox(1, "CON - Contruction", KiewitTeamBinderENums.TableComboBoxType.Disc)
                    .SelectTableComboBox(1, "HV - HVAC", KiewitTeamBinderENums.TableComboBoxType.Cat)
                    .SelectTableCheckbox(2)
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocumentsPage.ValidateRowIsSelected(2));

                var confirmDialog = bulkUploadDocumentsPage.ClickRemoveRowsButton();
                confirmDialog.LogValidation<ConfirmDialog>(ref validations, confirmDialog.ValidateDialogOpens(true));
                confirmDialog.LogValidation<ConfirmDialog>(ref validations, confirmDialog.ValidateMessageOnDialog("Do you want to remove the selected rows?"));
                confirmDialog.ConfirmAction(true);
                confirmDialog.LogValidation<ConfirmDialog>(ref validations, confirmDialog.ValidateDialogOpens(false));

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
