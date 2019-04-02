using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.UI.Common;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.Popup
{
    public class AgodaTravelerSelection : PageBase
    {
        private string dateTimeFormat = "ddd MMM dd yyyy";
        private CultureInfo culture = CultureInfo.GetCultureInfo("en-US");

        #region Locators
        static readonly LocatorLoader locator = new LocatorLoader("AgodaTravelerSelection");

        private By _popupTravelerSelection => locator.Get("_popupTravelerSelection");
        private By _eleTravelerType(string target) => locator.Get("_eleTravelerType", target);
        private By _eleRoomMinus => locator.Get("_eleRoomMinus");
        private By _eleRoomPlus => locator.Get("_eleRoomPlus");
        private By _lblRoomNumber => locator.Get("_lblRoomNumber");
        private By _eleAdultsMinus => locator.Get("_eleAdultsMinus");
        private By _eleAdultsPlus => locator.Get("_eleAdultsPlus");
        private By _lblAdultsNumber => locator.Get("_lblAdultsNumber");
        private By _eleChildrenMinus => locator.Get("_eleChildrenMinus");
        private By _eleChildrenPlus => locator.Get("_eleChildrenPlus");
        private By _lblChildrenNumber => locator.Get("_lblChildrenNumber");
        #endregion

        #region Elements
        public IWebElement TravelerSelectionPopup => StableFindElement(_popupTravelerSelection);
        public IWebElement TravelerTypeElement(string target) => StableFindElement(_eleTravelerType(target));
        public IWebElement RoomMinusElement => StableFindElement(_eleRoomMinus);
        public IWebElement RoomPlusElement => StableFindElement(_eleRoomPlus);
        public IWebElement RoomNumberElement => StableFindElement(_lblRoomNumber);
        public IWebElement AdultsMinusElement => StableFindElement(_eleAdultsMinus);
        public IWebElement AdultsPlusElement => StableFindElement(_eleAdultsPlus);
        public IWebElement AdultsNumberElement => StableFindElement(_lblAdultsNumber);
        public IWebElement ChildrenMinusElement => StableFindElement(_eleChildrenMinus);
        public IWebElement ChildrenPlusElement => StableFindElement(_eleChildrenPlus);
        public IWebElement ChildrenNumberElement => StableFindElement(_lblChildrenNumber);

        #endregion


        #region Methods

        public AgodaTravelerSelection(IWebDriver webDriver) : base(webDriver)
        {
            WaitForElementDisplay(_popupTravelerSelection);
        }

        public void EnterTravelerInfo(BookingInfo info)
        {
            TravelerTypeElement(Enum.GetName(typeof(TravelerType), info.TravelerType).ToLower()).Click();
            if (info.TravelerType == TravelerType.Families ||
                info.TravelerType == TravelerType.Group ||
                info.TravelerType == TravelerType.Business)
            {
                SelectRoomQuantity(info.Room);
                SelectAdultsQuantity(info.Adults);
                if (info.TravelerType != TravelerType.Business)
                {
                    SelectChildrenQuantity(info.Children);
                }
            }
        }

        public void SelectRoomQuantity(int quantity)
        {
            var node = ExtentReportsHelper.GetLastNode();
            node.Info(String.Format("Select Room Quantity: {0}", quantity));
            int currentQuantity = Convert.ToInt32(RoomNumberElement.Text);
            var direction = RoomMinusElement;
            if (quantity > currentQuantity)
            {
                direction = RoomPlusElement;
            }
            
            for(int i=0; i< Math.Abs(quantity- currentQuantity); i++)
            {
                direction.Click();
            }
        }

        public void SelectAdultsQuantity(int quantity)
        {
            var node = ExtentReportsHelper.GetLastNode();
            node.Info(String.Format("Select Adults Quantity: {0}", quantity));
            int currentQuantity = Convert.ToInt32(AdultsNumberElement.Text);
            var direction = AdultsMinusElement;
            if (quantity > currentQuantity)
            {
                direction = AdultsPlusElement;
            }

            for (int i = 0; i < Math.Abs(quantity - currentQuantity); i++)
            {
                direction.Click();
            }
        }

        public void SelectChildrenQuantity(int quantity)
        {
            var node = ExtentReportsHelper.GetLastNode();
            node.Info(String.Format("Select Children Quantity: {0}", quantity));
            int currentQuantity = Convert.ToInt32(ChildrenNumberElement.Text);
            var direction = ChildrenMinusElement;
            if (quantity > currentQuantity)
            {
                direction = ChildrenPlusElement;
            }

            for (int i = 0; i < Math.Abs(quantity - currentQuantity); i++)
            {
                direction.Click();
            }
        }

        #endregion
    }
}
