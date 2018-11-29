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

namespace KiewitTeamBinder.UI.Tests.VendorData
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
  
                test.Info("Navigate to DashBoard Page of Project: " + packagesData.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(packagesData.ProjectName);

                //when - 119703 Navigate to Modules from Left Nav
                //User Story 121329 - Part 3
                test = LogTest("119703 Navigate to Modules from Left Nav");
                projectDashBoard.SelectModuleMenuItem<PackagesInbox>(packagesData.NavigatePath[0])
                    .LogValidation<PackagesInbox>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(packagesData.SubItemLinks));

                var columnValuesInConditionList = new List<KeyValuePair<string, string>>
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

                var packagesDrafts = projectDashBoard.SelectModuleMenuItem<PackagesInbox>(packagesData.NavigatePath[2]);
                packagesDrafts.LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateSubPageIsDislayed(packagesData.SubItemLinks[1]))
                    .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                    .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateRecordItemsCount(packagesInbox.GetTableItemNumber(), packagesData.GridViewName))
                    .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateItemsAreShown(columnValuesInConditionList, packagesData.GridViewName))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, packagesData.GridViewName);

                var packagesSentItems = projectDashBoard.SelectModuleMenuItem<PackagesInbox>(packagesData.NavigatePath[3]);
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
