﻿using System;
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
    public class FilteringAndExporting : UITestBase
    {
        [TestMethod]
        public void VendorDataRegister_FilteringAndExporting_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                var filteringAndExportingData = new FilteringAndExportingSmoke();
                var gridViewNameModule = new FilteringAndExportingSmoke.GridViewNameModules();
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser, filteringAndExportingData.DefaultDownloadedFolderPath);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
                test.Info("Navigate to DashBoard Page of Project: " + filteringAndExportingData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(filteringAndExportingData.ProjectName);

                //when - User Story 121133 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 1
                test = LogTest("US 121133 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 1");

                projectDashBoard.SelectModuleMenuItemOnLeftNav<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
                VendorDataRegister vendorDataRegister = projectDashBoard.SelectModuleMenuItemOnLeftNav<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDisplayedViewFilterOption(filteringAndExportingData.DefaultFilter))
                    .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.More, false)
                    .HoverHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.RegisterView)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRegisterViewIsCorrect(filteringAndExportingData.RegisterViewFilter))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRecordItemsCount(gridViewNameModule.HierarchicalView))
                    .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.Export, false)
                    .ClickHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.Contracts, false)
                    .DownloadFile<VendorDataRegister>(filteringAndExportingData.DownloadContractsFilePath)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateExcelItemsCount(gridViewNameModule.HierarchicalView, filteringAndExportingData.DownloadContractsFilePath));

                //when - User Story 123548 - 120790 Filtering & Exporting Vendor Data Register Validation -Part 2
                test = LogTest("US 123548 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 2");
                Dashboard dashboard = vendorDataRegister.SelectModuleMenuItemOnLeftNav<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                vendorDataRegister = dashboard.ClickNumberOnRow<VendorDataRegister>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.Contracts.ToDescription());
                vendorDataRegister.LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDisplayedViewFilterOption(filteringAndExportingData.GridViewFilter))
                    .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.More, false)
                    .HoverHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.RegisterView)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRegisterViewIsCorrect(filteringAndExportingData.RegisterViewFilter))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRecordItemsCount(gridViewNameModule.ContractsGrid))
                    .ClickHeaderLabelToSort<VendorDataRegister>(MainPaneTableHeaderLabel.ContractNumber)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateColumnIsSorted(MainPaneTableHeaderLabel.ContractNumber.ToDescription()))
                    .FilterDocumentsByGridFilterRow<VendorDataRegister>(gridViewNameModule.ContractsGrid, MainPaneTableHeaderLabel.Status.ToDescription(), filteringAndExportingData.FilterValue)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateValueInColumnIsCorrect(gridViewNameModule.ContractsGrid, MainPaneTableHeaderLabel.Status.ToDescription(),
                                                                                                                          filteringAndExportingData.FilterValue))
                    .ClickClearHyperlink<VendorDataRegister>();

                //when - User Story 123549 - 120790 Filtering & Exporting Vendor Data Register Validation Part 3
                test = LogTest("US 123549 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 3");
                string[] blueHeader = { filteringAndExportingData.DeliverableNumber, filteringAndExportingData.ItemID, filteringAndExportingData.ContractNumber };
                vendorDataRegister.DoubleClickItem(filteringAndExportingData.ContractNumber, gridViewNameModule.ContractsGrid, filteringAndExportingData.ContractNumberDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.ItemPurchased, false, filteringAndExportingData.ContractNumber))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(gridViewNameModule.ItemPurchasedGrid))
                    .ClickOnCheckBox(gridViewNameModule.ItemPurchasedGrid, filteringAndExportingData.ItemDescription, uncheck: false)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateLineItemIsHighlighted(gridViewNameModule.ItemPurchasedGrid, filteringAndExportingData.ItemDescription))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 1))
                    .ClickOnCheckBox(gridViewNameModule.ItemPurchasedGrid, filteringAndExportingData.ItemDescription, uncheck: true)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 0, isDeCreased: true))
                    .DoubleClickItem(filteringAndExportingData.ItemID, gridViewNameModule.ItemPurchasedGrid, filteringAndExportingData.ItemDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.Deliverables, false, filteringAndExportingData.ContractNumber, 
                                                                                                                                filteringAndExportingData.ItemID))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(gridViewNameModule.DeliverableGrid))
                    .ClickOnCheckBox(gridViewNameModule.DeliverableGrid, filteringAndExportingData.DeliverableDescription, uncheck: false)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateLineItemIsHighlighted(gridViewNameModule.DeliverableGrid, filteringAndExportingData.DeliverableDescription))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 1))
                    .ClickOnCheckBox(gridViewNameModule.DeliverableGrid, filteringAndExportingData.DeliverableDescription, uncheck: true)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 0, isDeCreased: true))
                    .DoubleClickItem(filteringAndExportingData.DeliverableNumber, gridViewNameModule.DeliverableGrid, filteringAndExportingData.DeliverableDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.Documents, false, filteringAndExportingData.ContractNumber,
                                                                                                                                filteringAndExportingData.ItemID, filteringAndExportingData.DeliverableNumber))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(gridViewNameModule.DocumentsGrid))
                    .ClickOnBlueHeader(blueHeader)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.Contracts, true));

                //when - User Story 123550 - 120790 Filtering & Exporting Vendor Data Register Validation Part 4
                test = LogTest("US 123550 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 4");
                string documentNo;
                string deliverableItemWindow = filteringAndExportingData.ProjectID + " - " +filteringAndExportingData.DeliverableNumber;

                vendorDataRegister.DoubleClickItem(filteringAndExportingData.ContractNumber, gridViewNameModule.ContractsGrid, filteringAndExportingData.ContractNumberDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.ItemPurchased, false, filteringAndExportingData.ContractNumber))
                    .DoubleClickItem(filteringAndExportingData.ItemID, gridViewNameModule.ItemPurchasedGrid, filteringAndExportingData.ItemDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.Deliverables, false, filteringAndExportingData.ContractNumber, 
                                                                                                                                filteringAndExportingData.ItemID));

                VendorDeliverableDetail vendorDeliverableDetail = vendorDataRegister.ClickOnBlueItem<VendorDeliverableDetail>(gridViewNameModule.DeliverableGrid, filteringAndExportingData.DeliverableNumber);
                vendorDeliverableDetail.LogValidation<VendorDeliverableDetail>(ref validations, vendorDataRegister.ValidateWindowIsOpened(deliverableItemWindow))
                    .ClickToolbarButtonOnWinPopup<VendorDeliverableDetail>(ToolbarButton.More, false);

                LinkItems linkItems = vendorDeliverableDetail.ClickHeaderDropdownItem<LinkItems>(MainPaneHeaderDropdownItem.LinkItems, true, false, false);
                int countWindow = linkItems.GetCountWindow();

                linkItems.LogValidation<LinkItems>(ref validations, linkItems.ValidateWindowIsOpened(filteringAndExportingData.LinkItemsWindow))
                    .ClickToolbarButtonOnWinPopup<LinkItems>(ToolbarButton.Add);
                AddDocument addDocument = linkItems.ClickHeaderDropdownItem<AddDocument>(MainPaneHeaderDropdownItem.Documents, false, true, false);
                addDocument.LogValidation<AddDocument>(ref validations, addDocument.ValidateDocumentSearchWindowStatus())
                    .SwitchToFrameOnAddDocument()
                    .SelectOptionInRegisterViewDropDown(filteringAndExportingData.OptionAll)
                    .ClickToobarBottomButton<AddDocument>(ToolbarButton.Search.ToDescription(), gridViewNameModule.AddDocument)
                    .SelectItemByIndex(1, out documentNo)
                    .LogValidation<AddDocument>(ref validations, addDocument.ValidateDocumentIsHighlighted(byIndex: true, index: 1))
                    .LogValidation<AddDocument>(ref validations, addDocument.ValidateCheckBoxStatus(index: 1, uncheck: false))
                    .ClickToobarBottomButton<LinkItems>(ToolbarButton.OK.ToDescription(), gridViewNameModule.LinkItemsData, true);
                linkItems.LogValidation<LinkItems>(ref validations, addDocument.ValidateDocumentSearchWindowStatus(true))
                    .LogValidation<LinkItems>(ref validations, linkItems.ValidateDocumentIsAttached(documentNo, byIndex: true, index: 1))
                    .ClickSaveInToolbarHeader()
                    .LogValidation<LinkItems>(ref validations, linkItems.ValidateMessageDisplayCorrect(filteringAndExportingData.SaveMessageOnLinkItem))
                    .ClickOkButtonOnPopUp<LinkItems>()
                    .LogValidation<LinkItems>(ref validations, linkItems.ValidateSaveDialogStatus(true))
                    .ClickToolbarButtonOnWinPopup<VendorDeliverableDetail>(ToolbarButton.Close, false, false, true, deliverableItemWindow);
                vendorDeliverableDetail.LogValidation<VendorDeliverableDetail>(ref validations, linkItems.ValidateLinkItemsWindowIsClosed(countWindow));

                countWindow = linkItems.GetCountWindow();
                vendorDeliverableDetail.ClickToolbarButtonOnWinPopup<VendorDataRegister>(ToolbarButton.Close, false, false, true, filteringAndExportingData.ProjectName);
                vendorDataRegister.LogValidation<VendorDataRegister>(ref validations, vendorDeliverableDetail.ValidateDeliverableLinkItemClosed(countWindow))
                    .DoubleClickItem(filteringAndExportingData.DeliverableNumber, gridViewNameModule.DeliverableGrid, filteringAndExportingData.DeliverableDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.Documents, false, filteringAndExportingData.ContractNumber,
                                                                                                                                filteringAndExportingData.ItemID, filteringAndExportingData.DeliverableNumber))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(gridViewNameModule.DocumentsGrid))
                    .ClickOnCheckBox(gridViewNameModule.DocumentsGrid, documentNo, uncheck: false)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateLineItemIsHighlighted(gridViewNameModule.DocumentsGrid, documentNo))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 1))
                    .ClickOnCheckBox(gridViewNameModule.DocumentsGrid, documentNo, uncheck: true)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 0, isDeCreased: true))
                    .ClickOnBlueHeader(blueHeader);

                //when - User Story 123572 - 120790 Filtering & Exporting Vendor Data Register Validation Part 5
                test = LogTest("US 123572 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 5");
                vendorDataRegister.DoubleClickItem(filteringAndExportingData.ContractNumber, gridViewNameModule.ContractsGrid, filteringAndExportingData.ContractNumberDescription)
                    .DoubleClickItem(filteringAndExportingData.ItemID, gridViewNameModule.ItemPurchasedGrid, filteringAndExportingData.ItemDescription)
                    .DoubleClickItem(filteringAndExportingData.DeliverableNumber, gridViewNameModule.DeliverableGrid, filteringAndExportingData.DeliverableDescription);

                string documentDetailWindow = vendorDataRegister.GetDocumentDetailWindowByItemIndex(1);
                DocumentDetail documentDetail = vendorDataRegister.ClickOnBlueItem<DocumentDetail>(gridViewNameModule.DocumentsGrid, documentNo);
                //string currentWindow = documentDetail.GetCurrentWindow();
                countWindow = documentDetail.GetCountWindow();

                documentDetail.LogValidation<DocumentDetail>(ref validations, vendorDataRegister.ValidateWindowIsOpened(documentDetailWindow))
                    .ClickCloseButtonOnPopUp<VendorDataRegister>();

                vendorDataRegister.LogValidation<VendorDataRegister>(ref validations, documentDetail.ValidateDocumentDetailWindowIsClosed(countWindow))
                    .SelectFilterOption<VendorDataRegister>(filteringAndExportingData.ExpeditingViewFilter)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(gridViewNameModule.ExpeditingContracts))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidatePageSizeDefaultIsCorrectly(gridViewNameModule.ExpeditingContracts, filteringAndExportingData.PageSizeDefault))
                    .ClickHeaderLabelToSort<VendorDataRegister>(MainPaneTableHeaderLabel.ContractNumber)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateColumnIsSorted(MainPaneTableHeaderLabel.ContractNumber.ToDescription()))
                    .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.Export, false)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDisplayedSubItemLinks(filteringAndExportingData.SubItemMenus))
                    .SelectItemOnHeaderDropdown<VendorDataRegister>(MainPaneHeaderDropdownItem.ExpeditingView)
                    .DownloadFile<VendorDataRegister>(filteringAndExportingData.DownloadExpeditingFilePath)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateExcelItemsCount(gridViewNameModule.ExpeditingContracts, filteringAndExportingData.DownloadExpeditingFilePath))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDataExcelCorrectly(gridViewNameModule.ExpeditingContracts, filteringAndExportingData.DownloadExpeditingFilePath));

                //when - User Story 123595 - 120790 Filtering & Exporting Vendor Data Register Validation Part 6
                test = LogTest("US 123595 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 6");
                projectDashBoard.SelectModuleMenuItemOnLeftNav<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());

                int countOfItem = dashboard.GetCountValueFromRow(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.DocumentsSubmitted.ToDescription());
                HoldingArea holdingArea = dashboard.ClickNumberOnRow<HoldingArea>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.DocumentsSubmitted.ToDescription());
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(filteringAndExportingData.DocumentSubmitedPage))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(gridViewNameModule.HoldingArea, countOfItem));

                dashboard = holdingArea.SelectModuleMenuItemOnLeftNav<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                holdingArea = dashboard.ClickItemInExpandedWidget<HoldingArea>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.DocumentsSubmitted.ToDescription(), ModuleExpandedItemsInContractorView.NEW.ToDescription(), ref countOfItem);
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(filteringAndExportingData.NewDocumentSubmitedPage))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(gridViewNameModule.HoldingArea, countOfItem));

                dashboard = holdingArea.SelectModuleMenuItemOnLeftNav<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                holdingArea = dashboard.ClickItemInExpandedWidget<HoldingArea>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.DocumentsSubmitted.ToDescription(), ModuleExpandedItemsInContractorView.ACCEPTED.ToDescription(), ref countOfItem);
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(filteringAndExportingData.AcceptedDocumentSubmitedPage))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(gridViewNameModule.HoldingArea, countOfItem));

                dashboard = holdingArea.SelectModuleMenuItemOnLeftNav<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                countOfItem = dashboard.GetCountValueFromRow(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.ReturnedToVendor.ToDescription());
                holdingArea = dashboard.ClickNumberOnRow<HoldingArea>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.ReturnedToVendor.ToDescription());
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(filteringAndExportingData.ReturnedToVendorDocumentsPage))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(gridViewNameModule.DocumentRegister, countOfItem));

                dashboard = holdingArea.SelectModuleMenuItemOnLeftNav<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                countOfItem = dashboard.GetCountValueFromRow(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.Contracts.ToDescription());
                holdingArea = dashboard.ClickNumberOnRow<HoldingArea>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.Contracts.ToDescription());
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(filteringAndExportingData.ContractsVendorDataPage))
                    .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(gridViewNameModule.ContractsGrid, countOfItem));

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
