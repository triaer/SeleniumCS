using FluentAssertions;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models;
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
                bookingInfo.CheckOutDate = DateTime.Today.AddMonths(1).AddDays(3);
                bookingInfo.TravelerType = TravelerType.Group;
                bookingInfo.Room = 2;
                bookingInfo.Adults = 4;
                //1. Navigate to Agoda home page.
                //2. Select a language
                //3. Select a currency
                test.Info("Navigate to Agoda home page.");
                var driver = Browser.Open(Constant.AgodaHomePage, browser, language, currency);

                AgodaHomePage homePage = new AgodaHomePage(driver);
                homePage.EnterBookingInfo(bookingInfo);
                //When

            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }
    }
}
