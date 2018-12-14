using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.MailModule
{
    public class Mail : ProjectsDashboard
    {
        #region Entities
        private static string _menuButton = "//li[a='{0}']";
        private static string _subjectMailXpath = "//td[text()={0}";
        private static string _mailContentFieldValueXpath = "//td[contains(.,'{0}')][@class='MAILLABEL']/following-sibling::td";
        private static By _mailGridDataTable => By.XPath("//div[contains(@id,'cntPhMain_GridViewMail_GridData')]/table//tbody");
        private static By _closeMailButton => By.XPath("//div[@id='RadToolBarCloseBottom']//li//span[text()='Close']");
        //private static By _mailContentSubjectRow => By.XPath("//tr[./td[contains(.,'Subject:')]]");
        //private static By _mailContentFromCell => By.XPath("//td[contains(.,'From:')][@class='MAILLABEL']/following-sibling::td");
        //private static By _mailContentRefNoCell => By.XPath("//td[contains(.,'Reference No:')][@class='MAILLABEL']/following-sibling::td");
        //private static By _sentLabel => By.XPath("//td[@class = 'MAILTEXT' and contains(.,'Sent:')]");
        private static By _mailDetailSubject => By.Id("lblSubject");
        private static By _mailDetailFrom => By.Id("lblFrom");
        private static By _mailFrameContent => By.Id("mailFrameContent");


        public IWebElement MailGridDataTable { get { return StableFindElement(_mailGridDataTable); } }
        public IWebElement CloseMailButton { get { return StableFindElement(_closeMailButton); } }
        //public IWebElement SubjectLabel { get { return StableFindElement(_subjectLabel); } }
        //public IWebElement FromLabel { get { return StableFindElement(_fromLabel); } }
        //public IWebElement ReferenceNoLabel { get { return StableFindElement(_referenceNoLabel); } }
        //public IWebElement SentLabel { get { return StableFindElement(_sentLabel); } }
        public IWebElement MailDetailSubject { get { return StableFindElement(_mailDetailSubject); } }
        public IWebElement MailDetailFrom { get { return StableFindElement(_mailDetailFrom); } }

        #endregion

        #region Actions
        public Mail(IWebDriver webDriver) : base(webDriver)
        {

        }
        public IWebElement FindMailByInfomation(MailInformation mailInformation)
        {
            int rowIndex, colType, colSubject, colDate, colFrom;
            WaitForElement(_mailGridDataTable);

            GetTableCellValueIndex(MailGridDataTable, mailInformation.Type, out rowIndex, out colType);
            GetTableCellValueIndex(MailGridDataTable, mailInformation.Date, out rowIndex, out colDate);
            GetTableCellValueIndex(MailGridDataTable, mailInformation.Subject, out rowIndex, out colSubject);
            GetTableCellValueIndex(MailGridDataTable, mailInformation.From, out rowIndex, out colFrom);

            for (int row = 1; row < GetTableRowNumber(MailGridDataTable); row++)
            {
                if (TableCell(MailGridDataTable, row, colType).GetAttribute("innerText").Trim() == mailInformation.Type
                    && TableCell(MailGridDataTable, row, colDate).GetAttribute("innerText").Trim() == mailInformation.Date
                    && TableCell(MailGridDataTable, row, colFrom).GetAttribute("innerText").Trim() == mailInformation.From
                    && TableCell(MailGridDataTable, row, colSubject).GetAttribute("innerText").Trim() == mailInformation.Subject)
                {
                    return TableCell(MailGridDataTable, row, colType);
                }

            }
            return null;
        }

        //public IWebElement FindMailByInfomation(MailInformation mailInformation)
        //{
        //    int rowIndex, colType, colSubject, colDate, colFrom;
        //    WaitForElement(_mailGridDataTable);
        //    string[] temp = mailInformation.Date.Split(' ');
        //    DateTime dateConvert = DateTime.ParseExact(temp[0], "MM/dd/yyyy", CultureInfo.InvariantCulture);
        //    string date = dateConvert.ToString("MM-dd-yy") + " " + temp[1] + " " + temp[2];
        //    GetTableCellValueIndex(MailGridDataTable, mailInformation.Type, out rowIndex, out colType);
        //    GetTableCellValueIndex(MailGridDataTable, date, out rowIndex, out colDate);
        //    GetTableCellValueIndex(MailGridDataTable, mailInformation.Subject, out rowIndex, out colSubject);
        //    GetTableCellValueIndex(MailGridDataTable, mailInformation.From, out rowIndex, out colFrom);

        //    for (int row = 1; row < GetTableRowNumber(MailGridDataTable); row++)
        //    {
        //        if (TableCell(MailGridDataTable, row, colType).GetAttribute("innerText").Trim() == mailInformation.Type
        //            && TableCell(MailGridDataTable, row, colDate).GetAttribute("innerText").Trim() == date
        //            && TableCell(MailGridDataTable, row, colFrom).GetAttribute("innerText").Trim() == mailInformation.From
        //            && TableCell(MailGridDataTable, row, colSubject).GetAttribute("innerText").Trim() == mailInformation.Subject)
        //        {
        //            return TableCell(MailGridDataTable, row, colType);
        //        }

        //    }
        //    return null;
        //}
        public Mail OpenMail(MailInformation mailInformation, out string parentWindow)
        {
            IWebElement MailItem = FindMailByInfomation(mailInformation);
            
            string winHandleBefore = WebDriver.CurrentWindowHandle;
            int previousWinCount = WebDriver.WindowHandles.Count;
            
            // Perform the action to open a new Window
            MailItem.DoubleClick();
            WaitUntil(driver => driver.WindowHandles.Count == (previousWinCount + 1));

            var normalPageLoadTime = WebDriver.Manage().Timeouts().PageLoad;
            WebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            foreach (string handle in WebDriver.WindowHandles)
            {
                if (handle != winHandleBefore)
                {
                    try
                    {
                        WebDriver.SwitchTo().Window(handle);
                        Console.WriteLine(WebDriver.Title);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(WebDriver.Title);
                        System.Diagnostics.Debug.WriteLine(e);
                    }
                    if (WebDriver.Title.Contains(mailInformation.Subject))
                        break;
                }

            }
            WebDriver.Manage().Timeouts().PageLoad = normalPageLoadTime;
            string currHandle = WebDriver.CurrentWindowHandle;
            parentWindow = winHandleBefore;
            return this;
        }
        public Mail CloseMailDetailView(string windowHandle)
        {
            string currHandle = WebDriver.CurrentWindowHandle;
            CloseMailButton.Click();
            WebDriver.SwitchTo().Window(windowHandle);
            return this;
            
        }

        public KeyValuePair<string, bool> ValidateItemSentToUserShow(MailInformation mailInformation)
        {
            var node = StepNode();
            try
            {
                if (FindMailByInfomation(mailInformation) != null)
                    return SetPassValidation(node, Validation.Validate_Item_Sent_To_User_Are_Show);
                else
                    return SetFailValidation(node, Validation.Validate_Item_Sent_To_User_Are_Show);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Validate_Item_Sent_To_User_Are_Show, e);
            }
        }

        public List<KeyValuePair<string, bool>> ValidateMailDetailShow(MailInformation mailInformation)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            node.Info("Mail Detail View URL: " + WebDriver.Url);
            IWebElement MailInfoField;
            string[] date = mailInformation.Date.Split(' ');
            try
            {
                validation.Add((MailDetailSubject.Text.Equals(mailInformation.Subject))
                         ? SetPassValidation(node, Validation.Validate_Mail_Info_Shows_Correct_In_Header_Section_Of_Mail_Detail_View + "Subject:")
                         : SetFailValidation(node, Validation.Validate_Mail_Info_Shows_Correct_In_Header_Section_Of_Mail_Detail_View + "Subject:", mailInformation.Subject, MailDetailSubject.Text));

                validation.Add((MailDetailFrom.Text.Contains(mailInformation.From))
                         ? SetPassValidation(node, Validation.Validate_Mail_Info_Shows_Correct_In_Header_Section_Of_Mail_Detail_View + "From:")
                         : SetFailValidation(node, Validation.Validate_Mail_Info_Shows_Correct_In_Header_Section_Of_Mail_Detail_View + "From:", mailInformation.From, MailDetailFrom.Text));

                WebDriver.SwitchToIFrameBySelector(_mailFrameContent);

                MailInfoField = StableFindElement(By.XPath(string.Format(_mailContentFieldValueXpath, "Subject:")));
                validation.Add((MailInfoField.Text.Equals(mailInformation.Subject))
                         ? SetPassValidation(node, Validation.Validate_Mail_Info_Shows_Correct_In_Body_Section_Of_Mail_Detail_View + "Subject:")
                         : SetFailValidation(node, Validation.Validate_Mail_Info_Shows_Correct_In_Body_Section_Of_Mail_Detail_View + "Subject:", mailInformation.Subject, MailInfoField.Text));

                MailInfoField = StableFindElement(By.XPath(string.Format(_mailContentFieldValueXpath, "From:")));
                validation.Add((MailInfoField.Text.Contains(mailInformation.From))
                         ? SetPassValidation(node, Validation.Validate_Mail_Info_Shows_Correct_In_Body_Section_Of_Mail_Detail_View + "From:")
                         : SetFailValidation(node, Validation.Validate_Mail_Info_Shows_Correct_In_Body_Section_Of_Mail_Detail_View + "From:", mailInformation.From, MailInfoField.Text));

                WebDriver.SwitchOutOfIFrame();
                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Validate_Mail_Detail_Page_Are_Show, e));
                return validation;
            }
        }
        #endregion

        private static class Validation
        {
            public static string Validate_Item_Sent_To_User_Are_Show = "Validate that the items sent to user are shown in the inbox.";
            public static string Validate_Mail_Detail_Page_Are_Show = "Validate that the mail detail page is shown.";
            public static string Validate_Mail_Info_Shows_Correct_In_Header_Section_Of_Mail_Detail_View = "Validate that header section of Mail detail page shows correct mail ";
            public static string Validate_Mail_Info_Shows_Correct_In_Body_Section_Of_Mail_Detail_View = "Validate that body section of Mail detail page shows correct mail ";
        }
    }
}
