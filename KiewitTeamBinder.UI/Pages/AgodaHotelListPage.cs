using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.Popup;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages
{
    public class AgodaHotelListPage : PageBase
    {

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaHotelListPage");


        private By _eleHotelList => locator.Get("_eleHotelList");
        private By _txtSearch => locator.Get("_txtSearch");
        private By _linkTargetHotelName(string name) => locator.Get("_linkTargetHotelName", name);

        #endregion

        #region Elements
        public IWebElement HotelListElement => StableFindElement(_eleHotelList);
        public IWebElement SearchTextBox => StableFindElement(_txtSearch);
        public IWebElement TargetHotelNameLink(string name) => StableFindElement(_linkTargetHotelName(name));
        #endregion

        #region Methods

        public AgodaHotelListPage(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElement(_eleHotelList);
        }

        public AgodaHotelDetailPage SearchAndSelectHotel(HotelInfo info)
        {
            var node = CreateStepNode();
            node.Info(String.Format("Search and select hotel with name: {0}", info.HotelName));
            SearchTextBox.InputText(info.HotelName);
            SearchTextBox.SendKeys(Keys.Enter);
            Wait(1);
            //WaitForElement(_eleHotelList);
            info.ActualHotelName = TargetHotelNameLink(info.HotelName).Text;
            node.Info(String.Format("Get actual hotel name: {0}", info.ActualHotelName));
            TargetHotelNameLink(info.HotelName).Click();
            Wait(1);
            EndStepNode(node);
            return new AgodaHotelDetailPage(WebDriver);
        }

        #endregion
    }
}
