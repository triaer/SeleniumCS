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
    public class AgodaHotelDetailPage : PageBase
    {

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaHotelDetailPage");


        private By _eleRoomList => locator.Get("_eleRoomList");
        private By _lblTargetRoomName(string name) => locator.Get("_lblTargetRoomName", name);
        private By _lblTargetRoomPrice(string name) => locator.Get("_lblTargetRoomPrice", name);
        private By _btnBookNowOfTargetRoom(string name) => locator.Get("_btnBookNowOfTargetRoom", name);

        #endregion

        #region Elements
        public IWebElement RoomListElement => StableFindElement(_eleRoomList);
        public IWebElement TargetRoomName(string name) => StableFindElement(_lblTargetRoomName(name));
        public IWebElement TargetRoomPrice(string name) => StableFindElement(_lblTargetRoomPrice(name));
        public IWebElement BookNowOfTargetRoomButton(string name) => StableFindElement(_btnBookNowOfTargetRoom(name));
        #endregion

        #region Methods

        public AgodaHotelDetailPage(IWebDriver webDriver) : base(webDriver)
        {
            SwitchToWindow(WebDriver.WindowHandles.Last());
            Browser.MaximizeWindow();
            WaitForElement(_eleRoomList);
        }

        public AgodaBookingFormPage SelectSpecificRoom(HotelInfo info)
        {
            var node = CreateStepNode();
            node.Info(String.Format("Select room with name: {0}", info.RoomName));
            ScrollIntoView(TargetRoomName(info.RoomName));
            info.ActualRoomName = TargetRoomName(info.RoomName).Text;
            node.Info(String.Format("Get actual room name: {0}", info.ActualRoomName));
            info.RoomPrice = Convert.ToDouble(TargetRoomPrice(info.RoomName).Text);
            node.Info(String.Format("Get room price: {0}", info.RoomPrice));
            BookNowOfTargetRoomButton(info.RoomName).Click();
            EndStepNode(node);
            return new AgodaBookingFormPage(WebDriver);
        }

        #endregion
    }
}
