//using KiewitTeamBinder.Common.Helper;
//using KiewitTeamBinder.UI.Pages;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using OpenQA.Selenium;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace KiewitTeamBinder.UI.Tests.User
//{
//    [TestClass]
//    public class MouserTests:UITestBase
//    {
//        [TestMethod]
//        public void TC02()
//        {
//            try
//            {
//                //given
//                var driver = Browser.Open(Constant.MouserVN, "chrome");

//                //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
//                //js.ExecuteScript("localStorage.setItem('lpLastVisit-12757882','1554105342430');");
//                //js.ExecuteScript("localStorage.setItem('SZMKSessionId', {\"time\":1554112731778,\"id\":\"1016452831274060622\"});");
                

//                //when
//                test.Info("Navigate to www.mouser.vn");
//                MouserMain mouserMain = new MouserMain(driver);

//                mouserMain.SelectSubMenuProducts<Thermistors>("Thermal Management", "Thermistors");

//                //then
//            }
//            catch (Exception e)
//            {

//                throw;
//            }
//        }
//    }
//}
