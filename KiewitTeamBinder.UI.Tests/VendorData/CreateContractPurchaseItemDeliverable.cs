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
using KiewitTeamBinder.UI.Pages.DashboardModule;


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

                //when - 120794 Create New Purchase Item
                //User Story 121990
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

        [TestMethod]
        public void ValidatePurchaseItemUnderContract()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var validatePurchaseItemData = new ValidatePurchaseItemUnderContractSmoke();
                var columnValuePairList1 = new List<KeyValuePair<string, string>> { validatePurchaseItemData.ContractNumber };
                var columnValuePairList2 = new List<KeyValuePair<string, string>> { validatePurchaseItemData.ItemID,
                                                                                    validatePurchaseItemData.Description,
                                                                                    validatePurchaseItemData.Status };
                test.Info("Navigate to DashBoard Page of Project: " + validatePurchaseItemData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(validatePurchaseItemData.ProjectName);

                //when - 120795 Validate Purchase Item under Contract
                //User Story 121991
                test = LogTest("Validate Purchase Item under Contract");

                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
                VendorDataRegister holdingArea = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                holdingArea.FilterDocumentsByGridFilterRow<VendorDataRegister>(validatePurchaseItemData.GridViewName,
                                                                               validatePurchaseItemData.ContractNumber.Key,
                                                                               validatePurchaseItemData.ContractNumber.Value)
                    .LogValidation<VendorDataRegister>(ref validations, holdingArea.ValidateItemsAreShown(columnValuePairList1, validatePurchaseItemData.GridViewName))
                    .ClickExpandButton(validatePurchaseItemData.expandButtonIndex)
                    .LogValidation<VendorDataRegister>(ref validations, holdingArea.ValidatePurchaseItemsAreShown(columnValuePairList2));
                ItemDetail itemDetail = holdingArea.OpenItem(columnValuePairList2);
                itemDetail.LogValidation<ItemDetail>(ref validations, itemDetail.ValidateHeaderIsCorrect(validatePurchaseItemData.ItemID.Value))
                    .ClickToolbarButton<ItemDetail>(ToolbarButton.Close);

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
        public void ValidateContractorWidgetCounts()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var validateCountData = new ValidateContractorWidgetCountSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + validateCountData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(validateCountData.ProjectName);

                //when - 120798 Validate Contractor Widget Counts
                //User Story 122010
                test = LogTest("Validate Contractor Widget Counts");
                int CountOfDeliverables = 28;
                Dashboard dashboard =  projectDashBoard.SelectModuleMenuItem<Dashboard>(menuItem: ModuleNameInLeftNav.DASHBOARD.ToDescription());
                dashboard.ClickMoreOrLessButton(validateCountData.WidgetName, true)
                    .LogValidation<Dashboard>(ref validations, dashboard.ValidateCountValueIsCorrect(validateCountData.WidgetName, validateCountData.RowName, CountOfDeliverables + 1));

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
