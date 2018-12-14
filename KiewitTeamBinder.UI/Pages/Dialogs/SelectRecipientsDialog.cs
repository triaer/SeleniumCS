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
        private static By _okButton => By.Id("BtnOk");
        private static By _cancelButton => By.Id("BtnCancel");

        public IWebElement DropdownFilterButon { get { return StableFindElement(_dropdownFilterButon); } }
        public IWebElement CompanyItem(string companyName) => StableFindElement(_companyItem(companyName));
        public IWebElement UserNameInLeftTable(string userName) => StableFindElement(_userNameInLeftTable(userName));
        public IWebElement UserNameInToTable(string selectedUser) => StableFindElement(_userNameInToTable(selectedUser));
        public IWebElement OkButton { get { return StableFindElement(_okButton); } }
        public IWebElement CancelButton { get { return StableFindElement(_cancelButton); } }
        #endregion

        #region Actions
        public SelectRecipientsDialog(IWebDriver webDriver) : base(webDriver)
        {
            webDriver.SwitchTo().ActiveElement();
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

        private static class Validation
        {
            public static string User_Is_Added_To_The_To_Table = "Validate that user is added to the To table";
        }
        #endregion
    }
}

        