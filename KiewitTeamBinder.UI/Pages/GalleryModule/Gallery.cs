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
using KiewitTeamBinder.Common.TestData;

namespace KiewitTeamBinder.UI.Pages.GalleryModule
{
    public class Gallery : ProjectsDashboard
    {
        #region Entities
        private static By _sortByButton => By.Id("cboViewBy");
        private static By _clientStateValue => By.Id("cboViewBy_ClientState");
        private static By _sortByValueLabel => By.Id("cboViewBy_Input");
        private static By _sortByDropdownList => By.Id("cboViewBy_DropDown");
        private static By _visibleListViewItems => By.XPath("//table[contains(@id, 'ListView')]//tr[contains(@style, 'visibility: visible')]");
        private static By _visibleThumbnailItems => By.XPath("//table[contains(@id, 'Thumbnail')]//tr[contains(@style, 'visibility: visible')]//div[contains(@id, 'itemContainer')]");

        public IWebElement SortByButton { get { return StableFindElement(_sortByButton); } }
        public IWebElement SortByValueLabel { get { return StableFindElement(_sortByValueLabel); } }
        public IReadOnlyCollection<IWebElement> VisibleListViewItems { get { return StableFindElements(_visibleListViewItems); } }
        public IReadOnlyCollection<IWebElement> VisibleThumbnailItems { get { return StableFindElements(_visibleThumbnailItems); } }
        #endregion

        #region Actions
        public Gallery(IWebDriver webDriver) : base(webDriver)
        {
        }

        public Gallery SelectSortBy(string sortByOption)
        {
            SortByButton.Click();
            var SortByDropdownList = new SelectElement(StableFindElement(_sortByDropdownList));
            SortByDropdownList.SelectByText(sortByOption);
            return this;
        }

        private string GetSortByValue()
        {
            string sortByValue = "";
            string clientStateValue = FindElement(_clientStateValue).GetAttribute("value");
            if (clientStateValue == "")
            {
                sortByValue = SortByValueLabel.GetAttribute("value");
                return sortByValue;
            }

            string[] attributeValues = clientStateValue.Split(',');            
            foreach (var attributeValue in attributeValues)
            {
                if (attributeValue.Contains("text"))
                {
                    sortByValue = attributeValue.Split(':')[1];
                    sortByValue = sortByValue.Replace("\"", "");
                    break;
                }
            }
            return sortByValue;
        }

        /// <summary>
        /// Get the number of images on pane
        /// </summary>
        /// <param name="thumbnailView">Set to true if the view is in the thumbnail view</param>
        /// <returns>The number of images on pane</returns>
        public int GetTableItemNumber(bool thumbnailView)
        {
            var node = StepNode();
            node.Info("Get the number of images on pane");           

            try
            {
                if (thumbnailView)
                {
                    return VisibleThumbnailItems.Count;
                }
                return VisibleListViewItems.Count;
            }
            catch
            {
                return 0;
            }
        }

        public KeyValuePair<string, bool> ValidateSortByValue(string expectedSortByValue)
        {
            var node = StepNode();
            try
            {
                if (GetSortByValue().Equals(expectedSortByValue))
                    return SetPassValidation(node, Validation.Sort_By_Value_Displays_Correctly);
                else
                    return SetFailValidation(node, Validation.Sort_By_Value_Displays_Correctly);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Sort_By_Value_Displays_Correctly, e);
            }
        }

        public KeyValuePair<string, bool> ValidateRecordItemsCount()
        {
            int itemsNumber = GetTableItemNumber(true);
            var node = StepNode();
            node.Info($"Validate number of record items is equals to: {itemsNumber}");

            try
            {
                var actualQuantity = ItemsNumberLabel(new NavigateToModulesFromTheLeftNavSmoke.GalleryModules().GridViewName).Text;
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
            public static string Sort_By_Value_Displays_Correctly = "Validate that the Sort by value displays correctly";
            public static string Number_Of_Items_Counted_Is_Valid = "Validate that number of items counted is valid";
        }
            #endregion
    }
}
