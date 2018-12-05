using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using OpenQA.Selenium.Support.UI;
using KiewitTeamBinder.Common.Helper;
using System.Windows.Forms;
using KiewitTeamBinder.UI.Pages.Dialogs;


namespace KiewitTeamBinder.UI.Pages.Global
{
    public class NewTransmittal : ProjectsDashboard
    {
        #region Entities
        private static By _recipientsButton(string buttonName) => By.XPath($"//a[input[contains(@id, '{buttonName}']]");
        private static By _paneTable(string gridViewName) => By.XPath($"//table[contains(@id, '{gridViewName}_ctl00')]/thead");

        private static string _filterItemsXpath = "//tr[@valign='top']";

        public IWebElement RecipientsButton(string buttonName) => StableFindElement(_recipientsButton(buttonName));
        public IWebElement PaneTable(string gridViewName) => StableFindElement(_paneTable(gridViewName));
        #endregion

        #region Actions
        public NewTransmittal(IWebDriver webDriver) : base(webDriver) { }

        public SelectRecipientsDialog ClickRecipientsButton(string buttonName)
        {
            RecipientsButton(buttonName).Click();
            return new SelectRecipientsDialog(WebDriver);
        }

        public int GetTableItemNumberWithConditions(string gridViewName, string selectedDocuments)
        {
            IReadOnlyCollection<IWebElement> AvailableItems = GetAvailableItems(gridViewName, selectedDocuments);
            try
            {
                return AvailableItems.Count;
            }
            catch
            {
                return 0;
            }
        }

        private IReadOnlyCollection<IWebElement> GetAvailableItems(string gridViewName, string selectedDocument)
        {
            int rowIndex, colIndex = 1;
            
            GetTableCellValueIndex(PaneTable(gridViewName), "Document No.", out rowIndex, out colIndex, "th");
            if (colIndex < 2)
                return null;

            string itemsXpath = _filterItemsXpath + $"[td[{colIndex}][contains(., '{selectedDocument}')]]";

            return StableFindElements(By.XPath(itemsXpath));
        }

        public KeyValuePair<string, bool> ValidateAllSelectedDocumentsAreListed(ref string[] selectedDocuments)
        {
            var node = StepNode();
            try
            {
                if (GetTableItemNumber("GridViewDocuments") != selectedDocuments.Length)
                    return SetFailValidation(node, Validation.All_Selected_Documents_Are_Listed);

                for (int i = 0; i < selectedDocuments.Length; i++)
                {
                    if (GetTableItemNumberWithConditions("GridViewDocuments", selectedDocuments[i]) < 1)
                        return SetFailValidation(node, Validation.All_Selected_Documents_Are_Listed);
                }
                return SetPassValidation(node, Validation.All_Selected_Documents_Are_Listed);
               
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.All_Selected_Documents_Are_Listed, e);
            }
        }
        private static class Validation
        {
            public static string All_Selected_Documents_Are_Listed = "Validate that all selected documents are listed";
        }
            #endregion
    }
}
