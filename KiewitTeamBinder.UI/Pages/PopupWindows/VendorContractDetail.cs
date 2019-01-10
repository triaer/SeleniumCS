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
    public class VendorContractDetail : PopupWindow
    {
        #region Entities        
        
        #endregion

        #region Actions
        public VendorContractDetail(IWebDriver webDriver) : base(webDriver) { }

        public VendorContractDetail EnterContractRequiredInfo(Contract contractData, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            var node = StepNode();

            node.Info($"Enter {contractData.ContractNumber} in {ContractField.ContractNumber.ToDescription()} Field.");
            EnterTextField<VendorContractDetail>(ContractField.ContractNumber.ToDescription(), contractData.ContractNumber);

            node.Info($"Enter {contractData.Description} in Description Field.");
            EnterTextField<VendorContractDetail>(ContractField.Description.ToDescription(), contractData.Description);

            node.Info($"Click {ContractField.VendorCompany.ToDescription()} dropdown, and select: " + contractData.VendorCompany);
            SelectItemInDropdown<VendorContractDetail>(ContractField.VendorCompany.ToDescription(), contractData.VendorCompany, ref methodValidation);

            node.Info($"Click {ContractField.ExpeditingContract.ToDescription()} dropdown, and select: " + contractData.ExpeditingContract);
            SelectItemInDropdown<VendorContractDetail>(ContractField.ExpeditingContract.ToDescription(), contractData.ExpeditingContract, ref methodValidation);
    
            node.Info($"Click {ContractField.Status.ToDescription()} dropdown, and select: " + contractData.Status);
            SelectItemInDropdown<VendorContractDetail>(ContractField.Status.ToDescription(), contractData.Status, ref methodValidation);

            return this;
        }
        public List<KeyValuePair<string, bool>> ValidateSelectedItemShowInDropdownBoxesCorrect(Contract contractData)
        {
            var validation = new List<KeyValuePair<string, bool>>();

            validation.Add(ValidateItemDropdownIsSelected(contractData.VendorCompany, DropdownListInput(ContractField.VendorCompany.ToDescription()).GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(contractData.ExpeditingContract, DropdownListInput(ContractField.ExpeditingContract.ToDescription()).GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(contractData.Status, DropdownListInput(ContractField.Status.ToDescription()).GetAttribute("id")));
            return validation;
        }
        private static class Validation
        {          
        }
        #endregion
    }
}
