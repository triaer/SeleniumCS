using KiewitTeamBinder.UI.Pages.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages
{
    public class CompareDigiKey : LoggedInLanding
    {
        #region Locators
        private By _backButton => By.XPath("(//a[contains(text(),'Back')])[1]");
        private By _searchIcon => By.XPath("//button[@id='header-search-button']");
        private By _digiKey => By.XPath("//tr[1]//td/a");
        private By _manuNumber => By.XPath("//tr[2]//td");
        #endregion

        #region Elements
        public IWebElement BackButton { get { return StableFindElement(_backButton); } }
        public List<IWebElement> DigiKey => StableFindElements(_digiKey).ToList();
        public List<IWebElement> ManuNumber => StableFindElements(_manuNumber).ToList();
        #endregion

        #region Methods
        List<string> ExpDigiKeys;
        List<string> ExpManuNumbers;
        public T ClickBackButton<T>()
        {
            BackButton.Click();
            WaitForElement(_searchIcon);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);

        }
        public bool CompareDigiKeypart()
        {
            DigiKeyTestsSmoke digiData = new DigiKeyTestsSmoke();
            SubCategoryDigiKey SCD = new SubCategoryDigiKey(WebDriver);
            //List<string> digiKeyExpt =  SCD.getDigiKeys(digiData.Quantity);
                for (int i = 0; i < digiData.Quantity; i++)
                {
                string a = DigiKey[i].Text;
                    if (ExpDigiKeys[i] != DigiKey[i].Text)
                    {
                        return false;
                    }
                }
            
            return true;
        }

        public bool CompareDigiManuNumber()
        {
            DigiKeyTestsSmoke digiData = new DigiKeyTestsSmoke();
            SubCategoryDigiKey SCD = new SubCategoryDigiKey(WebDriver);
            //List<string> ManuExpt = CompareDigiKey.
            for (int i = 0; i < digiData.Quantity; i++)
            {
                if (ExpManuNumbers[i] != ManuNumber[i].Text)
                {
                    return false;
                }
            }

            return true;
        }

        public KeyValuePair<string, bool> ValidateNumber()
        {
            var node = CreateStepNode();
            try
            {
                if (CompareDigiKeypart() == true && CompareDigiManuNumber() ==true)
                {
                    return SetPassValidation(node, ValidationMessage.ValidateNumber);
                }
                return SetFailValidation(node, ValidationMessage.ValidateNumber);
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateNumber, e);
            }
        }

        #endregion

        public CompareDigiKey(IWebDriver webDriver) : base(webDriver)
        {
         
        }
        public CompareDigiKey(IWebDriver webDriver, List<string> digiKeyNumber, List<string> manuNumber) : base(webDriver)
        {
            ExpDigiKeys = digiKeyNumber;
            ExpManuNumbers = manuNumber;
        }

        private static class ValidationMessage
        {
            public static string ValidateNumber = "verify Digi-Key Part Number & Manufacturer Part Number is correct";
            
        }
    }
}
