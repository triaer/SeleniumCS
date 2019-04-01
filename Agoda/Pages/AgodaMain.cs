using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agoda.Pages;
using OpenQA.Selenium;
using static Agoda.ExtentReportsHelper;
using System.Globalization;
using Agoda.DataObject;

namespace Agoda.Pages
{
    public class AgodaMain : PageBase
    {
        private IWebDriver _driver;

        # region Locators
        static readonly By _destinationTxt = By.XPath("//input[@data-selenium='textInput']");
        string _suggestionDest = "//ul[@class='AutocompleteList']//*[text()='{0}']";
        static readonly By _checkInBox = By.XPath("//div[@data-selenium='checkInBox']");
        static readonly By _checkOutBox = By.XPath("//div[@data-selenium='checkOutBox']");
        string _generalDateBox = "//*[@aria-label='{0}']";
        static readonly By _occupancyBox = By.XPath("//div[@data-selenium='occupancyBox']");
        static readonly By _familyTravelOption = By.XPath("//div[@data-selenium='traveler-families']");
        static readonly By _groupTravelOption = By.XPath("//div[@data-selenium='traveler-group']");
        static readonly By _searchButton = By.XPath("//button[@data-selenium='searchButton']");

        static readonly By _datepicker_heading = By.XPath("//div[@role='heading']/div");
        static readonly By _datepicker_prevBtn = By.XPath("//span[@aria-label='Previous Month']");
        static readonly By _datepicker_nextBtn = By.XPath("//span[@aria-label='Next Month']");

        static readonly By _button_minusRoom = By.XPath("//div[@data-selenium='occupancyRooms']/span[text()='-']");
        static readonly By _button_plusRoom = By.XPath("//div[@data-selenium='occupancyRooms']/span[text()='+']");
        static readonly By _label_OccupiedRoom = By.XPath("//div[@data-selenium='occupancyRooms']/span[@class='PlusMinusRow__number']");
        static readonly By _button_minusAdult = By.XPath("//div[@data-selenium='occupancyAdults']/span[text()='-']");
        static readonly By _button_plusAdult = By.XPath("//div[@data-selenium='occupancyAdults']/span[text()='+']");
        static readonly By _label_OccupiedAdult = By.XPath("//div[@data-selenium='occupancyAdults']/span[@class='PlusMinusRow__number']");

        string _hotel_tagname = "//li[@data-selenium='hotel-item']//*[@class='hotel-name' and text()='{0}']";

        #endregion


        #region Elements
        public IWebElement TextDestination
        {
            get { return StableFindElement(_destinationTxt); }
        }
        
        public IWebElement selectSuggestDestination(string destination)
        {
            return StableFindElement(By.XPath(string.Format(_suggestionDest, destination)));
        }

        public IWebElement TextCheckIn
        {
            get { return StableFindElement(_checkInBox); }
        }

        public IWebElement ButtonSearch
        {
            get { return StableFindElement(_searchButton); }
        }

        public IWebElement ButtonPreviousMonth
        {
            get { return StableFindElement(_datepicker_prevBtn); }
        }

        public IWebElement ButtonNextMonth
        {
            get { return StableFindElement(_datepicker_nextBtn); }
        }

        public IWebElement DatePickerHeading
        {
            get { return StableFindElement(_datepicker_heading); }
        }

        public IWebElement OptionGroupTravel
        {
            get { return StableFindElement(_groupTravelOption);  }
        }

        public IWebElement PlusOneRoom
        {
            get { return StableFindElement(_button_plusRoom); }
        }

        public IWebElement LabelOccupiedRoom
        {
            get { return StableFindElement(_label_OccupiedRoom); }
        }

        public IWebElement LabelOccupiedAdults
        {
            get { return StableFindElement(_label_OccupiedAdult); }
        }

        public IWebElement PlusOneAdult
        {
            get { return StableFindElement(_button_plusAdult); }
        }

        public IWebElement HotelTagName(string hotelName)
        {
            return StableFindElement(By.XPath(string.Format(_hotel_tagname, hotelName)));
        }
        #endregion

        # region Methods
        public AgodaMain(IWebDriver driver) : base (driver)
        {
            this._driver = driver;
        }
        
        public DateTime getCheckInDate()
        {
            return DateTime.Today.AddMonths(1);
        }

        public DateTime getCheckOutDate()
        {
            return getCheckInDate().AddDays(3);
        }

        public string getFormattedDay(DateTime date)
        {
            return date.ToString("dd", new CultureInfo("en-US"));
        }

        public string getFormattedDayOfWeek(DateTime date)
        {
            return date.DayOfWeek.ToString("g").Substring(0, 3);
        }

        public string getFormattedYear(DateTime date)
        {
            return date.Year.ToString();
        }

        public string getFormattedMonth(DateTime date)
        {
            return date.ToString("MMMM", new CultureInfo("en-US")).Substring(0, 3);
            
        }

        public IWebElement selectDateBox(DateTime d)
        {
            //DateTime d = getCheckInDate();
            var value =  getFormattedDayOfWeek(d) 
                + " " + getFormattedMonth(d) 
                + " " + getFormattedDay(d) 
                + " " + getFormattedYear(d);
            Console.WriteLine(value);
            return StableFindElement(By.XPath(string.Format(_generalDateBox, value)));
        }

        public void SelectNeedRoom(int need)
        {
            int display = int.Parse(LabelOccupiedRoom.Text);
            while (display < need)
            {
                PlusOneRoom.Click();
                display++;
            }
        }

        public void SelectGuestNumber(int guestNumber)
        {
            int display = int.Parse(LabelOccupiedAdults.Text);
            while (display < guestNumber)
            {
                PlusOneAdult.Click();
                display++;
            }
        }

        public string getCurrentDisplayedMonthFromPicker()
        {
            return DatePickerHeading.Text.Split(' ').First();
        }

        public void selectCheckInMonth()
        {
            //int iterate;
            string checkinMonth = this.getFormattedMonth(getCheckInDate());
            string monthCase = this.getCurrentDisplayedMonthFromPicker();
            //switch (monthCase)
            //{
            //    case "May":
            //        iterate = 1;
            //        break;
            //    case "June":
            //        iterate = 2;
            //        break;
            //    default:
            //        break;
            //}
            while (checkinMonth != monthCase)
            {
                ButtonNextMonth.Click();
                monthCase = this.getCurrentDisplayedMonthFromPicker();
            }
            
        }

        public AgodaMain initSearch()
        {
            BookOrder order = new BookOrder();
            TextDestination.Clear();
            TextDestination.SendKeys(order.destination);
            selectSuggestDestination(order.destination).Click();

            //TextCheckIn.Click();
            selectDateBox(getCheckInDate()).Click();
            selectDateBox(getCheckOutDate()).Click();

            OptionGroupTravel.Click();
            SelectNeedRoom(order.roomInNeed);
            SelectGuestNumber(order.guestNumber);

            ButtonSearch.Click();

            return this;
        }

        public AgodaMain selectHotel (string hotelName)
        {
            ScrollIntoView(HotelTagName(hotelName));
            HotelTagName(hotelName).Click();
            return this;
        }

        public void test()
        {
            Console.WriteLine(DateTime.Today.ToString("yyyy'년' M'월' d'일' dddd", CultureInfo.GetCultureInfo("ko-KR")));
        }

        public static KeyValuePair<string, bool> ValidateFilledInformation()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool newPageFounded = true;

                if (newPageFounded)
                    validation = SetPassValidation(node, ValidationMessage.ValidateFilledInformation);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateFilledInformation);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateFilledInformation, e);
            }
            EndStepNode(node);
            return validation;
        }

        # endregion

        private static class ValidationMessage
        {
            public static string ValidateFilledInformation = "Validate That All Filled Information Is Correct.";
        }
    }


}
