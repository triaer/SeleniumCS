using FluentAssertions;
using SelenCS.Common;
using SelenCS.Common.Helper;
using SelenCS.Common.Models;
using SelenCS.Common.TestData;
using SelenCS.UI.Pages;
using SelenCS.UI.Pages.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Protractor;
using SimpleImpersonation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using static SelenCS.Common.SelenCSENums;
using static SelenCS.UI.ExtentReportsHelper;

namespace SelenCS.UI.Tests.Digikey
{
    [TestClass]
    public class ShoppingTests2 : UITestBase
    {
        [TestMethod]
        public void TC003()
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
                Browser.Open(Constant.DigikeyHomePage, "firefox");

                test = LogTest("DIGIKEY_SHOPPING_TC003 - Verify that user can add, edit, delete product in cart successfully.");
                DigikeyHomePage homePage = new DigikeyHomePage();
                DigikeyProductListPage productListPage = homePage.SelectProductMenu()
                                                                 .SelectTargetProductCategory(testData.Category, testData.SubCategory);



            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void TC004()
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
                Browser.Open(Constant.DigikeyHomePage, "chrome");

                test = LogTest("DIGIKEY_SHOPPING_TC004 - Verify that user can add, edit, delete product in cart successfully.");
                DigikeyHomePage homePage = new DigikeyHomePage();
                DigikeyProductListPage productListPage = homePage.SelectProductMenu()
                                                                 .SelectTargetProductCategory(testData.Category, testData.SubCategory);



            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }
    }
}
