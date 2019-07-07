using Breeze.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Pages.Foody
{
    public class HomePage : BasePage
    {
        #region Locators

        static readonly By _btnLogin = By.XPath("//div[@id='accountmanager']/a");
        static readonly By _txtFilter = By.XPath("//input[@id='pkeywords']");

        #endregion

        #region Elements

        public IWebElement BtnLogin
        {
            get { return StableFindElement(_btnLogin); }
        }

        public IWebElement TxtFilter
        {
            get { return StableFindElement(_txtFilter); }
        }

        #endregion

        #region Methods

        public HomePage() : base()
        {
        }

        public LoginPage OpenLoginPage()
        {
            BtnLogin.Click();
            return new LoginPage();
        }

        public void Search(String keywords)
        {
            WaitForElementDisplay(_txtFilter);
            TxtFilter.SendKeys(keywords);
        }

        #endregion
    }
}
