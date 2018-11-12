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

namespace KiewitTeamBinder.UI.Pages
{
    public class SsoSignOn : NonSsoSignOn
    {
        #region Entities

        private static By _username => By.Id("userNameInput");
        private static By _password => By.Id("passwordInput");
        private static By _email => By.Id("i0116");
        private static By _nextButton => By.Id("idSIButton9");
        private static By _signInButton => By.Id("submitButton");
        private static By _denyStaySignedIn => By.Id("idBtn_Back");

        public IWebElement Username { get { return StableFindElement(_username); } }
        public IWebElement Password { get { return StableFindElement(_password); } }
        public IWebElement Email { get { return StableFindElement(_email); } }
        public IWebElement NextButton { get { return StableFindElement(_nextButton); } }
        public IWebElement SignInButton { get { return StableFindElement(_signInButton); } }
        public IWebElement DenyStaySignedIn { get { return StableFindElement(_denyStaySignedIn); } }

        #endregion


        #region Actions

        public SsoSignOn(IWebDriver webDriver) : base(webDriver)
        {
            Title = "TeamBinder";
        }

        public LoggedInLanding KiewitUserLogon(TestAccount account)
        {
            string signinWindow;

            //Click KiewitUserLogin Button and Switch to OtherUserLogin Window
            SwitchToPopUpWindow(KiewitUserLoginBtn, out signinWindow, true);

            //Fill Kiewit account fields
            Email.InputText(account.kiewitUserName);
            NextButton.Click();
            Wait(shortTimeout);
            Username.InputText(account.kiewitUserName);
            Password.InputText(account.kiewitPassword);

            SignInButton.Click();
            if (FindElement(By.Id("idBtn_Back"), longTimeout) != null)
                DenyStaySignedIn.Click();

            //Fill TeamBinder account fields
            if (FindElement(By.Id("txtUserId"), longTimeout) != null)
            {
                UserIdTextbox.InputText(account.Username);
                CompanyIdTextbox.InputText(account.Project);
                PasswordTextbox.InputText(account.Password);
                //Click LogIn button
                LoginBtn.Click();
            }
            
            var dashboardPage = new Dashboard(WebDriver);
            WaitForElement(dashboardPage._dashBoardLabel);

            return dashboardPage;
            
        }

    }
    #endregion
}
