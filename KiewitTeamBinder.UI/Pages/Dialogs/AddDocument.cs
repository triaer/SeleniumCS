using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class AddDocument : LinkItem
    {
        #region Entities 
        private string _checkBoxXpath = "\\td[contains(@id, 'CheckBox')";
        private static By _toobarBottomButton(string value, string nameButton) => By.XPath($"//input[@value='{value}' and contains(@name, '{nameButton}')]");
        private static By _iframeWrapper => By.Name("RadWindowAddItems");
        private static By _iframe => By.Id("ifrmFilter");
        private static By _DocumentNoTextBox => By.Id("DataSelectionDocNo_RadTextBoxValue");
        private static By _gridVewData => By.XPath("//table[@id='GridView_ctl00']/tbody");

        public IWebElement IframeWrapper { get { return StableFindElement(_iframeWrapper); } }
        public IWebElement Iframe { get { return StableFindElement(_iframe); } }
        public IWebElement DocumentNoTextBox { get { return StableFindElement(_DocumentNoTextBox); } }
        public IWebElement ToobarBottomButton(string value, string nameButton) => StableFindElement(_toobarBottomButton(value, nameButton));
        public IWebElement GridVewData { get { return StableFindElement(_gridVewData); } }
        #endregion

        #region Actions
        public AddDocument(IWebDriver webDriver) : base(webDriver)
        {
            webDriver.SwitchTo().Frame(IframeWrapper);
            webDriver.SwitchTo().Frame(Iframe);
        }

        public AddDocument EnterAltDocumentNo(string value)
        {
            DocumentNoTextBox.InputText(value);
            return this;
        }

        public T ClickToobarBottomButton<T>(string value, string gridViewName, string nameButton = "")
        {
            if (nameButton == "")
                nameButton = value;
            if (value == "OK")
                nameButton = "Close";

            ToobarBottomButton(value, nameButton).Click();
            WaitForElement(By.Id(gridViewName));
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T SelectItemByDocumentNo<T>(string documentNo)
        {
            int rowIndex, colIndex;
            GetTableCellValueIndex(GridVewData, documentNo, out rowIndex, out colIndex);
            IWebElement ItemRow = TableCell(GridVewData, rowIndex, colIndex);
            IWebElement CheckBox = ItemRow.FindElement(By.XPath(_checkBoxXpath));
            CheckBox.Click();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        #endregion
    }
}
