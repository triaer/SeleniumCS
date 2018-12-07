using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.Common;
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
        private static By _transmittalNoInfo => By.XPath("//td[contains(text(), 'Transmittal No:')]/following-sibling::td[1]");
        private static By _headerTransmittalNoInfo => By.XPath("//td[contains(text(), 'Transmittal No.:')]/following-sibling::td[1]");
        private static By _fromInfo => By.XPath("//td[contains(text(), 'From:')]/following-sibling::td[1]");
        private static By _attachedDocumentName(string index) => By.XPath($"//tr[th[contains(text(),'Item')]]/following-sibling::tr[{index}]/td[2]");
        private static By _recipientList(string listType) => By.XPath($"//tr[td = '{listType}']/following-sibling::tr[2]//tr");

        public IWebElement ProjectNumberInfo { get { return StableFindElement(_projectNumberInfo); } }
        public IWebElement ProjectTitleInfo { get { return StableFindElement(_projectTitleInfo); } }
        public IWebElement DateInfo { get { return StableFindElement(_dateInfo); } }
        public IWebElement SubjectInfo { get { return StableFindElement(_subjectInfo); } }
        public IWebElement TransmittalNoInfo { get { return StableFindElement(_transmittalNoInfo); } }
        public IWebElement HeaderTransmittalNoInfo { get { return StableFindElement(_headerTransmittalNoInfo); } }
        public IWebElement FromInfo { get { return StableFindElement(_fromInfo); } }
        public IWebElement AttachedDocumentName(string index) => StableFindElement(_attachedDocumentName(index));
        public IReadOnlyCollection<IWebElement> RecipientList(string listType) => StableFindElements(_recipientList(listType));
        #endregion

        #region Actions
        public TransmittalDetail(IWebDriver webDriver) : base(webDriver) { }

        public T ClickCloseButton<T> (ref List<KeyValuePair<string, bool>> methodValidations)
        {
            ClickToolbarButton<T>(KiewitTeamBinderENums.ToolbarButton.Close);
            methodValidations.Add(ValidateTransmittalDetailWindowIsClosed());
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles.Last());
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        private string[] GetRecipientList(bool ccList)
        {
            string listType;
            if (ccList)            
                 listType = "Transmitted Cc:";            
            else            
                 listType = "Transmitted To:";

            string[] recipientList = new string[RecipientList(listType).Count - 1];
            for (int i = 0; i < recipientList.Length; i++)
            {
                var recipient = RecipientList(listType).ElementAt(i + 1);
                recipientList[i] = recipient.StableFindElement(By.XPath("./td[2]")).Text +
                                   " (" + recipient.StableFindElement(By.XPath("./td[1]")).Text + ")";
            }

            return recipientList;
        }

        /// <summary>
        /// Refactor Selected users with company name to standard form: "UserName (CompanyName)"
        /// </summary>
        /// <param name="selectedUsersWithCompanyName"></param>
        /// <returns>The string with standard form: UserName (CompanyName)</returns>
        private static string RefactorSelectedUsersWithCompanyName(string selectedUsersWithCompanyName)
        {
            return selectedUsersWithCompanyName.Replace(", ", " (") + ")";
        }

        private KeyValuePair<string, bool> ValidateTransmittalDetailWindowIsClosed()
        {
            var node = StepNode();
            try
            {
                if (WebDriver.WindowHandles.Count == 1)
                    return SetPassValidation(node, Validation.Transmittal_Detail_Window_Is_Closed);
                else
                    return SetFailValidation(node, Validation.Transmittal_Detail_Window_Is_Closed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Transmittal_Detail_Window_Is_Closed, e);
            }
            
        }

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

        public KeyValuePair<string, bool> ValidateProjectNameIsCorrect(string projectTitle)
        {
            var node = StepNode();
            node.Info("Validate the project title is correct");
            try
            {
                if (ProjectTitleInfo.Text == projectTitle)
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
            node.Info("Validate that the date is current date");
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

        public KeyValuePair<string, bool> ValidateTransmittalNoIsCorrectWithTheHeader()
        {
            var node = StepNode();
            node.Info("Validate that the Transmittal number is correct with the header");
            try
            {                
                if (TransmittalNoInfo.Text == HeaderTransmittalNoInfo.Text)                
                    return SetPassValidation(node, Validation.Transmittal_No_Is_Correct_With_The_Header);
                
                return SetFailValidation(node, Validation.Transmittal_No_Is_Correct_With_The_Header);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Transmittal_No_Is_Correct_With_The_Header, e);
            }
        }

        public KeyValuePair<string, bool> ValidateFromUserInfoIsCorrect(string fromUser)
        {
            var node = StepNode();
            node.Info("Validate that the From user infomation is correct");
            try
            {
                if (RefactorSelectedUsersWithCompanyName(FromInfo.Text) == fromUser)
                    return SetPassValidation(node, Validation.From_User_Info_Is_Correct);

                return SetFailValidation(node, Validation.From_User_Info_Is_Correct);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.From_User_Info_Is_Correct, e);
            }
        }

        public KeyValuePair<string, bool> ValidateAttachedDocumentsAreDisplayed(string[] selectedDocuments)
        {
            var node = StepNode();
            node.Info("");
            try
            {
                foreach (var selectedDocument in selectedDocuments)
                {
                    bool flag = false;
                    for (int i = 0; i < selectedDocuments.Length && flag == false; i++)                    
                        if (AttachedDocumentName((i + 1).ToString()).Text.Trim() == selectedDocument.Trim())
                            flag = true;                        
                    
                    if (flag == false)                    
                        return SetFailValidation(node, Validation.Attached_Documents_Are_Displayed);                    
                    
                }
                return SetPassValidation(node, Validation.Attached_Documents_Are_Displayed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Attached_Documents_Are_Displayed, e);
            }
        }

        public KeyValuePair<string, bool> ValidateRecipentsAreDisplayed(string[] selectedUsersWithCompanyName, bool ccList = false)
        {
            var node = StepNode();
            node.Info("");
            try
            {
                foreach (var recipient in GetRecipientList(ccList))
                {
                    bool flag = false;
                    for (int i = 0; i < selectedUsersWithCompanyName.Length && flag == false; i++)
                        if (recipient == selectedUsersWithCompanyName[i])
                            flag = true;

                    if (flag == false)
                        return SetFailValidation(node, Validation.Recipents_Are_Displayed);
                     
                }
                return SetPassValidation(node, Validation.Recipents_Are_Displayed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Recipents_Are_Displayed, e);
            }
        }

        private static class Validation
        {
            public static string Project_Number_Is_Correct = "Validate that the project number is correct";
            public static string Project_Name_Is_Correct = "Validate that the project name is correct";
            public static string Date_Is_Current_Date = "Validate that the date is current date";
            public static string Transmittal_No_Is_Correct_With_The_Header = "Validate that the Transmittal number is correct with the header";
            public static string From_User_Info_Is_Correct = "Validate that the From user infomation is correct";
            public static string Attached_Documents_Are_Displayed = "Validate that the attached documents are displayed";
            public static string Recipents_Are_Displayed = "Validate that the recipents are displayed";
            public static string Transmittal_Detail_Window_Is_Closed = "Validate that the Transmittal detail window is closed";
        }
        #endregion
    }
}
