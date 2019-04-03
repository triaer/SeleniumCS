using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.Common.TestData.Agoda;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Agoda;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Threading;
using static KiewitTeamBinder.Common.DashBoardENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.Agoda
{
    [TestClass]
    public class AgodaTest : UITestBase
    {
        [TestMethod]
        public void Agoda_TC01_ChromeEN()
        {
            try
            {
                test.Info("Navigate to www.agoda.com");
                var driver = Browser.Open(Constant.AgodaPage, browser);
                var agodaData = new AgodaData();

                test = LogTest("TC01 - Test case Agoda Chrome - English");
                AgodaMain agodaMain = new AgodaMain(driver);
                AgodaPayment agodaPayment = new AgodaPayment(driver);
                agodaMain.SearchForStayPlace(agodaData.placeToStay)
                    .SelectPlaceToStay(agodaData.choosenPlace)
                    .SelectRoom(agodaData.room)
                    .EnterCustomerInformation(agodaData.customerInfoFullname)
                    .BackToBookingDetails()
                    .LogValidation<AgodaPayment>(ref validations, agodaPayment.ValidateCustomerInfo(agodaData.customerInfoFullname))
                    .LogValidation<AgodaPayment>(ref validations, agodaPayment.ValidateHotelInformation(agodaData.choosenPlace))
                    .LogValidation<AgodaPayment>(ref validations, agodaPayment.ValidateCheckinCheckoutDateAndDuration(agodaData.placeToStay))
                    .LogValidation<AgodaPayment>(ref validations, agodaPayment.ValidateOccupancy(agodaData.placeToStay, agodaData.room))
                    .LogValidation<AgodaPayment>(ref validations, agodaPayment.ValidateRoomPrice(agodaData.placeToStay, agodaData.room));
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void Agoda_TC01_ChromeCN()
        {
            try
            {
                test.Info("Navigate to www.agoda.com");
                var driver = Browser.Open(Constant.AgodaCNPage, "chrome");
                var agodaData = new AgodaData();

                test = LogTest("TC01 - Test case Agoda Chrome - Chinese");
                AgodaMain agodaMain = new AgodaMain(driver);
                AgodaPayment agodaPayment = new AgodaPayment(driver);
                agodaMain.SearchForStayPlace(agodaData.placeToStayWithCurrency)
                    .SelectPlaceToStay(agodaData.choosenPlace)
                    .SelectRoom(agodaData.room)
                    .EnterCustomerInformation(agodaData.customerInfor)
                    .BackToBookingDetails()
                    .LogValidation<AgodaPayment>(ref validations, agodaPayment.ValidateCustomerInfo(agodaData.customerInfor))
                    .LogValidation<AgodaPayment>(ref validations, agodaPayment.ValidateHotelInformation(agodaData.choosenPlace))
                    .LogValidation<AgodaPayment>(ref validations, agodaPayment.ValidateCheckinCheckoutDateAndDuration(agodaData.placeToStay))
                    .LogValidation<AgodaPayment>(ref validations, agodaPayment.ValidateOccupancy(agodaData.placeToStayWithCurrency, agodaData.room))
                    .LogValidation<AgodaPayment>(ref validations, agodaPayment.ValidateRoomPrice(agodaData.placeToStayWithCurrency, agodaData.room));
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        public void demoMultiLanguage()
        {
            test.Info("Navigate to www.agoda.com");
            var driver = Browser.Open(Constant.AgodaCNPage, "chrome");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");

        }

    }
}
