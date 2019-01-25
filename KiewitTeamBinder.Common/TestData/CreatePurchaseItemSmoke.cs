﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;


namespace KiewitTeamBinder.Common.TestData
{
    public class CreatePurchaseItemSmoke
    {       
        public string ProjectName = "Automation Project 1";
        public string GridViewName = "GridViewContractVendor";
        public string GridViewItemsName = "GridViewItemsVendor";
        public string[] RequiredFields = {"Item ID", "Description", "Status"};
        public Contract ContractInfo = new Contract()
        {
            ContractNumber = Utils.GetRandomValue("CONTRACT"),
            Description = Utils.GetRandomValue("Description Contract"),
            VendorCompany = "Kiewit",
            ExpeditingContract = "No",
            Status = "STARTED"
        };
        public ItemPurchased PurchaseInfo(Contract ContractInfo)
        {
            return new ItemPurchased()
            {
                ContractNumber = ContractInfo.ContractNumber,
                ItemID = Utils.GetRandomValue("ITEMID"),
                Description = Utils.GetRandomValue("Description item content"),
                Status = "OPEN",
            };
        }

        public List<KeyValuePair<string, string>> ExpectedContractValuesInColumnList(ItemPurchased PurchaseInfo)
        {
            return new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Contract Number", PurchaseInfo.ContractNumber) };
        }
        public List<KeyValuePair<string, string>> ExpectedPurchasedValuesInColumnList(ItemPurchased PurchaseInfo)
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Item ID", PurchaseInfo.ItemID),
                new KeyValuePair<string, string>("Description", PurchaseInfo.Description),
                new KeyValuePair<string, string>("Status", PurchaseInfo.Status)
            };
        }
        public string SaveMessage = "Saved Successfully";        
        public int expandButtonIndex = 1;
        
    }
}
