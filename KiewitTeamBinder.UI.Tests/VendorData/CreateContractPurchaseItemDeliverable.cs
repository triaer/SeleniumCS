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
using KiewitTeamBinder.UI.Pages.PopupWindows;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;

namespace KiewitTeamBinder.UI.Tests.VendorData
{
    [TestClass]
    public class CreateContractPurchaseItemDeliverable : UITestBase
    {
        [TestMethod]
        public void CreateNewPurchaseItem()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var createNewPurchaseItemdData = new CreateNewPurchaseItemSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + createNewPurchaseItemdData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(createNewPurchaseItemdData.ProjectName);

                //when - User Story 121990 - 120794 Create New Purchase Item
                test = LogTest("Create New Purchase Item");

                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
                HoldingArea holdingArea = projectDashBoard.SelectModuleMenuItem<HoldingArea>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                holdingArea.ClickHeaderButton<HoldingArea>(MainPaneTableHeaderButton.New, false);
                ItemDetail itemDetail = holdingArea.ClickHeaderDropdownItem<ItemDetail>(MainPaneHeaderDropdownItem.ItemPurchased, true);
                itemDetail.LogValidation<ItemDetail>(ref validations, itemDetail.ValidateRequiredFieldsWithRedAsterisk(createNewPurchaseItemdData.RequiredFields))
                    .EnterTextField<ItemDetail>(createNewPurchaseItemdData.ItemID.Key, createNewPurchaseItemdData.ItemID.Value)
                    .EnterTextField<ItemDetail>(createNewPurchaseItemdData.Description.Key, createNewPurchaseItemdData.Description.Value)
                    .SelectItemInDropdown<ItemDetail>(createNewPurchaseItemdData.ContractNumber.Key, createNewPurchaseItemdData.ContractNumber.Value, ref methodValidations)
                    .SelectItemInDropdown<ItemDetail>(createNewPurchaseItemdData.Status.Key, createNewPurchaseItemdData.Status.Value, ref methodValidations)
                    .ClickSaveButton<ItemDetail>()
                    .LogValidation<ItemDetail>(ref validations, itemDetail.ValidateMessageDisplayCorrect(createNewPurchaseItemdData.SaveMessage))
                    .ClickOkButtonOnPopUp<ItemDetail>()
                    .LogValidation<ItemDetail>(ref validations, itemDetail.ValidateSaveDialogStatus(true))
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
    }
}
