using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Pages.PublishedReportsModule
{
    public class PublishedReports : ProjectsDashboard
    {
        #region Entities
        private static By _menuPublishReportButton(string valueNameButton) => By.XPath($"//div[contains(@id, 'RegisterHeaderCurveCenter')]//a[span='{valueNameButton}']");
        private static By _hierarchyRootButton => By.XPath("//span[contains(text(),'Automation Project')]/preceding::span[1]");

        public IWebElement HierarchyRootButton { get { return StableFindElement(_hierarchyRootButton); } }
        #endregion

        #region Actions
        public PublishedReports(IWebDriver webDriver) : base(webDriver)
        {
        }

        public PublishedReports ClickHierarchyRootButton()
        {
            HierarchyRootButton.Click();
            return this;
        }

        public KeyValuePair<string, bool> ValidateHierarchyTreeStatusIsCorrect(bool expand = false)
        {
            var node = StepNode();
            string actual;
            try
            {
                if(expand == true)
                {
                    node.Info("Expand Hierarchy Tree");

                    actual = HierarchyRootButton.GetAttribute("class");
                    if (actual.Contains("Minus"))
                        return SetPassValidation(node, Validation.Hierarchy_Tree_Status_Is_Correct);

                    return SetFailValidation(node, Validation.Hierarchy_Tree_Status_Is_Correct);
                }
                
                node.Info("Collapse Hierarchy Tree");

                actual = HierarchyRootButton.GetAttribute("class");
                node.Info("Actual Atribute: " + actual);
                if (actual.Contains("Plus"))
                    return SetPassValidation(node, Validation.Hierarchy_Tree_Status_Is_Correct);

                return SetFailValidation(node, Validation.Hierarchy_Tree_Status_Is_Correct);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Hierarchy_Tree_Status_Is_Correct, e);
            }
        }

        public List<KeyValuePair<string, bool>> ValidateButtonDisplayCorrect(string[] listButton)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();

            try
            {
                for (int i = 0; i < listButton.Length; i++)
                {
                    node.Info("Validate Button: " + listButton[i]);
                    if (StableFindElement(_menuPublishReportButton(listButton[i])) != null)
                        validation.Add(SetPassValidation(node, Validation.Button_Display_Correct + listButton[i]));
                    else
                        validation.Add(SetFailValidation(node, Validation.Button_Display_Correct + listButton[i]));
                }

                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Button_Display_Correct, e));
            }

            return validation;
        }

        #endregion

        private static class Validation
        {
            public static string Button_Display_Correct = "Validate that button display correctly: ";
            public static string Hierarchy_Tree_Status_Is_Correct = "Validate that Hierarchy Tree status is correctly";
        }
    }
}
