using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.Common.TestData.Mouser;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Agoda;
using KiewitTeamBinder.UI.Pages.Mouser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Threading;
using static KiewitTeamBinder.Common.DashBoardENums;
using static KiewitTeamBinder.Common.DigikeyEnum;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.Mouser
{
    [TestClass]
    public class MouserTest : UITestBase
    {
        [TestMethod]
        public void Mouser_TC01_ChromeEN()
        {
            try
            {
                test.Info("Navigate to www.mouser.com");
                var driver = Browser.Open(Constant.DigikeyPage, "chrome");
                var mouserData = new DigikeyData();

                test = LogTest("TC01 - Test case Mouser Chrome - English");
                MouserHome mouserHome = new MouserHome(driver);
                MouserProductsDetail mouserProductsDetail = new MouserProductsDetail(driver);
                mouserHome.SelectLocation(Location.VietNam.ToDescription())
                    .OpenAllProductPage()
                    .OpenSpecificProductList("Thermal Management", "Thermistors")
                    .SelectProduct(mouserData.selectedProduct)
                    .OpenProductDetailInfor(mouserData.selectedProduct[0])
                    .LogValidation<MouserProductsDetail>(ref validations, mouserProductsDetail.ValidateMouserAndMfrInfo())
                    .BackToPreviousPage<MouserProductsList>()
                    .OpenProductDetailInfor(mouserData.selectedProduct[1])
                    .LogValidation<MouserProductsDetail>(ref validations, mouserProductsDetail.ValidateMouserAndMfrInfo())
                    .BackToPreviousPage<MouserProductsList>()
                    .OpenProductDetailInfor(mouserData.selectedProduct[2])
                    .LogValidation<MouserProductsDetail>(ref validations, mouserProductsDetail.ValidateMouserAndMfrInfo())
                    .BackToPreviousPage<MouserProductsList>();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
