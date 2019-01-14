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
using System.Globalization;


namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    /// <summary>
    /// This page displays after click Send button on New Transmital page
    /// </summary>
    public class TransmittalDetail : PopupWindow
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
        private static By _transmittalNoHeaderLabel(string inforHeader) => By.XPath($"//td[contains(text(),'{inforHeader}') and @valign='top']");
        private static By _detailInfoLabel(string infor) => By.XPath($"//td[contains(text(),'{infor}')]");
        private static By _dateLabel => By.XPath("//td[contains(text(),'Date:')]/following::td[1]");
        private static By _linkText(string text) => By.XPath($"//a[text() = '{text}']");
        private static By _tabMenu(string nameMenu) => By.XPath($"//li//span[text()='{nameMenu}']");
        private static By _gridViewDocuments = By.Id("GridViewDocuments_ctl00");
        private static By _closeButtonInBottomPage = By.XPath("//div[@id='divTransmittalDetailFooter']//span[text()='Close']");
        private static By _downloadHyperlink => By.XPath("//a[text() = 'Click here to download all Transmittal files.']");
        private static By _documentHyperlink(string documentNo) => By.XPath($"//a[text() = '{documentNo}']");
        public IWebElement CloseButtonInBottomPage { get { return StableFindElement(_closeButtonInBottomPage); } }
        public IWebElement TabMenu(string nameMenu) => StableFindElement(_tabMenu(nameMenu));
        public IWebElement LinkText(string text) => StableFindElement(_linkText(text));
        public IWebElement DetailInfoLabel(string infor) => StableFindElement(_detailInfoLabel(infor));
        public IWebElement TransmittalNoHeaderLabel(string infor) => StableFindElement(_transmittalNoHeaderLabel(infor));
        public IWebElement ProjectNumberInfo { get { return StableFindElement(_projectNumberInfo); } }
        public IWebElement ProjectTitleInfo { get { return StableFindElement(_projectTitleInfo); } }
        public IWebElement DateInfo { get { return StableFindElement(_dateInfo); } }
        public IWebElement SubjectInfo { get { return StableFindElement(_subjectInfo); } }
        public IWebElement TransmittalNoInfo { get { return StableFindElement(_transmittalNoInfo); } }
        public IWebElement HeaderTransmittalNoInfo { get { return StableFindElement(_headerTransmittalNoInfo); } }
        public IWebElement FromInfo { get { return StableFindElement(_fromInfo); } }
        public IWebElement AttachedDocumentName(string index) => StableFindElement(_attachedDocumentName(index));
        public IReadOnlyCollection<IWebElement> RecipientList(string listType) => StableFindElements(_recipientList(listType));
        public IWebElement DownloadHyperlink { get { return StableFindElement(_downloadHyperlink); } }
        public IWebElement DocumentHyperlink(string documentNo) => StableFindElement(_documentHyperlink(documentNo));
        #endregion

        #region Actions
        public TransmittalDetail(IWebDriver webDriver) : base(webDriver) { }

        public T ClickCloseInBottomPage<T>()
        {
            CloseButtonInBottomPage.Click();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }
        public T ClickCloseButton<T> (ref List<KeyValuePair<string, bool>> methodValidations)
        {
            ClickToolbarButton<T>(KiewitTeamBinderENums.ToolbarButton.Close);
            methodValidations.Add(ValidateTransmittalDetailWindowIsClosed());
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles.Last());
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public TransmittalDetail ClickTabMenu(string tabName)
        {
            TabMenu(tabName).Click();
            WaitForElementDisplay(_gridViewDocuments);
            return this;
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

        public string GetTransmittalNo()
        {
            return HeaderTransmittalNoInfo.Text;
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

        public List<KeyValuePair<string, bool>> ValidateTransmittalDetailDiplayCorrect(TransmittalMailInformation transmittalMailInfor)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();

            string date = transmittalMailInfor.TransmittalDate;
            DateTime dateConvert = DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            date = dateConvert.ToString("dd-MMMM-yyyy").Replace('-', ' ');
            node.Info("Transmittal Date: " + date);
            List<string> listTransmittalInfor = new List<string>();
            listTransmittalInfor.Add(transmittalMailInfor.FromUser);
            listTransmittalInfor.Add(transmittalMailInfor.ProjectNumber);
            listTransmittalInfor.Add(transmittalMailInfor.ProjectTitle);
            try
            {
                if (TransmittalNoHeaderLabel(transmittalMailInfor.TransmittalNo) != null)
                    validation.Add(SetPassValidation(node, Validation.Information_Transmittal_Detail_Display_Correct + transmittalMailInfor.TransmittalNo));
                else
                    validation.Add(SetFailValidation(node, Validation.Information_Transmittal_Detail_Display_Correct + transmittalMailInfor.TransmittalNo));

                string[] actualDate = StableFindElement(_dateLabel).Text.Trim().Split(',');
                node.Info("Actual Date: " + actualDate[0]);
                if (actualDate[0].Equals(date))
                    validation.Add(SetPassValidation(node, Validation.Information_Transmittal_Detail_Display_Correct + date));
                else
                    validation.Add(SetFailValidation(node, Validation.Information_Transmittal_Detail_Display_Correct + date));

                foreach (var item in listTransmittalInfor)
                {
                    if (DetailInfoLabel(item) != null)
                        validation.Add(SetPassValidation(node, Validation.Information_Transmittal_Detail_Display_Correct + item));
                    else
                        validation.Add(SetFailValidation(node, Validation.Information_Transmittal_Detail_Display_Correct + item));
                }

                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Information_Transmittal_Detail_Display_Correct, e));
                return validation;
            }
        }

        public List<KeyValuePair<string, bool>> ValidateTransmittalDetailCotainsHyperLink(TransmittalMailInformation transmittalMailInfor)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            string hyperLink = "Click here to download all Transmittal files.";
            string[] listItemLinkText = { transmittalMailInfor.DocumentNo, hyperLink };
            try
            {
                for (int i = 0; i < listItemLinkText.Length; i++)
                {
                    if (LinkText(listItemLinkText[i]).GetAttribute("href").Contains("https://kiewittest.teambinder.com/"))
                        validation.Add(SetPassValidation(node, Validation.HyperLink_Is_Contained_In_Transmittal_Detail + listItemLinkText[i]));
                    else
                        validation.Add(SetFailValidation(node, Validation.HyperLink_Is_Contained_In_Transmittal_Detail + listItemLinkText[i]));
                }

                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.HyperLink_Is_Contained_In_Transmittal_Detail, e));
                return validation;
            }
        }

        public KeyValuePair<string, bool> ValidateTransmittalDetailWindowIsClosed()
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
                DateTime currentDate = DateTime.Now;
                DateTime documentDate = DateTime.Parse(DateInfo.Text.Split('-')[0]);
                TimeZone localZone = TimeZone.CurrentTimeZone;
                TimeSpan currentOffset = localZone.GetUtcOffset(documentDate);
                documentDate = documentDate.AddHours(currentOffset.TotalHours + 6);

                node.Info("Document date: " + documentDate);
                node.Info("Current date: " + currentDate);
                node.Info("Time zone: " + currentOffset.TotalHours);
                if (!(documentDate.Year == currentDate.Year &&
                    documentDate.Month == currentDate.Month &&
                    documentDate.Day == currentDate.Day &&
                    documentDate.Hour == currentDate.Hour &&
                    documentDate.Minute == currentDate.Minute))
                {
                    if (DateTime.Compare(documentDate, currentDate) < 0)
                    {
                        if (currentDate.Subtract(documentDate) > new TimeSpan(5))
                            return SetFailValidation(node, Validation.Date_Is_Current_Date, currentDate.ToString(), documentDate.ToString());
                    }
                    else //currentDate < documentDate
                    {
                        if (documentDate.Subtract(currentDate) > new TimeSpan(5))
                            return SetFailValidation(node, Validation.Date_Is_Current_Date, currentDate.ToString(), documentDate.ToString());
                    }
                    return SetPassValidation(node, Validation.Date_Is_Current_Date);
                }
                else
                {
                    return SetPassValidation(node, Validation.Date_Is_Current_Date);
                }
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

        public List<KeyValuePair<string, bool>> ValidateRecipentsAreDisplayed(string[] selectedUsersWithCompanyName, bool ccList = false, string companyName = "")
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            node.Info("Validate recipents are displayed");
            try
            {
                if (companyName != "")
                    for (int i = 0; i < selectedUsersWithCompanyName.Length; i++)
                        selectedUsersWithCompanyName[i] = selectedUsersWithCompanyName[i] + " (" + companyName + ")";

                foreach (var recipient in GetRecipientList(ccList))
                {
                    bool flag = false;
                    for (int i = 0; i < selectedUsersWithCompanyName.Length && flag == false; i++)
                    {
                        string b = selectedUsersWithCompanyName[i];
                        if (recipient == selectedUsersWithCompanyName[i])
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                        validation.Add(SetFailValidation(node, Validation.Recipents_Are_Displayed));
                    else
                        validation.Add(SetPassValidation(node, Validation.Recipents_Are_Displayed));
                }
                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Recipents_Are_Displayed, e));
                return validation;
            }
        }

        public KeyValuePair<string, bool> ValidateDocumentNumbersContainHyperlink(string[] documentNumbers)
        {
            var node = StepNode();
            node.Info("");
            try
            {
                foreach (var documentNumber in documentNumbers)
                {
                    string hrefAttribute = DocumentHyperlink(documentNumber).GetAttribute("href");
                    if (hrefAttribute == null || hrefAttribute == "")
                        return SetFailValidation(node, Validation.Document_Numbers_Contain_Hyperlink);
                }
                return SetPassValidation(node, Validation.Document_Numbers_Contain_Hyperlink);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Document_Numbers_Contain_Hyperlink, e);
            }
        }

        public KeyValuePair<string, bool> ValidateDownloadHyperlinkDisplays()
        {
            var node = StepNode();
            node.Info("");
            try
            {               
                string hrefAttribute = DownloadHyperlink.GetAttribute("href");
                if (hrefAttribute == null || hrefAttribute == "")
                    return SetFailValidation(node, Validation.Download_Hyperlink_Displays);                
                return SetPassValidation(node, Validation.Download_Hyperlink_Displays);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Download_Hyperlink_Displays, e);
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
            public static string HyperLink_Is_Contained_In_Transmittal_Detail = "Validate that the hyperlink is contained in trasmittal detail page: ";
            public static string Information_Transmittal_Detail_Display_Correct = "Validate that the information transmittal detail display correctly: ";
            public static string Document_Numbers_Contain_Hyperlink = "Validate that the document numbers contain hyperlink";
            public static string Download_Hyperlink_Displays = "Validate that the download hyperlink displays";
        }
        #endregion
    }
}
