using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KiewitTeamBinder.UI.Pages.Digikey
{
    public class DigikeyCart : DigikeyGeneral
    {
        public DigikeyCart(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _lnkProducts => By.XPath("//a[contains(@class,'products')]");
        static By _lnkDigikeyPartNumber => By.XPath("//div[@class='cart-partNumber']/a");
        static By _txtQty => By.XPath("//input[contains(@class,'part-quantity text-box')]");
        static By _txtCusRef => By.XPath("//input[@class='customer-reference']");
        static By _chkDelete(int qty) => By.XPath($"//input[@class='deleteCheckbox'][position()<={qty}]");
        #endregion

        #region Elements
        public IWebElement LnkProducts => StableFindElement(_lnkProducts);
        public IReadOnlyCollection<IWebElement> LnkDigikeyPartNumber => StableFindElements(_lnkDigikeyPartNumber);
        public IReadOnlyCollection<IWebElement> TxtQty => StableFindElements(_txtQty);
        public IReadOnlyCollection<IWebElement> TxtCusRef => StableFindElements(_txtCusRef);
        public IReadOnlyCollection<IWebElement> ChkDelete(int qty) => StableFindElements(_chkDelete(qty));
        #endregion

        #region Methods

        #endregion
    }
}
