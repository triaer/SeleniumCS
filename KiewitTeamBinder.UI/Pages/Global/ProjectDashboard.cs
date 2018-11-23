using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using Microsoft.Office.Interop.Excel;

namespace KiewitTeamBinder.UI.Pages.Global
{
    public class ProjectDashboard : LoggedInLanding
    {
        #region Entities
        public By _dashBoardLabel => By.XPath("//span[.='Dashboard']");
        private static By _nameProjectLabel => By.Id("projectInput");
        private string _projectListRows = "//table[@id='ctl00_MaterToolbar_ProjectGrid_GridViewProjList_ctl00']//tbody/tr[{0}]";
        private static By _moduleButton(string value) => By.XPath($"//div[@id='divNavigatorExpand']//div[text()='{value}']");
        public static By _subMenuItemLink(string value) => By.XPath($"//span[(text()='{value}')]");

        public By _logoutLink => By.Id("LogoutLabel");
        private static By _projectListDropdown => By.Id("btnShowProjectList");
        private static By _projectListSumary => By.Id("divProjectSummary");
        private static By _projectListTable => By.XPath("//table[@id='ctl00_MaterToolbar_ProjectGrid_GridViewProjList_ctl00']");
        private static By _helpButtonDropdown => By.XPath("//div[@id='divHelpButton']");
        private static By _helpButtonDropDownData => By.XPath("//div[@id='HelpDropDown_detached']/ul/li");
        private static By _pageHeading => By.Id("lblRegisterCaption");
        private static By _divSubMenu => By.XPath("//div[@id='divSubMenu']");


        public IWebElement PageHeading { get { return StableFindElement(_pageHeading); } }
        public IWebElement ProjectListDropdown { get { return StableFindElement(_projectListDropdown); } }
        public IWebElement ProjectListSumary { get { return StableFindElement(_projectListSumary); } }
        public IWebElement ProjectListTable { get { return StableFindElement(_projectListTable); } }
        public IWebElement HelpButtonDropDown { get { return StableFindElement(_helpButtonDropdown); } }
        public IWebElement NameProjectLabel { get { return StableFindElement(_nameProjectLabel); } }
        public IWebElement DivSubMenu { get { return StableFindElement(_divSubMenu); } }
        public IWebElement ModuleButton(string value) => StableFindElement(_moduleButton(value));
        public IWebElement SubMenuItemLink(string value) => StableFindElement(_subMenuItemLink(value));

        #endregion

        #region Actions
        public ProjectDashboard(IWebDriver webDriver) : base(webDriver)
        {
            
        }

        public ProjectDashboard ShowProjectList()
        {
            ProjectListDropdown.Click();
            WaitForElementAttribute(ProjectListSumary, "display", "block");

            return this;
        }

        public HelpAboutDialog OpenHelpDialog(string option)
        {
            SelectComboboxByText(HelpButtonDropDown, _helpButtonDropDownData, option);
            var helpAboutDialog = new HelpAboutDialog(WebDriver);
            WebDriver.SwitchTo().Frame(helpAboutDialog.IFrameName);
            WaitUntil(driver => helpAboutDialog.OkButton != null);

            return helpAboutDialog;
        }
		public KeyValuePair<string, bool> ValidateProjectIsOpened(string nameProject)
        {
            var node = StepNode();

            try
            {
                if (NameProjectLabel.GetAttribute("title").Equals(nameProject))
                    return SetPassValidation(node, Validation.Project_Is_Opened);

                else
                    return SetFailValidation(node, Validation.Project_Is_Opened, nameProject, NameProjectLabel.GetAttribute("title"));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Project_Is_Opened, e);
            }
        }

        // MenuPath example: Mail or Mail/Inbox 
        public ProjectDashboard SelectModuleMenuItem(string menuPath)
        {
            var node = StepNode();

            var separator = '/';
            var nodes = menuPath.Split(separator);
            if (nodes.Count() == 1)
            {
                node.Info($"Click on the root node: {nodes[0]}");
                ModuleButton(nodes[0]).Click();
                WaitForElement(_divSubMenu);
            }
            else
            {
                node.Info($"Click on the root node: {nodes[0]}");
                ModuleButton(nodes[0]).Click();
                WaitForElement(_divSubMenu);
                node.Info($"Click on the sub node: {nodes[1]}");
                SubMenuItemLink(nodes[1]).Click();
                WaitForElement(_pageHeading);
            }

            return this;
        }

        public T SelectModuleMenuItem<T>(string menuPath)
        {
            SelectMenuItem(menuPath);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        private static class Validation
        {
            public static string Project_Is_Opened = "Validate That Number of Items Counted Is Valid";
        }
        #endregion
    }
}
