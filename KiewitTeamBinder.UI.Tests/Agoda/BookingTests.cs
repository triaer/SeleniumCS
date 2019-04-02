using FluentAssertions;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
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
                //1. Navigate to Dashboard login page.
                //2. Select a language
                //3. Select a currency
                test.Info("Navigate to Agoda home page.");
                var driver = Browser.Open(Constant.AgodaHomePage, browser);
                AgodaHomePage homePage = new AgodaHomePage(driver);
                homePage.SelectLanguage(Language.VIETNAM);
                homePage.SelectCurrency(Currency.DOLLAR);

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
