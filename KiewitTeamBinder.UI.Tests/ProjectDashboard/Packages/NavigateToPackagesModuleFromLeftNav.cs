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

                var packagesData = new PackagesModuleSmoke();
                var columnValuePairList = new List<KeyValuePair<string, string>> {packagesData.ColumnNames.Package,
                                                                  packagesData.ColumnNames.Package };
                test.Info("Navigate to DashBoard Page of Project: " + packagesData.ProjectName);
                var projectDashBoard = projectsList.NavigateToProjectDashboardPage(packagesData.ProjectName);

                //when - 119703 Navigate to Modules from Left Nav
                //User Story 121329 - Part 3
                test = LogTest("119703 Navigate to Modules from Left Nav");
                projectDashBoard.SelectModuleMenuItem(packagesData.PackagesPath[0]).ValidateDisplayedSubItemLinks(packagesData.PackagesSubItemLinks);

                var packagesInbox = projectDashBoard.SelectModuleMenuItem(packagesData.PackagesPath[1]);
                packagesInbox.LogValidation<PackagesInbox>(ref validations, packagesInbox.ValidateSubPageIsDislayed(packagesData.PackagesSubItemLinks[0]))
                             .LogValidation<PackagesInbox>(ref validations, packagesInbox.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                             .LogValidation<PackagesInbox>(ref validations, packagesInbox.ValidateFilterBoxIsHighlighted(1))
                             .LogValidation<PackagesInbox>(ref validations, packagesInbox.ValidateRecordItemsCount(packagesInbox.GetTableItemNumber(), packagesData.PackageModule))
                             .LogValidation<PackagesInbox>(ref validations, packagesInbox.ValidateItemsAreShown(columnValuePairList));

                packagesInbox.ClickHeaderButton<ProjectsDashboard>(PackagesInboxHeaderButton.Refresh, true, packagesData.PackageModule);

                var packagesDrafts = projectDashBoard.SelectModuleMenuItem(packagesData.PackagesPath[2]);
                packagesDrafts.LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateSubPageIsDislayed(packagesData.PackagesSubItemLinks[1]))
                              .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                              .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateFilterBoxIsHighlighted(1))
                              .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateRecordItemsCount(packagesInbox.GetTableItemNumber(), packagesData.PackageModule))
                              .LogValidation<PackagesDrafts>(ref validations, packagesDrafts.ValidateItemsAreShown(columnValuePairList));
                packagesDrafts.ClickHeaderButton<ProjectsDashboard>(PackagesInboxHeaderButton.Refresh, true, packagesData.PackageModule);

                var packagesSentItems = projectDashBoard.SelectModuleMenuItem(packagesData.PackagesPath[3]);
                packagesSentItems.LogValidation<PackagesSentItems>(ref validations, packagesSentItems.ValidateSubPageIsDislayed(packagesData.PackagesSubItemLinks[2]))
                                 .LogValidation<PackagesSentItems>(ref validations, packagesSentItems.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                                 .LogValidation<PackagesSentItems>(ref validations, packagesSentItems.ValidateFilterBoxIsHighlighted(1))
                                 .LogValidation<PackagesSentItems>(ref validations, packagesSentItems.ValidateRecordItemsCount(packagesInbox.GetTableItemNumber(), packagesData.PackageModule))
                                 .LogValidation<PackagesSentItems>(ref validations, packagesSentItems.ValidateItemsAreShown(columnValuePairList));
                packagesSentItems.ClickHeaderButton<ProjectsDashboard>(PackagesInboxHeaderButton.Refresh, true, packagesData.PackageModule);

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
