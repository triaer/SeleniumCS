using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.Common.TestData.Digikey;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Digikey;
using KiewitTeamBinder.UI.Pages.Home;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Threading;
using static KiewitTeamBinder.Common.DigikeyEnum;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.Digikey
{
    [TestClass]
    public class DigikeyTest : UITestBase
    {
        [TestMethod]
        public void Digikey_TC01_ChromeEN()
        {
            try
            {
                test.Info("Navigate to www.mouser.com");
                var driver = Browser.Open(Constant.DigikeyPage, "chrome");
                var digikeyData = new DigikeyData();

                test = LogTest("TC01 - Test case Mouser Chrome - English");
                DigikeyHome digikeyHome = new DigikeyHome(driver);
                DigikeyCompare digikeyCompare = new DigikeyCompare(driver);
                digikeyHome.OpenAllProductPage()
                    .OpenSpecificProductList(digikeyData.productSection, digikeyData.productType)
                    .SelectProduct(3)
                    .LogValidation<DigikeyCompare>(ref validations, digikeyCompare.ValidateDigikeyAndMfrInfo());
                digikeyCompare.BackToDigikeyProductList()
                    .AddProductsToCart(digikeyData.productType, 3, digikeyData.quantity, digikeyData.customerRefence);
                    
                

            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
