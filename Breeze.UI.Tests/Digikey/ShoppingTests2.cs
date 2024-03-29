﻿using FluentAssertions;
using Breeze.Common;
using Breeze.Common.Helper;
using Breeze.Common.Models;
using Breeze.Common.TestData;
using Breeze.UI.Pages;
using Breeze.UI.Pages.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Protractor;
using SimpleImpersonation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using static Breeze.Common.BreezeENums;
using static Breeze.UI.ExtentReportsHelper;

namespace Breeze.UI.Tests.Digikey
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
