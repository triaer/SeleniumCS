//using FluentAssertions;
//using KiewitTeamBinder.Common;
//using KiewitTeamBinder.Common.Helper;
//using KiewitTeamBinder.Common.TestData;
//using KiewitTeamBinder.UI.Pages;
//using KiewitTeamBinder.UI.Pages.Global;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using OpenQA.Selenium;
//using SimpleImpersonation;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Threading;
//using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
//using static KiewitTeamBinder.UI.ExtentReportsHelper;

//namespace KiewitTeamBinder.UI.Tests.User
//{
//    [TestClass]
//    public class AgodaTests : UITestBase
//    {

//        [TestMethod]
//        public void TC001()
//        {

//            try
//            {
//                //given
//                var driver = Browser.Open(Constant.AgodaPage, "chrome", Constant.GB_Local);

//                //when
//                test.Info("Navigate to www.agoda.com.");
//                AgodaTestsSmoke agodaData = new AgodaTestsSmoke();
//                AgodaLogin agodaLogin = new AgodaLogin(driver);

//                test = LogTest("ABC");
//                agodaLogin.EnterRequiredData(agodaData.bookingInfo);
//                AgodaResults agodaResults = agodaLogin.ClickSearchButton<AgodaResults>();
//                AgodaBookingRoom agodaBookingRoom = agodaResults.SelectHotel(agodaData.HotelName);
//                BookingForm bookingForm = agodaBookingRoom.SelectSpecificRoomType();
//                bookingForm.LogValidation<BookingForm>(ref validations, bookingForm.ValidateRoomsAndGuests(agodaData.bookingInfo.Rooms, agodaData.bookingInfo.Guests))
//                           .LogValidation<BookingForm>(ref validations, bookingForm.ValidateBookingDay(agodaData.bookingInfo.Month, agodaData.bookingInfo.Duration));

//                //then
//                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
//                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
//            }
//            catch (Exception e)
//            {
//                lastException = e;
//                throw;
//            }
//        }

//        [TestMethod]
//        public void TC002()
//        {
//            try
//            {
//                //given
//                var driver = Browser.Open(Constant.AgodaPage, "chrome", Constant.GB_Local);

//                //when
//                test.Info("Navigate to www.agoda.com.");
//                AgodaTestsSmoke agodaData = new AgodaTestsSmoke();
//                AgodaLogin agodaLogin = new AgodaLogin(driver);

//                test = LogTest("TC002");
//                agodaLogin.EnterRequiredData(agodaData.bookingInfo);
//                AgodaResults agodaResults = agodaLogin.ClickSearchButton<AgodaResults>()
//                                                      .SelectPriceRange(FilterAgoda.Price.ToDescription(), agodaData.LeftPercent, agodaData.RightPercent);

//            }
//            catch (Exception e)
//            {
//                lastException = e;
//                throw;
//            }
//        }

//    }
//}
