using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.VendorDataModule;
using KiewitTeamBinder.UI.Pages.Dialogs;
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
        public void VendorDataRegister_CreateNewContract_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var createContractItemData = new CreateContractItemSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + createContractItemData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(createContractItemData.ProjectName);

                //when User Story 121989 - 120793 Create Contract Items Deliverable
                test = LogTest("Create Contract Item");
                Dashboard dashboard = projectDashBoard.SelectModuleMenuItem<Dashboard>(menuItem: ModuleNameInLeftNav.DASHBOARD.ToDescription(), waitForLoading: false);
                int expectedCountOfContract = dashboard.GetCountValueFromRow(DashboardWidgit.CONTRACTORVIEW.ToDescription(), ModuleSubContractViewWidget.Contracts.ToDescription());

                projectDashBoard = dashboard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
                VendorDataRegister vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSubPageIsDislayed(createContractItemData.VendorDataRegisterPaneName))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDisplayedViewFilterOption(createContractItemData.DefaultFilter))
                    .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.New, false);
                var contractInfo = createContractItemData.ContractInfo;
                VendorContractDetail contractDetail = vendorDataRegister.ClickHeaderDropdownItem<VendorContractDetail>(MainPaneHeaderDropdownItem.Contract, true);
                contractDetail.LogValidation<VendorContractDetail>(ref validations, contractDetail.ValidateRequiredFieldsWithRedAsterisk(createContractItemData.RequiredFields))
                    .EnterContractRequiredInfo(contractInfo, ref methodValidations)
                    .LogValidation<VendorContractDetail>(ref validations, contractDetail.ValidateSelectedItemShowInDropdownBoxesCorrect(contractInfo))
                    .ClickSaveButton<VendorContractDetail>()
                    .LogValidation<VendorContractDetail>(ref validations, contractDetail.ValidateMessageDisplayCorrect(createContractItemData.SaveMessage))
                    .ClickOkButtonOnPopUp<VendorContractDetail>()
                    .LogValidation<VendorContractDetail>(ref validations, contractDetail.ValidateSaveDialogStatus(true))
                    .ClickCloseButtonOnPopUp<VendorDataRegister>();

                dashboard = projectDashBoard.SelectModuleMenuItem<Dashboard>(menuItem: ModuleNameInLeftNav.DASHBOARD.ToDescription(), waitForLoading: false);
                dashboard.ValidateWidgetsOfDashboardDisplayed(createContractItemData.AllWidgitsInDashboardSection);
                dashboard.ValidateCountValueIsCorrect(DashboardWidgit.CONTRACTORVIEW.ToDescription(), ModuleSubContractViewWidget.Contracts.ToDescription(), expectedCountOfContract);

                projectDashBoard = dashboard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false)
                    .LogValidation<ProjectsDashboard>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(createContractItemData.SubItemMenus));

                vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.Refresh, false)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSubPageIsDislayed(createContractItemData.VendorDataRegisterPaneName))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDisplayedViewFilterOption(createContractItemData.DefaultFilter));

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
                var purchaseInfo = createPurchaseItemData.PurchaseInfo;
                VendorItemDetail itemDetail = vendorDataRegister.ClickHeaderDropdownItem<VendorItemDetail>(MainPaneHeaderDropdownItem.ItemPurchased, true);
                itemDetail.LogValidation<VendorItemDetail>(ref validations, itemDetail.ValidateRequiredFieldsWithRedAsterisk(createPurchaseItemData.RequiredFields))
                    .EnterItemPurchasedRequiredInfo(purchaseInfo, ref methodValidations)
                    .LogValidation<VendorItemDetail>(ref validations, itemDetail.ValidateSelectedItemShowInDropdownBoxesCorrect(purchaseInfo))
                    .ClickSaveButton<VendorItemDetail>()
                    .LogValidation<VendorItemDetail>(ref validations, itemDetail.ValidateMessageDisplayCorrect(createPurchaseItemData.SaveMessage))
                    .ClickOkButtonOnPopUp<VendorItemDetail>()
                    .LogValidation<VendorItemDetail>(ref validations, itemDetail.ValidateSaveDialogStatus(true))
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
                itemDetail.LogValidation<VendorItemDetail>(ref validations, itemDetail.ValidateHeaderIsCorrect(createPurchaseItemData.ItemID.Value))
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
        public void VendorDataRegister_CreateNewDeliverable_UI()
        {
            try
            {
                // given 
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var createNewDeliverableData = new CreateDeliverableItemSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + createNewDeliverableData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(createNewDeliverableData.ProjectName);

                //when UserStory 121992 - 120796 - Create New Deliverable
                test = LogTest("Create New Deliverable");
                string parrentWindow;
                string currentWindow;
                VendorDataRegister vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.New, false);
                var deliverableInfo = createNewDeliverableData.DeliverableInfo;
                VendorDeliverableDetail deliverableDetail = vendorDataRegister.ClickHeaderDropdownItem<VendorDeliverableDetail>(MainPaneHeaderDropdownItem.DeliverableLineItem, true);
                deliverableDetail.LogValidation<VendorDeliverableDetail>(ref validations, deliverableDetail.ValidateRequiredFieldsWithRedAsterisk(createNewDeliverableData.RequiredFields))
                    .EnterDeliverableRequiredInfo(deliverableInfo, ref methodValidations)
                    .LogValidation<VendorDeliverableDetail>(ref validations, deliverableDetail.ValidateSelectedItemShowInDropdownBoxesCorrect(deliverableInfo))
                    .ClickSaveButton<VendorDeliverableDetail>()
                    .LogValidation<VendorDeliverableDetail>(ref validations, deliverableDetail.ValidateMessageDisplayCorrect(createNewDeliverableData.SaveMessage))
                    .ClickOkButtonOnPopUp<VendorDeliverableDetail>()
                    .LogValidation<VendorDeliverableDetail>(ref validations, deliverableDetail.ValidateSaveDialogStatus(true));

                parrentWindow = deliverableDetail.GetCurrentWindow();

                deliverableDetail.ClickToolbarButton<VendorDeliverableDetail>(ToolbarButton.More, checkProgressPopup: false, isDisappear: true)
                                     .LogValidation<VendorDeliverableDetail>(ref validations, deliverableDetail.ValidateDisplayedSubItemLinks(createNewDeliverableData.SubItemOfMoreFunction));
                LinkItems linkItem = deliverableDetail.ClickHeaderDropdownItem<LinkItems>(MainPaneHeaderDropdownItem.LinkItems, true);

                currentWindow = linkItem.GetCurrentWindow();
                linkItem.LogValidation<LinkItems>(ref validations, linkItem.ValidateWindowIsOpened(createNewDeliverableData.LinkItemsWindowTitle))
                        .ClickToolbarButton<VendorDeliverableDetail>(ToolbarButton.Add)
                        .LogValidation<VendorDeliverableDetail>(ref validations, linkItem.ValidateDisplayedSubItemLinks(createNewDeliverableData.SubItemOfAddFunction));
                AddDocument addDocument = linkItem.ClickHeaderDropdownItem<AddDocument>(MainPaneHeaderDropdownItem.Documents, false, true);
                addDocument.LogValidation<AddDocument>(ref validations, addDocument.ValidateDocumentSearchWindowStatus())
                           .SwitchToFrameOnAddDocument()
                           .EnterDocumentNo(createNewDeliverableData.DocumentNo)
                           .ClickToobarBottomButton<AddDocument>(ToolbarButton.Search.ToDescription(), createNewDeliverableData.GridViewAddDocName)
                           .SelectItemByDocumentNo(createNewDeliverableData.DocumentNo)
                           .LogValidation<AddDocument>(ref validations, addDocument.ValidateDocumentIsHighlighted(createNewDeliverableData.DocumentNo))
                           .ClickToobarBottomButton<LinkItems>(ToolbarButton.OK.ToDescription(), createNewDeliverableData.GridViewLinkItemsName, true);
                linkItem.LogValidation<LinkItems>(ref validations, addDocument.ValidateDocumentSearchWindowStatus(true))
                        .LogValidation<LinkItems>(ref validations, linkItem.ValidateDocumentIsAttached(createNewDeliverableData.DocumentNo))
                        .ClickToolbarButton<AlertDialog>(ToolbarButton.Save);
                alertDialog.LogValidation<AlertDialog>(ref validations, linkItem.ValidateMessageDisplayCorrect(createNewDeliverableData.SaveMessageOnLinkItem))
                           .ClickOKButton<LinkItems>()
                           .SwitchToWindow(parrentWindow);
                deliverableItemDetail.LogValidation<VendorDeliverableDetail>(ref validations, deliverableItemDetail.ValidateSaveDialogStatus(true))
                                     .ClickToolbarButton<AlertDialog>(ToolbarButton.Save, checkProgressPopup: false, isDisappear: true);
                alertDialog.LogValidation<AlertDialog>(ref validations, deliverableItemDetail.ValidateMessageDisplayCorrect(createNewDeliverableData.SaveMessage))
                           .ClickOKButton<VendorDeliverableDetail>()
                           .SwitchToWindow(currentWindow);

                int countWindow = linkItem.GetCountWindow();
                linkItem.LogValidation<LinkItems>(ref validations, deliverableItemDetail.ValidateSaveDialogStatus(true))
                        .ClickToolbarButton<VendorDeliverableDetail>(ToolbarButton.Close)
                        .SwitchToWindow(parrentWindow);
                deliverableItemDetail.LogValidation<LinkItems>(ref validations, linkItem.ValidateLinkItemsWindowIsClosed(countWindow));

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
