using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using System.Threading;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.UI.Pages.TransmittalsModule;
using KiewitTeamBinder.UI.Pages.PackagesModule;
using KiewitTeamBinder.UI.Pages.TasksModule;
using KiewitTeamBinder.UI.Pages.FormModule;
 

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
                //User Story 121270 - 119703 Navigate to Modules from the Left Nav - Part 2 - Transmittals Modules 
                test = LogTest("119703 Navigate to Modules from Left Nav - Transmittals Modules");
                var columnValuesInConditionList = new List<KeyValuePair<string, string>> { transmittalsData.ColumnValuesInConditionList.Subject };

                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.TRANSMITTALS.ToDescription())
                    .LogValidation<ProjectsDashboard>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(transmittalsData.SubItemMenus));

                Transmittal transmittalsModule = projectDashBoard.SelectModuleMenuItem<Transmittal>(subMenuItem: ModuleSubMenuInLeftNav.INBOX.ToDescription());
                transmittalsModule.LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.INBOX.ToDescription()))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateRecordItemsCount(transmittalsData.GridViewName))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                    .ClickHeaderButton<Transmittal>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.TransmittalsModule);

                projectDashBoard.SelectModuleMenuItem<Transmittal>(subMenuItem: ModuleSubMenuInLeftNav.DRAFTS.ToDescription());
                transmittalsModule.LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.DRAFTS.ToDescription()))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateRecordItemsCount(transmittalsData.GridViewName))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                    .ClickHeaderButton<Transmittal>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewName);

                projectDashBoard.SelectModuleMenuItem<Transmittal>(subMenuItem: ModuleSubMenuInLeftNav.SENTITEMS.ToDescription());
                transmittalsModule.LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.SENTITEMS.ToDescription()))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilter))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateRecordItemsCount(transmittalsData.GridViewName))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateItemsAreShown(columnValuesInConditionList, transmittalsData.GridViewName))
                    .ClickHeaderButton<Transmittal>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewName);

                projectDashBoard.SelectModuleMenuItem<Transmittal>(subMenuItem: ModuleSubMenuInLeftNav.PENDING.ToDescription());
                transmittalsModule.LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateSubPageIsDislayed(transmittalsData.SubPendingTitle))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateDisplayedViewFilterOption(transmittalsData.DefaultFilterAtPendingPane))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<Transmittal>(ref validations, transmittalsModule.ValidateRecordItemsCount(transmittalsData.GridViewPendingName))
                    .ClickHeaderButton<Transmittal>(MainPaneTableHeaderButton.Refresh, true, transmittalsData.GridViewPendingName);

                //User Story 121272 - 119703 Navigate to Modules from the Left Nav - Part 3 - Packages Modules
                test = LogTest("119703 Navigate to Modules from Left Nav - Packages Module");
                var packagesData = new NavigateToModulesFromTheLeftNavSmoke.PackagesModules();
                columnValuesInConditionList = new List<KeyValuePair<string, string>> { packagesData.ColumnValuesInConditionList.PackageType };

                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.PACKAGES.ToDescription())
                    .LogValidation<ProjectsDashboard>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(packagesData.SubItemMenus));

                Package packagesModule = projectDashBoard.SelectModuleMenuItem<Package>(subMenuItem: ModuleSubMenuInLeftNav.INBOX.ToDescription());
                packagesModule.LogValidation<Package>(ref validations, packagesModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.INBOX.ToDescription()))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateRecordItemsCount(packagesData.GridViewName))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateItemsAreShown(columnValuesInConditionList, packagesData.GridViewName))
                    .ClickHeaderButton<Package>(MainPaneTableHeaderButton.Refresh, true, packagesData.GridViewName);

                projectDashBoard.SelectModuleMenuItem<Package>(subMenuItem: ModuleSubMenuInLeftNav.DRAFTS.ToDescription());
                packagesModule.LogValidation<Package>(ref validations, packagesModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.DRAFTS.ToDescription()))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateRecordItemsCount(packagesData.GridViewName))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateItemsAreShown(columnValuesInConditionList, packagesData.GridViewName))
                    .ClickHeaderButton<Package>(MainPaneTableHeaderButton.Refresh, true, packagesData.GridViewName);

                projectDashBoard.SelectModuleMenuItem<Package>(subMenuItem: ModuleSubMenuInLeftNav.SENTITEMS.ToDescription());
                packagesModule.LogValidation<Package>(ref validations, packagesModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.SENTITEMS.ToDescription()))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateDisplayedViewFilterOption(packagesData.DefaultFilter))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateRecordItemsCount(packagesData.GridViewName))
                    .LogValidation<Package>(ref validations, packagesModule.ValidateItemsAreShown(columnValuesInConditionList, packagesData.GridViewName))
                    .ClickHeaderButton<Package>(MainPaneTableHeaderButton.Refresh, true, packagesData.GridViewName);

                //User Story 121307 - 119703 Navigate to Modules from the Left Nav - Part 4 - Tasks Modules
                test = LogTest("119703 Navigate to Modules from Left Nav - Tasks Module");
                var tasksData = new NavigateToModulesFromTheLeftNavSmoke.TasksModules();
                columnValuesInConditionList = new List<KeyValuePair<string, string>> { tasksData.ColumnValuesInConditionList.Subject };

                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.TASKS.ToDescription())
                    .LogValidation<ProjectsDashboard>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(tasksData.SubItemMenus));

                Task tasksModule = projectDashBoard.SelectModuleMenuItem<Task>(subMenuItem: ModuleSubMenuInLeftNav.INBOX.ToDescription());
                tasksModule.LogValidation<Task>(ref validations, tasksModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.INBOX.ToDescription()))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateDisplayedViewFilterOption(tasksData.DefaultFilter))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateRecordItemsCount(tasksData.GridViewName))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateItemsAreShown(columnValuesInConditionList, tasksData.GridViewName))
                    .ClickHeaderButton<Task>(MainPaneTableHeaderButton.Refresh, true, tasksData.GridViewName);

                projectDashBoard.SelectModuleMenuItem<Task>(subMenuItem: ModuleSubMenuInLeftNav.DRAFTS.ToDescription());
                tasksModule.LogValidation<Task>(ref validations, tasksModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.DRAFTS.ToDescription()))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateDisplayedViewFilterOption(tasksData.DefaultFilter))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateRecordItemsCount(tasksData.GridViewName))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateItemsAreShown(columnValuesInConditionList, tasksData.GridViewName))
                    .ClickHeaderButton<Task>(MainPaneTableHeaderButton.Refresh, true, tasksData.GridViewName);

                projectDashBoard.SelectModuleMenuItem<Task>(subMenuItem: ModuleSubMenuInLeftNav.SENTITEMS.ToDescription());
                tasksModule.LogValidation<Task>(ref validations, tasksModule.ValidateSubPageIsDislayed(ModuleSubMenuInLeftNav.SENTITEMS.ToDescription()))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateDisplayedViewFilterOption(tasksData.DefaultFilter))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateRecordItemsCount(tasksData.GridViewName))
                    .LogValidation<Task>(ref validations, tasksModule.ValidateItemsAreShown(columnValuesInConditionList, tasksData.GridViewName))
                    .ClickHeaderButton<Task>(MainPaneTableHeaderButton.Refresh, true, tasksData.GridViewName);

                //User Story 121327 - 119703 Navigate to Modules from the Left Nav - Part 6 - Forms Modules
                test = LogTest("119703 Navigate to Modules from Left Nav - Forms Module");
                var formsData = new NavigateToModulesFromTheLeftNavSmoke.FormsModules();
                Form formsModule = projectDashBoard.SelectModuleMenuItem<Form>(menuItem: ModuleNameInLeftNav.FORMS.ToDescription());
                formsModule.LogValidation<Form>(ref validations, formsModule.ValidateDisplayedViewFilterOption(formsData.DefaultFilter))
                    .LogValidation<Form>(ref validations, formsModule.ValidateRecordItemsCount(formsData.GridViewName))
                    .LogValidation<Form>(ref validations, formsModule.ValidateFilterBoxIsHighlighted(filterBoxIndex: 1))
                    .ClickHeaderButton<ProjectsDashboard>(MainPaneTableHeaderButton.Refresh, true, formsData.FormsModule);
                                          
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
