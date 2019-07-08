using Breeze.UI;
using Breeze.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Pages.Foody
{
    public class LoginPage : BasePage
    {
        #region Locators

        static readonly By _txtUsername = By.XPath("//input[@id='Email']");
        static readonly By _txtPassword = By.XPath("//input[@id='Password']");
        static readonly By _cbRememberMe = By.XPath("//input[@id ='RememberMe']/following-sibling::label");
        static readonly By _btnLogin = By.XPath("//input[@id='bt_submit']");

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

        public IWebElement CbRememberMe
        {
            get { return StableFindElement(_cbRememberMe); }
        }

        public IWebElement BtnLogin
        {
            get { return StableFindElement(_btnLogin); }
        }

        #endregion

        #region Methods

        public LoginPage() : base()
        {
        }

        public HomePage Login(string username, string password, bool isRememberMe = false)
        {
            try
            {
                TxtUsername.SendKeys(username);
                TxtPassword.SendKeys(password);
                CbRememberMe.Click();
                BtnLogin.Click();
            }
            catch (Exception e)
            {

            }
            return new HomePage();
        }

        #endregion
    }
}
