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
    public class DigikeyCompare : DigikeyGeneral
    {
        public DigikeyCompare(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _lnkDigikeyPartNumber => By.XPath("//th[normalize-space(text())='Digi-Key Part Number']/following-sibling::td/a");
        static By _lnkMrfPartNumber => By.XPath("//th[normalize-space(text())='Manufacturer Part Number']/following-sibling::td");
        static By _lnkBackToSearchResult => By.XPath("(//a[text()='Back to Search Results'])[1]");
        #endregion

        #region Elements
        public IReadOnlyCollection<IWebElement> LnkDigikeyPartNumber => StableFindElements(_lnkDigikeyPartNumber);
        public IReadOnlyCollection<IWebElement> LnkMrfPartNumber => StableFindElements(_lnkMrfPartNumber);
        public IWebElement LnkBackToSearchResult => StableFindElement(_lnkBackToSearchResult);
        #endregion

        #region Methods
        public List<KeyValuePair<string, bool>> ValidateDigikeyAndMfrInfo()
        {
            var node = CreateStepNode();
            var validations = new List<KeyValuePair<string, bool>>();
            try
            {
                string[] expectedDigikey = Constant.digikeyPartNumber;
                string[] expectedMrf = Constant.digikeyMrfNumber;
                List<string> tempActualDigikey = new List<string>();
                foreach (var item in LnkDigikeyPartNumber)
                {
                    tempActualDigikey.Add(item.Text);
                }
                string[] actualDigikey = tempActualDigikey.ToArray();
                List<string> tempActualMrf = new List<string>();
                foreach (var item in LnkMrfPartNumber)
                {
                    tempActualMrf.Add(item.Text);
                }
                string[] actualMrf = tempActualMrf.ToArray();
                if (actualDigikey.Equals(expectedDigikey))
                    validations.Add(SetPassValidation(node, ValidationMessage.ValidateDigikeyInfo));
                else
                    validations.Add(SetFailValidation(node, ValidationMessage.ValidateDigikeyInfo));
                if (actualMrf.Equals(expectedMrf))
                    validations.Add(SetPassValidation(node, ValidationMessage.ValidatedMfrInfo));
                else
                    validations.Add(SetFailValidation(node, ValidationMessage.ValidatedMfrInfo));
            }
            catch (Exception e)
            {
                validations.Add(SetErrorValidation(node, ValidationMessage.ValidateDigikeyAndMfrInfo, e));
            }
            EndStepNode(node);
            return validations;
        }

        public DigikeyProductsList BackToDigikeyProductList()
        {
            var node = CreateStepNode();
            node.Info("Back to Digikey Product List page.");
            LnkBackToSearchResult.Click();
            EndStepNode(node);
            return new DigikeyProductsList(WebDriver);
        }

        private static class ValidationMessage
        {
            public static string ValidateDigikeyAndMfrInfo = "Validate Digikey number and Mfr information are correct.";
            public static string ValidateDigikeyInfo = "Validate Digikey number information is correct.";
            public static string ValidatedMfrInfo = "Validate Mfr information is correct.";
        }

        #endregion
    }
}
