using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using OpenQA.Selenium.Interactions;
using System.Diagnostics;


namespace KiewitTeamBinder.UI.Pages.Global
{
    public class ProjectsList : LoggedInLanding
    {
        #region Entities
        private string _projectListRows = "//table[contains(@id,'GridViewProjList')]//tbody/tr[@id='GridViewProjList_ctl00__{0}']";
        private static By _projListTitle => By.Id ("ProjListTitle");
        private static By _projGridDataTable => By.XPath("//div[@id='GridViewProjList_GridData']/table/tbody");
        private static By _projectTitleTextBox => By.XPath("//input[contains(@id,'ProjTitle')]");
        private static By _projectNoTextBox => By.XPath("//input[contains(@id,'ProjNo')]");
        private static By _projectNoImgFilter => By.XPath("//img[contains(@id,'ProjNo')]");
        private static By _projectTitleImgFilter => By.XPath("//img[contains(@id,'ProjTitle')]");
        private static By _projectImgFilterData => By.XPath("//div[@id='GridViewProjList_rfltMenu_detached']/ul/li");
        private static By _projectRows => By.XPath("//div[@id='GridViewProjList_GridData']/table/tbody/tr");

        public IWebElement ProjListTitle { get { return StableFindElement(_projListTitle); } }
        public IWebElement ProjGridDataTable { get { return StableFindElement(_projGridDataTable); } }
        public IWebElement ProjectTitleTextBox { get { return StableFindElement(_projectTitleTextBox); } }
        public IWebElement ProjectNoTextBox { get { return StableFindElement(_projectNoTextBox); } }
        public IWebElement ProjectNoImgFilter { get { return StableFindElement(_projectNoImgFilter); } }
        public IWebElement ProjectTitleImgFilter { get { return StableFindElement(_projectTitleImgFilter); } }

        #endregion

        #region Actions
        public ProjectsList(IWebDriver webDriver) : base(webDriver)
        {
        }

        public ProjectsDashboard NavigateToProjectDashboardPage(string filterValue)
        {

            //Filter project by title
            IWebElement ProjectItem = FilterProjectByIDOrTitle("Project Title", filterValue);
            //click on the project
            var dashboard = new ProjectsDashboard(WebDriver);
            ProjectItem.Click();
            WaitForElement(dashboard._dashBoardLabel);
            
            return dashboard;
        }

        public IWebElement FilterProjectByIDOrTitle(string filterColumnName, string filterValue)
        {
            int rowIndex, colIndex;
            var numberOfProject = StableFindElements(_projectRows).Count;
            if (filterColumnName.Equals("Project Title"))
            {
                ProjectTitleTextBox.InputText(filterValue);
                ProjectTitleTextBox.SendKeys(Keys.Enter);
            }
            else if (filterColumnName.Equals("Project No"))
            {
                ProjectNoTextBox.InputText(filterValue);
                ProjectNoTextBox.SendKeys(Keys.Enter);                
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (numberOfProject == StableFindElements(By.XPath("//div[@id='GridViewProjList_GridData']/table/tbody/tr")).Count
                   && stopwatch.ElapsedMilliseconds <= 1500) { }
  
            GetTableCellValueIndex(ProjGridDataTable, filterValue, out rowIndex, out colIndex);
            return TableCell(ProjGridDataTable, rowIndex, colIndex);
        }

        public KeyValuePair<string, bool> ValidateProjectIsHighlightedWhenHovered(string nameProject)
        {
            var node = StepNode();

            try
            {

                int rowIndex, colIndex;
                GetTableCellValueIndex(ProjGridDataTable, nameProject, out rowIndex, out colIndex);

                if (rowIndex == -1)
                    return SetFailValidation(node, Validation.Project_Is_Highlighted_When_Hovered);
                else
                {
                    IWebElement ProjectItem = StableFindElement(By.XPath(string.Format(_projectListRows, (rowIndex - 1))));
                    ScrollToElement(TableCell(ProjGridDataTable, rowIndex, colIndex));

                    string actualAttribute = ProjectItem.GetAttribute("class");
              
                    if (actualAttribute.Contains("HoveredRow"))
                        return SetPassValidation(node, Validation.Project_Is_Highlighted_When_Hovered);

                    else
                        return SetFailValidation(node, Validation.Project_Is_Highlighted_When_Hovered, "Attribute is contains HoveredRow", actualAttribute);
                }

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Project_Is_Highlighted_When_Hovered, e);
            }
        }

        public KeyValuePair<string, bool> ValidateDataInProjectListAvailable(string nameProject)
        {
            var node = StepNode();

            try
            {
                int rowIndex, colIndex;
                GetTableCellValueIndex(ProjGridDataTable, nameProject, out rowIndex, out colIndex);

                if (rowIndex == -1)
                    return SetFailValidation(node, Validation.Data_In_ProjectList_Availiable);
                else
                {
                    string actualProjectName = TableCell(ProjGridDataTable, rowIndex, colIndex).Text;

                    if (actualProjectName.Contains(nameProject))
                        return SetPassValidation(node, Validation.Data_In_ProjectList_Availiable);

                    else
                        return SetFailValidation(node, Validation.Data_In_ProjectList_Availiable, nameProject, actualProjectName);
                }

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Data_In_ProjectList_Availiable, e);
            }
        }

        private static class Validation
        {
            public static string Data_In_ProjectList_Availiable = "Validate That All Projected Availiable To The User Are Listed";
            public static string Project_Is_Highlighted_When_Hovered = "Validate That Project Is Highlighted When Hovered";
        }

        #endregion
    }
}
