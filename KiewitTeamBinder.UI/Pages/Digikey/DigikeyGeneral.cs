using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.DashBoardENums;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.Common.AgodaEnums;
using KiewitTeamBinder.Common.Models.Agoda;
using OpenQA.Selenium.Support.UI;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace KiewitTeamBinder.UI.Pages.Digikey
{
    public class DigikeyGeneral : LoggedInLanding
    {
        public DigikeyGeneral(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _lnkProducts => By.XPath("//a[contains(@class,'products')]");
        

        #endregion

        #region Elements
        public IWebElement LnkProducts => StableFindElement(_lnkProducts);

        #endregion

        #region Methods
        public DigikeyAllProducts OpenAllProductPage()
        {
            var node = CreateStepNode();
            node.Info("Open All Products page.");
            LnkProducts.Click();
            EndStepNode(node);
            return new DigikeyAllProducts(WebDriver);
        }

        public T BackToPreviousPage <T>()
        {
            var node = CreateStepNode();
            node.Info("Back to previous page.");
            WebDriver.Navigate().Back();
            EndStepNode(node);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }
        #endregion
    }
}
