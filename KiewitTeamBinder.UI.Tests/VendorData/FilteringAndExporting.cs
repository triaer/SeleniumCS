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
    public class FilteringAndExporting : UITestBase
    {
        [TestMethod]
        public void VendorDataRegister_FilteringAndExporting_UI()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var filteringAndExportingData = new FilteringAndExportingSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + filteringAndExportingData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(filteringAndExportingData.ProjectName);

                //when - User Story 121133 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 1
                test = LogTest("US 121133 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 1");

                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);
                VendorDataRegister vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDisplayedViewFilterOption(filteringAndExportingData.DefaultFilter))
                .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.More, false)
                .HoverHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.RegisterView)
                .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRegisterViewIsCorrect(filteringAndExportingData.RegisterView))
                .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRecordItemsCount(filteringAndExportingData.HierarchicalGridViewName))
                .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.Export, false)
                .ClickHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.Contracts, false)
                .DownloadFile<VendorDataRegister>(filteringAndExportingData.DownloadFilePath)
                .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateExcelItemsCount(filteringAndExportingData.HierarchicalGridViewName,
                                                                                                               filteringAndExportingData.DownloadFilePath));

                //when - User Story 123548 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 2
                test = LogTest("US 123548 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 2");
                Dashboard dashboard = vendorDataRegister.SelectModuleMenuItem<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                vendorDataRegister = dashboard.ClickNumberOnRow<VendorDataRegister>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.Contracts.ToDescription());
                vendorDataRegister.LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDisplayedViewFilterOption(filteringAndExportingData.GridViewFilter))
                    .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.More, false)
                    .HoverHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.RegisterView)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRegisterViewIsCorrect(filteringAndExportingData.RegisterView))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRecordItemsCount(filteringAndExportingData.GridViewContractsGrid))
                    .ClickHeaderLabelToSort<VendorDataRegister>(MainPaneTableHeaderLabel.ContractNumber)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateColumnIsSorted(MainPaneTableHeaderLabel.ContractNumber.ToDescription()))
                    .FilterDocumentsByGridFilterRow<VendorDataRegister>(filteringAndExportingData.GridViewContractsGrid, MainPaneTableHeaderLabel.Status.ToDescription(), filteringAndExportingData.FilterValue)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateValueInColumnIsCorrect(filteringAndExportingData.GridViewContractsGrid, MainPaneTableHeaderLabel.Status.ToDescription(), filteringAndExportingData.FilterValue))
                    .ClickClearHyperlink<VendorDataRegister>();

                //when - User Story 123549 - 120790 Filtering & Exporting Vendor Data Register Validation Part 3
                test = LogTest("US 123549 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 3");
                string[] blueHeader = { filteringAndExportingData.DeliverableNumber, filteringAndExportingData.ItemID, filteringAndExportingData.ContractNumber};

                vendorDataRegister.DoubleClickItem(filteringAndExportingData.ContractNumber, filteringAndExportingData.GridViewContractsGrid, filteringAndExportingData.ContractNumberDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.ItemPurchased, false, filteringAndExportingData.ContractNumber))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(filteringAndExportingData.GridViewItemGrid))
                    .ClickOnCheckBox(filteringAndExportingData.GridViewItemGrid, filteringAndExportingData.ItemDescription, uncheck: false)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateLineItemIsHighlighted(filteringAndExportingData.GridViewItemGrid, filteringAndExportingData.ItemDescription))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 1))
                    .ClickOnCheckBox(filteringAndExportingData.GridViewItemGrid, filteringAndExportingData.ItemDescription, uncheck: true)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 0, isDeCreased: true))
                    .DoubleClickItem(filteringAndExportingData.ItemID, filteringAndExportingData.GridViewItemGrid, filteringAndExportingData.ItemDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.Deliverables, false, filteringAndExportingData.ContractNumber, filteringAndExportingData.ItemID))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(filteringAndExportingData.GridViewDeliverableGrid))
                    .ClickOnCheckBox(filteringAndExportingData.GridViewDeliverableGrid, filteringAndExportingData.DeliverableDescription, uncheck: false)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateLineItemIsHighlighted(filteringAndExportingData.GridViewDeliverableGrid, filteringAndExportingData.DeliverableDescription))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 1))
                    .ClickOnCheckBox(filteringAndExportingData.GridViewDeliverableGrid, filteringAndExportingData.DeliverableDescription, uncheck: true)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 0, isDeCreased: true))
                    .DoubleClickItem(filteringAndExportingData.DeliverableNumber, filteringAndExportingData.GridViewDeliverableGrid, filteringAndExportingData.DeliverableDescription)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.Documents, false, filteringAndExportingData.ContractNumber, filteringAndExportingData.ItemID, filteringAndExportingData.DeliverableNumber))
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(filteringAndExportingData.GridViewDocumentGrid))
                    .ClickOnBlueHeader(blueHeader)
                    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.Contracts, true));

                //when - User Story 123550 - 120790 Filtering & Exporting Vendor Data Register Validation Part 4
                test = LogTest("US 123550 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 4");
                string documentNo;
                string deliverableItemWindow = filteringAndExportingData.DeliverableItemWindow + filteringAndExportingData.DeliverableNumber;

                vendorDataRegister.DoubleClickItem(filteringAndExportingData.ContractNumber, filteringAndExportingData.GridViewContractsGrid, filteringAndExportingData.ContractNumberDescription)
                                  .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.ItemPurchased, false, filteringAndExportingData.ContractNumber))
                                  .DoubleClickItem(filteringAndExportingData.ItemID, filteringAndExportingData.GridViewItemGrid, filteringAndExportingData.ItemDescription)
                                  .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.Deliverables, false, filteringAndExportingData.ContractNumber, filteringAndExportingData.ItemID));
                VendorDeliverableDetail vendorDeliverableDetail = vendorDataRegister.ClickOnBlueDeliverableLineItemNumber(filteringAndExportingData.DeliverableNumber);
                vendorDeliverableDetail.LogValidation<VendorDeliverableDetail>(ref validations, vendorDataRegister.ValidateWindowIsOpened(deliverableItemWindow))
                                       .ClickHeaderButton<VendorDeliverableDetail>(MainPaneTableHeaderButton.More, false);
                LinkItems linkItems = vendorDeliverableDetail.ClickHeaderDropdownItem<LinkItems>(MainPaneHeaderDropdownItem.LinkItems, true, false, false);
                int countWindow = linkItems.GetCountWindow();
                linkItems.LogValidation<LinkItems>(ref validations, linkItems.ValidateWindowIsOpened(filteringAndExportingData.LinkItemsWindow))
                         .ClickToolbarButton<LinkItems>(ToolbarButton.Add);
                AddDocument addDocument = linkItems.ClickHeaderDropdownItem<AddDocument>(MainPaneHeaderDropdownItem.Documents, false, true, false);
                addDocument.LogValidation<AddDocument>(ref validations, addDocument.ValidateDocumentSearchWindowStatus())
                           .SwitchToFrameOnAddDocument()
                           .SelectOptionInRegisterViewDropDown(filteringAndExportingData.OptionAll)
                           .ClickToobarBottomButton<AddDocument>(ToolbarButton.Search.ToDescription(), filteringAndExportingData.GridViewAddDocName)
                           .SelectItemByIndex(1, out documentNo)
                           .LogValidation<AddDocument>(ref validations, addDocument.ValidateDocumentIsHighlighted(documentNo: "", byIndex: true, index: 1))
                           .LogValidation<AddDocument>(ref validations, addDocument.ValidateCheckBoxStatus(index: 1, uncheck: false))
                           .ClickToobarBottomButton<LinkItems>(ToolbarButton.OK.ToDescription(), filteringAndExportingData.GridViewLinkItemsData, true);
                linkItems.LogValidation<LinkItems>(ref validations, addDocument.ValidateDocumentSearchWindowStatus(true))
                         .LogValidation<LinkItems>(ref validations, linkItems.ValidateDocumentIsAttached(documentNo, byIndex: true, index: 1))
                         .ClickSaveButton<LinkItems>()
                         .LogValidation<LinkItems>(ref validations, linkItems.ValidateMessageDisplayCorrect(filteringAndExportingData.SaveMessageOnLinkItem))
                         .ClickOkButtonOnPopUp<LinkItems>()
                         .LogValidation<LinkItems>(ref validations, linkItems.ValidateSaveDialogStatus(true))
                         .ClickToolbarButton<VendorDeliverableDetail>(ToolbarButton.Close, false, false, true, deliverableItemWindow);
                vendorDeliverableDetail.LogValidation<VendorDeliverableDetail>(ref validations, linkItems.ValidateLinkItemsWindowIsClosed(countWindow));
                countWindow = linkItems.GetCountWindow();
                vendorDeliverableDetail.ClickToolbarButton<VendorDataRegister>(ToolbarButton.Close, false, false, true, filteringAndExportingData.ProjectName)
                                       .LogValidation<VendorDataRegister>(ref validations, vendorDeliverableDetail.ValidateDeliverableLinkItemClosed(countWindow))
                                       .DoubleClickItem(filteringAndExportingData.DeliverableNumber, filteringAndExportingData.GridViewDeliverableGrid, filteringAndExportingData.DeliverableDescription)
                                       .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateBredCrumbTrailDisplayCorrect(filteringAndExportingData.Documents, false, filteringAndExportingData.ContractNumber, filteringAndExportingData.ItemID, filteringAndExportingData.DeliverableNumber))
                                       .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(filteringAndExportingData.GridViewDocumentGrid))
                                       .ClickOnCheckBox(filteringAndExportingData.GridViewDocumentGrid, documentNo, uncheck: false)
                                       .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateLineItemIsHighlighted(filteringAndExportingData.GridViewDocumentGrid, documentNo))
                                       .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 1));
                vendorDataRegister.ClickOnCheckBox(filteringAndExportingData.GridViewDocumentGrid, documentNo, uncheck: true)
                                  .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountStatus(rowSelectedCount: 0, isDeCreased: true))
                                  .ClickOnBlueHeader(blueHeader);

                //when - User Story 123572 - 120790 Filtering & Exporting Vendor Data Register Validation Part 5
                test = LogTest("US 123572 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 5");

                //when - User Story 123595 - 120790 Filtering & Exporting Vendor Data Register Validation Part 6
                test = LogTest("US 123595 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 6");
                projectDashBoard.SelectModuleMenuItem<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                int countOfItem = dashboard.GetCountValueFromRow(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.DocumentsSubmitted.ToDescription());
                HoldingArea holdingArea = dashboard.ClickNumberOnRow<HoldingArea>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.DocumentsSubmitted.ToDescription());
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(filteringAndExportingData.DocumentSubmitedPage))
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(filteringAndExportingData.GridViewHoldingAreaName, countOfItem))
                           .SelectModuleMenuItem<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                dashboard.ClickItemInExpandedWidget<HoldingArea>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.DocumentsSubmitted.ToDescription(), ModuleExpandedItemsInContractorView.NEW.ToDescription(), ref countOfItem);
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(filteringAndExportingData.NewDocumentSubmitedPage))
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(filteringAndExportingData.GridViewHoldingAreaName, countOfItem))
                           .SelectModuleMenuItem<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                dashboard.ClickItemInExpandedWidget<HoldingArea>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.DocumentsSubmitted.ToDescription(), ModuleExpandedItemsInContractorView.ACCEPTED.ToDescription(), ref countOfItem);
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(filteringAndExportingData.AcceptedDocumentSubmitedPage))
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(filteringAndExportingData.GridViewHoldingAreaName, countOfItem))
                           .SelectModuleMenuItem<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                countOfItem = dashboard.GetCountValueFromRow(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.ReturnedToVendor.ToDescription());
                dashboard.ClickNumberOnRow<HoldingArea>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.ReturnedToVendor.ToDescription());
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(filteringAndExportingData.ReturnedToVendorDocumentsPage))
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(filteringAndExportingData.GridViewDocumentRegister, countOfItem))
                           .SelectModuleMenuItem<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                countOfItem = dashboard.GetCountValueFromRow(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.Contracts.ToDescription());
                dashboard.ClickNumberOnRow<HoldingArea>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), ModuleSubItemsInContractorView.Contracts.ToDescription());
                holdingArea.LogValidation<HoldingArea>(ref validations, holdingArea.ValidateSubPageIsDislayed(filteringAndExportingData.ContractsVendorDataPage))
                           .LogValidation<HoldingArea>(ref validations, holdingArea.ValidateRecordItemsCount(filteringAndExportingData.GridViewContractsGrid, countOfItem));

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
