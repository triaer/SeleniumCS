using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.DashboardModule
{
    public class Dashboard : ProjectsDashboard
    {
        #region Entities
        private static string _widgitDashboardXpath = "//span[@class='Title' and text()='{0}']";
        #endregion

        #region Actions
        public Dashboard(IWebDriver webDriver) : base(webDriver)
        {

        }

        public KeyValuePair<string, bool> ValidateWidgetsOfDashboardDisplayed(string[] widgits)
        {
            var node = StepNode();
            
            try
            {
                if(widgits.Length >= 0)
                {
                    for (int i = 0; i < widgits.Length; i++)
                    {
                        if (StableFindElement(By.XPath(string.Format(_widgitDashboardXpath, widgits[i]))) != null)
                            return SetPassValidation(node, Validation.Widgit_Dashboard_Dispalyed + widgits[i]);
                        else
                            return SetFailValidation(node, Validation.Widgit_Dashboard_Dispalyed + widgits[i]);
                    }
                }
                return SetFailValidation(node, Validation.Widgit_Dashboard_Dispalyed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Widgit_Dashboard_Dispalyed, e);
            }
        }
        #endregion

        private static class Validation
        {
            public static string Widgit_Dashboard_Dispalyed = "Validate that the widgit is displayed: ";
        }
    }
}
