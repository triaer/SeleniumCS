using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using static KiewitTeamBinder.UI.ExtentReportsHelper;


namespace KiewitTeamBinder.UI.Pages.VendorData
{
    public class HoldingArea : Dashboard
    {
        #region Entities
        private string _functionButton = "//li[@class='rtbItem rtbBtn'][a='{0}']";
        private static By _holdingAreaLabel => By.Id("lblRegisterCaption");

        public IWebElement HoldingAreaLabel { get { return StableFindElement(_holdingAreaLabel); } }
        #endregion

        #region Actions
        public HoldingArea(IWebDriver webDriver) : base(webDriver) { }

        public BulkUploadDocuments ClickBulkUploadButton()
        {
            IWebElement FunctionButton = StableFindElement(By.XPath(string.Format(_functionButton, "Bulk Upload")));
            string currentWindow;
            SwitchToPopUpWindow(FunctionButton, out currentWindow, true);
            return new BulkUploadDocuments(WebDriver);
        }

        public KeyValuePair<string, bool> ValidateHoldingAreaPageDisplays()
        {
            var node = StepNode();
            try
            {
                WaitForElementDisplay(By.Id("lblRegisterCaption"));               
                return SetPassValidation(node, Validation.Holding_Area_Page_Displays);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Holding_Area_Page_Displays, e);
            }
        }



        private static class Validation
        {
            public static string Holding_Area_Page_Displays = "Validate That The Holding Area Page Displays";
        }


        #endregion
    }
}
