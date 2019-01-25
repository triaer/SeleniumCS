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

namespace KiewitTeamBinder.UI.Tests.VendorData
{
    [TestClass]
    public class UploadUnrestrainedDocument : UITestBase
    {
        [TestMethod]
        public void HoldingArea_UploadUnrestrainedDocument_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var uploadUnrestrainedDocData = new UploadUnrestrainedDocSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + uploadUnrestrainedDocData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(uploadUnrestrainedDocData.ProjectName);

                // when 
                // User Story 120278 - 120081 - Upload Unrestrained Document Part 1
                test = LogTest("Upload Unrestrained Document");
                string username = projectDashBoard.GetUserNameLogon();
                string currentWindow;
                projectDashBoard.SelectModuleMenuItemOnLeftNav<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
                HoldingArea holdingArea = projectDashBoard.SelectModuleMenuItemOnLeftNav<HoldingArea>(subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription());

                DocumentDetail newDocument = holdingArea.ClickNewButton(out currentWindow);
                newDocument.LogValidation<DocumentDetail>(ref validations, newDocument.ValidateRequiredFieldsWithRedAsterisk(uploadUnrestrainedDocData.RequiredFields))
                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateFromUserFieldShowCorrectDataAndFormat(username, uploadUnrestrainedDocData.ColorGrey))
                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateDocumentNoIsLimitRetained(uploadUnrestrainedDocData.MaxLengthOfDocNo))
                    .EnterDocumentInformation(uploadUnrestrainedDocData.SingleDocInformation, ref methodValidations)
                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateSelectedItemShowInDropdownBoxesCorrect(uploadUnrestrainedDocData.SingleDocInformation))
                    .ClickAttachFilesButton(Utils.GetInputFilesLocalPath(), uploadUnrestrainedDocData.FileNames);
                AlertDialog messageDialog = newDocument.ClickSaveInToolbarHeader();
                messageDialog.LogValidation<DocumentDetail>(ref validations, newDocument.ValidateSaveDialogStatus(closed: false))
                    .LogValidation<AlertDialog>(ref validations, messageDialog.ValidateMessageDisplayCorrect(uploadUnrestrainedDocData.SaveMessage))
                    .ClickOKOnMessageDialog<DocumentDetail>()
                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateSaveDialogStatus(closed: true))
                    .ClickToolbarButtonOnWinPopup<HoldingArea>(ToolbarButton.Close);

                // User Story 120278 - 120081 - Upload Unrestrained Document Part 2
                var columnValuesInConditionList = new List<KeyValuePair<string, string>> { uploadUnrestrainedDocData.ColumnValuesInConditionList.DocumentNo };
                var columnValuePairList = uploadUnrestrainedDocData.ExpectedDocumentInforInColumnList(uploadUnrestrainedDocData.SingleDocInformation);
                string[] selectedUsersWithCompanyName = uploadUnrestrainedDocData.ListUserTo.ToArray();
                holdingArea.SwitchToWindow(currentWindow);
                holdingArea.SelectModuleMenuItemOnLeftNav<HoldingArea>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription())
                           .EnterDocumentNo(uploadUnrestrainedDocData.SingleDocInformation.DocumentNo)
                           //.PressEnterKey<HoldingArea>(uploadUnrestrainedDocData.GridViewHoldingAreaData)
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordsMatchingFilterAreReturned(uploadUnrestrainedDocData.GridViewHoldingAreaName, columnValuesInConditionList, 1))
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateItemsAreShown(columnValuesInConditionList, uploadUnrestrainedDocData.GridViewHoldingAreaName));
                           //.OpenDocumentByIndex<DocumentDetail>(1, out currentWindow);
                newDocument.LogValidation<DocumentDetail>(ref validations, newDocument.ValidateWindowIsOpened(uploadUnrestrainedDocData.DocumentDetailWindow(uploadUnrestrainedDocData.SingleDocInformation)))
                           .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateDocumentDetailsDisplayCorrect(columnValuePairList))
                           .ClickToolbarButtonOnWinPopup<HoldingArea>(ToolbarButton.Close)
                           .SwitchToWindow(currentWindow);
                holdingArea.ClickCheckboxOfDocumentAtRow(indexRow: 1)
                           .ClickHeaderButton<HoldingArea>(MainPaneTableHeaderButton.Transmit, false);
                NewTransmittal newTransmittal = holdingArea.ClickCreateTransmittalsButton();
                newTransmittal.LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateWindowIsOpened(uploadUnrestrainedDocData.VendorDocumentSubmissionWindow));
                SelectRecipientsDialog selectRecipientsDialog = newTransmittal.ClickRecipientsButton(uploadUnrestrainedDocData.ToButton, false);
                selectRecipientsDialog.LogValidation<SelectRecipientsDialog>(ref validations, selectRecipientsDialog.ValidateSelectRecipientWindowStatus())
                                      .SwitchToFrame()
                                      .SelectCompany(uploadUnrestrainedDocData.CompanyName)
                                      .LogValidation<SelectRecipientsDialog>(ref validations, selectRecipientsDialog.ValidateListItemUserInLeftGrid(uploadUnrestrainedDocData.ListUser))
                                      .AddUserToTheTable(uploadUnrestrainedDocData.toTableTo, uploadUnrestrainedDocData.ListUserTo)
                                      .LogValidation<SelectRecipientsDialog>(ref validations, selectRecipientsDialog.ValidateUserIsAddedToTheTable(uploadUnrestrainedDocData.toTableTo, uploadUnrestrainedDocData.CompanyName, uploadUnrestrainedDocData.ListUserTo))
                                      .LogValidation<SelectRecipientsDialog>(ref validations, selectRecipientsDialog.ValidateUserIsHighlightedInTheTable(uploadUnrestrainedDocData.toTableTo, uploadUnrestrainedDocData.CompanyName, uploadUnrestrainedDocData.ListUserTo))
                                      .LogValidation<SelectRecipientsDialog>(ref validations, selectRecipientsDialog.ValidateUserIsRetainedCheckMarkInTheTable(uploadUnrestrainedDocData.toTableTo, uploadUnrestrainedDocData.CompanyName, uploadUnrestrainedDocData.ListUserTo))
                                      .AddUserToTheTable(uploadUnrestrainedDocData.toTableCc, uploadUnrestrainedDocData.ListUserCc)
                                      .LogValidation<SelectRecipientsDialog>(ref validations, selectRecipientsDialog.ValidateUserIsAddedToTheTable(uploadUnrestrainedDocData.toTableCc, uploadUnrestrainedDocData.CompanyName, uploadUnrestrainedDocData.ListUserCc))
                                      .ClickOkButton<NewTransmittal>();
                newTransmittal.LogValidation<NewTransmittal>(ref validations, selectRecipientsDialog.ValidateSelectRecipientWindowStatus(closed: true))
                              .LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateSelectedUsersPopulateInTheToField(uploadUnrestrainedDocData.ListUserTo.ToArray(), uploadUnrestrainedDocData.CompanyName))
                              .EnterSubject(uploadUnrestrainedDocData.Subject)
                              .EnterMessage(uploadUnrestrainedDocData.Message);
                TransmittalDetail transmittalDetail = newTransmittal.ClickSendButton(ref methodValidations);

                string tranmisttalNo = transmittalDetail.GetTransmittalNo();
                string transmittalDetailWindow = string.Format(uploadUnrestrainedDocData.TransmittalDetailWindow, tranmisttalNo, uploadUnrestrainedDocData.Subject);
                transmittalDetail.LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateWindowIsOpened(transmittalDetailWindow))
                                 .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateProjectNumberIsCorrect(uploadUnrestrainedDocData.ProjectNumber))
                                 .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateProjectNameIsCorrect(uploadUnrestrainedDocData.ProjectName))
                                 .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateTransmittalNoIsCorrectWithTheHeader())
                                 .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateFromUserInfoIsCorrect(uploadUnrestrainedDocData.DescriptionUserVendor2))
                                 .LogValidation<TransmittalDetail>(ref validations, transmittalDetail.ValidateRecipentsAreDisplayed(selectedUsersWithCompanyName, false, uploadUnrestrainedDocData.CompanyName))
                                 .ClickToolbarButtonOnWinPopup<HoldingArea>(ToolbarButton.Close)
                                 .SwitchToWindow(currentWindow);
                holdingArea.LogValidation<HoldingArea>(ref validations, transmittalDetail.ValidateTransmittalDetailWindowIsClosed())
                           .Logout();

                // User Story 120278 - 120081 - Upload Unrestrained Document Part 3
                // Pre - Condition
                teambinderTestAccount = GetTestAccount("StandardAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
                test.Info("Navigate to DashBoard Page of Project: " + uploadUnrestrainedDocData.ProjectName);
                projectsList.NavigateToProjectDashboardPage(uploadUnrestrainedDocData.ProjectName);

                // when
                columnValuesInConditionList = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(MainPaneTableHeaderLabel.TransmittalNo.ToDescription(), tranmisttalNo) };
                Transmittal transmittalsModule = projectDashBoard.SelectModuleMenuItemOnLeftNav<Transmittal>(menuItem: ModuleNameInLeftNav.TRANSMITTALS.ToDescription(),subMenuItem: ModuleSubMenuInLeftNav.INBOX.ToDescription());
                transmittalsModule.LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateItemsAreShown(columnValuesInConditionList, uploadUnrestrainedDocData.TransmittalGridViewName));
                
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
