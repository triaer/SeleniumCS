using FluentAssertions;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.i18n;
using KiewitTeamBinder.Common.TestData;
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
using System.Threading;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.User
{
    [TestClass]
    public class PilotTC : UITestBase
    {

        [TestMethod]
        public void TCPLi18n()
        {

            try
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("ko-KR");
                Console.WriteLine(Resource.Hello);



                //Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                //validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

    }
}
