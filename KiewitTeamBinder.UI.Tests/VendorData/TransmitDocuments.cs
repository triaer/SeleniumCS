using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.VendorDataModule;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI.Pages.Dialogs;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.UI.Pages.Dialogs;

namespace KiewitTeamBinder.UI.Tests.VendorData
{
    [TestClass]
    public class TransmitDocuments : UITestBase
    {
        [TestMethod]
        public void CreateTransmittals()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var transmitDocData = new TransmitDocumentSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + transmitDocData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(transmitDocData.ProjectName);

                //when - 119696 Transmit Documents
                //User Story 120157
                test = LogTest("Transmit Documents");
                string[] selectedDocuments = new string[transmitDocData.NumberOfSelectedDocumentRow];
                string[] selectedUserWithCompanyName = new string[] { transmitDocData.SelectedUserWithCompany.Admin1Kiewit };

                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription());

                HoldingArea holdingArea = projectDashBoard.SelectModuleMenuItem<HoldingArea>(subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription());
                holdingArea.SelectRowCheckboxesWithoutTransmittalNo<HoldingArea>(transmitDocData.GridViewHoldingAreaName, transmitDocData.NumberOfSelectedDocumentRow, true,  ref selectedDocuments)
                    .ClickHeaderButton<HoldingArea>(MainPaneTableHeaderButton.Transmit, false);

                NewTransmittal newTransmittal = holdingArea.ClickCreateTransmittalsButton();
                newTransmittal.LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateAllSelectedDocumentsAreListed(ref selectedDocuments))
                    .LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateRecordItemsCount(transmitDocData.GridViewTransmitDocName));
                SelectRecipientsDialog selectRecipientsDialog = newTransmittal.ClickRecipientsButton(transmitDocData.ToButton);
                selectRecipientsDialog.SelectCompany(transmitDocData.CompanyName)
                    .ClickUserInLeftTable(transmitDocData.UserName)
                    .LogValidation<SelectRecipientsDialog>(ref validations, selectRecipientsDialog.ValidateUserIsAddedToTheToTable(selectedUserWithCompanyName))
                    .ClickOkButton<NewTransmittal>();
                newTransmittal.LogValidation<NewTransmittal>(ref validations, newTransmittal.ValidateSelectedUsersPopulateInTheToField(selectedUserWithCompanyName))
                    .EnterSubject(transmitDocData.Subject)
                    .EnterMessage(transmitDocData.Message);

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
