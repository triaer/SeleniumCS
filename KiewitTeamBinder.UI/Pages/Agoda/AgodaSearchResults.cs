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

namespace KiewitTeamBinder.UI.Pages.Agoda
{
    public class AgodaSearchResults : AgodaGeneral
    {
        public AgodaSearchResults(IWebDriver webDriver) : base(webDriver)
        {
        }

        #region Locators
        static By _choosePlace(string placeName) => By.XPath($"//h3[contains(text(),'{placeName}')]/ancestor::a");
        static By _txtSearch => By.XPath("//div[@class='TextSearchContainer']//input");

        #endregion

        #region Elements
        public IWebElement ChoosePlace(string placeName) => StableFindElement(_choosePlace(placeName));
        public IWebElement TxtSearch => StableFindElement(_txtSearch);

        #endregion

        #region Methods
        public AgodaHotelDetail SelectPlaceToStay(string placeName)
        {
            var node = CreateStepNode();
            node.Info("Select place to stay: " + placeName);
            //ScrollToElement(_choosePlace(placeName));
            TxtSearch.InputText(placeName);
            TxtSearch.ActionsPressEnter();
            string hotelUrl = ChoosePlace(placeName).GetAttribute("href");
            WebDriver.Navigate().GoToUrl(hotelUrl);
            EndStepNode(node);
            return new AgodaHotelDetail(WebDriver);
        }

        #endregion
    }
}
