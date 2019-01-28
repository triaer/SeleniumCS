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

namespace KiewitTeamBinder.UI.Tests.VendorData
{
    [TestClass]
    public class SingleDocUpload : UITestBase
    {
        [TestMethod]
        public void HoldingArea_UploadSingleDoc_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("VendorAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info(Browser.GetActiveDriverInfo());
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var uploadSingleDocData = new UploadSingleDocSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + uploadSingleDocData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(uploadSingleDocData.ProjectName);

                //when User Story 120221 - 120034 - Upload Single Doc
                test = LogTest("Upload Single Doc");
                string username = projectDashBoard.GetUserNameLogon();
                string currentWindow;
                projectDashBoard.SelectModuleMenuItemOnLeftNav<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
                HoldingArea holdingArea = projectDashBoard.SelectModuleMenuItemOnLeftNav<HoldingArea>(subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription());

                DocumentDetail newDocument = holdingArea.ClickNewButton(out currentWindow);
                newDocument.LogValidation<DocumentDetail>(ref validations, newDocument.ValidateRequiredFieldsWithRedAsterisk(uploadSingleDocData.RequiredFields))
                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateFromUserFieldShowCorrectDataAndFormat(username, uploadSingleDocData.ColorGrey))
                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateDocumentNoIsLimitRetained(uploadSingleDocData.MaxLengthOfDocNo))
                    .EnterDocumentInformation(uploadSingleDocData.SingleDocInformation, ref methodValidations)
                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateSelectedItemShowInDropdownBoxesCorrect(uploadSingleDocData.SingleDocInformation))
                    .ClickAttachFilesButton(Utils.GetInputFilesLocalPath(), uploadSingleDocData.FileNames);
                AlertDialog messageDialog = newDocument.ClickSaveInToolbarHeader();
                messageDialog.LogValidation<AlertDialog>(ref validations, newDocument.ValidateSaveDialogStatus(closed: false))
                    .LogValidation<AlertDialog>(ref validations, messageDialog.ValidateMessageDisplayCorrect(uploadSingleDocData.SaveMessage))
                    .ClickOKOnMessageDialog<DocumentDetail>()
                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateSaveDialogStatus(closed: true))
                    .ClickToolbarButtonOnWinPopup<HoldingArea>(ToolbarButton.Close);

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
        public void HoldingArea_TransmitSingleDocument_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("VendorAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                string currentWindow;
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var transmitSingleDocData = new TransmitSingleDocSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + transmitSingleDocData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(transmitSingleDocData.ProjectName);

                test = LogTest("Pre-condition: Upload two documents");
                projectDashBoard.SelectModuleMenuItemOnLeftNav<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
                HoldingArea holdingArea = projectDashBoard.SelectModuleMenuItemOnLeftNav<HoldingArea>(subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription());
                BulkUploadDocuments bulkUploadDocuments = holdingArea.ClickBulkUploadButton(out currentWindow);
                bulkUploadDocuments.CreateDataOnRow<HoldingArea>(1, transmitSingleDocData.DocumentNo);

                //when User Story 120222 - 120035 - Transmit Single Doc
                test = LogTest("Transmit Single Document");
                string[] selectedDocuments = new string[transmitSingleDocData.NumberOfSelectedDocumentRow];
                string[] selectedUsersWithCompanyName = new string[] { transmitSingleDocData.KiewitUser.Description };
                                
                holdingArea.SelectRowsByDocumentNo(transmitSingleDocData.GridViewHoldingAreaName, transmitSingleDocData.DocumentNo, transmitSingleDocData.NumberOfSelectedDocumentRow, true, ref selectedDocuments)
                    .ClickHeaderButton<HoldingArea>(MainPaneTableHeaderButton.Transmit, false);

                NewTransmittal newTransmittal = holdingArea.ClickCreateTransmittalsButton();
                newTransmittal.LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateAllSelectedDocumentsAreListed(ref selectedDocuments))
                    .LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateRecordItemsCount(transmitSingleDocData.GridViewTransmitDocName));
                SelectRecipientsDialog selectRecipientsDialog = newTransmittal.ClickRecipientsButton(transmitSingleDocData.ToButton);
                selectRecipientsDialog.SelectCompany(transmitSingleDocData.KiewitUser.CompanyName)
                    .ClickUserInLeftTable(transmitSingleDocData.KiewitUser.UserName)
                    .LogValidation<SelectRecipientsDialog>(ref validations, selectRecipientsDialog.ValidateUserIsAddedToTheToTable(selectedUsersWithCompanyName))
                    .ClickOkButton<NewTransmittal>();
                newTransmittal.LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateSelectedUsersPopulateInTheToField(selectedUsersWithCompanyName))
                    .EnterSubject(transmitSingleDocData.Subject)
                    .EnterMessage(transmitSingleDocData.Message)
                    .SelectNewTransmitDetailInforByText(transmitSingleDocData.ReasonForIssue, transmitSingleDocData.RespondByMessage, transmitSingleDocData.RespondByDate);
                TransmittalDetail transmittalDetail = newTransmittal.ClickSendButton(ref methodValidations);
                transmittalDetail.LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateDateIsCurrentDate())
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateProjectNumberIsCorrect(transmitSingleDocData.ProjectNumber))
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateProjectNameIsCorrect(transmitSingleDocData.ProjectName))
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateTransmittalNoIsCorrectWithTheHeader())
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateFromUserInfoIsCorrect(transmitSingleDocData.KiewitUser.Description))
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateAttachedDocumentsAreDisplayed(selectedDocuments))
                    .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateRecipentsAreDisplayed(selectedUsersWithCompanyName))
                    //TO-DO: Failed by bug: No hyperlink in Document number
                    //.LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateDocumentNumbersContainHyperlink(selectedDocuments))
                    //TO-DO: Failed by bug: No hyperlink in "Click here to download all Transmittal files."
                    //.LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateDownloadHyperlinkDisplays())
                    .ClickToolbarButtonOnWinPopup<HoldingArea>(ToolbarButton.Close);


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
