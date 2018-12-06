using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.Common.Helper;
using System.Timers;


namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class TransmittalDetail : ProjectsDashboard
    {
        #region Entities
        private static By _projectNumberInfo => By.XPath("//td[contains(text(), 'Project Number:')]/following-sibling::td[1]");
        private static By _projectTitleInfo => By.XPath("//td[contains(text(), 'Project Title:')]/following-sibling::td[1]");
        private static By _dateInfo => By.XPath("//td[contains(text(), 'Date:')]/following-sibling::td[1]");
        private static By _subjectInfo => By.XPath("//td[contains(text(), 'Subject:')]/following-sibling::td[1]");

        public IWebElement ProjectNumberInfo { get { return StableFindElement(_projectNumberInfo); } }
        public IWebElement ProjectTitleInfo { get { return StableFindElement(_projectTitleInfo); } }
        public IWebElement DateInfo { get { return StableFindElement(_dateInfo); } }
        public IWebElement SubjectInfo { get { return StableFindElement(_subjectInfo); } }
        #endregion

        #region Actions
        public TransmittalDetail(IWebDriver webDriver) : base(webDriver) { }

        public KeyValuePair<string, bool> ValidateProjectNumberIsCorrect(string projectNumber)
        {
            var node = StepNode();
            node.Info("Validate the project number is correct");
            try
            {
                if (ProjectNumberInfo.Text == projectNumber)
                    return SetPassValidation(node, Validation.Project_Number_Is_Correct);
                return SetFailValidation(node, Validation.Project_Number_Is_Correct);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Project_Number_Is_Correct, e);
            }
        }

        public KeyValuePair<string, bool> ValidateProjectNameIsCorrect(string projectName)
        {
            var node = StepNode();
            node.Info("Validate the project number is correct");
            try
            {
                if (ProjectTitleInfo.Text == projectName)
                    return SetPassValidation(node, Validation.Project_Name_Is_Correct);
                return SetFailValidation(node, Validation.Project_Name_Is_Correct);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Project_Name_Is_Correct, e);
            }
        }

        public KeyValuePair<string, bool> ValidateDateIsCurrentDate()
        {
            var node = StepNode();
            node.Info("Validate the project number is correct");
            try
            {                               
                DateTime documentDate = DateTime.Parse(DateInfo.Text.Split('-')[0]);
                documentDate = documentDate.AddHours(+13);
                DateTime currentDate = DateTime.Now;
                if (documentDate.Year == currentDate.Year &&
                    documentDate.Month == currentDate.Month &&
                    documentDate.Day == currentDate.Day &&
                    documentDate.Hour == currentDate.Hour &&
                    documentDate.Minute == currentDate.Minute)
                {
                    return SetPassValidation(node, Validation.Date_Is_Current_Date);
                }
                return SetFailValidation(node, Validation.Date_Is_Current_Date);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Date_Is_Current_Date, e);
            }
        }

        private static class Validation
        {
            public static string Project_Number_Is_Correct = "Validate that the project number is correct";
            public static string Project_Name_Is_Correct = "Validate that the project name is correct";
            public static string Date_Is_Current_Date = "Validate that the date is current date";

        }
         

        #endregion
    }
}
