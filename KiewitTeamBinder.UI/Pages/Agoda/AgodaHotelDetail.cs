using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.DashBoardENums;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.Common.AgodaEnums;
using KiewitTeamBinder.Common.Models.Agoda;

namespace KiewitTeamBinder.UI.Pages.Agoda
{
    public class AgodaHotelDetail : AgodaGeneral
    {
        public AgodaHotelDetail(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _chkRoomFilter(string filterType) => By.XPath($"//div[contains(@data-element-name,'{filterType}')]");
        static By _btnBook(string roomName, int roomPosition) => By.XPath($"(//span[contains(text(),'{roomName}') and @data-selenium='masterroom-title-name']/ancestor::div[@class='MasterRoom-header']/following-sibling::div[@class='MasterRoom-table']//button)[{roomPosition}]");
        static By _lblCurrency(string roomName, int roomPosition) => By.XPath($"(//span[contains(text(),'{roomName}') and @data-selenium='masterroom-title-name']/ancestor::div[@class='MasterRoom-header']/following-sibling::div[@class='MasterRoom-table']//span[@class='pd-currency'])[{roomPosition}]");
        static By _lblPrice(string roomName, int roomPosition) => By.XPath($"(//span[contains(text(),'{roomName}') and @data-selenium='masterroom-title-name']/ancestor::div[@class='MasterRoom-header']/following-sibling::div[@class='MasterRoom-table']//strong[@data-ppapi='room-price'])[{roomPosition}]");
        #endregion

        #region Elements
        public IWebElement ChkRoomFilter(string filterType) => StableFindElement(_chkRoomFilter(filterType));
        public IWebElement BtnBook(string roomName, int roomPosition) => StableFindElement(_btnBook(roomName, roomPosition));
        public IWebElement LblCurrency(string roomName, int roomPosition) => StableFindElement(_lblCurrency(roomName, roomPosition));
        public IWebElement LblPrice(string roomName, int roomPosition) => StableFindElement(_lblPrice(roomName, roomPosition));

        #endregion

        #region Methods
        public AgodaHotelDetail FilterRoom <T> (T filterValue)
        {
            var node = CreateStepNode();
            node.Info("Filter room.");
            if (typeof(T).Equals(typeof(string)))
                ChkRoomFilter(filterValue.ToString()).Check();
            else
            {
                var values = filterValue as Array;
                foreach (var item in values)
                    ChkRoomFilter(item.ToString()).Check();
            }
            EndStepNode(node);
            return this;
        }

        public AgodaHotelDetail FilterRoom(bool IsFreeBreakfast = false, bool IsFreeCancellation = false, bool IsNonSmoking = false, bool IsTwinBed = false)
        {
            var node = CreateStepNode();
            node.Info("Filter room with: Free breakfast is " + IsFreeBreakfast + ", Free Cancallation is " + IsFreeCancellation + ", Non-Smoking is " + IsNonSmoking + ", Twin Bed is " + IsTwinBed);
            if (IsFreeBreakfast)
                ChkRoomFilter(RoomProperties.FreeBreakfast.ToDescription()).ScrollAndClick();
            if (IsFreeCancellation)
                ChkRoomFilter(RoomProperties.FreeCancellation.ToDescription()).ScrollAndClick();
            if (IsNonSmoking)
                ChkRoomFilter(RoomProperties.NonSmoking.ToDescription()).ScrollAndClick();
            if (IsTwinBed)
                ChkRoomFilter(RoomProperties.TwinBed.ToDescription()).ScrollAndClick();
            EndStepNode(node);
            return this;
        }

        public AgodaPayment ClickBookNowButton(string roomType, int roomPosition = 1)
        {
            var node = CreateStepNode();
            node.Info("Click Book Now button of room: " + roomType);
            IWebElement element = ScrollToElement(_btnBook(roomType, roomPosition));
            WaitForElementClickable(_btnBook(roomType, roomPosition));
            element.Click();
            EndStepNode(node);
            return new AgodaPayment(WebDriver);
        }

        public AgodaPayment SelectRoom(Room room)
        {
            var node = CreateStepNode();
            node.Info("Select room");
            //FilterRoom(room.IsFreeBreakfast, room.IsFreeCancellation, room.IsNonSmoking, room.IsTwinBed);
            GetCurrency(room.RoomType, room.RoomPosition);
            GetOneRoomPrice(room.RoomType, room.RoomPosition);
            ClickBookNowButton(room.RoomType, room.RoomPosition);
            EndStepNode(node);
            return new AgodaPayment(WebDriver);
        }

        public string GetCurrency(string roomType, int roomPosition = 1)
        {
            var node = CreateStepNode();
            string currency = LblCurrency(roomType, roomPosition).Text;
            node.Info("Get the currency: " + currency);
            EndStepNode(node);
            return Constant.currency = currency;
        }

        public string GetOneRoomPrice(string roomType, int roomPosition = 1)
        {
            var node = CreateStepNode();
            string price = LblPrice(roomType, roomPosition).Text;
            node.Info("Get the currency: " + price);
            EndStepNode(node);
            return Constant.roomPrice = price;
        }

        #endregion
    }
}
