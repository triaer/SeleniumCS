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
                // and 
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                var projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
                test.Info("Navigate to DashBoard Page of Project: " + BulkUploadDocumentsSmoke.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(BulkUploadDocumentsSmoke.ProjectName);

                //when - 119695 Bulk Upload Documents to Holding Area
                test = LogTest("Bulk Upload Documents to Holding Area");
                string currentWindow;
                var bulkUploadDocuments = projectDashBoard.ClickVendorDataButton()
                        .LogValidation<ProjectsDashboard>(ref validations,
                                                          projectDashBoard.ValidateVendorDataMenusDisplay(BulkUploadDocumentsSmoke.VendorDataMenu))
                        .ClickHoldingAreaButton()
                        .LogValidation<HoldingArea>(ref validations,
                                                           holdingAreaPage.ValidateHoldingAreaPageDisplays())
                        .LogValidation<HoldingArea>(ref validations,
                                                           holdingAreaPage.ValidateDefaultFilter("New Documents"));
                holdingAreaPage.LogValidation<HoldingArea>(ref validations,
                                                           holdingAreaPage.ValidateFirstFileterBoxIsHighlighted());

                var bulkUploadDocumentsPage = holdingAreaPage.ClickBulkUploadButton(out currentWindow);
                bulkUploadDocumentsPage.LogValidation<HoldingArea>(ref validations,
                                                                   bulkUploadDocumentsPage.ValidateWindowIsOpened("Bulk Upload Documents"));
                bulkUploadDocumentsPage.LogValidation<HoldingArea>(ref validations,
                                                                   bulkUploadDocumentsPage.ValidateFormTitle("Bulk Upload Documents"));

                bulkUploadDocumentsPage.AddFilesInBulk("C:\\Working\\BulkUpLoadInFiles", BulkUploadDocumentsSmoke.FileNames);
                bulkUploadDocumentsPage.LogValidation<HoldingArea>(ref validations,
                                                                   bulkUploadDocumentsPage.ValidateFilesDisplay(15));
                bulkUploadDocumentsPage.LogValidation<HoldingArea>(ref validations,
                                                                   bulkUploadDocumentsPage.ValidateFileNamesAreListedInColumn("Version*"));

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

                /*
                var bulkUploadDocuments = holdingAreaPage.ClickBulkUploadButton(out currentWindow);
                //bulkUploadDocumentspage.ValidateWindowIsOpened("Bulk Upload Documents");
                //bulkUploadDocumentspage.ValidateFormTitle("Bulk Upload Documents");
                //bulkUploadDocumentspage.ClickAddFilesInBulk();
                bulkUploadDocuments.ClickValidateButton()
                                   .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateMessageDialogBoxDisplayedForValidateFunction())
                                   .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateButtonDialogBoxDisplayed(BulkUploadDocumentsSmoke.ValidateFuntionButton))
                                   .ClickOkButtonOnDialogBox()
                                   .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateDialogBoxClosed())
                                   .ClickSaveButton()
                                   //.ClickCancelButton()
                                   .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateMessageDialogBoxDisplayedForSaveAndCancelFunction())
                                   .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateButtonDialogBoxDisplayed(BulkUploadDocumentsSmoke.SaveFuntionButton))
                                   .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateButtonIsHighlightedWhenHovered(BulkUploadDocumentsSmoke.ValueButtonOnPopUp))
                                   .ClickNoButtonOnDialogBox()
                                   .LogValidation<HoldingArea>(ref validations, bulkUploadDocuments.ValidateDialogBoxClosed())
                                   .SwitchToWindow(currentWindow)
                                   ;
                bulkUploadDocuments.EnterDocumentNo(BulkUploadDocumentsSmoke.DocumentNo)
                                   .PressEnter();
                */

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
