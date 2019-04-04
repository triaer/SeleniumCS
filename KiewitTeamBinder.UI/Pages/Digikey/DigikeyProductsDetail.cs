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

namespace KiewitTeamBinder.UI.Pages.Digikey
{
    public class DigikeyProductsDetail : DigikeyGeneral
    {
        public DigikeyProductsDetail(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _txtQty => By.XPath("//input[@name='qty']");
        static By _txtCustomerReference => By.XPath("//input[@name='cref']");
        static By _btnAddToCart => By.XPath("//input[@id='addtoorderbutton']");
        #endregion

        #region Elements
        public IWebElement TxtQty => StableFindElement(_txtQty);
        public IWebElement TxtCustomerReference => StableFindElement(_txtCustomerReference);
        public IWebElement BtnAddToCart => StableFindElement(_btnAddToCart);

        #endregion

        #region Methods


        private static class ValidationMessage
        {
            public static string ValidateMouserAndMfrInfo = "Validate Mouser and Mfr information are correct.";
            public static string ValidateMouserInfo = "Validate Mouser information is correct.";
            public static string ValidatedMfrInfo = "Validate Mfr information is correct.";
        }

        #endregion
    }
}
