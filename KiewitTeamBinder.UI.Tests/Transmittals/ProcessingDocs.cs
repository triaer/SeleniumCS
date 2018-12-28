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

namespace KiewitTeamBinder.UI.Tests.Transmittals
{
    [TestClass]
    public class ProcessingDocs : UITestBase
    {
        [TestMethod]
        public void ValidateTransmittalReceipt()
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

                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.TRANSMITTALS.ToDescription())
                    .LogValidation<ProjectsDashboard>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(transmittalReceiptData.SubItemMenus));

                Transmittal transmittal = projectDashBoard.SelectModuleMenuItem<Transmittal>(subMenuItem: ModuleSubMenuInLeftNav.INBOX.ToDescription());
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
    }
}
