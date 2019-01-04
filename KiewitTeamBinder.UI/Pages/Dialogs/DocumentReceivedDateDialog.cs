using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.VendorDataModule;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.Dialogs
{
    public class DocumentReceivedDateDialog : ProcessDocuments
    {
        #region Entities
        private static By _receivedDateIcon => By.XPath("//div[@id='RadDatePickerRecievedDate_wrapper']//a[@id='RadDatePickerRecievedDate_popupButton']");
        private static By _receivedDateData => By.Id("RadDatePickerRecievedDate_calendar_wrapper");
        public static By _yesButton => By.XPath("//button[@id='btnOk' and contains(text(),'Yes')]");
        private static By _iframe => By.Name("RadWindowReceivedDate");

        public IWebElement ReceivedDateData { get { return StableFindElement(_receivedDateIcon); } }
        public IWebElement YesButton { get { return StableFindElement(_yesButton); } }
        public IWebElement Iframe { get { return StableFindElement(_iframe); } }
        #endregion

        #region Actions
        public DocumentReceivedDateDialog(IWebDriver webDriver) : base(webDriver)
        {
           
        }

        public T SelectDate<T>(string recievedDate)
        {
            var node = StepNode();
            WaitForElementClickable(_receivedDateIcon);
            node.Info("Select Date: " + recievedDate);
            SelectDateOnCalendar(recievedDate, ReceivedDateData, _receivedDateData);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public ConfirmDialog ClickYesButton()
        {
            YesButton.Click();
            return new ConfirmDialog(WebDriver);
        }

        #endregion
    }
}
