using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Windows.Forms;
using KiewitTeamBinder.UI.Pages.Global;
using AventStack.ExtentReports;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using SeleniumExtras.WaitHelpers;

namespace KiewitTeamBinder.UI.Pages.Dialogs
{
    public class SelectRecipientsDialog : LoggedInLanding
    {
        #region Entities
        public string IFrameName = "RadWindowSelection";
        private static By _dropdownFilterButon => By.XPath("//a[contains(@id, 'ComboBoxCompany')]");
        private static By _companyItem(string companyName) => By.XPath($"//li[text() ='{companyName}']");
        private static By _userNameInLeftTable(string userName) => By.XPath($"//tr[contains(@id,'RecipientSelect_RadGridViewContacts')][td='{userName}']");
        private static By _userNameInToTable(string selectedUser) => By.XPath($"//div[@id = 'divToGrid']//tr[td='{selectedUser}']");
        private static By _userNameInTable(string toTable, string selectedUser, string companyName) => By.XPath($"//div[contains(@id,'{toTable}Grid')]//tr[td='{selectedUser} ({companyName})']");
        private static By _okButton => By.Id("BtnOk");
        private static By _cancelButton => By.Id("BtnCancel");
        private static By _addUserToButton => By.Id("AddToBtn");
        private static By _addUserCCButton => By.Id("AddCcBtn");
        private static By _selectRecipientsWindow => By.XPath("//div[contains(@id,'_RadWindowSelection')]");
        private static By _userNameCheckBoxInLeftTable(string userName) => By.XPath($"//tr[contains(@id,'RecipientSelect_RadGridViewContacts')][td='{userName}']/td/input[contains(@id, 'CheckBox')]");

        public IWebElement DropdownFilterButon { get { return StableFindElement(_dropdownFilterButon); } }
        public IWebElement CompanyItem(string companyName) => StableFindElement(_companyItem(companyName));
        public IWebElement UserNameInLeftTable(string userName) => StableFindElement(_userNameInLeftTable(userName));
        public IWebElement UserNameInToTable(string selectedUser) => StableFindElement(_userNameInToTable(selectedUser));
        public IWebElement UserNameInTable(string toTable, string selectedUser, string companyName) => StableFindElement(_userNameInTable(toTable, selectedUser, companyName));
        public IWebElement OkButton { get { return StableFindElement(_okButton); } }
        public IWebElement CancelButton { get { return StableFindElement(_cancelButton); } }
        public IWebElement AddUserToButton { get { return StableFindElement(_addUserToButton); } }
        public IWebElement AddUserCCButton { get { return StableFindElement(_addUserCCButton); } }
        public IWebElement UserNameCheckBoxInLeftTable(string userName) => StableFindElement(_userNameCheckBoxInLeftTable(userName));
        #endregion

        #region Actions
        public SelectRecipientsDialog(IWebDriver webDriver) : base(webDriver)
        {
            
        }

        public SelectRecipientsDialog SwitchToFrame()
        {
            WebDriver.SwitchTo().Frame(IFrameName);
            WaitUntil(driver => DropdownFilterButon != null);
            return this;
        }


        public SelectRecipientsDialog SelectCompany(string companyName)
        {
            var node = StepNode();
            node.Info("Click Dropdown filter button");
            DropdownFilterButon.Click();
            node.Info("Select " + companyName + " from dropdown list");
            CompanyItem(companyName).Click();
            return this;
        }

        public SelectRecipientsDialog ClickUserInLeftTable(string userName)
        {
            UserNameInLeftTable(userName).Click();
            return this;
        }

        public SelectRecipientsDialog AddUserToTheTable(string toTable, List<string> listUser)
        {
            foreach (var item in listUser)
                UserNameCheckBoxInLeftTable(item).Click();
            if (toTable == "To")
                AddUserToButton.Click();
            else
                AddUserCCButton.Click();
            return this;
        }

        public T ClickOkButton<T>()
        {
            OkButton.Click();
            WebDriver.SwitchTo().DefaultContent();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T ClickCancelButton<T>()
        {
            CancelButton.Click();
            WebDriver.SwitchTo().DefaultContent();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public KeyValuePair<string, bool> ValidateUserIsAddedToTheToTable(string[] selectedUsers)
        {
            var node = StepNode();
            try
            {
                for (int i = 0; i < selectedUsers.Length; i++)
                {
                    if (UserNameInToTable(selectedUsers[i]) == null)
                        return SetFailValidation(node, Validation.User_Is_Added_To_The_To_Table);
                }
                return SetPassValidation(node, Validation.User_Is_Added_To_The_To_Table);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.User_Is_Added_To_The_To_Table, e);
            }
        }

        public List<KeyValuePair<string, bool>> ValidateUserIsAddedToTheTable(string toTable, string companyName, List<string> selectedUsers)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                foreach (var item in selectedUsers)
                {
                    if (UserNameInTable(toTable, item, companyName) != null)
                        validation.Add(SetPassValidation(node, string.Format(Validation.User_Is_Added_To_The_Table, item, toTable)));
                    else
                        validation.Add(SetFailValidation(node, string.Format(Validation.User_Is_Added_To_The_Table, item, toTable)));
                }
                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, string.Format(Validation.User_Is_Added_To_The_Table, toTable), e));
                return validation;
            }
        }
        public List<KeyValuePair<string, bool>> ValidateUserIsHighlightedInTheTable(string toTable, string nameCompny, List<string> selectedUsers)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                foreach (var item in selectedUsers)
                {
                    UserNameInTable(toTable, item, nameCompny).HoverElement();
                    if (UserNameInTable(toTable, item, nameCompny).GetAttribute("class").Contains("HoveredRow"))
                        validation.Add(SetPassValidation(node, string.Format(Validation.User_Is_Highlighted_In_The_Table, item)));
                    else
                        validation.Add(SetFailValidation(node, string.Format(Validation.User_Is_Highlighted_In_The_Table, item), "HoveredRow", UserNameInTable(toTable, item, nameCompny).GetAttribute("class")));
                }
                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.User_Is_Highlighted_In_The_Table, e));
                return validation;
            }
        }

        public List<KeyValuePair<string, bool>> ValidateUserIsRetainedCheckMarkInTheTable(string toTable, string nameCompny, List<string> selectedUsers)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                foreach (var item in selectedUsers)
                {
                    string _userRowXpath = _userNameInTable(toTable, item, nameCompny).ToString().Replace("By.XPath:", "").Trim();
                    string _checkMarkXpath = _userRowXpath.Insert(_userRowXpath.Length, "//input[@type='image']");
                    if (StableFindElement(By.XPath(_checkMarkXpath)).GetAttribute("src").Contains("tbDelete.gif"))
                        validation.Add(SetPassValidation(node, string.Format(Validation.User_Is_Retained_Check_Mark_In_The_Table, item)));
                    else
                        validation.Add(SetFailValidation(node, string.Format(Validation.User_Is_Retained_Check_Mark_In_The_Table, item)));
                }
                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.User_Is_Retained_Check_Mark_In_The_Table, e));
                return validation;
            }
        }

        public KeyValuePair<string, bool> ValidateSelectRecipientWindowStatus(bool closed = false)
        {
            var node = StepNode();
            try
            {
                if (closed == true)
                {
                    if (StableFindElement(_selectRecipientsWindow) == null)
                        return SetPassValidation(node, Validation.Select_Recipient_Window_Closed);
                    return SetFailValidation(node, Validation.Select_Recipient_Window_Closed);
                }
                else
                {
                    if (StableFindElement(_selectRecipientsWindow) != null)
                        return SetPassValidation(node, Validation.Select_Recipient_Window_Opened);
                    return SetFailValidation(node, Validation.Select_Recipient_Window_Opened);
                }
            }
            catch (Exception e)
            {
                if (closed == true)
                    return SetErrorValidation(node, Validation.Select_Recipient_Window_Closed, e);
                else
                    return SetErrorValidation(node, Validation.Select_Recipient_Window_Opened, e);
            }
        }

        public List<KeyValuePair<string, bool>> ValidateListItemUserInLeftGrid(List<string> listUser)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                foreach (var item in listUser)
                {
                    if (UserNameInLeftTable(item) != null)
                        validation.Add(SetPassValidation(node, Validation.Item_User_In_Left_GridView));
                    else
                        validation.Add(SetFailValidation(node, Validation.Item_User_In_Left_GridView));
                }
                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Item_User_In_Left_GridView, e));
                return validation;
            }
        }

        private static class Validation
        {
            public static string User_Is_Added_To_The_To_Table = "Validate that user is added to the To table";
            public static string Select_Recipient_Window_Opened = "Validate that select recipient window is opened";
            public static string Select_Recipient_Window_Closed = "Validate that select recipient window is closed";
            public static string Item_User_In_Left_GridView = "Validate that item user is displayed in left gridview.";
            public static string User_Is_Added_To_The_Table = "Validate that user: {0} is added to the {1} table.";
            public static string User_Is_Highlighted_In_The_Table = "Validate that user: {0} is highlighted in the table after added.";
            public static string User_Is_Retained_Check_Mark_In_The_Table = "Validate that user: {0} is retained check mark in the table after added.";
        }
        #endregion
    }
}

