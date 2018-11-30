using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.VendorData;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using System.Threading;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.UI.Pages.PackagesModule;
using KiewitTeamBinder.UI.Pages.TransmittalsModule;
using KiewitTeamBinder.UI.Pages.DashboardModule;
using KiewitTeamBinder.UI.Pages.DocumentModule;

namespace KiewitTeamBinder.UI.Tests.ProjectDashboard
{
    [TestClass]
    public class NavigateToModulesFromLeftNav : UITestBase
    {
        [TestMethod]
        public void NavigateToModulesFromLeftNavSmoke()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");

                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                // and log on via Other User Login Kiewit Account
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                var projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;
                var transmittalsData = new NavigateToModulesFromTheLeftNavSmoke.TransmittalsModules();
                test.Info("Navigate to DashBoard Page of Project: " + transmittalsData.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(transmittalsData.ProjectName);

                //when - 119703 Navigate to Modules from Left Nav
                //User Story 121270 - 119703 Navigate to Modules from the Left Nav - Part 2 - Transmittals Module 
                //test = LogTest("119703 Navigate to Modules from Left Nav - Transmittals Module");
                //var columnValuesInConditionList = new List<KeyValuePair<string, string>> { transmittalsData.ColumnValuesInConditionList.Subject };
                //projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(ModuleNameInLeftNav.TRANSMITTALS.ToDescription())
                //    .LogValidation<ProjectsDashboard>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(transmittalsData.SubItemLinks));

                //var transmittalsModule = projectDashBoard.SelectModuleMenuItem<Transmittal>(ModuleNameInLeftNav.TRANSMITTALS.ToDescription() + "/" + ModuleSubMenuInLeftNav.INBOX.ToDescription());
                //transmittalsModule.LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.INBOX.ToDescription()))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateRecordItemsCount(transmittalsModule.GetTableItemNumber(), transmittalsData.GridViewName))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                //    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.TransmittalsModule);

                //projectDashBoard.SelectModuleMenuItem<Transmittal>(ModuleNameInLeftNav.TRANSMITTALS.ToDescription() + "/" + ModuleSubMenuInLeftNav.DRAFTS.ToDescription())
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.DRAFTS.ToDescription()))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateRecordItemsCount(transmittalsModule.GetTableItemNumber(), transmittalsData.GridViewName))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                //    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewName);

                //projectDashBoard.SelectModuleMenuItem<Transmittal>(ModuleNameInLeftNav.TRANSMITTALS.ToDescription() + "/" + ModuleSubMenuInLeftNav.SENTITEMS.ToDescription())
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.SENTITEMS.ToDescription()))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                //    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewName);

                //projectDashBoard.SelectModuleMenuItem<Transmittal>(ModuleNameInLeftNav.TRANSMITTALS.ToDescription() + "/" + ModuleSubMenuInLeftNav.PENDING.ToDescription())
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateSubPageIsDislayed(transmittalsData.SubPendingTitle))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilterAtPendingPane))
                //    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                //    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewPendingName);

                ////User Story 121272 - 119703 Navigate to Modules from the Left Nav - Part 3
                //test = LogTest("119703 Navigate to Modules from Left Nav - Packages Module");
                //var packagesData = new NavigateToModulesFromTheLeftNavSmoke.PackagesModules();
                //columnValuesInConditionList = new List<KeyValuePair<string, string>> { packagesData.ColumnValuesInConditionList.PackageType };
                //projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(ModuleNameInLeftNav.PACKAGES.ToDescription())
                //    .LogValidation<ProjectsDashboard>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(packagesData.SubItemLinks));

                //var packagesModule = projectDashBoard.SelectModuleMenuItem<Package>(ModuleNameInLeftNav.PACKAGES.ToDescription() + "/" + ModuleSubMenuInLeftNav.INBOX.ToDescription());
                //packagesModule.LogValidation<Package>(ref validations, packagesModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.INBOX.ToDescription()))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateRecordItemsCount(packagesModule.GetTableItemNumber(), packagesData.GridViewName))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateItemsAreShown(columnValuesInConditionList, packagesData.GridViewName))
                //    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, packagesData.GridViewName);

                //projectDashBoard.SelectModuleMenuItem<Package>(ModuleNameInLeftNav.PACKAGES.ToDescription() + "/" + ModuleSubMenuInLeftNav.DRAFTS.ToDescription())
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.DRAFTS.ToDescription()))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateRecordItemsCount(packagesModule.GetTableItemNumber(), packagesData.GridViewName))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateItemsAreShown(columnValuesInConditionList, packagesData.GridViewName))
                //    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, packagesData.GridViewName);

                //projectDashBoard.SelectModuleMenuItem<Package>(ModuleNameInLeftNav.PACKAGES.ToDescription() + "/" + ModuleSubMenuInLeftNav.SENTITEMS.ToDescription())
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.SENTITEMS.ToDescription()))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateRecordItemsCount(packagesModule.GetTableItemNumber(), packagesData.GridViewName))
                //    .LogValidation<Package>(ref validations, packagesModule.ValidateItemsAreShown(columnValuesInConditionList, packagesData.GridViewName))
                //    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, packagesData.GridViewName);

                //User Story 121272 - 119703 Navigate to Modules from the Left Nav - Part 9 - Dashboard Module - Documents Module
                string parrentWindow;
                var dashboardData = new NavigateToModulesFromTheLeftNavSmoke.DashboardModules();

                var dashboardModule = projectDashBoard.SelectModuleMenuItem<Dashboard>(ModuleNameInLeftNav.DASHBOARD.ToDescription());
                dashboardModule.LogValidation<ProjectsDashboard>(ref validations, dashboardModule.ValidateWidgetsOfDashboardDisplayed(dashboardData.ListWidgits));

                var documentModule = dashboardModule.SelectModuleMenuItem<Document>(ModuleNameInLeftNav.DOCUMENTS.ToDescription());
                documentModule.LogValidation<Document>(ref validations, dashboardModule.ValidateDisplayedViewFilterOption(dashboardData.DefaultFilter))
                              //issue
                              .LogValidation<Document>(ref validations, dashboardModule.ValidateRecordItemsCount(dashboardModule.GetTableItemNumber(), dashboardData.GridViewName))
                              .LogValidation<Document>(ref validations, dashboardModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                              .OpenDocument(dashboardData.DocumentNo, out parrentWindow)
                              //.SelectDocmentByIndexRandom(dashboardModule.GetTableItemNumber(), out parrentWindow)
                              .LogValidation<Document>(ref validations, documentModule.ValidateDocumentsDetailIsOpened(dashboardData.DocumentNo))
                              .ClickCloseButton()
                              .SwitchToWindow(parrentWindow)
                              .Logout();

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
