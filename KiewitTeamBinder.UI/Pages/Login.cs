using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Pages
{
    public class Login : MainPage
    {

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

        public Login(IWebDriver webDriver) : base(webDriver)
        {
        }

        /// <summary>
        /// Login to TA Dashboard page
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="repositoryName">Name of the repository.</param>
        /// <Author>Long and Phat</Author>
        /// <returns></returns>
        public MainPage SignOn(string username, string password, bool isWaitForAlert = false, string repositoryName = null)
        {
            if (repositoryName != null)
            {
                CmbRepo.SelectItem(repositoryName);
            }
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
            BtnLogin.Click();
            if (isWaitForAlert)
            {
                WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            }
            return new MainPage(WebDriver);
        }

        #endregion
    }
}
