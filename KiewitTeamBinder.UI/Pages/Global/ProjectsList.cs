using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Pages.Global
{
    public class ProjectsList : LoggedInLanding
    {
        #region Entities
        private static By _projListTitle => By.Id ("ProjListTitle");
        private static By _projGridDataTable => By.XPath("//div[@id='GridViewProjList_GridData']/table");

        public IWebElement ProjListTitle { get { return StableFindElement(_projListTitle); } }
        public IWebElement ProjGridDataTable { get { return StableFindElement(_projGridDataTable); } }
        #endregion

        #region Actions
        public ProjectsList(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Dashboard NavigateToProjectDashboardPage(string filterValue)
        {
            FilterProjectByIDOrTitle("Project Title", filterValue);
            
            //click on the project

            var dashboard = new Dashboard(WebDriver);

            return dashboard;
        }

        public string FilterProjectByIDOrTitle(string filterColumnName, string filterValue)
        {

            //Click filter icon of selected column
            

            

            

            return "";

        }

        #endregion
    }
}
