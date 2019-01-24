using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class VendorDeliverableDetail : PopupWindow
    {
        #region Entities
        private static By _criticalityTextBox => By.Id("cboCriticality_Input");

        public IWebElement CriticalityTextBox { get { return StableFindElement(_criticalityTextBox); } }
        #endregion

        #region Actions
        public VendorDeliverableDetail(IWebDriver webDriver) : base(webDriver)
        {
        }

        public VendorDeliverableDetail EnterDeliverableRequiredInfo(DeliverableLine deliverableItemInfo, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            var node = StepNode();

            node.Info($"Click {DeliverableField.ContractNumber.ToDescription()} dropdown, and select: " + deliverableItemInfo.ContractNumber);
            SelectItemInDropdown<VendorDeliverableDetail>(DeliverableField.ContractNumber.ToDescription(), deliverableItemInfo.ContractNumber, ref methodValidation);

            node.Info($"Click {DeliverableField.ItemID.ToDescription()} dropdown, and select: " + deliverableItemInfo.ItemID);
            SelectItemInDropdown<VendorDeliverableDetail>(DeliverableField.ItemID.ToDescription(), deliverableItemInfo.ItemID, ref methodValidation);

            node.Info($"Enter {deliverableItemInfo.LineItemNumber} in {DeliverableField.LineItemNumber.ToDescription()} Field.");
            EnterTextField<VendorDeliverableDetail>(DeliverableField.LineItemNumber.ToDescription(), deliverableItemInfo.LineItemNumber);

            node.Info($"Enter {deliverableItemInfo.Description} in Description Field.");
            EnterTextField<VendorDeliverableDetail>(DeliverableField.Description.ToDescription(), deliverableItemInfo.Description);

            node.Info($"Click {DeliverableField.DeliverableType.ToDescription()} dropdown, and select: " + deliverableItemInfo.DeliverableType);
            SelectItemInDropdown<VendorDeliverableDetail>(DeliverableField.DeliverableType.ToDescription(), deliverableItemInfo.DeliverableType, ref methodValidation);

            node.Info($"Click {DeliverableField.Criticality.ToDescription()} dropdown, and select: " + deliverableItemInfo.Criticality);
            if (CriticalityTextBox.GetAttribute("value") != "Normal")
                SelectItemInDropdown<VendorDeliverableDetail>(DeliverableField.Criticality.ToDescription(), deliverableItemInfo.Criticality, ref methodValidation);

            node.Info($"Click {DeliverableField.Status.ToDescription()} dropdown, and select: " + deliverableItemInfo.Status);
            SelectItemInDropdown<VendorDeliverableDetail>(DeliverableField.Status.ToDescription(), deliverableItemInfo.Status, ref methodValidation);

            return this;
        }

        public KeyValuePair<string, bool> ValidateDeliverableLinkItemClosed(int countWindow)
        {
            var node = StepNode();
            try
            {
                if (WebDriver.WindowHandles.Count == countWindow - 1)
                    return SetPassValidation(node, string.Format(Validation.Deliverable_Link_Items_Window_Is_Closed));
                else
                    return SetFailValidation(node, string.Format(Validation.Deliverable_Link_Items_Window_Is_Closed));
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, string.Format(Validation.Deliverable_Link_Items_Window_Is_Closed), e);
            }
        }
        public List<KeyValuePair<string, bool>> ValidateSelectedItemShowInDropdownBoxesCorrect(DeliverableLine deliverableItemInfo)
        {
            var validation = new List<KeyValuePair<string, bool>>();

            validation.Add(ValidateItemDropdownIsSelected(deliverableItemInfo.ContractNumber, DropdownListInput(DeliverableField.ContractNumber.ToDescription()).GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(deliverableItemInfo.ItemID, DropdownListInput(DeliverableField.ItemID.ToDescription()).GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(deliverableItemInfo.DeliverableType, DropdownListInput(DeliverableField.DeliverableType.ToDescription()).GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(deliverableItemInfo.Criticality, DropdownListInput(DeliverableField.Criticality.ToDescription()).GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(deliverableItemInfo.Status, DropdownListInput(DeliverableField.Status.ToDescription()).GetAttribute("id")));
            return validation;
        }

        private static class Validation
        {
            public static string Deliverable_Link_Items_Window_Is_Closed  = "Validate that the deliverable link items window is closed";
        }
        #endregion
    }
}
