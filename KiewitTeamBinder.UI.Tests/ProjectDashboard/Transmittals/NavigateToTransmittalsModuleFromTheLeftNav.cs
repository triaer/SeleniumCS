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
using static KiewitTeamBinder.UI.KiewitTeamBinderENums;
using KiewitTeamBinder.UI.Pages.Packages;

namespace KiewitTeamBinder.UI.Tests.ProjectDashboard.Transmittals
{
    [TestClass]
    public class NavigateToTransmittalsModuleFromTheLeftNav : UITestBase
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

                var transmittalsData = new NavigateToModulesFromTheLeftNavSmoke.TransmittalsModules();
                test.Info("Navigate to DashBoard Page of Project: " + transmittalsData.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(transmittalsData.ProjectName);

                //when - 119703 Navigate to Modules from Left Nav
                //User Story 121270 - 119703 Navigate to Modules from the Left Nav - Part 2
                test = LogTest("119703 Navigate to Modules from Left Nav");
                projectDashBoard.SelectModuleMenuItem<PackagesInbox>(transmittalsData.NavigatePath[0])
                    .LogValidation<PackagesInbox>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(transmittalsData.SubItemLinks));
                var columnValuesInConditionList = new List<KeyValuePair<string, string>>
                {
                    transmittalsData.ColumnValuesInConditionList.Subject
                };

                var transmittalsInbox = projectDashBoard.SelectModuleMenuItem<PackagesInbox>(transmittalsData.NavigatePath[1]);
                transmittalsInbox.LogValidation<PackagesInbox>(ref validations, transmittalsInbox.ValidateSubPageIsDislayed(transmittalsData.SubItemLinks[0]))
                    .LogValidation<PackagesInbox>(ref validations, transmittalsInbox.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                    .LogValidation<PackagesInbox>(ref validations, transmittalsInbox.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<PackagesInbox>(ref validations, transmittalsInbox.ValidateRecordItemsCount(transmittalsInbox.GetTableItemNumber(), transmittalsData.GridViewName))
                    .LogValidation<PackagesInbox>(ref validations, transmittalsInbox.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.TransmittalsModule);

                var transmittalsDrafts = projectDashBoard.SelectModuleMenuItem<PackagesInbox>(transmittalsData.NavigatePath[2]);
                transmittalsDrafts.LogValidation<PackagesDrafts>(ref validations, transmittalsDrafts.ValidateSubPageIsDislayed(transmittalsData.SubItemLinks[1]))
                    .LogValidation<PackagesDrafts>(ref validations, transmittalsDrafts.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                    .LogValidation<PackagesDrafts>(ref validations, transmittalsDrafts.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<PackagesDrafts>(ref validations, transmittalsDrafts.ValidateRecordItemsCount(transmittalsInbox.GetTableItemNumber(), transmittalsData.GridViewName))
                    .LogValidation<PackagesDrafts>(ref validations, transmittalsDrafts.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewName);

                var transmittalsSentItems = projectDashBoard.SelectModuleMenuItem<PackagesInbox>(transmittalsData.NavigatePath[3]);
                transmittalsSentItems.LogValidation<PackagesSentItems>(ref validations, transmittalsSentItems.ValidateSubPageIsDislayed(transmittalsData.SubItemLinks[2]))
                    .LogValidation<PackagesSentItems>(ref validations, transmittalsSentItems.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                    .LogValidation<PackagesSentItems>(ref validations, transmittalsSentItems.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<PackagesSentItems>(ref validations, transmittalsSentItems.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewName);

                var transmittalsPendingItems = projectDashBoard.SelectModuleMenuItem<PackagesInbox>(transmittalsData.NavigatePath[4]);
                transmittalsSentItems.LogValidation<PackagesSentItems>(ref validations, transmittalsSentItems.ValidateSubPageIsDislayed(transmittalsData.SubPendingTitle))
                    .LogValidation<PackagesSentItems>(ref validations, transmittalsSentItems.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilterAtPendingPane))
                    .LogValidation<PackagesSentItems>(ref validations, transmittalsSentItems.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewPendingName);

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
