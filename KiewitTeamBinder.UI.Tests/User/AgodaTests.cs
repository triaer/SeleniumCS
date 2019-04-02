using FluentAssertions;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SimpleImpersonation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.User
{
    [TestClass]
    public class AgodaTests : UITestBase
    {

        [TestMethod]
        public void TC001()
        {

            try
            {
                //given
                var driver = Browser.Open(Constant.AgodaCNPage, "chrome");

                //when
                test.Info("Navigate to www.agoda.com.");
                AgodaTestsSmoke agodaData = new AgodaTestsSmoke();
                AgodaLogin agodaLogin = new AgodaLogin(driver);
                
                agodaLogin.EnterRequiredData(agodaData.Place, agodaData.Month, agodaData.Duration, agodaData.Rooms, agodaData.Guests);
                AgodaResults agodaResults = agodaLogin.ClickSearchButton<AgodaResults>();
                AgodaBookingRoom agodaBookingRoom = agodaResults.SelectHotel(agodaData.HotelName);
                BookingForm bookingForm = agodaBookingRoom.SelectSpecificRoomType();
                            bookingForm.LogValidation<BookingForm>(ref validations, bookingForm.ValidateRoomsAndGuests(agodaData.Rooms, agodaData.Guests))
                                       .LogValidation<BookingForm>(ref validations, bookingForm.ValidateBookingDay(agodaData.Month, agodaData.Duration));                              
                
                //then
                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }
        
    }
}
