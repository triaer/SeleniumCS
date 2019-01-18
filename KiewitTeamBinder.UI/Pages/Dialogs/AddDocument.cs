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
        private string _documentNoXpath = "./td[2]";
        private static By _toobarBottomButton(string value, string nameButton) => By.XPath($"//input[@value='{value}' and contains(@name, '{nameButton}')]");
        private static By _iframeWrapper => By.Name("RadWindowAddItems");
        private static By _iframe => By.Id("ifrmFilter");
        private static By _DocumentNoTextBox => By.Id("DataSelectionDocNo_RadTextBoxValue");
        private static By _gridVewData => By.XPath("//table[@id='GridView_ctl00']/tbody");
        private static By _documentSearchWindow => By.Id("RadWindowWrapper_RadWindowAddItems");
        private static By _registerViewDropdown => By.XPath("//a[contains(@id, 'ComboxSelectionView1')]");
        private static By _registerViewDropdownData => By.XPath("//div[contains(@id, 'ComboxSelectionView1')]//ul/li");
        private static By _rowItemInAddDocumentPopup(int index) => By.XPath($"//div[@id='GridView_GridData']//tbody//tr[{index}]");    

        public IWebElement IframeWrapper { get { return StableFindElement(_iframeWrapper); } }
        public IWebElement Iframe { get { return StableFindElement(_iframe); } }
        public IWebElement DocumentNoTextBox { get { return StableFindElement(_DocumentNoTextBox); } }
        public IWebElement ToobarBottomButton(string value, string nameButton) => StableFindElement(_toobarBottomButton(value, nameButton));
        public IWebElement GridVewData { get { return StableFindElement(_gridVewData); } }
        public IWebElement RegisterViewDropdown { get { return StableFindElement(_registerViewDropdown); } }
        public IWebElement RowItemInAddDocumentPopup(int index) => StableFindElement(_rowItemInAddDocumentPopup(index));
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
        public AddDocument SelectItemByIndex(int index, out string documentNo)
        {
            var node = StepNode();
            SwitchFrameParrent();
            IWebElement CheckBox = RowItemInAddDocumentPopup(index).StableFindElement(By.XPath("./td/input[contains(@id,'CheckBox')]"));
            CheckBox.Check();
            documentNo = RowItemInAddDocumentPopup(index).StableFindElement(By.XPath(_documentNoXpath)).Text;
            node.Info("Document No: " + documentNo);
            return this;
        }

        public IWebElement FindItemByDocumentNo(string documentNo)
        {
            int rowIndex, colIndex;
            GetTableCellValueIndex(GridVewData, documentNo, out rowIndex, out colIndex);
            IWebElement Element = TableCell(GridVewData, rowIndex, colIndex);
            return Element;
        }

        public AddDocument SelectOptionInRegisterViewDropDown(string value)
        {
            var node = StepNode();
            string separator = " -- ";
            if (value == "All")
            {
                value = value.Insert(0, separator);
                value = value.Insert(value.Length, separator).Trim();
            }
            node.Info("Select Option: " + value);
            SelectComboboxByText(RegisterViewDropdown, _registerViewDropdownData, value);
            return this;
        }

        private void SwitchFrameParrent()
        {
            WebDriver.SwitchTo().DefaultContent();
            WebDriver.SwitchTo().Frame(IframeWrapper);
        }

        public KeyValuePair<string, bool> ValidateDocumentIsHighlighted(string documentNo = "", bool byIndex = false, int index = 0)
        {
            var node = StepNode();
            IWebElement ItemRow;
            if (byIndex)
                ItemRow = RowItemInAddDocumentPopup(index);
            else
            {
                IWebElement Element = FindItemByDocumentNo(documentNo);
                ItemRow = Element.StableFindElement(By.XPath("./.."));
            }
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

        public KeyValuePair<string, bool> ValidateCheckBoxStatus(int index, bool uncheck = false)
        {
            var node = StepNode();
            try
            {
                if (uncheck)
                {
                    if (RowItemInAddDocumentPopup(index).GetAttribute("class").Contains("SelectedRow"))
                        return SetFailValidation(node, Validation.CheckBox_Is_Not_Retained);
                    else
                        return SetPassValidation(node, Validation.CheckBox_Is_Not_Retained);
                }
                else
                {
                    if (RowItemInAddDocumentPopup(index).GetAttribute("class").Contains("SelectedRow"))
                        return SetPassValidation(node, Validation.CheckBox_Is_Retained);
                    else
                        return SetFailValidation(node, Validation.CheckBox_Is_Retained);
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
            public static string CheckBox_Is_Retained = "Validate that the checkbox is retained";
            public static string CheckBox_Is_Not_Retained = "Validate that the checkbox is not retained";
        }
        #endregion
    }
}
