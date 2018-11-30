using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using System.Threading.Tasks;
using KiewitTeamBinder.Common.TestData;


namespace KiewitTeamBinder.UI.Pages.DocumentModule
{
    public class Document : ProjectsDashboard
    {
        #region Entities
        private string _gridViewDocRegRowXpath = "//table[@id = 'ctl00_cntPhMain_GridViewDocReg_ctl00']/tbody/tr[{0}]";

        private static By _menuHeaderFunction(string buttonName) => By.XPath($"//li[a='{buttonName}']");
        private static By _visibleItems => By.XPath("//tr[@title= 'Double click to view details']");
        private static By _gridViewDocReg => By.XPath("//table[@id = 'ctl00_cntPhMain_GridViewDocReg_ctl00']/tbody");
        private static By _documentNoTextBox => By.XPath("//input[@id='txtDocumentNo']");

        public IWebElement MenuHeaderFunction(string buttonName) => StableFindElement(_menuHeaderFunction(buttonName));
        public IReadOnlyCollection<IWebElement> VisibleItems { get { return StableFindElements(_visibleItems); } }
        public IWebElement GridViewDocReg { get { return StableFindElement(_gridViewDocReg); } }
        public IWebElement DocumentNoTextBox { get { return StableFindElement(_documentNoTextBox); } }
        #endregion

        #region Actions
        public Document(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Document ClickCloseButton()
        {
            MenuHeaderFunction("Close").Click();
            return this;
        }

        public KeyValuePair<string, bool> ValidateDocumentsDetailIsOpened(string documentNo)
        {
            var node = StepNode();
            string url = WebDriver.Url;
            try
            {
                if (url.Contains("DocDetailView") && DocumentNoTextBox.GetAttribute("value") == documentNo)
                    return SetPassValidation(node, Validation.Document_Detail_Is_Opened);

                return SetFailValidation(node, Validation.Document_Detail_Is_Opened);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Document_Detail_Is_Opened, e);
            }
        }

        public IWebElement FindDocumentByDocumentNo(string documentNo)
        {
            int rowIndex, colIndex;
            GetTableCellValueIndex(GridViewDocReg, documentNo, out rowIndex, out colIndex);
            return TableCell(GridViewDocReg, rowIndex, colIndex);
        }

        public Document SelectDocmentByIndexRandom(int indexRandom, out string parrentWindow)
        {
            int index = Utils.GetRandomNumber(0, indexRandom - 1);
            IWebElement ItemDocument = StableFindElement(By.XPath(string.Format(_gridViewDocRegRowXpath, index + 1)));
            SwitchToPopUpWindow(ItemDocument, out parrentWindow, true);

            return this;
        }

        public Document OpenDocument(string docmentNo, out string parrentWindow)
        {
            IWebElement ItemDocument =  FindDocumentByDocumentNo(docmentNo);
            SwitchToPopUpWindow(ItemDocument, out parrentWindow, true);
            return this;
        }

        private int GetTableItemNumber()
        {
            var node = StepNode();            
            try
            {
                var rows = VisibleItems.Count;
                node.Info("Get number of items in table: " + rows);
                return rows;
            }
            catch
            {
                return 0;
            }
        }

        public KeyValuePair<string, bool> ValidateRecordItemsCount()
        {
            int itemsNumber = GetTableItemNumber();
            var node = StepNode();
            node.Info($"Validate number of record items is equals to: {itemsNumber}");

            try
            {
                var actualQuantity = ItemsNumberLabel(new NavigateToModulesFromTheLeftNavSmoke.DocumentsModules().GridViewName).Text;
                if (Int32.Parse(actualQuantity) == itemsNumber)
                    return SetPassValidation(node, Validation.Number_Of_Items_Counted_Is_Valid);

                return SetFailValidation(node, Validation.Number_Of_Items_Counted_Is_Valid);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Number_Of_Items_Counted_Is_Valid, e);
            }
        }

        private static class Validation
        {
            public static string Document_Detail_Is_Opened = "Validate that the document detail is opened and the detail document is correctly";
            public static string Number_Of_Items_Counted_Is_Valid = "Validate that number of items counted is valid";
        }
        #endregion

    }
}
