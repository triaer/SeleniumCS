using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;

namespace KiewitTeamBinder.UI.Pages.Mail
{
    class MailInboxPage : LoggedInLanding
    {
        #region Entities

        private static By _defaultViewLabel(string value) => By.XPath($"//li[@class='rfItem']/span[text()='{value}']");
        private static By _itemsNumberLabel(string value) => By.XPath($"//span[contains(@id, 'GridView{value}_ctl00DSC')]");
        private static By _refreshButton => By.XPath("//span[@class='rtbIn' and text() = 'Refresh']");


        public IWebElement DefaultViewLabel(string value) => StableFindElement(_defaultViewLabel(value));
        public IWebElement ItemsNumberLabel(string value) => StableFindElement(_itemsNumberLabel(value));
        public IWebElement RefreshButton { get { return StableFindElement(_refreshButton); } }

        #endregion

        #region Actions
        public MailInboxPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        private static class Validation
        {
        }
        #endregion

    }
}
