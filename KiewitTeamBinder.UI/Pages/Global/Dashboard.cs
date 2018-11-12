using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI;
using KiewitIn8.UI.Pages.Dialogs;

namespace KiewitTeamBinder.UI.Pages.Global
{
    public class Dashboard : LoggedInLanding
    {
        #region Entities
        public By _dashBoardLabel => By.XPath("//span[.='Dashboard']");
        private string _projectListRows = "//table[@id='ctl00_MaterToolbar_ProjectGrid_GridViewProjList_ctl00']//tbody/tr[{0}]";

        public By _logoutLink => By.Id("LogoutLabel");
        private static By _projectListDropdown => By.Id("btnShowProjectList");
        private static By _projectListSumary => By.Id("divProjectSummary");
        private static By _projectListTable => By.XPath("//table[@id='ctl00_MaterToolbar_ProjectGrid_GridViewProjList_ctl00']");
        private static By _helpButtonDropdown => By.XPath("//div[@id='divHelpButton']");
        private static By _helpButtonDropDownData => By.XPath("//div[@id='HelpDropDown_detached']/ul/li");

        public IWebElement LogoutLink { get { return StableFindElement(_logoutLink); } }
        public IWebElement ProjectListDropdown { get { return StableFindElement(_projectListDropdown); } }
        public IWebElement ProjectListSumary { get { return StableFindElement(_projectListSumary); } }
        public IWebElement ProjectListTable { get { return StableFindElement(_projectListTable); } }
        public IWebElement HelpButtonDropDown { get { return StableFindElement(_helpButtonDropdown); } }

        #endregion

        #region Actions
        public Dashboard(IWebDriver webDriver) : base(webDriver)
        {
            
        }

        public Dashboard ShowProjectList()
        {
            ProjectListDropdown.Click();
            WaitForElementAttribute(ProjectListSumary, "display", "block");

            return this;
        }

        public KeyValuePair<string, bool> ValidateDataInProjectListAvailable(string nameProject)
        {
            var node = StepNode();
            
            try
            {
                int rowIndex, colIndex;
                GetTableCellValueIndex(ProjectListTable, nameProject, out rowIndex, out colIndex);

                if (rowIndex == -1)
                    return SetFailValidation(node, Validation.Data_In_ProjectList_Availiable);
                else
                {
                    string actualProjectName = TableCell(ProjectListTable, rowIndex, colIndex).Text;

                    if (actualProjectName.Equals(nameProject))
                        return SetPassValidation(node, Validation.Data_In_ProjectList_Availiable);

                    else
                        return SetFailValidation(node, Validation.Data_In_ProjectList_Availiable, nameProject, "Actual Project Name: " + actualProjectName);
                }
                   
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Data_In_ProjectList_Availiable, e);
            }
        }

        public KeyValuePair<string, bool> ValidateProjectIsHighlightedWhenHovered(string nameProject)
        {
            var node = StepNode();

            try
            {
                
                int rowIndex, colIndex;
                GetTableCellValueIndex(ProjectListTable, nameProject, out rowIndex, out colIndex);

                ScrollToElement(StableFindElement(By.XPath(string.Format(_projectListRows, rowIndex))));

                if (rowIndex == -1)
                    return SetFailValidation(node, Validation.Data_In_ProjectList_Availiable);
                else
                {
                    string actualAttribute = StableFindElement(By.XPath(string.Format(_projectListRows, rowIndex))).GetAttribute("class");

                    if (actualAttribute.Contains("HoveredRow"))
                        return SetPassValidation(node, Validation.Data_In_ProjectList_Availiable);

                    else
                        return SetFailValidation(node, Validation.Data_In_ProjectList_Availiable, "Attribute is contains HoveredRow", "Actual Attribute: " + actualAttribute);
                }

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Data_In_ProjectList_Availiable, e);
            }
        }

        public HelpAboutDialog OpenHelpDialog(string option)
        {
            SelectComboboxByText(HelpButtonDropDown, _helpButtonDropDownData, option);
            var helpAboutDialog = new HelpAboutDialog(WebDriver);
            WaitUntil(driver => helpAboutDialog.OkButton != null);

            return helpAboutDialog;
        }
        private static class Validation
        {
            public static string Data_In_ProjectList_Availiable = "Validate That All Projected Availiable To The User Are Listed";
            public static string Project_Is_Highlighted_When_Hovered = "Validate That Project Is Highlighted When Hovered";
        }

        #endregion

    }
}
