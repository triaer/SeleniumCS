using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class AddDocument : LinkItems
    {
        #region Entities 
        private string _checkBoxXpath = "./preceding::td[1]/input[contains(@id,'CheckBox')]";
        private static By _toobarBottomButton(string value, string nameButton) => By.XPath($"//input[@value='{value}' and contains(@name, '{nameButton}')]");
        private static By _iframeWrapper => By.Name("RadWindowAddItems");
        private static By _iframe => By.Id("ifrmFilter");
        private static By _DocumentNoTextBox => By.Id("DataSelectionDocNo_RadTextBoxValue");
        private static By _gridVewData => By.XPath("//table[@id='GridView_ctl00']/tbody");
        private static By _documentSearchWindow => By.Id("RadWindowWrapper_RadWindowAddItems");

        public IWebElement IframeWrapper { get { return StableFindElement(_iframeWrapper); } }
        public IWebElement Iframe { get { return StableFindElement(_iframe); } }
        public IWebElement DocumentNoTextBox { get { return StableFindElement(_DocumentNoTextBox); } }
        public IWebElement ToobarBottomButton(string value, string nameButton) => StableFindElement(_toobarBottomButton(value, nameButton));
        public IWebElement GridVewData { get { return StableFindElement(_gridVewData); } }
        #endregion

        #region Actions
        public AddDocument(IWebDriver webDriver) : base(webDriver)
        {
            //webDriver.SwitchTo().Frame(IframeWrapper);
            //webDriver.SwitchTo().Frame(Iframe);
        }

        public AddDocument SwitchToFrameOnAddDocument()
        {
            var node = StepNode();
            WebDriver.SwitchTo().Frame(IframeWrapper);
            WebDriver.SwitchTo().Frame(Iframe);
            return this;
        }

        public AddDocument EnterDocumentNo(string value)
        {
            var node = StepNode();
            DocumentNoTextBox.InputText(value);
            return this;
        }

        public T ClickToobarBottomButton<T>(string value, string gridViewName = "", bool closedPopUp = false, string nameButton = "")
        {
            if (nameButton == "")
                nameButton = value;
            if (value == "OK")
                nameButton = "Close";

            ToobarBottomButton(value, nameButton).Click();

            if (gridViewName != "")
                WaitForElement(By.Id(gridViewName));
            if (closedPopUp)
                WebDriver.SwitchTo().DefaultContent();

            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public AddDocument SelectItemByDocumentNo(string documentNo)
        {
            var node = StepNode();
            SwitchFrameParrent();
            IWebElement Element = FindItemByDocumentNo(documentNo);
            IWebElement CheckBox = Element.StableFindElement(By.XPath(_checkBoxXpath));
            CheckBox.Click();
            return this;
        }

        public IWebElement FindItemByDocumentNo(string documentNo)
        {
            int rowIndex, colIndex;
            GetTableCellValueIndex(GridVewData, documentNo, out rowIndex, out colIndex);
            IWebElement Element = TableCell(GridVewData, rowIndex, colIndex);
            return Element;
        }

        private void SwitchFrameParrent()
        {
            WebDriver.SwitchTo().DefaultContent();
            WebDriver.SwitchTo().Frame(IframeWrapper);
        }

        public KeyValuePair<string, bool> ValidateDocumentIsHighlighted(string documentNo)
        {
            var node = StepNode();
            IWebElement Element = FindItemByDocumentNo(documentNo);
            IWebElement ItemRow = Element.StableFindElement(By.XPath("./.."));
            try
            {
                if (ItemRow.GetAttribute("class").Contains("HoveredRow"))
                    return SetPassValidation(node, Validation.Document_Is_Highlighted);
                else
                    return SetFailValidation(node, Validation.Document_Is_Highlighted);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Document_Is_Highlighted, e);
            }
        }

        public KeyValuePair<string, bool> ValidateDocumentSearchWindowStatus(bool closed = false)
        {
            var node = StepNode();
            try
            {
                if (closed)
                {
                    if (StableFindElement(_documentSearchWindow) != null)
                        return SetFailValidation(node, Validation.Document_Search_Is_Closed);
                    else
                        return SetPassValidation(node, Validation.Document_Search_Is_Closed);
                }
                else
                {
                    if (StableFindElement(_documentSearchWindow) != null)
                        return SetPassValidation(node, Validation.Document_Search_Is_Opened);
                    else
                        return SetFailValidation(node, Validation.Document_Search_Is_Opened);
                }
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Document_Search_Is_Opened, e);
            }
        }

        private static class Validation
        {
            public static string Document_Is_Highlighted = "Validate that the document is highlighted";
            public static string Document_Search_Is_Opened = "Validate that the document search is opened";
            public static string Document_Search_Is_Closed = "Validate that the document search is closed";
        }
        #endregion
    }
}
