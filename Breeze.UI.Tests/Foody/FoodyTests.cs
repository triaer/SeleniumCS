using Breeze.Common.Helper;
using KiewitTeamBinder.UI.Pages.Foody;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.UI.Tests.Foody
{
    [TestClass]
    public class FoodyTests: UITestBase
    {
        [TestMethod]
        public void Test()
        {
            try
            {
                Browser.Open("chrome", Constant.Foody);
                HomePage home = new HomePage();
                home.OpenLoginPage().Login("striker9917@gmail.com", "Th99171590");
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }
    }
}
