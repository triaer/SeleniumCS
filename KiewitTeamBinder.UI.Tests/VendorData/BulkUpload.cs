using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.VendorDataModule;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using System.Threading;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;


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
                var teambinderTestAccount = GetTestAccount("VendorAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var bulkUploadData = new BulkUploadDocumentsSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + bulkUploadData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(bulkUploadData.ProjectName);

                //when - 119695 Bulk Upload Documents to Holding Area
                //User Story 120032 - Part 1
                test = LogTest("Bulk Upload Documents to Holding Area");
                string currentWindow;
                int indexOfCopyAttributeItem = 0;
                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription())
                    .LogValidation(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(bulkUploadData.SubItemLinks));

                HoldingArea holdingArea = projectDashBoard.SelectModuleMenuItem<HoldingArea>(subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription());
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(bulkUploadData.HoldingAreaPaneName))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateDisplayedViewFilterOption(bulkUploadData.DefaultFilter))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1));

                BulkUploadDocuments bulkUploadDocuments = holdingArea.ClickBulkUploadButton(out currentWindow);
                bulkUploadDocuments.LogValidation(ref validations, bulkUploadDocuments.ValidateWindowIsOpened(bulkUploadData.WindowTitle))
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateFormTitle(bulkUploadData.FormTitle))
                    .AddFilesInBulk(Utils.GetInputFilesLocalPath(), bulkUploadData.FileNames)
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateFilesDisplay(bulkUploadData.numberOfUploadFiles));
                //bulkUploadDocuments.LogValidation(ref validations, bulkUploadDocuments.ValidateFileNamesAreListedInColumn(bulkUploadData.VersionColumn));

                bulkUploadDocuments.ClickACheckboxInDocumentRow(documentRow: 1)
                    .LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateDocumentRowIsSelected(documentRow: 1))
                    .SelectDataOfDocumentPropertyDropdown(bulkUploadData.DataOfComboBoxRev, DocBulkUploadDropdownType.Rev, documentRow: 1)
                    .SelectDataOfDocumentPropertyDropdown(bulkUploadData.DataOfComboBoxSts, DocBulkUploadDropdownType.Sts, documentRow: 1)
                    .EnterDataOfDocumentPropertyTextbox(bulkUploadData.DataOfTitle, DocBulkUploadInputText.Title.ToDescription(), documentRow: 1)
                    .SelectDataOfDocumentPropertyDropdown(bulkUploadData.DataOfComboBoxDics, DocBulkUploadDropdownType.Disc, documentRow: 1)
                    .SelectDataOfDocumentPropertyDropdown(bulkUploadData.DataOfComboBoxCat, DocBulkUploadDropdownType.Cat, documentRow: 1)
                    .SelectDataOfDocumentPropertyDropdown(bulkUploadData.DataOfComboBoxType, DocBulkUploadDropdownType.Type, documentRow: 1)
                    .ClickToolbarButton<BulkUploadDocuments>(ToolbarButton.CopyAttributes)
                    .HoverOnCopyAttributesMainItem(bulkUploadData.HoverCopyAttributesItem, ref indexOfCopyAttributeItem)
                    .LogValidation<BulkUploadDocuments>(ref validations,
                                                        bulkUploadDocuments.ValidateSubMenuDisplaysAfterHovering(ref indexOfCopyAttributeItem));
                ApplyToNRowsDialog applyToNextNRowsDialog = bulkUploadDocuments.ClickToNRowsItem(ref indexOfCopyAttributeItem);
                applyToNextNRowsDialog.LogValidation<ApplyToNRowsDialog>(ref validations, applyToNextNRowsDialog.ValidateApplyToNRowsDialogDisplaysCorrectly(bulkUploadData.MessageOnToNextNRowsDialog))
                    .EnterNumberOfRow(bulkUploadData.NumberOfRow)
                    .ClickOKButton<BulkUploadDocuments>();
                bulkUploadDocuments.LogValidation<BulkUploadDocuments>(ref validations, bulkUploadDocuments.ValidateDocumentPropertiesAreCopiedToAllRows(rowIndexOfStandardRow: 1))
                    .EnterTextboxes(bulkUploadData.DocumentNoTextboxContent, DocBulkUploadInputText.DocumentNo.ToDescription());

                AlertDialog validateDialog = bulkUploadDocuments.ClickValidateDocumentDetails(ToolbarButton.Validate, ref methodValidations);
                validateDialog.LogValidation<AlertDialog>(ref validations, validateDialog.ValidateMessageDialogAsExpected(bulkUploadData.MessageOnValidateDocumentsDialog))
                    .ClickOKButton<BulkUploadDocuments>();

                ConfirmDialog saveDocumentDialog = bulkUploadDocuments.ClickSaveBulkUploadDocuments(ref methodValidations);
                saveDocumentDialog.LogValidation<ConfirmDialog>(ref validations, saveDocumentDialog.ValidateMessageDialogAsExpected(bulkUploadData.MessageOnSaveDocumentsDialog))
                    .ClickPopupButton<HoldingArea>(DialogPopupButton.No, true);

                string filterValue = bulkUploadData.DocumentNoTextboxContent + bulkUploadData.FilterWithValue;
                holdingArea.FilterDocumentsByGridFilterRow(bulkUploadData.HoldingAreaFilterByColumn, filterValue)
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateHoldingAreaGridShownDataCorrect(bulkUploadData.HoldingAreaFilterByColumn, filterValue))
                    .ClickCheckboxOfDocumentAtRow(indexRow: 1)
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateDocumentRowIsHighlighted(indexRow: 1));

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

        [TestMethod]
        public void TransmitDocuments()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                string currentWindow;
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var transmitDocData = new TransmitDocumentSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + transmitDocData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(transmitDocData.ProjectName);

                test = LogTest("Pre-condition: Upload two documents");
                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription());
                HoldingArea holdingArea = projectDashBoard.SelectModuleMenuItem<HoldingArea>(subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription());
                BulkUploadDocuments bulkUploadDocuments = holdingArea.ClickBulkUploadButton(out currentWindow);
                bulkUploadDocuments.CreateDataOnRow<HoldingArea>(2);
                
                //when User Story 120157 - 119696 Transmit Documents
                test = LogTest("Transmit Documents");
                string[] selectedDocuments = new string[transmitDocData.NumberOfSelectedDocumentRow];
                string[] selectedUsersWithCompanyName = new string[] { transmitDocData.KiewitUser.Description };

                holdingArea.SelectRowsWithoutTransmittalNo(transmitDocData.GridViewHoldingAreaName, transmitDocData.NumberOfSelectedDocumentRow, true, ref selectedDocuments)
                    .ClickHeaderButton<HoldingArea>(MainPaneTableHeaderButton.Transmit, false);

                NewTransmittal newTransmittal = holdingArea.ClickCreateTransmittalsButton();
                newTransmittal.LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateAllSelectedDocumentsAreListed(ref selectedDocuments))
                    .LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateRecordItemsCount(transmitDocData.GridViewTransmitDocName));
                SelectRecipientsDialog selectRecipientsDialog = newTransmittal.ClickRecipientsButton(transmitDocData.ToButton);
                selectRecipientsDialog.SelectCompany(transmitDocData.KiewitUser.CompanyName)
                    .ClickUserInLeftTable(transmitDocData.KiewitUser.UserName)
                    .LogValidation<SelectRecipientsDialog>(ref validations, selectRecipientsDialog.ValidateUserIsAddedToTheToTable(selectedUsersWithCompanyName))
                    .ClickOkButton<NewTransmittal>();
                newTransmittal.LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateSelectedUsersPopulateInTheToField(selectedUsersWithCompanyName))
                    .EnterSubject(transmitDocData.Subject)
                    .EnterMessage(transmitDocData.Message);
                TransmittalDetail transmittalDetail = newTransmittal.ClickSendButton(ref methodValidations);
                transmittalDetail.LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateDateIsCurrentDate())
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateProjectNumberIsCorrect(transmitDocData.ProjectNumber))
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateProjectNameIsCorrect(transmitDocData.ProjectName))                    
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateTransmittalNoIsCorrectWithTheHeader())
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateFromUserInfoIsCorrect(transmitDocData.KiewitUser.Description))
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateAttachedDocumentsAreDisplayed(selectedDocuments))
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateRecipentsAreDisplayed(selectedUsersWithCompanyName))
                    //TO-DO: Failed by bug: No hyperlink in Document number
                    //.LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateDocumentNumbersContainHyperlink(selectedDocuments))
                    //TO-DO: Failed by bug: No hyperlink in "Click here to download all Transmittal files."
                    //.LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateDownloadHyperlinkDisplays())
                    .ClickToolbarButton<HoldingArea>(ToolbarButton.Close);

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

        [TestMethod]
        public void Filtering()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var filteringData = new FilteringSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + filteringData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(filteringData.ProjectName);

                //when - 119697 Filtering
                //User Story 120159
                test = LogTest("Filtering");
                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription());

                HoldingArea holdingArea = projectDashBoard.SelectModuleMenuItem<HoldingArea>(subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription());

                var clearRecords = holdingArea.GetTableItemNumber(filteringData.GridViewHoldingAreaName);
                var filteredRecords = holdingArea.GetTableItemNumberWithConditions(filteringData.GridViewHoldingAreaName, filteringData.ValueInDocumentNoColumn1);

                holdingArea.FilterDocumentsByGridFilterRow(MainPaneTableHeaderLabel.DocumentNo.ToDescription(), filteringData.FilterValue1)
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordsMatchingFilterAreReturned(filteringData.GridViewHoldingAreaName,
                                                                                                                      filteringData.ValueInDocumentNoColumn1,
                                                                                                                      filteredRecords))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateValueInColumnIsCorrect(filteringData.GridViewHoldingAreaName,
                                                                                                            MainPaneTableHeaderLabel.HoldProcessStatus.ToDescription(),
                                                                                                            filteringData.ValueInHoldingProcessStatusColumn))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(filteringData.GridViewHoldingAreaName))
                    .ClickClearHyperlink<HoldingArea>()
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateFilteredRecordsAreCleared(filteringData.GridViewHoldingAreaName,
                                                                                                               clearRecords))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(filteringData.GridViewHoldingAreaName))
                    .SelectFilterOption<HoldingArea>(ViewFilterOptions.All.ToDescription())
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateFilterBoxIsHighlighted(ViewFilterOptions.All.ToDescription()));

                holdingArea.SelectFilterOption<HoldingArea>(ViewFilterOptions.NewDocument.ToDescription());
                filteredRecords = holdingArea.GetTableItemNumberWithConditions(filteringData.GridViewHoldingAreaName, filteringData.ValueInDocumentNoColumn2);
                holdingArea.FilterDocumentsByGridFilterRow(MainPaneTableHeaderLabel.DocumentNo.ToDescription(), filteringData.FilterValue2)
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateFilterBoxIsHighlighted(ViewFilterOptions.NewDocument.ToDescription()))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordsMatchingFilterAreReturned(filteringData.GridViewHoldingAreaName,
                                                                                                                      filteringData.ValueInDocumentNoColumn2,
                                                                                                                      filteredRecords))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateValueInColumnIsCorrect(filteringData.GridViewHoldingAreaName,
                                                                                                            MainPaneTableHeaderLabel.HoldProcessStatus.ToDescription(),
                                                                                                            filteringData.ValueInHoldingProcessStatusColumn))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(filteringData.GridViewHoldingAreaName))
                    .ClickClearHyperlink<HoldingArea>()
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateFilteredRecordsAreCleared(filteringData.GridViewHoldingAreaName,
                                                                                                               clearRecords))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(filteringData.GridViewHoldingAreaName));

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
