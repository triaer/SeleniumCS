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
using KiewitTeamBinder.UI.Pages.Packages;

namespace KiewitTeamBinder.UI.Tests.ProjectDashboard
{
    [TestClass]
    public class NavigateToPackagesModuleFromLeftNav : UITestBase
    {
        [TestMethod]
        public void Validate_NavigateToPackagesModuleFromLeftNav()
        {
            try
            {
                // given
                validations = new List<KeyValuePair<string, bool>>();
                List<KeyValuePair<string, bool>> methodValidations = new List<KeyValuePair<string, bool>>();
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                var projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var packagesData = new NavigateToModulesFromTheLeftNavSmoke.PackagesModules();
                var transmittalsData = new NavigateToModulesFromTheLeftNavSmoke.TransmittalsModules();                

                test.Info("Navigate to DashBoard Page of Project: " + packagesData.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(packagesData.ProjectName);

                //when - 119703 Navigate to Modules from Left Nav
                //User Story 121270 - 119703 Navigate to Modules from the Left Nav - Part 2
                test = LogTest("119703 Navigate to Modules from Left Nav");

                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(transmittalsData.NavigatePath[0])
                    .LogValidation<ProjectsDashboard>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(transmittalsData.SubItemLinks));

                var columnValuesInConditionList = new List<KeyValuePair<string, string>>
                {
                    transmittalsData.ColumnValuesInConditionList.Subject
                };

                var transmittalsInbox = projectDashBoard.SelectModuleMenuItem<TransmittalsInbox>(transmittalsData.NavigatePath[1]);
                transmittalsInbox.LogValidation<TransmittalsInbox>(ref validations, transmittalsInbox.ValidateSubPageIsDislayed(transmittalsData.SubItemLinks[0]))
                    .LogValidation<TransmittalsInbox>(ref validations, transmittalsInbox.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                    .LogValidation<TransmittalsInbox>(ref validations, transmittalsInbox.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<TransmittalsInbox>(ref validations, transmittalsInbox.ValidateRecordItemsCount(transmittalsInbox.GetTableItemNumber(), transmittalsData.GridViewName))
                    .LogValidation<TransmittalsInbox>(ref validations, transmittalsInbox.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.TransmittalsModule);

                var transmittalsDrafts = projectDashBoard.SelectModuleMenuItem<TransmittalsDrafts>(transmittalsData.NavigatePath[2]);
                transmittalsDrafts.LogValidation<TransmittalsDrafts>(ref validations, transmittalsDrafts.ValidateSubPageIsDislayed(transmittalsData.SubItemLinks[1]))
                    .LogValidation<TransmittalsDrafts>(ref validations, transmittalsDrafts.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                    .LogValidation<TransmittalsDrafts>(ref validations, transmittalsDrafts.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<TransmittalsDrafts>(ref validations, transmittalsDrafts.ValidateRecordItemsCount(transmittalsInbox.GetTableItemNumber(), transmittalsData.GridViewName))
                    .LogValidation<TransmittalsDrafts>(ref validations, transmittalsDrafts.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewName);

                var transmittalsSentItems = projectDashBoard.SelectModuleMenuItem<TransmittalsSentItems>(transmittalsData.NavigatePath[3]);
                transmittalsSentItems.LogValidation<TransmittalsSentItems>(ref validations, transmittalsSentItems.ValidateSubPageIsDislayed(transmittalsData.SubItemLinks[2]))
                    .LogValidation<TransmittalsSentItems>(ref validations, transmittalsSentItems.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                    .LogValidation<TransmittalsSentItems>(ref validations, transmittalsSentItems.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<TransmittalsSentItems>(ref validations, transmittalsSentItems.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewName);

                var transmittalsPendingItems = projectDashBoard.SelectModuleMenuItem<TransmittalsPending>(transmittalsData.NavigatePath[4]);
                transmittalsSentItems.LogValidation<TransmittalsPending>(ref validations, transmittalsSentItems.ValidateSubPageIsDislayed(transmittalsData.SubPendingTitle))
                    .LogValidation<TransmittalsPending>(ref validations, transmittalsSentItems.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilterAtPendingPane))
                    .LogValidation<TransmittalsPending>(ref validations, transmittalsSentItems.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewPendingName);
                
                //User Story 121272 - 119703 Navigate to Modules from the Left Nav - Part 3
                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(packagesData.NavigatePath[0])
                    .LogValidation<ProjectsDashboard>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(packagesData.SubItemLinks));

                columnValuesInConditionList = new List<KeyValuePair<string, string>>
                {
                    packagesData.ColumnValuesInConditionList.PackageType
                };

                var packagesInbox = projectDashBoard.SelectModuleMenuItem<PackagesInbox>(packagesData.NavigatePath[1]);
                packagesInbox.LogValidation<PackagesInbox>(ref validations, packagesInbox.ValidateSubPageIsDislayed(packagesData.SubItemLinks[0]))
                    .LogValidation<PackagesInbox>(ref validations, packagesInbox.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                    .LogValidation<PackagesInbox>(ref validations, packagesInbox.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<PackagesInbox>(ref validations, packagesInbox.ValidateRecordItemsCount(packagesInbox.GetTableItemNumber(), packagesData.GridViewName))
                    .LogValidation<PackagesInbox>(ref validations, packagesInbox.ValidateItemsAreShown(columnValuesInConditionList, packagesData.GridViewName))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, packagesData.GridViewName);

                var packagesDrafts = projectDashBoard.SelectModuleMenuItem<PackagesDrafts>(packagesData.NavigatePath[2]);
                packagesDrafts.LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateSubPageIsDislayed(packagesData.SubItemLinks[1]))
                    .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                    .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateRecordItemsCount(packagesInbox.GetTableItemNumber(), packagesData.GridViewName))
                    .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateItemsAreShown(columnValuesInConditionList, packagesData.GridViewName))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, packagesData.GridViewName);

                var packagesSentItems = projectDashBoard.SelectModuleMenuItem<PackagesSentItems>(packagesData.NavigatePath[3]);
                packagesSentItems.LogValidation<PackagesSentItems>(ref validations, packagesSentItems.ValidateSubPageIsDislayed(packagesData.SubItemLinks[2]))
                    .LogValidation<PackagesSentItems>(ref validations, packagesSentItems.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                    .LogValidation<PackagesSentItems>(ref validations, packagesSentItems.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<PackagesSentItems>(ref validations, packagesSentItems.ValidateRecordItemsCount(packagesInbox.GetTableItemNumber(), packagesData.GridViewName))
                    .LogValidation<PackagesSentItems>(ref validations, packagesSentItems.ValidateItemsAreShown(columnValuesInConditionList, packagesData.GridViewName))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, packagesData.GridViewName);
                               
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
