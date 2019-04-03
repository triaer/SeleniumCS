using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.Resource;
using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.Popup;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages
{
    public class AgodaBookingFormPage : PageBase
    {

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaBookingFormPage");


        private By _lblHotelName => locator.Get("_lblHotelName");
        private By _lblBookingDateRange => locator.Get("_lblBookingDateRange");
        private By _lblDuration => locator.Get("_lblDuration");
        private By _lblRoomTypeList => locator.Get("_lblRoomTypeList");
        private By _lblRoomOccupancy => locator.Get("_lblRoomOccupancy");
        private By _lblBasePrice => locator.Get("_lblBasePrice");

        #endregion

        #region Elements
        public IWebElement HotelName => StableFindElement(_lblHotelName);
        public IWebElement BookingDateRange => StableFindElement(_lblBookingDateRange);
        public IWebElement BookingDuration => StableFindElement(_lblDuration);
        public IWebElement RoomTypeList => StableFindElement(_lblRoomTypeList);
        public IWebElement BookingOccupancy => StableFindElement(_lblRoomOccupancy);
        public IWebElement BasePrice => StableFindElement(_lblBasePrice);
        #endregion

        #region Methods

        public AgodaBookingFormPage(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElement(_lblHotelName);
        }

        public KeyValuePair<string, bool> ValidateHotelName(HotelInfo info)
        {
            var node = CreateStepNode();
            try
            {

                string actualReSult = HotelName.Text;
                string expectedResult = info.ActualHotelName;
                if (info.ActualHotelName == actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateHotelName, expectedResult);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateHotelName, info.ActualHotelName, actualReSult);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateHotelName, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidateBookingDateRange(BookingInfo info)
        {
            var node = CreateStepNode();
            try
            {
                CultureInfo culture = CultureInfo.GetCultureInfo(Browser.CurrentLanguage);
                string actualReSult = BookingDateRange.Text;
                string expectedResult = String.Format("{0} - {1}", info.CheckInDate.ToString(Resource.DateFormat, culture), info.CheckOutDate.ToString(Resource.DateFormat, culture));
                if (expectedResult == actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateBookingDateRange, expectedResult);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateBookingDateRange, expectedResult, actualReSult);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateBookingDateRange, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidateBookingDuration(BookingInfo info)
        {
            var node = CreateStepNode();
            try
            {
                string actualReSult = BookingDuration.Text;
                string expectedResult = String.Format(Resource.Night, info.Duration);
                if (expectedResult == actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateBookingDuration, expectedResult);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateBookingDuration, expectedResult, actualReSult);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateBookingDuration, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidateRoomTypeList(HotelInfo info)
        {
            var node = CreateStepNode();
            try
            {
                string actualReSult = RoomTypeList.Text;
                string expectedResult = String.Format("{0} x {1}", info.RoomQuantity, info.ActualRoomName);
                if (expectedResult == actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateRoomTypeList, expectedResult);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateRoomTypeList, expectedResult, actualReSult);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateRoomTypeList, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidateBookingOccupancy(BookingInfo info)
        {
            var node = CreateStepNode();
            try
            {
                string actualReSult = BookingOccupancy.Text;
                string expectedResult = String.Format(Resource.Room, info.Room);
                if (info.Adults > 0)
                {
                    expectedResult = String.Format("{0}, {1}", expectedResult, String.Format(Resource.Adult, info.Adults));
                }
                if (info.Children > 0)
                {
                    expectedResult = String.Format("{0}, {1}", expectedResult, String.Format(Resource.Children, info.Children));
                }

                if (expectedResult == actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateBookingOccupancy, expectedResult);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateBookingOccupancy, expectedResult, actualReSult);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateBookingOccupancy, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidatePrice(HotelInfo info)
        {
            var node = CreateStepNode();
            try
            {
                string actualReSult = BasePrice.Text;
                string expectedResult = ConvertCurrency(info.RoomPrice*info.RoomQuantity);
                if (expectedResult == actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidatePrice, expectedResult);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidatePrice, expectedResult, actualReSult);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidatePrice, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        private string ConvertCurrency(double number)
        {
            string currency = number.ToString("C", CultureInfo.GetCultureInfo("en-US"));
            currency = currency.Replace("$", "").Replace(".", ":").Replace(",", ".").Replace(":", ",");
            return String.Format(Resource.Price, currency, Browser.CurrentCurrencyIcon);
        }

        private static class ValidationMessage
        {
            public static string ValidateHotelName = "Verify Hotel Name is correct";
            public static string ValidateBookingDateRange = "Validate Booking Date Range is correct";
            public static string ValidateBookingDuration = "Validate Booking Duration is correct";
            public static string ValidateRoomTypeList = "Validate Room Type list are correct";
            public static string ValidateBookingOccupancy = "Validate Booking Occupancy is correct";
            public static string ValidatePrice = "Validate Booking Price are correct";
        }

        #endregion
    }
}
