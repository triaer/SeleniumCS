using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;


namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class VendorItemDetail : PopupWindow
    {
        #region Entities        
        
        #endregion

        #region Actions
        public VendorItemDetail(IWebDriver webDriver) : base(webDriver) { }

        public VendorItemDetail EnterItemPurchasedRequiredInfo(ItemPurchased itemPurchasedData, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            var node = StepNode();

            node.Info($"Enter {itemPurchasedData.ItemID} in {ItemPurchasedField.ItemID.ToDescription()} Field.");
            EnterTextField<VendorItemDetail>(ItemPurchasedField.ItemID.ToDescription(), itemPurchasedData.ItemID);

            node.Info($"Enter {itemPurchasedData.Description} in Description Field.");
            EnterTextField<VendorItemDetail>(ItemPurchasedField.Description.ToDescription(), itemPurchasedData.Description);

            node.Info($"Click {ItemPurchasedField.ContractNumber.ToDescription()} dropdown, and select: " + itemPurchasedData.ContractNumber);
            SelectItemInDropdown<VendorItemDetail>(ItemPurchasedField.ContractNumber.ToDescription(), itemPurchasedData.ContractNumber, ref methodValidation);

            node.Info($"Click {ItemPurchasedField.Status.ToDescription()} dropdown, and select: " + itemPurchasedData.Status);
            SelectItemInDropdown<VendorItemDetail>(ItemPurchasedField.Status.ToDescription(), itemPurchasedData.Status, ref methodValidation);

            return this;
        }
        public List<KeyValuePair<string, bool>> ValidateSelectedItemShowInDropdownBoxesCorrect(ItemPurchased itemPurchasedData)
        {
            var validation = new List<KeyValuePair<string, bool>>();

            validation.Add(ValidateItemDropdownIsSelected(itemPurchasedData.ContractNumber, DropdownListInput(ItemPurchasedField.ContractNumber.ToDescription()).GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(itemPurchasedData.Status, DropdownListInput(ItemPurchasedField.Status.ToDescription()).GetAttribute("id")));
            return validation;
        }
        private static class Validation
        {          
        }
        #endregion
    }
}
