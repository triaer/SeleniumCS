using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Windows.Forms;
using KiewitTeamBinder.UI.Pages;
using AventStack.ExtentReports;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.Dialogs
{
    public class HelpAboutDialog : LoggedInLanding
    {
        #region Entities
        public string IFrameName = "RadWindowAbout";
        private static By _teamBinderVersion => By.XPath("//span[@id='TBVersion']");
        public static By _okButton => By.XPath("//input[@name='btnOK']");

        public IWebElement TeamBinderVersion { get { return StableFindElement(_teamBinderVersion); } }
        public IWebElement OkButton { get { return StableFindElement(_okButton); } }
        #endregion

        #region Actions
        public HelpAboutDialog(IWebDriver webDriver) : base(webDriver)
        {
        }

        public void CloseHelpDialog()
        {
            OkButton.Click();
            WaitForElementDisappear(_teamBinderVersion);
        }

        public KeyValuePair<string, bool> ValidateTeamBinderVersion(string version)
        {
            var node = StepNode();

            try
            {
                var actualVersion = TeamBinderVersion.Text;
                //if (TeamBinderVersion.GetAttribute("text") == version)
                if (actualVersion == version)
                        return SetPassValidation(node,
                        Validation.TeamBinder_Version_Field_Displayed_Correctly + " - " + actualVersion);

                else
                    return SetFailValidation(node,
                        Validation.TeamBinder_Version_Field_Displayed_Incorrectly, version, actualVersion);
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, Validation.TeamBinder_Version_Field_Displayed_Incorrectly, e); ;
            }
            
        }

        private static class Validation
        {
            public static string TeamBinder_Version_Field_Displayed_Correctly = "Team Binder's version is displayed correctly.";
            public static string TeamBinder_Version_Field_Displayed_Incorrectly = "Team Binder's version is displayed incorrectly.";
        }

        #endregion
    }
}
