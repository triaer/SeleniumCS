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
        private static By _gridViewDocReg => By.XPath("//table[@id = 'ctl00_cntPhMain_GridViewDocReg_ctl00']/tbody");
        private static By _documentNoTextBox => By.XPath("//input[@id='txtDocumentNo']");

        public IWebElement MenuHeaderFunction(string buttonName) => StableFindElement(_menuHeaderFunction(buttonName));
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
            SwitchToNewPopUpWindow(ItemDocument, out parrentWindow, false, true);

            return this;
        }

        public Document OpenDocument(string documentNo, out string parrentWindow)
        {
            IWebElement ItemDocument =  FindDocumentByDocumentNo(documentNo);
            SwitchToNewPopUpWindow(ItemDocument, out parrentWindow, false, true);
            return this;
        }

        private static class Validation
        {
            public static string Document_Detail_Is_Opened = "Validate that the document detail is opened and the detail document is correctly";
   
        }
        #endregion

    }
}
