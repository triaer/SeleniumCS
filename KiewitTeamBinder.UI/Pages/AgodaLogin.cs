using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using System.Collections.Generic;

namespace KiewitTeamBinder.UI.Pages
{
    public class AgodaLogin : LoggedInLanding
    {
        #region Locators
        private By _searchInput => By.XPath("//*[@id='SearchBoxContainer']/div/div/div[1]/div/div/input");
        private By _listResults => By.XPath("//div[@class='Popup__content']");
        private By _firstResult => By.XPath("//div[@class='Popup__content']//li[1]");
        private By _dateTimePicker => By.XPath("//div[@class='DayPicker']");
        private By _monthLabels => By.XPath("//div[@class='DayPicker-Caption']//div");
        private By _fowardButton => By.XPath("//span[contains(@class,'ficon ficon-18 ficon-edge-arrow-right')]");
        private By _chosenday(string day) => By.XPath($"//div[contains(@aria-label,'{day}')]");
        private By _popUpBooking => By.XPath("//div[@class='SegmentOccupancy']");
        private By _familyTravel => By.XPath("//div[@data-selenium='traveler-families']");
        private By _rooms => By.XPath("//span[@data-selenium='desktop-occ-room-value']");
        private By _guests => By.XPath("//span[@data-selenium='desktop-occ-adult-value']");
        private By _roomsPlus => By.XPath("//div[@data-selenium='occupancyRooms']//span[@data-selenium='plus']");
        private By _guestsPlus => By.XPath("//div[@data-selenium='occupancyAdults']//span[@data-selenium='plus']");
        private By _searchButton => By.XPath("//button[@data-selenium='searchButton']");
        private By _starRatting => By.XPath("//i[@class='ficon ficon-16 PillDropdown__Icon ficon-hotel-star']");
        private By _hotel(string name) => By.XPath($"//h3[contains(text(),'{name}')]");
        #endregion

        #region Elements
        public IWebElement SearchInput { get { return StableFindElement(_searchInput); } }
        public IWebElement FirstResult { get { return StableFindElement(_firstResult); } }
        public IWebElement DateTimePicker { get { return StableFindElement(_dateTimePicker); } }
        public IWebElement FowardButton { get { return StableFindElement(_fowardButton); } }
        public IWebElement ChosenDay(string day) => StableFindElement(_chosenday(day));
        public IWebElement FamilyTravel { get { return StableFindElement(_familyTravel); } }
        public IWebElement Rooms { get { return StableFindElement(_rooms); } }
        public IWebElement Guests { get { return StableFindElement(_guests); } }
        public IWebElement RoomsPlus { get { return StableFindElement(_roomsPlus); } }
        public IWebElement GuestsPlus { get { return StableFindElement(_guestsPlus); } }
        public IWebElement SearchButton { get { return StableFindElement(_searchButton); } }
        public IWebElement StarRatting { get { return StableFindElement(_starRatting); } }
        public IWebElement Hotel(string name) => StableFindElement(_hotel(name));
        public IReadOnlyCollection<IWebElement> MonthLables { get { return StableFindElements(_monthLabels); } }
        #endregion

        #region Methods
        public AgodaLogin InputSearchField(string place)
        {
            SearchInput.SendKeys(place);
            WaitForElement(_listResults);
            FirstResult.Click();
            WaitForElement(_dateTimePicker);
            return this;
        }

        public List<string> GetMonthLabels()
        {
            List<string> lablelsList = new List<string>();
            foreach (var item in MonthLables)
            {
                lablelsList.Add(item.Text);
            }
            return lablelsList;
        }

        public AgodaLogin SelectDay(int month, int duration = 0, bool wait = false,  bool checkOut = false)
        {
            DateTime checkInDay = DateTime.Now.AddMonths(month).AddDays(duration);
            string [] typeOfChoosenDay = checkInDay.GetDateTimeFormats('R')[0].Split(' ');
            string chosenDay = typeOfChoosenDay[2] + " " + typeOfChoosenDay[1] + " " + typeOfChoosenDay[3];
            while (FindElement(_chosenday(chosenDay))==null)
            {
                FowardButton.Click();
            }
            ChosenDay(chosenDay).Click();
            Thread.Sleep(1000);
            if (wait)
            {
                WaitForElement(_popUpBooking);
            }
            return this;
        }

        //public AgodaLogin SelectCheckOutDay()
        //{
        //    DateTime CheckOutDate = DateTime.Now.AddMonths(1).AddDays(3);
        //    string[] typeOfChoosenDay = CheckOutDate.GetDateTimeFormats('R')[0].Split(' ');
        //    string chosenDay = typeOfChoosenDay[2] + " " + typeOfChoosenDay[1] + " " + typeOfChoosenDay[3];
        //    //string chosenMonth = CheckOutDate.GetDateTimeFormats('Y')[0];
        //    //List<string> ListofLabels = GetMonthLabels();
        //    //while (ListofLabels[0] != chosenMonth && ListofLabels[1] != chosenMonth)
        //    //{
        //    //    FowardButton.Click();
        //    //    GetMonthLabels();
        //    //}
        //    while (FindElement(_chosenday(chosenDay)) == null)
        //    {
        //        FowardButton.Click();
        //    }
        //    ChosenDay(chosenDay).Click();
        //    WaitForElement(_popUpBooking);
        //    return this;
        //}

        public AgodaLogin SelectRoomsAndGuets(string rooms, string guests)
        {
            FamilyTravel.Click();
            while (rooms != Rooms.Text)
            {
                RoomsPlus.Click();
            }
            while(guests != Guests.Text)
            {
                GuestsPlus.Click();
            }
            return this;
        }

       public AgodaLogin EnterRequiredData(string place, int month, int duration, string room, string guests)
        {
            var node = CreateStepNode();
            node.Info("Enter required data ");
            InputSearchField(place);
            SelectDay(month);
            SelectDay(month, duration, true, true);
            SelectRoomsAndGuets(room, guests);
            EndStepNode(node);
            return this;
        } 

       public T ClickSearchButton<T>()
        {
            var node = CreateStepNode();
            node.Info("Click Search button");
            SearchButton.Click();
            WaitForElement(_starRatting);
            EndStepNode(node);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public void SelectHotel(string name)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)WebDriver;
            var hieght = jse.ExecuteScript("return document.body.scrollHeight");
            var scrollLocate = 600;
            //jse.ExecuteScript($"window.scrollTo(0, {scrollLocate})");
            while (FindElement(_hotel(name), 1)==null)
            {
                jse.ExecuteScript($"window.scrollTo(0, {scrollLocate})");
                scrollLocate += 200;
            }
            Hotel(name).Click();
            //ScrollIntoView(Hotel(name));
        }
        #endregion

        public AgodaLogin(IWebDriver webDriver) : base(webDriver)
        {
            
        }
    }
}
