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
        public void VendorDataRegister_CreateNewPurchaseItem_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var createPurchaseItemData = new CreatePurchaseItemSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + createPurchaseItemData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(createPurchaseItemData.ProjectName);

                //when - User Story 121990 - 120794 Create New Purchase Item
                test = LogTest("US 121990 - 120794 Create New Purchase Item");

                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
                VendorDataRegister vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.New, false);
                ItemDetail itemDetail = vendorDataRegister.ClickHeaderDropdownItem<ItemDetail>(MainPaneHeaderDropdownItem.ItemPurchased, true);
                itemDetail.LogValidation<ItemDetail>(ref validations, itemDetail.ValidateRequiredFieldsWithRedAsterisk(createPurchaseItemData.RequiredFields))
                    .EnterTextField<ItemDetail>(createPurchaseItemData.ItemID.Key, createPurchaseItemData.ItemID.Value)
                    .EnterTextField<ItemDetail>(createPurchaseItemData.Description.Key, createPurchaseItemData.Description.Value)
                    .SelectItemInDropdown<ItemDetail>(createPurchaseItemData.ContractNumber.Key, createPurchaseItemData.ContractNumber.Value, ref methodValidations)
                    .SelectItemInDropdown<ItemDetail>(createPurchaseItemData.Status.Key, createPurchaseItemData.Status.Value, ref methodValidations)
                    .ClickSaveButton<ItemDetail>()
                    .LogValidation<ItemDetail>(ref validations, itemDetail.ValidateMessageDisplayCorrect(createPurchaseItemData.SaveMessage))
                    .ClickOkButtonOnPopUp<ItemDetail>()
                    .LogValidation<ItemDetail>(ref validations, itemDetail.ValidateSaveDialogStatus(true))
                    .ClickCloseButtonOnPopUp<VendorDataRegister>();

                //when - User Story 121991 - 120795 Validate Purchase Item under Contract
                test = LogTest("US 121991 - 120795 Validate Purchase Item under Contract");

                var columnValuePairList1 = new List<KeyValuePair<string, string>> { createPurchaseItemData.ContractNumber };
                var columnValuePairList2 = new List<KeyValuePair<string, string>> { createPurchaseItemData.ItemID,
                                                                                    createPurchaseItemData.Description,
                                                                                    createPurchaseItemData.Status };

                vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.FilterDocumentsByGridFilterRow<VendorDataRegister>(createPurchaseItemData.GridViewName, createPurchaseItemData.ContractNumber.Key, createPurchaseItemData.ContractNumber.Value)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsAreShown(columnValuePairList1, createPurchaseItemData.GridViewName))
                    .ClickExpandButton(createPurchaseItemData.expandButtonIndex)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidatePurchaseItemsAreShown(columnValuePairList2));
                itemDetail = vendorDataRegister.OpenItem(columnValuePairList2);
                itemDetail.LogValidation<ItemDetail>(ref validations, itemDetail.ValidateHeaderIsCorrect(createPurchaseItemData.ItemID.Value))
                    .ClickCloseButtonOnPopUp<VendorDataRegister>();

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
        public void ValidateDeliverableUnderContractItem()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var validateDeliverableUnderContractItemData = new ValidateDeliverableUnderContractItemSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + validateDeliverableUnderContractItemData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(validateDeliverableUnderContractItemData.ProjectName);
                var columnValuePairList1 = new List<KeyValuePair<string, string>> { validateDeliverableUnderContractItemData.ContractNumber };
                var columnValuePairList2 = new List<KeyValuePair<string, string>> { validateDeliverableUnderContractItemData.ItemID,
                                                                                    validateDeliverableUnderContractItemData.Description,
                                                                                    validateDeliverableUnderContractItemData.Status };
                var columnValuePairList3 = new List<KeyValuePair<string, string>> { validateDeliverableUnderContractItemData.ItemNumber };
                //when - 120797 Validate Deliverable under Contract/Item
                //User Story 125084
                test = LogTest("Create New Purchase Item");

                VendorDataRegister vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription(), waitForLoading: true);
                vendorDataRegister.FilterDocumentsByGridFilterRow<VendorDataRegister>(validateDeliverableUnderContractItemData.GridViewName,
                                                                                      validateDeliverableUnderContractItemData.ContractNumber.Key,
                                                                                      validateDeliverableUnderContractItemData.ContractNumber.Value)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsAreShown(columnValuePairList1, validateDeliverableUnderContractItemData.GridViewName))
                    .ClickExpandButton(validateDeliverableUnderContractItemData.ExpanButtonIndex)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidatePurchaseItemsAreShown(columnValuePairList2))
                    .ClickExpandButton(validateDeliverableUnderContractItemData.ExpanSubButtonIndex)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDeliverablesAreShown(columnValuePairList3));

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
