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
                   .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateRecordItemsCount(filteringAndExportingData.GridViewName))
                   .ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.Export, false)
                   .ClickHeaderDropdownItem<VendorDataRegister>(MainPaneHeaderDropdownItem.Contracts, false)
                   .DownloadFile<VendorDataRegister>(filteringAndExportingData.DownloadFilePath)
                   .LogValidation<VendorDataRegister>(ref validations, vendorDataRegister.ValidateExcelItemsCount(filteringAndExportingData.GridViewName,
                                                                                                                  filteringAndExportingData.DownloadFilePath));

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
