using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Pages.Popup
{
    public class AgodaDayPicker : PageBase
    {

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaDayPicker");

        private By _popupDayPicker => locator.Get("_popupDayPicker");
        private By _elePreviousMonth => locator.Get("_elePreviousMonth");
        private By _eleNextMonth => locator.Get("_eleNextMonth");
        private By _eleTargetDay(string targetDay) => locator.Get("_eleTargetDay", targetDay);

        #endregion

        #region Elements
        public IWebElement PopupDayPicker => StableFindElement(_popupDayPicker);
        public IWebElement ElementPreviousMonth => StableFindElement(_elePreviousMonth);
        public IWebElement ElementNextMonth => StableFindElement(_eleNextMonth);
        public IWebElement ElementTargetDay(string targetDay) => StableFindElement(_eleTargetDay(targetDay));
        #endregion

        #region Methods

        public AgodaDayPicker(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElementDisplay(_popupDayPicker);
        }

        #endregion
    }
}
