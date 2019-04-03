using FluentAssertions;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.Resource;
using KiewitTeamBinder.UI.Common;
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
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.Agoda
{
    [TestClass]
    public class BookingTests : UITestBase
    {

        [TestMethod]
        public void TC001()
        {

            try
            {
                //Given
                //0. Prepare data
                BookingInfo bookingInfo = new BookingInfo();
                bookingInfo.Destination = "Phu Quoc";
                bookingInfo.CheckInDate = DateTime.Today.AddMonths(1);
                bookingInfo.Duration = 3;
                bookingInfo.TravelerType = TravelerType.Group;
                bookingInfo.Room = 2;
                bookingInfo.Adults = 4;

                HotelInfo hotelInfo = new HotelInfo();
                hotelInfo.HotelName = "Arcadia Phu Quoc Resort";
                hotelInfo.RoomName = "Superior Garden View";
                hotelInfo.RoomQuantity = 2;

                //1. Navigate to Agoda home page.
                //2. Enter booking info and click search
                //3. Select “Arcadia Phu Quoc Resort”
                test.Info("Navigate to Agoda home page.");
                var driver = Browser.Open(Constant.AgodaHomePage, browser, language, currency);
                AgodaHomePage homePage = new AgodaHomePage(driver);

                test = LogTest("AGODA_BOOKING_TC001 - Verify that user can book hotel successfully.");
                
                AgodaHotelDetailPage hotelDetailPage = homePage.EnterBookingInfo(bookingInfo)
                                                               .SearchAndSelectHotel(hotelInfo);
                //When
                //4. Select a specific room type
                AgodaBookingFormPage bookingFormPage = hotelDetailPage.SelectSpecificRoom(hotelInfo);

                //Then
                //VP: Verify all filled information is correct
                validations.Add(bookingFormPage.ValidateHotelName(hotelInfo));
                validations.Add(bookingFormPage.ValidateBookingDateRange(bookingInfo));
                validations.Add(bookingFormPage.ValidateBookingDuration(bookingInfo));
                validations.Add(bookingFormPage.ValidateRoomTypeList(hotelInfo));
                validations.Add(bookingFormPage.ValidateBookingOccupancy(bookingInfo));
                //validations.Add(bookingFormPage.ValidatePrice(hotelInfo));
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
