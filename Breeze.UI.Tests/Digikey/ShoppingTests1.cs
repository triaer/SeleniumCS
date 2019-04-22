using Breeze.Common.Helper;
using Breeze.Common.TestData;
using Breeze.UI.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Breeze.UI.ExtentReportsHelper;

namespace Breeze.UI.Tests.Digikey
{
    [TestClass]
    public class ShoppingTests1 : UITestBase
    {
        [TestMethod]
        public void TC002()
        {

            try
            {
                //Given
                //0. Prepare data
                DigiBookingTestSmoke testData = new DigiBookingTestSmoke();

                //1. Navigate to www.digikey.com.
                //2. Select Products on top menu
                //3. Select Accessories under Battery Products section
                test.Info("Navigate to Digikey home page.");
                Browser.Open(Constant.DigikeyHomePage, browser);

                test = LogTest("DIGIKEY_SHOPPING_TC002 - Verify that user can add, edit, delete product in cart successfully.");
                DigikeyHomePage homePage = new DigikeyHomePage();
                DigikeyProductComparisonPage comparePage = homePage.SelectProductMenu()
                                                                 .SelectTargetProductCategory(testData.Category, testData.SubCategory)
                                                                 .SelectProductsAndCompare(testData.Products);



            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }
    }
}
