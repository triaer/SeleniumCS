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
    public class DeliverableItemDetail : PopupWindow
    {
        #region Entities

        #endregion

        #region Actions
        public DeliverableItemDetail(IWebDriver webDriver) : base(webDriver)
        {
        }

        public DeliverableItemDetail EnterDeliverableItemInfo(DeliverableItemInfo deliverableItemInfo, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            var node = StepNode();

            node.Info($"Click {DeliverableField.ContractNumber.ToDescription()} dropdown, and select: " + deliverableItemInfo.ContractNumber);
            SelectItemInDropdown<DocumentDetail>(DeliverableField.ContractNumber.ToDescription(), deliverableItemInfo.ContractNumber, ref methodValidation);

            node.Info($"Click {DeliverableField.ItemID.ToDescription()} dropdown, and select: " + deliverableItemInfo.ItemID);
            SelectItemInDropdown<DocumentDetail>(DeliverableField.ItemID.ToDescription(), deliverableItemInfo.ItemID, ref methodValidation);

            node.Info($"Enter {deliverableItemInfo.LineItemNumber} in {DeliverableField.LineItemNumber.ToDescription()} Field.");
            EnterTextField<DeliverableItemDetail>(DeliverableField.LineItemNumber.ToDescription(), deliverableItemInfo.LineItemNumber);

            node.Info($"Enter {deliverableItemInfo.Description} in Description Field.");
            EnterTextField<DeliverableItemDetail>(DeliverableField.Description.ToDescription(), deliverableItemInfo.Description);

            node.Info($"Click {DeliverableField.DeliverableType.ToDescription()} dropdown, and select: " + deliverableItemInfo.Type);
            SelectItemInDropdown<DocumentDetail>(DeliverableField.DeliverableType.ToDescription(), deliverableItemInfo.Type, ref methodValidation);

            node.Info($"Click {DeliverableField.Criticality.ToDescription()} dropdown, and select: " + deliverableItemInfo.Criticality);
            SelectItemInDropdown<DocumentDetail>(DeliverableField.Criticality.ToDescription(), deliverableItemInfo.Criticality, ref methodValidation);

            node.Info($"Click {DeliverableField.Status.ToDescription()} dropdown, and select: " + deliverableItemInfo.Status);
            SelectItemInDropdown<DocumentDetail>(DeliverableField.Status.ToDescription(), deliverableItemInfo.Status, ref methodValidation);

            return this;
        }
        public List<KeyValuePair<string, bool>> ValidateSelectedItemShowInDropdownBoxesCorrect(DeliverableItemInfo deliverableItemInfo)
        {
            var documentDetail = new DocumentDetail(WebDriver);
            var validation = new List<KeyValuePair<string, bool>>();

            validation.Add(documentDetail.ValidateItemDropdownIsSelected(deliverableItemInfo.ContractNumber, DropdownListInput(DeliverableField.ContractNumber.ToDescription()).GetAttribute("id")));
            validation.Add(documentDetail.ValidateItemDropdownIsSelected(deliverableItemInfo.ItemID, DropdownListInput(DeliverableField.ItemID.ToDescription()).GetAttribute("id")));
            validation.Add(documentDetail.ValidateItemDropdownIsSelected(deliverableItemInfo.Type, DropdownListInput(DeliverableField.DeliverableType.ToDescription()).GetAttribute("id")));
            validation.Add(documentDetail.ValidateItemDropdownIsSelected(deliverableItemInfo.Criticality, DropdownListInput(DeliverableField.Criticality.ToDescription()).GetAttribute("id")));
            validation.Add(documentDetail.ValidateItemDropdownIsSelected(deliverableItemInfo.Status, DropdownListInput(DeliverableField.Status.ToDescription()).GetAttribute("id")));
            return validation;
        }
        #endregion
    }
}
