using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.PopupWindows;
using KiewitTeamBinder.UI.Pages.VendorDataModule;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using System.Threading;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.UI.Pages.TransmittalsModule;
using KiewitTeamBinder.UI.Pages.DocumentModule;

namespace KiewitTeamBinder.UI.Tests.Transmittals
{
    [TestClass]
    public class ProcessingDocs : UITestBase
    {
        [TestMethod]
        public void Transmittal_ValidateTransmittalReceipt_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var transmittalReceiptData = new TransmittalReceiptSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + transmittalReceiptData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(transmittalReceiptData.ProjectName);

                //when User Story 120224 - 120012 - A User Log in validate transmittal receipt
                test = LogTest("A User Log in validate transmittal receipt");
                string dashboardWindow;
                string[] selectedUsersWithCompanyName = new string[] { transmittalReceiptData.SelectedUserWithCompany.Description };
                var columnValuesInConditionList = new List<KeyValuePair<string, string>> { transmittalReceiptData.ColumnValuesInConditionList.Subject };

                projectDashBoard.SelectModuleMenuItemOnLeftNav<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.TRANSMITTALS.ToDescription(), waitForLoading: false)
                    .LogValidation<ProjectsDashboard>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(transmittalReceiptData.SubItemMenus));

                Transmittal transmittal = projectDashBoard.SelectModuleMenuItemOnLeftNav<Transmittal>(subMenuItem: ModuleSubMenuInLeftNav.INBOX.ToDescription());
                transmittal.LogValidation<Transmittal>(ref validations, transmittal.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.INBOX.ToDescription()))
                    .LogValidation<Transmittal>(ref validations, transmittal.ValidateDisplayedViewFilterOption(transmittalReceiptData.DefaultFilter))
                    .LogValidation<Transmittal>(ref validations, transmittal.ValidateItemsAreShown(columnValuesInConditionList, transmittalReceiptData.GridViewName))
                    .LogValidation<Transmittal>(ref validations, transmittal.ValidateNewTransmittalFromVendorInbox());

                columnValuesInConditionList = new List<KeyValuePair<string, string>> { transmittalReceiptData.ColumnValuesInConditionList.DocumentNo };
                TransmittalDetail transmittalDetail = transmittal.OpenTransmittalMail(transmittalReceiptData.transmittalMailInformation.TransmittalNo, out dashboardWindow);
                transmittalDetail.LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateTransmittalDetailDiplayCorrect(transmittalReceiptData.transmittalMailInformation))
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateTransmittalNoIsCorrectWithTheHeader())
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateTransmittalDetailCotainsHyperLink(transmittalReceiptData.transmittalMailInformation))
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateRecipentsAreDisplayed(selectedUsersWithCompanyName))
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateAttachedDocumentsAreDisplayed(transmittalReceiptData.transmittalMailInformation.AttachedDocumentInfor))
                    .ClickTabMenu(TabMenuInTransmittalDetail.Documents.ToString())
                    .LogValidation<TransmittalDetail>(ref validations, transmittal.ValidateItemsAreShown(columnValuesInConditionList, transmittalReceiptData.GridViewDocumentName))
                    .ClickCloseInBottomPage<Transmittal>()
                    .LogValidation<Transmittal>(ref validations, transmittalDetail.ValidateTransmittalDetailWindowIsClosed());

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
        public void HoldingArea_ProcessDocument_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                test.Info(Browser.GetActiveDriverInfo());
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var processDocumentData = new ProcessDocumentSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + processDocumentData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(processDocumentData.ProjectName);

                //when
                //Pre-Condition Upload Single Document
                string currentWindow;

                HoldingArea holdingArea = projectDashBoard.SelectModuleMenuItemOnLeftNav<HoldingArea>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription());
                DocumentDetail newDocument = holdingArea.ClickNewButton(out currentWindow);
                newDocument.EnterDocumentInformation(processDocumentData.SingleDocInformation, ref methodValidations)
                           .ClickSaveInToolbarHeader()
                           .ClickOKOnMessageDialog<DocumentDetail>()
                           .ClickCloseButtonOnPopUp<HoldingArea>();

                //User Story 120013 - Process Document
                test = LogTest("Process Document");
                var columnValuesInConditionList = new List<KeyValuePair<string, string>> { processDocumentData.ColumnValuesInConditionList.DocumentNo };
                projectDashBoard.SelectModuleMenuItemOnLeftNav<HoldingArea>(subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription());
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateDisplayedViewFilterOption(processDocumentData.DefaultFilterAtHoldingAreaPane))
                           .FilterDocumentsByGridFilterRow < HoldingArea >(processDocumentData.GridViewHoldingAreaName, MainPaneTableHeaderLabel.DocumentNo.ToDescription(), processDocumentData.SingleDocInformation.DocumentNo)
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(processDocumentData.GridViewHoldingAreaName))
                           .ClickCheckboxOfDocumentAtRow(indexRow: 1)
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateDocumentRowIsHighlighted(indexRow: 1))
                           .FilterDocumentsByGridFilterRow<HoldingArea>(processDocumentData.GridViewHoldingAreaName, MainPaneTableHeaderLabel.Title.ToDescription(), processDocumentData.SingleDocInformation.Title)
                           .ClickCheckboxOfDocumentAtRow(indexRow: 1)
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateDocumentRowIsHighlighted(indexRow: 1));

                ProcessDocuments processDocuments = holdingArea.ClickProcessDocumentButton(MainPaneTableHeaderButton.ProcessDocuments.ToDescription(), out currentWindow);
                processDocuments.LogValidation<ProcessDocuments>(ref validations, processDocuments.ValidateWindowIsOpened(processDocumentData.WindowTitle))
                                .LogValidation<ProcessDocuments>(ref validations, processDocuments.ValidateDocumentDetailDisplayCorrect(processDocumentData.SingleDocInformation, processDocumentData.listHeader));

                int countWindow = processDocuments.GetCountWindow();
                AlertDialog validateDialog = processDocuments.ClickValidateDocumentDetails(ToolbarButton.Validate, ref methodValidations, processDocumentData.ProcessMessage);
                validateDialog.LogValidation<AlertDialog>(ref validations, validateDialog.ValidateMessageDialogAsExpected(processDocumentData.MessageOnValidateDocumentsDialog))
                              .ClickOKOnMessageDialog<ProcessDocuments>();

                DocumentReceivedDateDialog receivedDateDialog = processDocuments.ClickProcessDocumentDetails(ToolbarButton.Process);
                receivedDateDialog.SelectDate<DocumentReceivedDateDialog>(processDocumentData.ReceivedDate);

                ConfirmDialog confirmDialog = receivedDateDialog.ClickYesButton();
                confirmDialog.LogValidation<ConfirmDialog>(ref validations, confirmDialog.ValidateMessageDialogAsExpected(processDocumentData.MessageOnSaveDocumentsDialog))
                             .ClickPopupButton<HoldingArea>(DialogPopupButton.No, true)
                             .LogValidation<HoldingArea>(ref validations, processDocuments.ValidateProcessDocumentlWindowIsClosed(countWindow));

                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateItemsAreNotShown(processDocumentData.ColumnNameFilter, processDocumentData.SingleDocInformation.DocumentNo, processDocumentData.GridViewHoldingAreaName))
                           .SelectFilterOption<HoldingArea>(processDocumentData.AcceptedOptionFilterInHoldingArea)
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateItemsAreShown(columnValuesInConditionList, processDocumentData.GridViewHoldingAreaName));

                Document documentModule = projectDashBoard.SelectModuleMenuItemOnLeftNav<Document>(ModuleNameInLeftNav.DOCUMENTS.ToDescription());
                documentModule.SelectFilterOption<Document>(processDocumentData.IndexOptionFilterInDocument, false)
                              .LogValidation<Document>(ref validations, documentModule.ValidateItemsAreShown(columnValuesInConditionList, processDocumentData.GridViewDocumentName))
                              .Logout();

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
