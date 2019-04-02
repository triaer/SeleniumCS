using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.Popup
{
    public class AgodaDayPicker : PageBase
    {
        private string dateTimeFormat = "ddd MMM dd yyyy";
        private CultureInfo culture = CultureInfo.GetCultureInfo("en-US");

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaDayPicker");

        private By _popupDayPicker => locator.Get("_popupDayPicker");
        private By _elePreviousMonth => locator.Get("_elePreviousMonth");
        private By _eleNextMonth => locator.Get("_eleNextMonth");
        private By _eleTargetDay(string targetDay) => locator.Get("_eleTargetDay", targetDay);
        private By _eleFirstDayInMonth => locator.Get("_eleFirstDayInMonth");

        #endregion

        #region Elements
        public IWebElement DayPickerPopup => StableFindElement(_popupDayPicker);
        public IWebElement PreviousMonthElement => StableFindElement(_elePreviousMonth);
        public IWebElement NextMonthElement => StableFindElement(_eleNextMonth);
        public IWebElement TargetDayElement(string targetDay) => StableFindElement(_eleTargetDay(targetDay));
        public IWebElement FirstDayInMonthElement => StableFindElement(_eleFirstDayInMonth);
        #endregion


        #region Methods

        public AgodaDayPicker(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElementDisplay(_popupDayPicker);
        }

        public void SelectDate(DateTime date)
        {
            string dateAsString = date.ToString(dateTimeFormat, culture);
            string firstDateAsString = FirstDayInMonthElement.GetAttribute("aria-label");
            DateTime firstDate = DateTime.ParseExact(firstDateAsString, dateTimeFormat, culture);
            int monthDiff = GetMonthDifference(date, firstDate);
            if (monthDiff>0)
            {
                for (int i = 0; i< monthDiff/2; i++)
                {
                    NextMonthElement.Click();
                    Wait(1);
                }
            }
            TargetDayElement(dateAsString).Click();
        }

        public void SelectDateRange(DateTime checkInDate, DateTime checkOutDate)
        {
            var node = ExtentReportsHelper.GetLastNode();
            string checkInDateAsString = checkInDate.ToString("MMM dd yyyy");
            string checkOutDateAsString = checkOutDate.ToString("MMM dd yyyy");
            node.Info(String.Format("Select Date Range: {0} -> {1}", checkInDateAsString, checkOutDateAsString));
            SelectDate(checkInDate);
            SelectDate(checkOutDate);
        }

        private int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return monthsApart;
        }

        #endregion
    }
}
