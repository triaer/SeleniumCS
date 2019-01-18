﻿using OpenQA.Selenium;
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
        private static By _deliverableLineItem => By.XPath("//span[(text()='Deliverable Line Item')]");
        private static By _purchaseTable => By.XPath("//table[contains(@id,'GridViewItemsVendor')]/thead");
        private static By _deliverableTable => By.XPath("//table[contains(@id,'GridViewItemsVendor') and contains(@id, 'ViewDeliverableVendor')]/thead");
        private static By _expandButton => By.XPath("//td[@class = 'rgExpandCol']/input");
        private static By _contractNumber(string contractNumber, string gridView, string contractDescription) => By.XPath($"//span[contains(text(),'{contractNumber}')]/ancestor::tr[contains(@id,'{gridView}_ctl00')]//nobr[contains(text(),'{contractDescription}')]");
        private static By _checkBoxItemContract(string gridView, string description) => By.XPath($"//*[contains(text(),'{description}')]/ancestor::tr[contains(@id,'{gridView}_ctl00')]//input[contains(@id,'{gridView}_ctl00')]");
        private static By _selectedRecordCount => By.XPath("//span[@id='lblRegisterSelectedRecordCount']");
        private static By _blueHeader(string blueHeader) => By.XPath($"//div[@id='lblRegisterCaption']/span[text()='{blueHeader}']");
        private static By _itemsList(string gridView) => By.XPath($"//tr[contains(@id,'ctl00_cntPhMain_{gridView}_ctl00')]");
        private static By _totalItem(string gridView) => By.XPath($"//span[contains(@id,'ctl00_cntPhMain_{gridView}_ctl00DSC')]");
        private static By _selectedRow(string gridView, string Description) => By.XPath($"//*[contains(text(),'{Description}')]/ancestor::tr[contains(@id,'{gridView}_ctl00')]");
        private static By _deliverableLineItemNmberItem(string value) => By.XPath($"//span[@class='HyperLinkWithNoLine' and text() = '{value}']");
        private static By _registerCaption => By.XPath("//div[@id='lblRegisterCaption']/span");


        private static string _filterItemsXpath = "//table[contains(@id,'GridViewItemsVendor')]//tr[@valign='top']";

        public IWebElement DeliverableLineItemNmberItem(string value) => StableFindElement(_deliverableLineItemNmberItem(value));
        public IWebElement DeliverableTable { get { return StableFindElement(_deliverableTable); } }
        public IWebElement DeliverableLineItem => StableFindElement(_deliverableLineItem);
        public IReadOnlyCollection<IWebElement> ExpandButton { get { return StableFindElements(_expandButton); } }
        public IWebElement PurchaseTable { get { return StableFindElement(_purchaseTable); } }
        public IWebElement ContractNumber(string contractNumber, string gridView, string description) => StableFindElement(_contractNumber(contractNumber, gridView, description));
        public IWebElement CheckBoxItemContract(string gridView, string description) => StableFindElement(_checkBoxItemContract(gridView, description));
        public IWebElement SelectedRecordCount { get { return StableFindElement(_selectedRecordCount); } }
        public IWebElement BlueHeader(string blueHeader) => StableFindElement(_blueHeader(blueHeader));
        public IWebElement TotalItem(string gridView) => StableFindElement(_totalItem(gridView));
        public IReadOnlyCollection<IWebElement> ItemsList(string gridView) => StableFindElements(_itemsList(gridView));
        public IReadOnlyCollection<IWebElement> SelectedRow(string gridView, string description) => StableFindElements(_selectedRow(gridView, description));
        #endregion

        #region Actions
        public VendorDataRegister(IWebDriver webDriver) : base(webDriver)
        {
        }

        public VendorItemDetail OpenItem(List<KeyValuePair<string, string>> columnValuePairList)
        {
            var PurchaseItemsList = GetAvailablePurchaseItems(columnValuePairList);
            var item = PurchaseItemsList.ElementAt(0).StableFindElement(By.XPath("./td[3]"));
            ScrollIntoView(item);
            string currentWindow;
            SwitchToNewPopUpWindow(item, out currentWindow, doubleClick: true);
            return new VendorItemDetail(WebDriver);
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

        private IReadOnlyCollection<IWebElement> GetAvalibleDeliverable(List<KeyValuePair<string, string>> columnValuePairList)
        {
            int rowIndex, colIndex = 1;
            string itemsXpath = _filterItemsXpath;
            GetTableCellValueIndex(DeliverableTable, columnValuePairList.ElementAt(0).Key, out rowIndex, out colIndex, "th");
            if (colIndex < 2)
                return null;
            itemsXpath += $"[td[{colIndex}][contains(., '{columnValuePairList.ElementAt(0).Value}')]";

            for (int i = 1; i < columnValuePairList.Count; i++)
            {
                GetTableCellValueIndex(DeliverableTable, columnValuePairList.ElementAt(i).Key, out rowIndex, out colIndex, "th");
                if (colIndex < 2)
                    return null;
                itemsXpath += $" and td[{colIndex}][contains(., '{columnValuePairList.ElementAt(i).Value}')]";
            }
            itemsXpath += "]";

            return StableFindElements(By.XPath(itemsXpath));

        }

        public VendorDeliverableDetail OpenDeliverableLineItemTemplate(out string parrentWindow)
        {
            SwitchToNewPopUpWindow(DeliverableLineItem, out parrentWindow, true);
            return new VendorDeliverableDetail(WebDriver);
        }

        public int GetTotalItems(string gridView, bool waitForLoading = true)
        {
            if (waitForLoading)
                WaitForLoading(_totalItem(gridView));
            return int.Parse(TotalItem(gridView).Text);
        }

        public int GetSelectedRecordCount(bool waitForLoading = true)
        {
            string selectedText = "";
            string selectedCount = SelectedRecordCount.Text;

            selectedText = selectedCount.Split(':')[1];
            selectedText = selectedText.Replace(" ", "");
            return int.Parse(selectedText);
        }

        public VendorDataRegister ClickOnBlueHeader(string blueHeader)
        {
            var node = StepNode();
            node.Info($"Click on the {blueHeader} BlueHeader");
            BlueHeader(blueHeader).Click();
            return this;
        }

        public VendorDataRegister ClickOnCheckBox(string gridView, string contractDescription, bool uncheck = false, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info("Check box a line item");
            if (waitForLoading)
                WaitForLoadingPanel();
            if (uncheck)
                CheckBoxItemContract(gridView, contractDescription.Trim()).UnCheck();
            else
                CheckBoxItemContract(gridView, contractDescription.Trim()).Check();
            return this;
        }

        public VendorDataRegister DoubleClickItem(string contractNumber, string gridView, string description, bool waitForLoading = true)
        {
            var node = StepNode();
            node.Info("Double click on a item");
            if (waitForLoading)
                WaitForLoadingPanel();
            ScrollIntoView(ContractNumber(contractNumber, gridView, description));
            ContractNumber(contractNumber, gridView, description).DoubleClick();
            return this;
        }

        public VendorDeliverableDetail ClickOnBlueDeliverableLineItemNumber(string value)
        {
            WaitForElementClickable(_deliverableLineItemNmberItem(value));
            DeliverableLineItemNmberItem(value).Click();
            SwitchToWindow(WebDriver.WindowHandles.Last());
            return new VendorDeliverableDetail(WebDriver);
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

        public KeyValuePair<string, bool> ValidateDeliverablesAreShown(List<KeyValuePair<string, string>> columnValuePairList)
        {
            var node = StepNode();
            try
            {
                var DeliverableList = GetAvalibleDeliverable(columnValuePairList);
                if (DeliverableList.Count > 0)
                {
                    return SetPassValidation(node, Validation.Deliverables_Are_Shown);
                }
                return SetFailValidation(node, Validation.Deliverables_Are_Shown);
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, Validation.Deliverables_Are_Shown, e);
            }
        }

        public KeyValuePair<string, bool> ValidateItemsCountedAreMatches(string gridView, int expectedValue = 0)
        {
            var node = StepNode();
            try
            {
                expectedValue = GetTotalItems(gridView);
                int itemListSize = 0;
                //int itemListSize = ItemsList(gridView).Count;
                foreach (IWebElement item in ItemsList(gridView))
                {
                    if (item.IsDisplayed())
                    {
                        itemListSize += 1;
                    }
                }
                if (itemListSize == expectedValue)
                    return SetPassValidation(node, Validation.Items_Counted_Are_Matches);
                return SetFailValidation(node, Validation.Items_Counted_Are_Matches);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Items_Counted_Are_Matches, e);
            }
        }

        public KeyValuePair<string, bool> ValidateLineItemsIsHighlighted(string gridView, string description, int expectedValue = 0)
        {
            var node = StepNode();
            try
            {
                expectedValue = GetSelectedRecordCount();
                string actual;
                int count = 0;

                foreach (IWebElement item in SelectedRow(gridView, description.Trim()))
                {
                    actual = item.GetAttribute("class");
                    if ((actual.Contains("Selected")))
                    {
                        count += 1;
                    }
                }
                if (count == expectedValue)
                    return SetPassValidation(node, Validation.Line_Items_Is_Highlighted);
                return SetFailValidation(node, Validation.Line_Items_Is_Highlighted);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Line_Items_Is_Highlighted, e);
            }
        }

        public KeyValuePair<string, bool> ValidateSelectedCountInCreased(string gridView, string description, int expectedValue = 0)
        {
            var node = StepNode();
            try
            {
                int count = 0;
                int selectedRow = GetSelectedRecordCount();
                string actual;

                foreach (IWebElement item in SelectedRow(gridView, description.Trim()))
                {
                    actual = item.GetAttribute("class");
                    if ((actual.Contains("Selected")))
                    {
                        count += 1;
                    }
                }

                if ((expectedValue + count) == selectedRow)
                {
                    return SetPassValidation(node, Validation.Selected_Count_Is_Increased);
                }
                return SetFailValidation(node, Validation.Selected_Count_Is_Increased);

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Selected_Count_Is_Increased, e);
            }
        }

        public KeyValuePair<string, bool> ValidateSelectedCountDeCreased(string gridView, string description, int expectedValue)
        {
            var node = StepNode();
            try
            {
                int count = 0;
                int selectedRow = GetSelectedRecordCount();
                string actual;

                foreach (IWebElement item in SelectedRow(gridView, description.Trim()))
                {
                    actual = item.GetAttribute("class");
                    if ((actual.Contains("Selected")))
                    {
                        count += 1;
                    }
                }

                if ((expectedValue + count) > selectedRow)
                {
                    return SetPassValidation(node, Validation.Selected_Count_Is_Increased);
                }
                return SetFailValidation(node, Validation.Selected_Count_Is_Increased);

            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Selected_Count_Is_Increased, e);
            }
        }


        public KeyValuePair<string, bool> ValidateBredCrumbTrailDisplayCorrect(string type, string contractNumber = "", string itemID = "", string deliverableLinkItem = "")
        {
            var node = StepNode();
            string bredCrumbTrail = "Vendor Data - ";
            string[] path = { contractNumber, itemID, deliverableLinkItem };
            List<IWebElement> ListElement = StableFindElements(_registerCaption).ToList();
            try
            {
                string expected = bredCrumbTrail;
                for (int item = 0; item < path.Length; item++)
                {
                    if (path[item] != "")
                    {
                        expected = expected.Insert(bredCrumbTrail.Length, path[item]);
                        expected += " > ";
                    }
                }
                string actual = bredCrumbTrail;
                foreach (var item in ListElement)
                {
                    actual = actual.Insert(bredCrumbTrail.Length, item.Text);
                    actual += " > ";
                }
                if (actual + type == expected + type)
                    return SetPassValidation(node, Validation.Bred_Crumb_Trail_Is_Displayed_Correct);
                return SetFailValidation(node, Validation.Bred_Crumb_Trail_Is_Displayed_Correct, expected + type, actual + type);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Bred_Crumb_Trail_Is_Displayed_Correct, e);
            }
        }


        private static class Validation
        {
            public static string Purchase_Items_Are_Shown = "Validate that purchase items are shown";
            public static string Items_Counted_Are_Matches = "Validate that number of line items matches the count";
            public static string Line_Items_Is_Highlighted = "Validate that the line item is highlighted when cliked checkbox";
            public static string Selected_Count_Is_Increased = "Validate that selected count increased on bottom right corner";
            public static string Selected_Count_Is_Decreased = "Validate that selected count decreased on bottom right corner";
            public static string Deliverables_Are_Shown = "Validate that deliverables are shown";
            public static string Bred_Crumb_Trail_Is_Displayed_Correct = "Validate that bred crumb is displayed correctly";
        }
        #endregion
    }
}
