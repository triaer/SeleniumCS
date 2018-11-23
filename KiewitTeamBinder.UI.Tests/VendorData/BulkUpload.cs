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
using System.Threading;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using static KiewitTeamBinder.UI.KiewitTeamBinderENums;

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
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                var projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var bulkUploadData = new BulkUploadDocumentsSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + bulkUploadData.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(bulkUploadData.ProjectName);

                //when - 119695 Bulk Upload Documents to Holding Area
                //User Story 120032 - Part 1
                test = LogTest("Bulk Upload Documents to Holding Area");
                projectDashBoard.ClickVendorDataButton()
                        .LogValidation(ref validations, projectDashBoard.ValidateVendorDataMenusDisplay(bulkUploadData.VendorDataMenu));

                var holdingArea = projectDashBoard.ClickHoldingAreaButton();
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateHoldingAreaPageDisplays())
                        .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateDisplayedViewFilterOption(bulkUploadData.DefaultFilter))
                        .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1));
                string currentWindow;
                var bulkUploadDocuments = holdingArea.ClickBulkUploadButton(out currentWindow);
                bulkUploadDocuments.LogValidation(ref validations, bulkUploadDocuments.ValidateWindowIsOpened(bulkUploadData.WindowTitle))
                        .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateFormTitle(bulkUploadData.FormTitle))
                        .AddFilesInBulk(Utils.GetInputFilesLocalPath(), bulkUploadData.FileNames)
                        .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateFilesDisplay(numberOfFiles: 15));
                //bulkUploadDocuments.LogValidation(ref validations, bulkUploadDocuments.ValidateFileNamesAreListedInColumn(bulkUploadData.VersionColumn));
                             
                bulkUploadDocuments.ClickACheckboxInDocumentRow(rowIndex: 1)
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateRowIsSelected(1));

                bulkUploadDocuments.SelectDataOfDocumentPropertyDropdown(1, bulkUploadData.DataOfComboBoxRev, DocBulkUploadDropdownType.Rev)
                    .SelectDataOfDocumentPropertyDropdown(1, bulkUploadData.DataOfComboBoxSts, DocBulkUploadDropdownType.Sts)
                    .EnterDataOfDocumentPropertyTextbox(1, bulkUploadData.DataOfTitle, DocBulkUploadInputText.Title.ToDescription())
                    .SelectDataOfDocumentPropertyDropdown(1, bulkUploadData.DataOfComboBoxDics, DocBulkUploadDropdownType.Disc)
                    .SelectDataOfDocumentPropertyDropdown(1, bulkUploadData.DataOfComboBoxCat, DocBulkUploadDropdownType.Cat);
                
                int indexOfSubmenu = 0;
                bulkUploadDocuments.ClickHeaderButton<BulkUploadDocuments>(DocBulkUploadHeaderButton.CopyAttributes)
                    .HoverOnCopyAttributesMainItem(bulkUploadData.HoverCopyAttributesItem, ref indexOfSubmenu)
                    .LogValidation<BulkUploadDocuments>(ref validations, 
                                                        bulkUploadDocuments.ValidateSubMenuDisplaysAfterHovering(ref indexOfSubmenu));
                
                var applyToNextNRows = bulkUploadDocuments.ClickToNRowsItem(ref indexOfSubmenu);
                applyToNextNRows.LogValidation<ApplyToNRowsDialog>(ref validations,
                                                                   applyToNextNRows.ValidateApplyToNRowsDialogDisplaysCorrectly())
                    .LogValidation<ApplyToNRowsDialog>(ref validations,
                                                       applyToNextNRows.ValidateMessageOnDialog(bulkUploadData.MessageOnToNextNRowsDialog))
                    .EnterNumberOfRow(bulkUploadData.NumberOfRow)
                    .ClickOKButton<ApplyToNRowsDialog>()
                    .LogValidation<ApplyToNRowsDialog>(ref validations, applyToNextNRows.ValidateDialogOpens(false));
                bulkUploadDocuments.LogValidation<BulkUploadDocuments>(ref validations,
                                                                       bulkUploadDocuments.ValidateDocumentPropertiesAreCopiedToAllRows(1));

                //var validateAlert = bulkUploadDocuments.EnterTextboxes(bulkUploadData.DocumentNoTextboxContent,
                //                                                       DocBulkUploadInputText.DocumentNo.ToDescription())
                //    .ClickHeaderButton<AlertDialog>(DocBulkUploadHeaderButton.Validate);
                //validateAlert.LogValidation<AlertDialog>(ref validations,
                //                                       validateAlert.ValidateDialogOpens(true))
                //    .ClickOKButton<AlertDialog>()
                //    .LogValidation<AlertDialog>(ref validations,
                //                                       validateAlert.ValidateDialogOpens(false));

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
