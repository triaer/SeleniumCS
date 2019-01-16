using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class LinkItems : PopupWindow
    {
        #region Entities 
        private static By _gridViewLinkItems => By.XPath("//div[@id='LinkedDocumentsGrid_GridData']/table/tbody");

        public IWebElement GridViewLinkItems { get { return StableFindElement(_gridViewLinkItems); } }
        #endregion

        #region Actions
        public LinkItems(IWebDriver webDriver) : base(webDriver)
        { }

        public int GetCountWindow()
        {
            return WebDriver.WindowHandles.Count;
        }

        public KeyValuePair<string, bool> ValidateLinkItemsWindowIsClosed(int countWindow)
        {
            var node = StepNode();
            try
            {
                if (WebDriver.WindowHandles.Count == countWindow - 1)
                    return SetPassValidation(node, Validation.Link_Items_Window_Is_Closed);
                else
                    return SetFailValidation(node, Validation.Link_Items_Window_Is_Closed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Link_Items_Window_Is_Closed, e);
            }

        }

        public KeyValuePair<string, bool> ValidateDocumentIsAttached(string documentNo)
        {
            var node = StepNode();
            int rowIndex, colIndex;
            GetTableCellValueIndex(GridViewLinkItems, documentNo, out rowIndex, out colIndex);
            try
            {
                if (TableCell(GridViewLinkItems, rowIndex, colIndex) != null)
                    return SetPassValidation(node, Validation.Document_Is_Attached);
                else
                    return SetFailValidation(node, Validation.Document_Is_Attached);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Document_Is_Attached, e);
            }

        }

        private static class Validation
        {
            public static string Link_Items_Window_Is_Closed = "Validate that the Link Items window is closed";
            public static string Document_Is_Attached = "Validate that the Document is attached";
        }
        #endregion
    }
}
