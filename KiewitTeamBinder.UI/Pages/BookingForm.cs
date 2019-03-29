using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using System.Threading;
using System.Globalization;
using KiewitTeamBinder.Common.i18n;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.TestData;

namespace KiewitTeamBinder.UI.Pages.Global
{
    public class BookingForm : LoggedInLanding
    {
        #region Locators
        private By _roomsConfirmed => By.XPath("//span[@id='occupancyDetails']");
        private By _dateComfirmed => By.XPath("//h4[@data-bind='html: checkinCheckoutDetails.checkinCheckoutDate']");

        #endregion
        #region Elements
        public IWebElement RoomComfirmed { get { return StableFindElement(_roomsConfirmed); } }
        public IWebElement DateComfirmed { get { return StableFindElement(_dateComfirmed); } }
        #endregion
        #region Methods
        public KeyValuePair<string, bool> ValidateRoomsAndGuests(string rooms, string guests)
        {
            var node = CreateStepNode();
            try
            {
                
                string actualReSult = String.Format(Resource.RoomsAndGuests, rooms, guests);
                if (RoomComfirmed.Text == actualReSult)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateRoomsAndGuests);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateRoomsAndGuests);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateRoomsAndGuests, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }
        public string ConvertDate(string[] dateToConvert, int mothOrday)
        {
            if (dateToConvert[mothOrday].StartsWith("0"))
            {
                return dateToConvert[mothOrday].Substring(1);
            }
            else
            {
                return dateToConvert[mothOrday];
            }
        }

        public string getExptDate(int month, int duration)
        {
            DateTime checkInDay = DateTime.Now.AddMonths(month);
            DateTime CheckoutDate = DateTime.Now.AddMonths(month).AddDays(duration);
            string actualDate;
            if (Constant.GB_Local != "en-US")
            {
                string[] typeOfCheckInDay = checkInDay.GetDateTimeFormats('d')[0].Split('/');
                string checkInMonthCv = ConvertDate(typeOfCheckInDay, 0);
                string checkInDayCv = ConvertDate(typeOfCheckInDay, 1);
                string yearCheckInCv = typeOfCheckInDay[2];

                string[] typeOfCheckOutDay = CheckoutDate.GetDateTimeFormats('d')[0].Split('/');
                string checkOutMonthCv = ConvertDate(typeOfCheckOutDay, 0);
                string checkOutDayCv = ConvertDate(typeOfCheckOutDay, 1);
                string yearCheckOutCv = typeOfCheckInDay[2];
                actualDate = String.Format(Resource.CheckInAndCheckOutDate, yearCheckInCv, checkInMonthCv, checkInDayCv, yearCheckOutCv, checkOutMonthCv, checkOutDayCv);
                //return actualDate;
            }
            else
            {
                CultureInfo enUS = new CultureInfo("en-US");
                string actualDateRaw = checkInDay.ToString("R", enUS).Substring(4, 12).Replace(" 0", " ") + " - " + CheckoutDate.ToString("R", enUS).Substring(4, 12).Replace("  0", " ");
                actualDate = actualDateRaw.Trim().Replace("  ", " ");
                //actualDate = checkInDay.ToString
                //return actualDate;
            }
            return actualDate;
        } 

        public KeyValuePair<string, bool> ValidateBookingDay(int month, int duration)
        {
            var node = CreateStepNode();
            try
            {
                if (getExptDate(month, duration) == DateComfirmed.Text)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateBookingDate);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.ValidateBookingDate);
                }

            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateBookingDate, e);
            }
            finally
            {
                EndStepNode(node);
            }
            
            
        }

        private static class ValidationMessage
        {
            public static string ValidateBookingDate = "Verify Booking Date is correct";
            public static string ValidateRoomsAndGuests = "Validate Rooms and Guests Booked are correct";
            public static string ValidateAllFieldPopulated = "Validate all field are populated";
        }

        #endregion
        public BookingForm(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}
