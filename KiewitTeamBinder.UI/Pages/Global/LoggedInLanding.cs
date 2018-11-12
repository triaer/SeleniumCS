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

namespace KiewitTeamBinder.UI.Pages
{
    public class LoggedInLanding : PageBase
    {

        #region Entities

        public static string _settingXpath => "//span[@class='icon icon-settings platform-icon-menu']";
        public static string _menuItemXpath => "//ul[@id ='platform-main-menu-dropdown']//span[@class ='k-link' and text()='{0}']";
        public static string _breadCrumbPathXpath => "//div[@class = 'float-left breadcrumb-container']/div";
        public static string _breadcrumbPathLastItemXath => _breadCrumbPathXpath + "//app-breadcrumb-item[@id = 'lastItem']/div";
        public static By _closeReleaseNotesButton => By.XPath("//div[@class = 'wm-close-button walkme-x-button']");
        private static By _settingLoading => By.XPath("//*[@id='platform-settings-menu-dropdown_mn_active']/span[contains(@class,'k-link k-sate-active')]");
        private static By _userIcon => By.CssSelector("span.icon.icon-user-alerts.platform-icon-menu");
        private static By _signOutButton => By.XPath("//*[@value='Sign out']");
        private static By _menuItem => By.XPath("//div[@id='platform-navbar']//i[@class = 'core-align-middle icon icon-burger']");
        private static By _settingIcon => By.XPath("//span[contains(@class,'icon-settings platform-icon-menu')]");
        private static By _alertPopup => By.Id("kendoAlertWindow");
        private static By _saveChangeYesButton => By.Id("Yes");
        private static By _saveChangeNoButton => By.Id("No");
        private static By _loggedInUserLabel => By.XPath("//span[@ng-if = 'enableAppInSightsOperationId && operationId']/following-sibling::span");
        private static By _announcementPopup => By.XPath("//div[contains(@id,'wm-shoutout')]");
        private static By _closeAnnouncement => By.XPath("//div[contains(@class,'wm-close-button')]");

        public IWebElement UserIcon { get { return StableFindElement(_userIcon); } }
        public IWebElement SignOutButton { get { return StableFindElement(_signOutButton); } }
        public IWebElement MenuItem { get { return StableFindElement(_menuItem); } }
        public IWebElement SettingIcon { get { return StableFindElement(_settingIcon); } }
        public IWebElement SaveChangeYesButton { get { return StableFindElement(_saveChangeYesButton); } }
        public IWebElement SaveChangeNoButton { get { return StableFindElement(_saveChangeNoButton); } }
        public IWebElement LoggedInUserLabel { get { return StableFindElement(_loggedInUserLabel); } }
        public IWebElement CloseAnnouncement { get { return StableFindElement(_closeAnnouncement); } }
        public IWebElement AnnouncementPopup { get { return StableFindElement(_announcementPopup); } }
        #endregion


        #region Actions

        public LoggedInLanding(IWebDriver webDriver) : base(webDriver)
        {

        }

        public SsoSignOn Logoff()
        {
            UserIcon.Click();
            SignOutButton.Click();
            Wait(shortTimeout);
            return new SsoSignOn(WebDriver);

        }

        public LoggedInLanding SelectMenuItem(string menuPath, char separator = '/', bool saveChange = false)
        {
            var node = StepNode();
            node.Info("Select Menu Item: " + menuPath);

            MenuItem.Click();
            string[] item = menuPath.Split(separator);

            for (int i = 0; i < item.Length; i++)
            {

                string menuItemXpath = string.Format(_menuItemXpath, item[i]);
                IWebElement MenuItem = StableFindElement(By.XPath(menuItemXpath));

                if (i > 0 && i < item.Length - 1)
                {
                    ScrollToElement(MenuItem);
                }
                else
                {
                    MenuItem.Click();
                    if (FindElement(_alertPopup) != null)
                        HandleSaveChangesPopup(saveChange);
                }
            }
            WaitForElement(_userIcon, shortTimeout);
            if (FindElement(_closeReleaseNotesButton) != null)
                ClickElement(_closeReleaseNotesButton);
            return this;
        }

        public T SelectMenuItem<T>(string menuPath, char separator = '/', bool saveChange = false)
        {
            SelectMenuItem(menuPath, separator, saveChange);
            WaitForAngularJSLoad();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        /// <summary>
        /// Go to Setting
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// Save change when pop up appears
        /// </summary>
        public void HandleSaveChangesPopup(bool saveChange = false)
        {
            if (FindElement(_alertPopup) != null)
            {
                if (saveChange == true)
                    SaveChangeYesButton.Click();
                else
                    SaveChangeNoButton.Click();
            }
        }

        public bool HandleAnnouncementPopup(out string contentsPopup, bool closePopup = true)
        {
            bool isPopupDisplayed = false;
            contentsPopup = "";
            if (FindElement(_announcementPopup) != null)
            {
                isPopupDisplayed = true;
                contentsPopup = AnnouncementPopup.Text;
                if (closePopup)
                    CloseAnnouncement.Click();
            }
            return isPopupDisplayed;
        }



        public T ReloadPage<T>()
        {
            Reload();
            WaitForElement(_userIcon);
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
    }
}
