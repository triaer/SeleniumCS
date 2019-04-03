using System;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using Agoda.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Agoda.ExtentReportsHelper;

namespace Agoda.Tests
{
    [TestClass]
    public class AgodaTest : UITestBase
    {
        [TestMethod]
        public void Agoda_Test()
        {
            try
            {
                //Given
                test.Info("1. Navigate to www.agoda.com.");
                var driver = Browser.Open(Constant.HomePage, "chrome");

                //When
                test.Info("2. Enter required data.");
                test.Info("3. Click Search");
                /*
                    - City, destination,… as “Phu Quoc”
                    - Check -in date is next 1 month
                    - Duration as 3 nights
                    - 4 guests, 2 rooms
                */
                test.Info("4. Select 'Arcadia Phu Quoc Resort'");
                test.Info("5. Select a specific room type");
                test.Info("6. Enter required data for Information");
                
                AgodaMain agodaPage = new AgodaMain(driver);
                agodaPage.initSearch()
                    .selectHotel("Arcadia Phu Quoc Resort")
                    .selectRoom()
                    .fillInformation();
                
                //Then, VP: 
                test = LogTest("Verify all filled information is correct");
                validations.Add(agodaPage.ValidateFilledInformation());

                Console.WriteLine(string.Join(Environment.NewLine, validations.ToArray()));
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
