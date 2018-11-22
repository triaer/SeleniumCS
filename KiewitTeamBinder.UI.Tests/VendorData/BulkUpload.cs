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
                var bulkUploadData = new BulkUploadDocumentsSmoke();
                projectsList.LogValidation<ProjectsList>(ref validations,
                                                         projectsList.ValidateDataInProjectListAvailable(bulkUploadData.ProjectName))
                            .LogValidation<ProjectsList>(ref validations,
                                                         projectsList.ValidateProjectIsHighlightedWhenHovered(bulkUploadData.ProjectName));

                test.Info("Navigate to DashBoard Page of Project: " + bulkUploadData.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(bulkUploadData.ProjectName);
                projectDashBoard.LogValidation(ref validations,
                                               projectDashBoard.ValidateProjectIsOpened(bulkUploadData.ProjectName));

                //when - 119695 Bulk Upload Documents to Holding Area
                test = LogTest("Bulk Upload Documents to Holding Area");
                
                projectDashBoard.ClickVendorDataButton();

                projectDashBoard.LogValidation<Dashboard>(ref validations,
                                                          projectDashBoard.ValidateVendorDataMenusDisplay(bulkUploadData.VendorDataMenu));

                var holdingArea = projectDashBoard.ClickHoldingAreaButton();
                holdingArea.LogValidation<HoldingArea>(ref validations,
                                                       holdingArea.ValidateHoldingAreaPageDisplays());
                holdingArea.LogValidation<HoldingArea>(ref validations,
                                                       holdingArea.ValidateDefaultFilter(bulkUploadData.DefaultFilter));
                holdingArea.LogValidation<HoldingArea>(ref validations,
                                                       holdingArea.ValidateFirstFileterBoxIsHighlighted());

                var bulkUploadDocuments = holdingArea.ClickBulkUploadButton();
                bulkUploadDocuments.LogValidation<HoldingArea>(ref validations,
                                                               bulkUploadDocuments.ValidateWindowIsOpened(bulkUploadData.WindowTitle));
                bulkUploadDocuments.LogValidation<HoldingArea>(ref validations,
                                                               bulkUploadDocuments.ValidateFormTitle(bulkUploadData.FormTitle));

                bulkUploadDocuments.AddFilesInBulk(bulkUploadData.FilePath, bulkUploadData.FileNames);
                bulkUploadDocuments.LogValidation<HoldingArea>(ref validations,
                                                               bulkUploadDocuments.ValidateFilesDisplay(15));
                bulkUploadDocuments.LogValidation<HoldingArea>(ref validations,
                                                               bulkUploadDocuments.ValidateFileNamesAreListedInColumn(bulkUploadData.VersionColumn));

                bulkUploadDocuments.SelectAllCheckboxes(false)
                   .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateAllRowsAreSelected(false));
                bulkUploadDocuments.SelectTableCheckbox(rowIndex: 1)
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateRowIsSelected(1));

                bulkUploadDocuments.SelectTableComboBox(1, bulkUploadData.DataOfComboBoxRev, 
                                                        KiewitTeamBinderENums.TableComboBoxType.Rev)
                    .SelectTableComboBox(1, bulkUploadData.DataOfComboBoxSts, KiewitTeamBinderENums.TableComboBoxType.Sts)
                    .EnterTextbox(1, bulkUploadData.DataOfTitle, KiewitTeamBinderENums.TextboxName.Title.ToDescription())
                    .SelectTableComboBox(1, bulkUploadData.DataOfComboBoxDics, KiewitTeamBinderENums.TableComboBoxType.Disc)
                    .SelectTableComboBox(1, bulkUploadData.DataOfComboBoxCat, KiewitTeamBinderENums.TableComboBoxType.Cat)
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateRowIsSelected(rowIndex: 2));

                int indexOfSubmenu = 0;
                bulkUploadDocuments.ClickButton<BulkUploadDocuments>(KiewitTeamBinderENums.ButtonName.CopyAttributes)
                    .HoverItem(bulkUploadData.HoverItem, ref indexOfSubmenu)
                    .LogValidation<BulkUploadDocuments>(ref validations, 
                                                        bulkUploadDocuments
                                                            .ValidateSubMenuDisplaysAfterHovering(ref indexOfSubmenu));
                
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
