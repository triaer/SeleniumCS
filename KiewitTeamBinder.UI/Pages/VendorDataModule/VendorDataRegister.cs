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


namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class VendorDataRegister : ProjectsDashboard
    {
        #region Entities
        private static By _visibleRows => By.XPath("//tr[contains(@id, 'GridViewContractVendor')]");

        public new IReadOnlyCollection<IWebElement> VisibleRows { get { return StableFindElements(_visibleRows); } }
        #endregion

        #region Actions
        public VendorDataRegister(IWebDriver webDriver) : base(webDriver)
        {
        }

        private int GetTableItemNumber()
        {
            var node = StepNode();            

            try
            {
                var rows = VisibleRows.Count;
                node.Info("Get number of items in table: " + rows);
                return rows;
            }
            catch
            {
                return 0;
            }
        }

        public override KeyValuePair<string, bool> ValidateRecordItemsCount(string moduleName)
        {
            int itemsNumber = GetTableItemNumber();
            var node = StepNode();
            node.Info($"Validate number of record items is equals to: {itemsNumber}");

            try
            {
                var actualQuantity = ItemsNumberLabel(moduleName).Text;
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
            public static string Number_Of_Items_Counted_Is_Valid = "Validate that number of items counted is valid";
        }
            #endregion
    }
}
