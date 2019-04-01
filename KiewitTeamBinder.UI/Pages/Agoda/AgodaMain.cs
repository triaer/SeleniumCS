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
    public class AgodaMain : AgodaGeneral
    {
        public AgodaMain(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _txtSearch => By.XPath("//input[contains(@class, 'SearchBoxTextEditor')]");
        static By _lblSearchResult(string searchValue) => By.XPath($"//li[contains(@data-text,'{searchValue}')][1]");
        static By _boxCheckin => By.XPath("//div[@data-element-name='check-in-box']");
        static By _boxCheckout => By.XPath("//div[@data-element-name='check-out-box']");
        static By _boxOccupancy => By.XPath("//div[@data-element-name='occupancy-box']");
        static By _btnSearch => By.XPath("//button[contains(@class, 'btn Searchbox')]");
        static By _dtmCheckInDatePicker => By.XPath("//div[@data-element-name='search-box-check-in']");
        static By _dtmAllDates => By.XPath("//span/parent::div[contains(@class,'DayPicker-Day')]");
        static By _dtmToday => By.XPath("//span/parent::div[contains(@class,'DayPicker-Day--today')]");
        static By _dtmDateNextMonth(string date) => By.XPath($"//span/parent::div[contains(@class,'DayPicker-Day--today')]/ancestor::div[@class='DayPicker-Month']/following-sibling::div//span[text()='{date}']/parent::div");
        static By _dtmChooseDate(int index) => By.XPath($"(//span/parent::div[contains(@class,'DayPicker-Day')])[{index}]");
        static By _dtmChooseDateRightMonth(int index) => By.XPath($"(//div[@class='DayPicker-Month'][2]//span/parent::div[contains(@class,'DayPicker-Day')])[{index}]");
        static By _dtmPrevButton => By.XPath("//span[@aria-label='Previous Month']");
        static By _dtmNextButton => By.XPath("//span[@aria-label='Next Month']");
        static By _lnkTypeOccupancy(string travelType) => By.XPath($"//div[@data-element-name='{travelType}']");
        static By _symbolMinusOccupancy(string type) => By.XPath($"//div[@data-selenium='{type}']/span[@data-selenium='minus']");
        static By _symbolPlusOccupancy(string type) => By.XPath($"//div[@data-selenium='{type}']/span[@data-selenium='plus']");
        static By _lblRoomsNumber => By.XPath("//div[@data-selenium='occupancyRooms']/span[@class='PlusMinusRow__number']");
        static By _lblAdultsNumber => By.XPath("//div[@data-selenium='occupancyAdults']/span[@class='PlusMinusRow__number']");
        static By _lblBoxCheckinValue => By.XPath("//div[@data-selenium='checkInText']");
        static By _lblBoxCheckoutValue => By.XPath("//div[@data-selenium='checkOutText']");
        static By _lblBoxOccupancyAdultValue => By.XPath("//span[@data-selenium='adultValue']");
        static By _lblBoxOccupancyRoomValue => By.XPath("//div[@data-selenium='roomValue']");

        #endregion

        #region Elements
        public IWebElement TxtSearch => StableFindElement(_txtSearch);
        public IWebElement LblSearchResult(string searchValue) => StableFindElement(_lblSearchResult(searchValue));
        public IWebElement BoxCheckIn => StableFindElement(_boxCheckin);
        public IWebElement BoxCheckOut => StableFindElement(_boxCheckout);
        public IWebElement BoxOccupancy => StableFindElement(_boxOccupancy);
        public IWebElement BtnSearch => StableFindElement(_btnSearch);
        public IWebElement DtmCheckInDatePicker => StableFindElement(_dtmCheckInDatePicker);
        public IReadOnlyCollection<IWebElement> DtmAllDates => StableFindElements(_dtmAllDates);
        public IWebElement DtmToday => StableFindElement(_dtmToday);
        public IWebElement DtmChooseDate(int index) => StableFindElement(_dtmChooseDate(index));
        public IWebElement DtmDateNextMonth(string date) => StableFindElement(_dtmDateNextMonth(date));
        public IWebElement DtmChooseDateRightMonth(int index) => StableFindElement(_dtmChooseDateRightMonth(index));
        public IWebElement DtmPrevButton => StableFindElement(_dtmPrevButton);
        public IWebElement DtmNextButton => StableFindElement(_dtmNextButton);
        public IWebElement LnkTypeOccupancy(string travelType) => StableFindElement(_lnkTypeOccupancy(travelType));
        public IWebElement SymbolMinusOccupancy(string type) => StableFindElement(_symbolMinusOccupancy(type));
        public IWebElement SymbolPlusOccupancy(string type) => StableFindElement(_symbolPlusOccupancy(type));
        public IWebElement LblAdultsNumber => StableFindElement(_lblAdultsNumber);
        public IWebElement LblRoomsNumber => StableFindElement(_lblRoomsNumber);
        public IWebElement LblBoxCheckinValue => StableFindElement(_lblBoxCheckinValue);
        public IWebElement LblBoxCheckoutValue => StableFindElement(_lblBoxCheckoutValue);
        public IWebElement LblBoxOccupancyAdultValue => StableFindElement(_lblBoxOccupancyAdultValue);
        public IWebElement LblBoxOccupancyRoomValue => StableFindElement(_lblBoxOccupancyRoomValue);
        #endregion

        #region Methods
        public AgodaMain SelectDate(int duration, int checkinDate = 0, string checkinTime = null)
        {
            var node = CreateStepNode();
            node.Info("Select date, with checkin date: " + checkinDate + " days ahead or checkin time: " + checkinTime + ", duration: " + duration);
            if (!DtmCheckInDatePicker.IsDisplayed())
                BoxCheckIn.Click();
            List<string> currentDateValue = new List<string>();
            foreach (var item in DtmAllDates)
                currentDateValue.Add(item.GetAttribute("class"));
            bool isContainsToday = currentDateValue.Contains("DayPicker-Day DayPicker-Day--today");
            int index;
            List<IWebElement> dates = DtmAllDates.ToList();
            if (isContainsToday)
            {
                index = dates.FindIndex(a => a.Equals(DtmToday)) + 1;
            }
            else
            {
                DtmPrevButton.Click();
                dates = DtmAllDates.ToList();
                index = dates.FindIndex(a => a.Equals(DtmToday)) + 1;
            }
            int checkin;
            int checkout;
            if (checkinDate != 0)
            {
                checkin = index + checkinDate;
                checkout = index + checkinDate + duration;
                DtmChooseDate(checkin).Click();
                DtmChooseDate(checkout).Click();
            }
            else if(checkinTime != null)
            {
                if (checkinTime == "Next month")
                {
                    string todayDate = DtmToday.Text;
                    DtmDateNextMonth(todayDate).Click();
                    dates = DtmAllDates.ToList();
                    index = dates.FindIndex(a => a.Equals(DtmDateNextMonth(todayDate))) + 1;
                    checkout = index + duration;
                    if (checkout > dates.Count - 1)
                    {
                        DtmNextButton.Click();
                        int newCheckout = checkout - (dates.Count - 1);
                        DtmChooseDateRightMonth(newCheckout).Click();
                    }
                    else
                        DtmChooseDate(checkout).Click();
                }
            }
            EndStepNode(node);
            return this;
        }

        public AgodaMain SelectOccupancy(string travelType, int room = 1, int adults = 2, int children = 0)
        {
            var node = CreateStepNode();
            node.Info("Select occupancy");
            if (!LnkTypeOccupancy(travelType).IsDisplayed())
                BoxOccupancy.Click();
            LnkTypeOccupancy(travelType).Click();
            int currentRooms = Int32.Parse(LblRoomsNumber.Text);
            if (room > 1)
                for (int i = currentRooms; i < room; i++)
                    SymbolPlusOccupancy(Occupancy.Room.ToDescription()).Click();
            if(adults != 2)
            {
                int currentAdults = Int32.Parse(LblAdultsNumber.Text);
                if (adults < 2)
                    for (int i = currentAdults; i > adults; i--)
                        SymbolMinusOccupancy(Occupancy.Adults.ToDescription()).Click();
                else if (adults > 2)
                {
                    for (int i = currentAdults; i < adults; i++)
                        SymbolPlusOccupancy(Occupancy.Adults.ToDescription()).Click();
                }
            }
            if (children > 0)
                for (int i = 0; i < children; i++)
                    SymbolPlusOccupancy(Occupancy.Children.ToDescription()).Click();
            EndStepNode(node);
            return this;
        }

        public AgodaMain SearchDestination(string searchValue)
        {
            var node = CreateStepNode();
            node.Info("Search destination: " + searchValue);
            TxtSearch.InputText(searchValue);
            if (LblSearchResult(searchValue).IsDisplayed())
                LblSearchResult(searchValue).Click();
            else
                TxtSearch.ActionsPressEnter();
            EndStepNode(node);
            return this;
        }

        public AgodaSearchResults SearchForStayPlace(PlaceToStay placeToStay)
        {
            var node = CreateStepNode();
            node.Info("Search for place to stay.");
            SearchDestination(placeToStay.Destination);
            SelectDate(placeToStay.Duration, placeToStay.CheckinDate, placeToStay.CheckinTime);
            SelectOccupancy(placeToStay.TravelType, placeToStay.Room, placeToStay.Adults, placeToStay.Children);
            GetCheckinCheckoutDate();
            GetOccupancyDetails();
            BtnSearch.Click();
            EndStepNode(node);
            return new AgodaSearchResults(WebDriver);
        }

        public string GetCheckinCheckoutDate()
        {
            var node = CreateStepNode();
            string checkinDate = LblBoxCheckinValue.Text;
            string checkoutDate = LblBoxCheckoutValue.Text;
            string checkinCheckoutDate = checkinDate + " - " + checkoutDate;
            node.Info("Checkin date is: " + checkinDate + ", checkout date is: " + checkoutDate);
            EndStepNode(node);
            return Constant.checkinCheckoutDate = checkinCheckoutDate;
        }

        public string GetOccupancyDetails()
        {
            var node = CreateStepNode();
            string adult = LblBoxOccupancyAdultValue.Text;
            string room = LblBoxOccupancyRoomValue.Text;
            string occupancy = room + ", " + adult;
            node.Info("Occupancy Details: " + adult + " adults, " + room + " rooms.");
            EndStepNode(node);
            return Constant.occupancy = occupancy;
        }

        #endregion
    }
}
