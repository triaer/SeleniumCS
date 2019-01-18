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
using OpenQA.Selenium;

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
                //vendorDataRegister.LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDisplayedViewFilterOption(filteringAndExportingData.DefaultFilter));
                //   .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.More, false)
                //   .HoverHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.RegisterView)
                //   .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRegisterViewIsCorrect(filteringAndExportingData.RegisterView))
                //   .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRecordItemsCount(filteringAndExportingData.HierarchicalGridViewName))
                //   .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.Export, false)
                //   .ClickHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.Contracts, false)
                //   .DownloadFile<VendorDataRegister>(filteringAndExportingData.DownloadFilePath)
                //   .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateExcelItemsCount(filteringAndExportingData.HierarchicalGridViewName,
                //                                                                                                  filteringAndExportingData.DownloadFilePath));

                //when - User Story 123548 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 2
                test = LogTest("US 123548 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 2");
                Dashboard dashboard = vendorDataRegister.SelectModuleMenuItem<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                vendorDataRegister = dashboard.ClickNumberOnRow<VendorDataRegister>(WidgetUniqueName.CONTRACTORVIEW.ToDescription(), filteringAndExportingData.RowName);
                //vendorDataRegister.LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateDisplayedViewFilterOption(filteringAndExportingData.GridViewFilter))
                //    .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.More, false)
                //    .HoverHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.RegisterView)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRegisterViewIsCorrect(filteringAndExportingData.RegisterView))
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRecordItemsCount(filteringAndExportingData.GridGridViewName))
                //    .ClickHeaderLabelToSort<VendorDataRegister>(MainPaneTableHeaderLabel.ContractNumber)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateColumnIsSorted(MainPaneTableHeaderLabel.ContractNumber.ToDescription()))
                //    .FilterDocumentsByGridFilterRow<VendorDataRegister>(filteringAndExportingData.GridGridViewName, MainPaneTableHeaderLabel.Status.ToDescription(), filteringAndExportingData.FilterValue)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateValueInColumnIsCorrect(filteringAndExportingData.GridGridViewName, MainPaneTableHeaderLabel.Status.ToDescription(), filteringAndExportingData.FilterValue))
                //    .ClickClearHyperlink<VendorDataRegister>();

                //when - User Story 123549 - 120790 Filtering & Exporting Vendor Data Register Validation Part 3
                test = LogTest("US 123549 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 3");

                //projectDashBoard.SelectModuleMenuItem<ProjectsList>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), waitForLoading: false);               
                //vendorDataRegister.DoubleClickItem( filteringAndExportingData.ContractNumber, filteringAndExportingData.GridViewContract, filteringAndExportingData.ContractNumberDescription)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateTitlePageAreShownCorrectly(filteringAndExportingData.ContractNumber))
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(filteringAndExportingData.GridViewItem))
                //    .ClickOnCheckBox(filteringAndExportingData.GridViewItem, filteringAndExportingData.ItemDescription, uncheck: false)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateLineItemsIsHighlighted(filteringAndExportingData.GridViewItem, filteringAndExportingData.ItemDescription))
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountInCreased(filteringAndExportingData.GridViewItem, filteringAndExportingData.ItemDescription));

                //int selectedRow = vendorDataRegister.GetSelectedRecordCount();

                //vendorDataRegister.ClickOnCheckBox(filteringAndExportingData.GridViewItem, filteringAndExportingData.ItemDescription, uncheck: true)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountDeCreased(filteringAndExportingData.GridViewItem, filteringAndExportingData.ItemDescription, selectedRow))
                //    .DoubleClickItem(filteringAndExportingData.ItemIDNumber, filteringAndExportingData.GridViewItem, filteringAndExportingData.ItemDescription)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateTitlePageAreShownCorrectly(filteringAndExportingData.ContractNumber, filteringAndExportingData.ItemIDNumber))
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(filteringAndExportingData.GridViewDeliverable))
                //    .ClickOnCheckBox(filteringAndExportingData.GridViewDeliverable, filteringAndExportingData.DeliverableDescription, uncheck: false)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateLineItemsIsHighlighted(filteringAndExportingData.GridViewDeliverable, filteringAndExportingData.DeliverableDescription))
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountInCreased(filteringAndExportingData.GridViewDeliverable, filteringAndExportingData.DeliverableDescription));

                //selectedRow = vendorDataRegister.GetSelectedRecordCount();

                //vendorDataRegister.ClickOnCheckBox(filteringAndExportingData.GridViewDeliverable, filteringAndExportingData.DeliverableDescription, uncheck: true)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateSelectedCountDeCreased(filteringAndExportingData.GridViewDeliverable, filteringAndExportingData.DeliverableDescription, selectedRow))
                //    .DoubleClickItem(filteringAndExportingData.DeliverableNumber, filteringAndExportingData.GridViewDeliverable, filteringAndExportingData.DeliverableDescription)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateTitlePageAreShownCorrectly(filteringAndExportingData.ContractNumber, filteringAndExportingData.ItemIDNumber, filteringAndExportingData.DeliverableNumber))
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateItemsCountedAreMatches(filteringAndExportingData.GridViewDocument))
                //    .ClickOnBlueHeader(filteringAndExportingData.ContractNumber)
                //    .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateTitlePageAreShownCorrectly());

                //when - User Story 123595 - 120790 Filtering & Exporting Vendor Data Register Validation Part 5
                test = LogTest("US 123595 - 120790 Filtering & Exporting Vendor Data Register Validation - Part 5");

                int countWindowHandles = driver.WindowHandles.Count;

                VendorContractDetail contractDetail = vendorDataRegister.ClickOnBlueItem<VendorContractDetail>(filteringAndExportingData.GridViewContract, filteringAndExportingData.ContractNumber);
                contractDetail.LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateWindowIsOpen(countWindowHandles, open: true));

                vendorDataRegister = contractDetail.ClickCloseButtonOnPopUp<VendorDataRegister>();

                countWindowHandles = driver.WindowHandles.Count;

                vendorDataRegister.LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateWindowIsOpen(countWindowHandles, open: false))
                    .SelectFilterOption<VendorDataRegister>(filteringAndExportingData.ExpeditingViewFilter)
                    .ClickHeaderLabelToSort<VendorDataRegister>(MainPaneTableHeaderLabel.ContractNumber)
                    .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.Export, false)
                    .ClickHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.ExpeditingView, false);
                    //.DownloadFile<VendorDataRegister>(filteringAndExportingData.DownloadFilePath)
                    //.LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateExcelItemsCount(filteringAndExportingData.HierarchicalGridViewName,
                    //                                                                                               filteringAndExportingData.DownloadFilePath));

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
