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
using KiewitTeamBinder.Common.Models.VendorData;


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
                Dashboard dashboard = projectDashBoard.SelectModuleMenuItem<Dashboard>(menuItem: ModuleNameInLeftNav.DASHBOARD.ToDescription());
                dashboard.ClickMoreOrLessButton(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), false);
                int CountOfContracts = dashboard.GetCountValueFromRow(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), createContractItemData.RowName);

                //when User Story 121989 - 120793 Create Contract Items Deliverable
                test = LogTest("Create Contract Item");
                
                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
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

                dashboard = projectDashBoard.SelectModuleMenuItem<Dashboard>(menuItem: ModuleNameInLeftNav.DASHBOARD.ToDescription());
                dashboard.LogValidation<Dashboard>(ref validations, dashboard.ValidateCountValueIsCorrect(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), createContractItemData.RowName, CountOfContracts + 1));

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

                //and - User Story 121991 - 120795 Validate Purchase Item under Contract
                test = LogTest("US 121991 - 120795 Validate Purchase Item under Contract");

                var columnValuePairList1 = createPurchaseItemData.ExpectedContractValuesInColumnList(purchaseInfo);
                var columnValuePairList2 = createPurchaseItemData.ExpectedPurchasedValuesInColumnList(purchaseInfo);
                                
                vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.FilterDocumentsByGridFilterRow<VendorDataRegister>(createPurchaseItemData.GridViewName, ContractField.ContractNumber.ToDescription(), purchaseInfo.ContractNumber)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsAreShown(columnValuePairList1, createPurchaseItemData.GridViewName))
                    .ClickExpandButton(createPurchaseItemData.expandButtonIndex)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidatePurchaseItemsAreShown(columnValuePairList2));
                itemDetail = vendorDataRegister.OpenItem(columnValuePairList2);
                itemDetail.LogValidation<VendorItemDetail>(ref validations, itemDetail.ValidateHeaderIsCorrect(purchaseInfo.ItemID))
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
                Dashboard dashboard = projectDashBoard.SelectModuleMenuItem<Dashboard>(menuItem: ModuleNameInLeftNav.DASHBOARD.ToDescription());
                dashboard.ClickMoreOrLessButton(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), true);
                int CountOfDeliverables = dashboard.GetCountValueFromRow(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), createNewDeliverableData.RowName);

                //when UserStory 121992 - 120796 - Create New Deliverable
                test = LogTest("Create New Deliverable");
                string parrentWindow;
                
                VendorDataRegister vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.New, false);
                DeliverableLine deliverableInfo = createNewDeliverableData.DeliverableInfo;
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
                        .ClickSaveButton<LinkItems>()
                        .LogValidation<LinkItems>(ref validations, deliverableDetail.ValidateMessageDisplayCorrect(createNewDeliverableData.SaveMessageOnLinkItem))
                        .ClickOkButtonOnPopUp<LinkItems>()
                        .ClickToolbarButton<VendorDeliverableDetail>(ToolbarButton.Close)
                        .SwitchToWindow(parrentWindow);
                deliverableDetail.ClickSaveButton<VendorDeliverableDetail>()
                    .LogValidation<VendorDeliverableDetail>(ref validations, deliverableDetail.ValidateMessageDisplayCorrect(createNewDeliverableData.SaveMessage))
                    .ClickOkButtonOnPopUp<VendorDeliverableDetail>()
                    .LogValidation<VendorDeliverableDetail>(ref validations, deliverableDetail.ValidateSaveDialogStatus(true))
                    .ClickCloseButtonOnPopUp<VendorDataRegister>();

                //and - User Story 125084 - 120797 Validate Deliverable under Contract/Item
                test = LogTest("US 125084 - 120797 Validate Deliverable under Contract/Item");
                vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.FilterDocumentsByGridFilterRow<VendorDataRegister>(createNewDeliverableData.GridViewName,
                                                                                      DeliverableField.ContractNumber.ToDescription(),
                                                                                      deliverableInfo.ContractNumber)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsAreShown(createNewDeliverableData.ExpectedContractValuesInColumnList(deliverableInfo), 
                                                                                            createNewDeliverableData.GridViewName))
                    .ClickExpandButton(createNewDeliverableData.ExpanButtonIndex)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidatePurchaseItemsAreShown(createNewDeliverableData.ExpectedPurchasedValuesInColumnList(deliverableInfo)))
                    .ClickExpandButton(createNewDeliverableData.ExpanSubButtonIndex)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDeliverablesAreShown(createNewDeliverableData.ExpectedDeliverableValuesInColumnList(deliverableInfo)));

                //and - User Story 122010 - 120798 Validate Contractor Widget Counts
                test = LogTest("US 122010 - 120798 Validate Contractor Widget Counts");
                
                dashboard = projectDashBoard.SelectModuleMenuItem<Dashboard>(menuItem: ModuleNameInLeftNav.DASHBOARD.ToDescription());
                dashboard.ClickMoreOrLessButton(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), true)
                    .LogValidation<Dashboard>(ref validations, dashboard.ValidateCountValueIsCorrect(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), createNewDeliverableData.RowName, CountOfDeliverables + 1));


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
