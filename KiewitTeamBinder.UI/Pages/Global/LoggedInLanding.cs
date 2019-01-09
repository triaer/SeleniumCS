using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.Common.Helper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;

namespace KiewitTeamBinder.UI.Pages.Global
{
    public class LoggedInLanding : PageBase
    {

        #region Entities

        public static By _walkMe => By.XPath("//div[@id='walkme-player']//div[contains(@class,'walkme-in')]");
        private static By _userNameLable => By.XPath("//div[@id='divUserName']/span");
        private static By _logoutLink => By.Id("LogoutLabel");
        private static By _logoutYesButton => By.XPath("//div[@class='rwDialogPopup radconfirm']//a[.//span[text()='Yes']]");
        private static By _alertPopup => By.Id("kendoAlertWindow");
        private static By _saveChangeYesButton => By.Id("Yes");
        private static By _saveChangeNoButton => By.Id("No");
        private static By _saveItemPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]");


        public IWebElement LogoutLink { get { return StableFindElement(_logoutLink); } }
        public IWebElement LogoutYesButton { get { return StableFindElement(_logoutYesButton); } }
        public IWebElement SaveChangeYesButton { get { return StableFindElement(_saveChangeYesButton); } }
        public IWebElement SaveChangeNoButton { get { return StableFindElement(_saveChangeNoButton); } }
        public IWebElement WalkMe { get { return StableFindElement(_walkMe); } }        
        #endregion


        #region Actions

        public LoggedInLanding(IWebDriver webDriver) : base(webDriver)
        {
        }

        public NonSsoSignOn Logout()
        {
            LogoutLink.Click();
            WebDriver.SwitchTo().ActiveElement();
            LogoutYesButton.Click();
            return new NonSsoSignOn(WebDriver);
        }
                
        public T ReloadPage<T>()
        {
            Reload();
            WaitForElement(_userNameLable);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public LoggedInLanding SwitchToWindow(string window, bool closePreviousWindow = false)
        {
            if (closePreviousWindow == true)
            {
                Browser.Close();
            }
            WebDriver.SwitchTo().Window(window);
            return this;
        }

        public KeyValuePair<string, bool> ValidateSaveDialogStatus(bool closed = false)
        {
            var node = StepNode();

            try
            {
                if (closed == true)
                {
                    if (FindElement(_saveItemPopUp) != null)
                        return SetFailValidation(node, Validation.Save_PopUp_Closed);

                    return SetPassValidation(node, Validation.Save_PopUp_Closed);
                }

                if (StableFindElement(_saveItemPopUp) != null)
                    return SetPassValidation(node, Validation.Save_PopUp_Opened);

                return SetFailValidation(node, Validation.Save_PopUp_Opened);
            }
            catch (Exception e)
            {
                if (closed == true)
                    return SetErrorValidation(node, Validation.Save_PopUp_Closed, e);

                return SetErrorValidation(node, Validation.Save_PopUp_Opened, e);
            }
        }

        public string GetUrl()
        {
            return WebDriver.Url;
        }

        public LoggedInLanding LogValidation(ref List<KeyValuePair<string, bool>> validations, List<KeyValuePair<string, bool>> addedValidations)
        {
            Utils.AddCollectionToCollection(validations, addedValidations);
            return this;
        }

        public T LogValidation<T>(ref List<KeyValuePair<string, bool>> validations, List<KeyValuePair<string, bool>> addedValidations)
        {
            LogValidation(ref validations, addedValidations);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public LoggedInLanding LogValidation(ref List<KeyValuePair<string, bool>> validations, KeyValuePair<string, bool> addedValidation)
        {
            validations.Add(addedValidation);
            return this;
        }

        public T LogValidation<T>(ref List<KeyValuePair<string, bool>> validations, KeyValuePair<string, bool> addedValidation)
        {
            validations.Add(addedValidation);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        #endregion
        private static class Validation
        {
            public static string Save_PopUp_Closed = "Validate that the save popup is closed";
            public static string Save_PopUp_Opened = "Validate that the save popup is opened";
            
        }
    }
}
