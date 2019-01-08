using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.PopupWindows;


namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class VendorDataRegister : ProjectsDashboard
    {
        #region Entities
        private static By _expandButton => By.XPath("//td[@class = 'rgExpandCol']/input");
        private static By _purchaseTable => By.XPath("//table[contains(@id,'GridViewItemsVendor')]/thead");

        private static string _filterItemsXpath = "//table[contains(@id,'GridViewItemsVendor')]//tr[@valign='top']";

        public IReadOnlyCollection<IWebElement> ExpandButton { get { return StableFindElements(_expandButton); } }
        public IWebElement PurchaseTable { get { return StableFindElement(_purchaseTable); } }
        #endregion

        #region Actions
        public VendorDataRegister(IWebDriver webDriver) : base(webDriver)
        {
        }

        public ItemDetail OpenItem(List<KeyValuePair<string, string>> columnValuePairList)
        {
            var PurchaseItemsList = GetAvailablePurchaseItems(columnValuePairList);
            var item = PurchaseItemsList.ElementAt(0).StableFindElement(By.XPath("./td[3]"));
            ScrollIntoView(item);
            string currentWindow;
            SwitchToNewPopUpWindow(item, out currentWindow, doubleClick: true);
            return new ItemDetail(WebDriver);
        }

        public VendorDataRegister ClickExpandButton(int index, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info("Click the expand button");
            index = Utils.RefactorIndex(index);
            ExpandButton.ElementAt(index).Click();
            if (waitForLoading)
            {
                WaitForLoadingPanel(shortTimeout * 2);
            }
            return this;
        }

        private IReadOnlyCollection<IWebElement> GetAvailablePurchaseItems(List<KeyValuePair<string, string>> columnValuePairList)
        {
            int rowIndex, colIndex = 1;
            string itemsXpath = _filterItemsXpath;
            GetTableCellValueIndex(PurchaseTable, columnValuePairList.ElementAt(0).Key, out rowIndex, out colIndex, "th");
            if (colIndex < 2)
                return null;
            itemsXpath += $"[td[{colIndex}][contains(., '{columnValuePairList.ElementAt(0).Value}')]";                                    

            for (int i = 1; i < columnValuePairList.Count; i++)
            {
                GetTableCellValueIndex(PurchaseTable, columnValuePairList.ElementAt(i).Key, out rowIndex, out colIndex, "th");
                if (colIndex < 2)
                    return null;
                itemsXpath += $" and td[{colIndex}][contains(., '{columnValuePairList.ElementAt(i).Value}')]";                                        
            }
            itemsXpath += "]";

            return StableFindElements(By.XPath(itemsXpath));
        }

        public KeyValuePair<string, bool> ValidatePurchaseItemsAreShown(List<KeyValuePair<string, string>> columnValuePairList)
        {
            var node = StepNode();
            try
            {
                foreach (var columnValuePair in columnValuePairList)
                {
                    if (columnValuePair.Key == "Status")
                    {
                        string status = columnValuePair.Value.Split('-')[0].Trim();
                        columnValuePairList.Remove(columnValuePair);
                        columnValuePairList.Add(new KeyValuePair<string, string>("Status", status));
                        break;
                    }
                }
                var PurchaseItemsList = GetAvailablePurchaseItems(columnValuePairList);
                if (PurchaseItemsList.Count > 0)
                    return SetPassValidation(node, Validation.Purchase_Items_Are_Shown);
                return SetFailValidation(node, Validation.Purchase_Items_Are_Shown);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Purchase_Items_Are_Shown, e);
            }
        }

        private static class Validation
        {
            public static string Purchase_Items_Are_Shown = "Validate that purchase items are shown";
        }
        #endregion
    }
}
