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
        static readonly By _cbRememberMe = By.XPath("//input[@id='RememberMe']");
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

        public IWebElement CbRememberMe => StableFindElement(_cbRememberMe);

        public IWebElement BtnLogin
        {
            get { return StableFindElement(_btnLogin); }
        }

        #endregion

        #region Methods

        public LoginPage() : base()
        {
        }

        public object Login(String username, String password, Boolean isRememberMe = false)
        {
            try
            {
                TxtUsername.SendKeys(username);
                TxtPassword.SendKeys(password);
                //CbRememberMe.Click();

                BtnLogin.Click();
                WaitForElementDisappear(_btnLogin);
                if (BtnLogin.IsDisplayed()) return this;
            }
            catch (Exception e)
            {

            }
            return new HomePage();
        }

        #endregion
    }
}
