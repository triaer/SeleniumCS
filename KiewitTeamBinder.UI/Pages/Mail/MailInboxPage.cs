using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.UI.KiewitTeamBinderENums;

namespace KiewitTeamBinder.UI.Pages.Mail
{
    class MailInboxPage : LoggedInLanding
    {
        #region Entities

        private static By _defaultViewLabel(string value) => By.XPath($"//li[@class='rfItem']/span[text()='{value}']");
        private static By _itemsNumberLabel(string value) => By.XPath($"//span[contains(@id, 'GridView{value}_ctl00DSC')]");
        private static By _refreshButton => By.XPath("//span[@class='rtbIn' and text() = 'Refresh']");


        public IWebElement DefaultViewLabel(string value) => StableFindElement(_defaultViewLabel(value));
        public IWebElement ItemsNumberLabel(string value) => StableFindElement(_itemsNumberLabel(value));
        public IWebElement RefreshButton { get { return StableFindElement(_refreshButton); } }

        #endregion

        #region Actions
        public MailInboxPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public KeyValuePair<string, bool> ValidateViewFilterOption(string value)
        {

            var node = StepNode();
            node.Info($"Validate the view filter option is set to: '{value}' view");

            try
            {
                if (DefaultViewLabel(value).IsDisplayed() == true)
                    return SetPassValidation(node, Validation.View_Filter_Option_Is_Displayed);

                return SetFailValidation(node, Validation.View_Filter_Option_Is_Not_Displayed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.View_Filter_Option_Is_Not_Displayed, e);
            }
        }

        public KeyValuePair<string, bool> ValidateRecordItemsCount(int itemsNumber, string moduleName)
        {
            var node = StepNode();
            node.Info($"Validate number of items is equals to: {itemsNumber}");

            try
            {
                var actualQuantity = ItemsNumberLabel(moduleName).Text;
                if (Int32.Parse(actualQuantity) == itemsNumber)
                    return SetPassValidation(node, Validation.Number_Of_Items_Counted_Is_Valid);

                return SetFailValidation(node, Validation.Number_Of_Items_Counted_Is_Invalid, "Actual number is: " + actualQuantity);

            }
            catch (Exception e)
            {

                return SetErrorValidation(node, Validation.Number_Of_Items_Counted_Is_Invalid, e);
            }
        }



        private static class Validation
        {
            public static string View_Filter_Option_Is_Displayed = "Validate That The View Filter Option Is Displayed";
            public static string View_Filter_Option_Is_Not_Displayed = "Validate That The View Filter Option Is Not Displayed";
            public static string Number_Of_Items_Counted_Is_Valid = "Validate That Number of Items Counted Is Valid";
            public static string Number_Of_Items_Counted_Is_Invalid = "Validate That Number of Items Counted Is Invalid";
        }
        #endregion

    }
}
