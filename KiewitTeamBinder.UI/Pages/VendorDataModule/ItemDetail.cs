using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using OpenQA.Selenium.Interactions;
using System.Drawing;
using KiewitTeamBinder.UI.Pages.Global;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;

namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class ItemDetail : ProjectsDashboard
    {
        #region Entities
        private static By _requiredField(string fieldLabel) => By.XPath($"//span[text()='{fieldLabel}']/following::span[1]");
        private static By _textField(string fieldLabel) => By.XPath($"//td[span[(text()= '{fieldLabel}')]]/following-sibling::td[2]//*[contains(@class,'Text')]");
        private static By _dropdownList(string fieldLabel, string type) => By.XPath($"//td[span[(text()= '{fieldLabel}')]]/following-sibling::td[2]//input[contains(@id, '{type}')]");
        private static By _itemDropdown(string dropdownListName) => By.XPath($"//ul/li[text()='{dropdownListName}']");       
        private static By _saveDocButton => By.Id("SaveDocToolBar");
        private static By _saveSingleDocPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]");
        private static By _saveSingleDocMessage => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]//div[contains(@id, '_message')]");
        private static By _okButtonOnPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]//span[text()='OK']");
       
        public IWebElement RequiredField(string fieldLabel) => StableFindElement(_requiredField(fieldLabel));
        public IWebElement TextField(string fieldLabel) => StableFindElement(_textField(fieldLabel));
        public IWebElement DropdownListInput(string fieldLabel, string type = "") => StableFindElement(_dropdownList(fieldLabel, type + "_Input"));
        public IWebElement DropdownListClientState(string fieldLabel, string type = "") => StableFindElement(_dropdownList(fieldLabel, type + "_ClientState"));        
        public IWebElement ItemDropdown(string dropdownListName) => StableFindElement(_itemDropdown(dropdownListName));        
        public IWebElement SaveDocButton { get { return StableFindElement(_saveDocButton); } }
        public IWebElement SaveMessage { get { return StableFindElement(_saveSingleDocMessage); } }
        public IWebElement OkButtonOnPopUp { get { return StableFindElement(_okButtonOnPopUp); } }
        #endregion

        #region Actions
        public ItemDetail(IWebDriver webDriver) : base(webDriver) { }
        
        public ItemDetail EnterTextField(string fieldName, string content)
        {
            var node = StepNode();
            node.Info($"Enter {content} in {fieldName} Field.");
            TextField(fieldName).InputText(content);            
            return this;
        }
               
        public ItemDetail SelectItemInDropdown(string fieldLabel, string selectedValue, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            IWebElement DropdownList = DropdownListInput(fieldLabel);
            DropdownList.Click();
            methodValidation.Add(ValidateItemDropdownIsHighlighted(selectedValue, fieldLabel));
            ItemDropdown(selectedValue).Click();
            DropdownList.SendKeys(OpenQA.Selenium.Keys.Tab);
            return this;
        }               
        
        public ItemDetail ClickSaveButton()
        {
            ToolBarButton(ToolbarButton.Save.ToDescription()).Click();
            WebDriver.SwitchTo().ActiveElement();
            return this;
        }

        public ItemDetail ClickOkButtonOnPopUp()
        {
            OkButtonOnPopUp.Click();
            WebDriver.SwitchTo().DefaultContent();
            return this;
        }               
               
        public KeyValuePair<string, bool> ValidateItemDropdownIsHighlighted(string value, string idDropdown)
        {
            var node = StepNode();
            try
            {
                node.Info("The Dropdown: " + idDropdown);
                string actual;
                int i = 0;
                do
                {
                    ScrollToElement(ItemDropdown(value));
                    actual = ItemDropdown(value).GetAttribute("class");
                    i++;
                }
                while (!actual.Contains("Hovered") && i < 3);
                if (actual.Contains("Hovered"))
                    return SetPassValidation(node, Validation.Item_Dropdown_Is_Highlighted + idDropdown);

                return SetFailValidation(node, Validation.Item_Dropdown_Is_Highlighted + idDropdown);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Item_Dropdown_Is_Highlighted + idDropdown, e);
            }
        }
                
        public List<KeyValuePair<string, bool>> ValidateRequiredFieldsWithRedAsterisk(string[] requiredFields)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                for (int i = 0; i < requiredFields.Length; i++)
                {
                    node.Info("Check " + requiredFields[i] + " field is marked red asterisk");
                    if (RequiredField(requiredFields[i]).GetAttribute("style") != "display:none")
                        validation.Add(SetPassValidation(node, Validation.Required_Fields_Are_Marked_Red_Asterisk + requiredFields[i]));
                    else
                        validation.Add(SetFailValidation(node, Validation.Required_Fields_Are_Marked_Red_Asterisk + requiredFields[i]));
                }
                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Required_Fields_Are_Marked_Red_Asterisk, e));
                return validation;
            }            
        }
              
        public KeyValuePair<string, bool> ValidateSaveDialogStatus(bool close = false)
        {
            var node = StepNode();

            try
            {
                if (close == true)
                {
                    if (FindElement(_saveSingleDocPopUp) == null)
                        return SetPassValidation(node, Validation.Save_SingleDoc_PopUp_Closed);

                    return SetFailValidation(node, Validation.Save_SingleDoc_PopUp_Closed);
                }

                if (StableFindElement(_saveSingleDocPopUp) != null)
                    return SetPassValidation(node, Validation.Save_SingleDoc_PopUp_Opened);

                return SetFailValidation(node, Validation.Save_SingleDoc_PopUp_Opened);
            }
            catch (Exception e)
            {
                if (close == true)
                    return SetFailValidation(node, Validation.Save_SingleDoc_PopUp_Closed);

                return SetErrorValidation(node, Validation.Document_No_Limit_Retained, e);
            }
        }

        public KeyValuePair<string, bool> ValidateMessageDisplayCorrect()
        {
            var node = StepNode();

            try
            {
                string message = "Saved Successfully";
                string actual = SaveMessage.Text.Trim();
                if (actual == message)
                    return SetPassValidation(node, Validation.Message_Display_Correct);

                return SetFailValidation(node, Validation.Message_Display_Correct, message, actual);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Message_Display_Correct, e);
            }
        }
        #endregion
        private static class Validation
        {
            public static string Document_No_Limit_Retained = "Validate that the Document No is retained limited";
            public static string Required_Fields_Are_Marked_Red_Asterisk = "Validate that the Required Fields are marked red asterisk: - ";
            public static string Item_Dropdown_Is_Highlighted = "Validate that the item is highlighted when hovered or scrolled over in the dropdown: ";
            public static string Save_SingleDoc_PopUp_Closed = "Validate that the Save Single Document PopUp is closed";
            public static string Save_SingleDoc_PopUp_Opened = "Validate that the Save Single Document PopUp is opened";
            public static string Message_Display_Correct = "Validate that the Message is displayed correctly";            
        }
    }
}
