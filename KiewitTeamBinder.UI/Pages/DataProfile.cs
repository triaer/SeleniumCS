using Agoda.UI.Pages.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Agoda.UI.ExtentReportsHelper;

namespace Agoda.UI.Pages
{
    public class DataProfile : PageBase
    {
        private IWebDriver _driver;

        #region Locators

        static readonly By _txtUsername = By.XPath("//input[@id='username']");
        static readonly By _txtPassword = By.XPath("//input[@id='password']");
        static readonly By _btnLogin = By.XPath("//div[@class='btn-login']");
        static readonly By _cmbRepo = By.XPath("//select[@id='repository']");

        #endregion
        #region Elements
        public IWebElement TxtUsername
        {
            get { return StableFindElement(_txtUsername); }
        }

        public IWebElement TxtPassword
        {
            get { return StableFindElement(_txtPassword); }
        }

        public IWebElement BtnLogin
        {
            get { return StableFindElement(_btnLogin); }
        }

        public IWebElement CmbRepo
        {
            get { return StableFindElement(_cmbRepo); }
        }

        #endregion

        #region Methods

        public DataProfile(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
        }

        /// <summary>
        /// Login to TA Dashboard page
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="repositoryName">Name of the repository.</param>
        /// <Author>Long and Phat</Author>
        /// <returns></returns>
        public MainPage SignOn(string username, string password, string repositoryName = null)
        {
            if (repositoryName != null)
            {
                CmbRepo.SelectItem(repositoryName);
            }
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
            return new MainPage(WebDriver);
        }

        public Login SignOnFailed(string username, string password, string repositoryName = null)
        {
            if (repositoryName != null)
            {
                CmbRepo.SelectItem(repositoryName);
            }
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
            Browser.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            return new Login(WebDriver);
        }

        public KeyValuePair<string, bool> ValidateDashboardDashboardErrorMessageAppeared()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                String foundErrorMessage = _driver.SwitchTo().Alert().Text;
                
                if (foundErrorMessage.Equals("Username or password is invalid"))
                    validation = SetPassValidation(node, ValidationMessage.ValidateDashboardErrorMessage);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateDashboardErrorMessage);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateDashboardErrorMessage, e);
            }

            EndStepNode(node);
            return validation;
        }

        private static class ValidationMessage
        {
            public static string ValidateDashboardErrorMessage = "Validate That Error Message Is Correct";
        }

        #endregion
    }
}
