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
    public class FilteringExportingVendorDataRegisterValidation : UITestBase
    {
        [TestMethod]
        public void VendorDataRegister_FilteringExportingVendorDataRegisterValidation_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var filteringExportingItemData = new FilteringExportingVendorDataRegisterValidationSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + filteringExportingItemData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(filteringExportingItemData.ProjectName);

                //when - User Story 123549 - 120790 Filtering & Exporting Vendor Data Register Validation Part 3
                test = LogTest("UserStory 123549 - 120790 Filtering & Exporting Vendor Data Register Validation Part 3");

                //projectDashBoard.SelectModuleMenuItem<ProjectsList>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
                VendorDataRegister vendorDataRegister = projectDashBoard.ClickItemInWidget<VendorDataRegister>(filteringExportingItemData.ContractViewWidget, filteringExportingItemData.ContractsInContractView);
                vendorDataRegister.DoubleClickItem(filteringExportingItemData.ContractNumber, filteringExportingItemData.GridViewContract, filteringExportingItemData.ContractNumberDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(filteringExportingItemData.GridViewItem))
                    .ClickOnCheckBox(filteringExportingItemData.GridViewItem, filteringExportingItemData.ItemDescription, uncheck: false)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateLineItemsIsHighlighted(filteringExportingItemData.GridViewItem, filteringExportingItemData.ItemDescription))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountInCreased(filteringExportingItemData.GridViewItem, filteringExportingItemData.ItemDescription));

                int selectedRow = vendorDataRegister.GetSelectedRecordCount();

                vendorDataRegister.ClickOnCheckBox(filteringExportingItemData.GridViewItem, filteringExportingItemData.ItemDescription, uncheck: true)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountDeCreased(filteringExportingItemData.GridViewItem, filteringExportingItemData.ItemDescription, selectedRow))
                    .DoubleClickItem(filteringExportingItemData.ItemID, filteringExportingItemData.GridViewItem, filteringExportingItemData.ItemDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(filteringExportingItemData.GridViewDeliverable))
                    .ClickOnCheckBox(filteringExportingItemData.GridViewDeliverable, filteringExportingItemData.DeliverableDescription, uncheck: false)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateLineItemsIsHighlighted(filteringExportingItemData.GridViewDeliverable, filteringExportingItemData.DeliverableDescription))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountInCreased(filteringExportingItemData.GridViewDeliverable, filteringExportingItemData.DeliverableDescription));

                selectedRow = vendorDataRegister.GetSelectedRecordCount();

                vendorDataRegister.ClickOnCheckBox(filteringExportingItemData.GridViewDeliverable, filteringExportingItemData.DeliverableDescription, uncheck: true)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountDeCreased(filteringExportingItemData.GridViewDeliverable, filteringExportingItemData.DeliverableDescription, selectedRow))
                    .DoubleClickItem(filteringExportingItemData.DeliverableNumber, filteringExportingItemData.GridViewDeliverable, filteringExportingItemData.DeliverableDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(filteringExportingItemData.GridViewDocument))
                    .ClickOnBlueHeader(filteringExportingItemData.ContractNumber);

                //VendorDataRegister vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                //vendorDataRegister.SelectFilterOption<VendorDataRegister>(filteringExportingItemData.GridView)
                //    .DoubleClickItemContract(filteringExportingItemData.ContractNumber, filteringExportingItemData.Description);
                //vendorDataRegister.ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.New, false);
                //ItemDetail itemDetail = vendorDataRegister.ClickHeaderDropdownItem<ItemDetail>(MainPaneHeaderDropdownItem.ItemPurchased, true);
                //itemDetail.LogValidation<ItemDetail>(ref validations, itemDetail.ValidateRequiredFieldsWithRedAsterisk(createPurchaseItemData.RequiredFields))
                //    .EnterTextField<ItemDetail>(createPurchaseItemData.ItemID.Key, createPurchaseItemData.ItemID.Value)
                //    .EnterTextField<ItemDetail>(createPurchaseItemData.Description.Key, createPurchaseItemData.Description.Value)
                //    .SelectItemInDropdown<ItemDetail>(createPurchaseItemData.ContractNumber.Key, createPurchaseItemData.ContractNumber.Value, ref methodValidations)
                //    .SelectItemInDropdown<ItemDetail>(createPurchaseItemData.Status.Key, createPurchaseItemData.Status.Value, ref methodValidations)
                //    .ClickSaveButton<ItemDetail>()
                //    .LogValidation<ItemDetail>(ref validations, itemDetail.ValidateMessageDisplayCorrect(createPurchaseItemData.SaveMessage))
                //    .ClickOkButtonOnPopUp<ItemDetail>()
                //    .LogValidation<ItemDetail>(ref validations, itemDetail.ValidateSaveDialogStatus(true))
                //    .ClickCloseButtonOnPopUp<VendorDataRegister>();

                //when - User Story 121991 - 120795 Validate Purchase Item under Contract
                //test = LogTest("US 121991 - 120795 Validate Purchase Item under Contract");

                //var columnValuePairList1 = new List<KeyValuePair<string, string>> { createPurchaseItemData.ContractNumber };
                //var columnValuePairList2 = new List<KeyValuePair<string, string>> { createPurchaseItemData.ItemID,
                //                                                                    createPurchaseItemData.Description,
                //                                                                    createPurchaseItemData.Status };

                //vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                //vendorDataRegister.FilterDocumentsByGridFilterRow<VendorDataRegister>(createPurchaseItemData.GridViewName, createPurchaseItemData.ContractNumber.Key, createPurchaseItemData.ContractNumber.Value)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsAreShown(columnValuePairList1, createPurchaseItemData.GridViewName))
                //    .ClickExpandButton(createPurchaseItemData.expandButtonIndex)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidatePurchaseItemsAreShown(columnValuePairList2));
                //itemDetail = vendorDataRegister.OpenItem(columnValuePairList2);
                //itemDetail.LogValidation<ItemDetail>(ref validations, itemDetail.ValidateHeaderIsCorrect(createPurchaseItemData.ItemID.Value))
                //    .ClickCloseButtonOnPopUp<VendorDataRegister>();

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


        //[TestMethod]
        //public void VendorDataRegister_ValidateContractorWidgetCounts_UI()
        //{
        //    try
        //    {
        //        // given
        //        var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
        //        test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
        //        var driver = Browser.Open(teambinderTestAccount.Url, browser);
        //        test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
        //        ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

        //        var validateCountData = new ValidateContractorWidgetCountSmoke();
        //        test.Info("Navigate to DashBoard Page of Project: " + validateCountData.ProjectName);
        //        ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(validateCountData.ProjectName);

        //        //when - 120798 Validate Contractor Widget Counts
        //        //User Story 122010
        //        test = LogTest("Validate Contractor Widget Counts");
        //        int CountOfDeliverables = 28;
        //        Dashboard dashboard =  projectDashBoard.SelectModuleMenuItem<Dashboard>(menuItem: ModuleNameInLeftNav.DASHBOARD.ToDescription());
        //        dashboard.ClickMoreOrLessButton(validateCountData.WidgetName, true)
        //            .LogValidation<Dashboard>(ref validations, dashboard.ValidateCountValueIsCorrect(validateCountData.WidgetName, validateCountData.RowName, CountOfDeliverables + 1));

        //        // then
        //        Utils.AddCollectionToCollection(validations, methodValidations);
        //        Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
        //        validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
        //    }
        //    catch (Exception e)
        //    {
        //        lastException = e;
        //        validations = Utils.AddCollectionToCollection(validations, methodValidations);
        //        throw;
        //    }
        //}
    }
}
