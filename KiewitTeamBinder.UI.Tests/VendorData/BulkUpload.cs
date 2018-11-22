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
                test = LogTest("Bulk Upload Documents to Holding Area");
                projectDashBoard.ClickVendorDataButton()
                        .LogValidation(ref validations, projectDashBoard.ValidateVendorDataMenusDisplay(bulkUploadData.VendorDataMenu));

                var holdingArea = projectDashBoard.ClickHoldingAreaButton();
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateHoldingAreaPageDisplays())
                        .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateDefaultFilter(bulkUploadData.DefaultFilter))
                        .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateFirstFileterBoxIsHighlighted());
                string currentWindow;
                var bulkUploadDocuments = holdingArea.ClickBulkUploadButton(out currentWindow);
                bulkUploadDocuments.LogValidation(ref validations, bulkUploadDocuments.ValidateWindowIsOpened(bulkUploadData.WindowTitle))
                        .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateFormTitle(bulkUploadData.FormTitle))
                        .AddFilesInBulk(bulkUploadData.FilePath, bulkUploadData.FileNames)
                        .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateFilesDisplay(15))
                        .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateFileNamesAreListedInColumn(bulkUploadData.VersionColumn))
                        .SelectTableCheckbox(rowIndex: 1)
                        .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateRowIsSelected(1))
                        .SelectDataOfDocumentPropertyDropdown(1, bulkUploadData.DataOfComboBoxRev, DocBulkUploadDropdownType.Rev)
                    .SelectDataOfDocumentPropertyDropdown(1, bulkUploadData.DataOfComboBoxSts, DocBulkUploadDropdownType.Sts)
                    .EnterDataOfDocumentPropertyTextbox(1, bulkUploadData.DataOfTitle, DocBulkUploadInputText.Title.ToDescription())
                    .SelectDataOfDocumentPropertyDropdown(1, bulkUploadData.DataOfComboBoxDics, DocBulkUploadDropdownType.Disc)
                    .SelectDataOfDocumentPropertyDropdown(1, bulkUploadData.DataOfComboBoxCat, DocBulkUploadDropdownType.Cat) 
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateRowIsSelected(rowIndex: 2));

                int indexOfSubmenu = 0;
                bulkUploadDocuments.ClickHeaderButton<BulkUploadDocuments>(DocBulkUploadHeaderButton.CopyAttributes)
                    .HoverOnCopyAttributesMainItem(bulkUploadData.HoverCopyAttributesItem, ref indexOfSubmenu)
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateSubMenuDisplaysAfterHovering(ref indexOfSubmenu));
                
                var applyToNextNRowsDialog = bulkUploadDocuments.ClickToNRowsItem(ref indexOfSubmenu);
                applyToNextNRowsDialog.LogValidation<ApplyToNRowsDialog>(ref validations,
                                                                         applyToNextNRowsDialog.ValidateDialogOpens(true));
                applyToNextNRowsDialog.LogValidation<ApplyToNRowsDialog>(ref validations,
                                                                         applyToNextNRowsDialog
                                                                            .ValidateMessageOnDialog(bulkUploadData
                                                                                .MessageOnToNRowsDialog));
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
