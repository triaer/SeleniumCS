using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.UI.Pages.VendorData;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;


namespace KiewitTeamBinder.UI.Pages.Global
{
    public class ProjectsDashboard : LoggedInLanding
    {
        #region Entities                
        public By _dashBoardLabel => By.XPath("//span[.='Dashboard']");
        private static By _nameProjectLabel => By.Id("projectInput");
        private static By _projectListDropdown => By.Id("btnShowProjectList");
        private static By _projectListSumary => By.Id("divProjectSummary");
        private static By _helpButtonDropdown => By.XPath("//div[@id='divHelpButton']");
        private static By _helpButtonDropDownData => By.XPath("//div[@id='HelpDropDown_detached']/ul/li");
        private static By _vendorButton => By.Id("divVendorData");
        private static By _viewFilter => By.Id("lblView");        
        private static By _formTitle => By.Id("formTitle");

        private static string _menuButton = "//li[a='{0}']";
        private static string _imageOfFilterBox = "//li[@id = 'FilterView{0}']//img[contains(@id,'Link{1}')]";

        public IWebElement FormTitle { get { return StableFindElement(_formTitle); } }        
        public IWebElement ViewFilter { get { return StableFindElement(_viewFilter); } }
        public IWebElement VendorButton { get { return StableFindElement(_vendorButton); } }
        public IWebElement ProjectListDropdown { get { return StableFindElement(_projectListDropdown); } }
        public IWebElement ProjectListSumary { get { return StableFindElement(_projectListSumary); } }
        public IWebElement HelpButtonDropDown { get { return StableFindElement(_helpButtonDropdown); } }
        public IWebElement NameProjectLabel { get { return StableFindElement(_nameProjectLabel); } }

        #endregion

        #region Actions
        public ProjectsDashboard(IWebDriver webDriver) : base(webDriver)
        {  }

        public ProjectsDashboard ShowProjectList()
        {
            ProjectListDropdown.Click();
            WaitForElementAttribute(ProjectListSumary, "display", "block");
            return this;
        }

        public ProjectsDashboard ClickVendorDataButton()
        {
            var node = StepNode();
            node.Info("Click Vendot Data button");
            VendorButton.Click();            
            WaitForElementDisplay(By.XPath(string.Format(_menuButton, "Holding Area")));
            return this;
        }

        public IWebElement MenuButton(string menu)
        {
            return StableFindElement(By.XPath(string.Format(_menuButton, menu)));
        }

        public HoldingArea ClickHoldingAreaButton()
        {
            var node = StepNode();
            node.Info("Click Holding Area button");
            MenuButton("Holding Area").Click();
            return new HoldingArea(WebDriver);
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

        public List<KeyValuePair<string, bool>> ValidateVendorDataMenusDisplay(string[] subMenu)
        {
            var node = StepNode();
            List<KeyValuePair<string, bool>> validations = new List<KeyValuePair<string, bool>> { };
            try
            {
                foreach (var item in subMenu)
                {
                    if (MenuButton(item).IsDisplayed())
                        validations.Add(SetPassValidation(node, Validation.Vendor_Data_Menus_Display));
                    else
                        validations.Add(SetFailValidation(node, Validation.Vendor_Data_Menus_Display));
                }
                return validations;
            }
            catch (Exception e)
            {
                validations.Add(SetErrorValidation(node, Validation.Vendor_Data_Menus_Display, e));
                return validations;
            }
        }

        public KeyValuePair<string, bool> ValidateDisplayedViewFilterOption(string filterOption)
        {
            var node = StepNode();
            try
            {
                WaitForElementDisplay(_viewFilter);
                if (ViewFilter.Text == filterOption)
                    return SetPassValidation(node, string.Format(Validation.Default_Filter_Display, filterOption));

                return SetFailValidation(node, string.Format(Validation.Default_Filter_Display, filterOption));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Default_Filter_Display, filterOption), e);
            }
        }

        public KeyValuePair<string, bool> ValidateFilterBoxIsHighlighted(int filterBoxIndex, bool isHighlighted = true)
        {
            var node = StepNode();
            try
            {
                filterBoxIndex = Utils.RefactorIndex(filterBoxIndex);
                string highlight = "Selected";
                if (!isHighlighted)
                    highlight = "NotSelected";

                IWebElement ImageOfFilterBox = StableFindElement(By.XPath(string.Format(_imageOfFilterBox, filterBoxIndex, highlight)));
                if (ImageOfFilterBox.IsDisplayed())                
                    return SetPassValidation(node, Validation.First_Filter_Box_Is_Highlighted);
                
                return SetFailValidation(node, Validation.First_Filter_Box_Is_Highlighted);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.First_Filter_Box_Is_Highlighted, e);
            }
        }

        public KeyValuePair<string, bool> ValidateWindowIsOpened(string windowName)
        {
            var node = StepNode();
            try
            {
                if (WebDriver.Title == windowName)
                    return SetPassValidation(node, string.Format(Validation.Validate_Window_Is_Opened, windowName));
                else
                    return SetFailValidation(node, string.Format(Validation.Validate_Window_Is_Opened, windowName));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Validate_Window_Is_Opened, windowName), e);
            }
        }

        public KeyValuePair<string, bool> ValidateFormTitle(string formTitle)
        {
            var node = StepNode();
            try
            {
                if (FormTitle.Text == formTitle)
                    return SetPassValidation(node, string.Format(Validation.Validate_Form_Title_Is_Correct, formTitle));
                else
                    return SetFailValidation(node, string.Format(Validation.Validate_Form_Title_Is_Correct, formTitle));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Validate_Form_Title_Is_Correct, formTitle), e);
            }
        }

        private static class Validation
        {
			public static string Project_Is_Opened = "Validate That The Project Is Opened";
            public static string Vendor_Data_Menus_Display = "Validate That The Vendor Data Menus Display";
            public static string Default_Filter_Display = "Validate that the Default Filter is {0}";
            public static string First_Filter_Box_Is_Highlighted = "Validate That The First Filter Box Is Highlighted";
            public static string Validate_Window_Is_Opened = "Validate That {0} window is opened";
            public static string Validate_Form_Title_Is_Correct = "Validate That form title is {0}";
        }

        #endregion

    }
}
