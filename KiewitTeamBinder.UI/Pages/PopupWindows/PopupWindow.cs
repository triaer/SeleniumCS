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


namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class PopupWindow : ProjectsDashboard
    {
        #region Entities     
        private static By _headerLabel => By.Id("LabelType");
        private static By _toolBarButton(string buttonName) => By.XPath($"//div[contains(@class, 'ToolBar')]//a[span='{buttonName}']");
        private static By _asteriskLabel(string fieldLabel) => By.XPath($"//span[text()='{fieldLabel}']/following::span[1]");
        private static By _textField(string fieldLabel) => By.XPath($"//*[span[(text()= '{fieldLabel}')]]/following-sibling::*[2]//*[contains(@class,'Text')]");
        private static By _dropdownList(string fieldLabel, string type) => By.XPath($"//*[span[(text()= '{fieldLabel}')]]/following-sibling::*[2]//input[contains(@id, '{type}')]");
        private static By _itemDropdown(string dropdownListName) => By.XPath($"//ul/li[text()='{dropdownListName}']");
        private static By _saveDocButton => By.Id("SaveDocToolBar");
        private static By _okButtonOnPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]//span[text()='OK']");
        private static By _saveItemPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]");
        private static By _saveItemMessage => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]//div[contains(@id, '_message')]");

        public IWebElement HeaderLabel { get { return StableFindElement(_headerLabel); } }
        public IWebElement DropdownListInput(string fieldLabel, string idType = "") => StableFindElement(_dropdownList(fieldLabel, idType + "_Input"));
        public IWebElement DropdownListClientState(string fieldLabel, string idType = "") => StableFindElement(_dropdownList(fieldLabel, idType + "_ClientState"));
        public IWebElement TextField(string fieldLabel) => StableFindElement(_textField(fieldLabel));
        public IWebElement ToolBarButton(string buttonName) => StableFindElement(_toolBarButton(buttonName));
        public IWebElement AsteriskLabel(string fieldLabel) => StableFindElement(_asteriskLabel(fieldLabel));
        public IWebElement ItemDropdown(string dropdownListName) => StableFindElement(_itemDropdown(dropdownListName));
        public IWebElement SaveDocButton { get { return StableFindElement(_saveDocButton); } }
        public IWebElement OkButtonOnPopUp { get { return StableFindElement(_okButtonOnPopUp); } }
        public IWebElement SaveMessage { get { return StableFindElement(_saveItemMessage); } }
        #endregion

        #region Actions
        public PopupWindow(IWebDriver webDriver) : base(webDriver)
        { }
        public string GetCurrentWindow()
        {
            return WebDriver.CurrentWindowHandle;
        }

        public T ClickToolbarButton<T>(ToolbarButton buttonName, bool checkProgressPopup = false, bool isDisappear = false)
        {
            var node = StepNode();
            node.Info("Click the button: " + buttonName.ToDescription());

            if (isDisappear == true)
                IWebElementExtensions.HoverAndClickWithJS(ToolBarButton(buttonName.ToDescription()));   
            else 
                ToolBarButton(buttonName.ToDescription()).Click();

            if (checkProgressPopup)
                WaitForLoading(_progressPopUp);

            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T EnterTextField<T>(string fieldName, string content)
        {
            var node = StepNode();
            node.Info($"Enter {content} in {fieldName} Field.");
            TextField(fieldName).InputText(content);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T SelectItemInDropdown<T>(string fieldLabel, string selectedValue, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            IWebElement DropdownList = DropdownListInput(fieldLabel);
            DropdownList.Click();
            methodValidation.Add(ValidateItemDropdownIsHighlighted(selectedValue, fieldLabel));
            ItemDropdown(selectedValue).Click();
            DropdownList.SendKeys(OpenQA.Selenium.Keys.Tab);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T ClickSaveButton<T>()
        {
            ClickToolbarButton<T>(ToolbarButton.Save, true);
            WebDriver.SwitchTo().ActiveElement();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T ClickOkButtonOnPopUp<T>()
        {
            OkButtonOnPopUp.Click();
            WebDriver.SwitchTo().DefaultContent();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }
        public AlertDialog ClickValidateDocumentDetails(ToolbarButton buttonName, ref List<KeyValuePair<string, bool>> methodValidation, string processMessage)
        {
            var dialog = ClickToolbarButton<AlertDialog>(buttonName, true);
            methodValidation.Add(ValidateProgressContentMessage(processMessage));
            return dialog;
        }

        public T ClickCloseButtonOnPopUp<T>()
        {
            ClickToolbarButton<T>(ToolbarButton.Close);
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles.Last());
            WaitForLoadingPanel();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public virtual KeyValuePair<string, bool> ValidateItemDropdownIsHighlighted(string value, string idDropdown)
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
                    if (AsteriskLabel(requiredFields[i]).GetAttribute("style") != "display:none")
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

        public KeyValuePair<string, bool> ValidateSaveDialogStatus(bool closed = false)
        {
            var node = StepNode();

            try
            {
                if (closed == true)
                {
                    if (StableFindElement(_saveItemPopUp) != null)
                        return SetFailValidation(node, Validation.Save_PopUp_Closed);

                    return SetPassValidation(node, Validation.Save_PopUp_Closed);
                }

                if (StableFindElement(_saveItemPopUp) != null)
                    return SetPassValidation(node, Validation.Save_PopUp_Opened);

                return SetFailValidation(node, Validation.Save_PopUp_Opened);
            }
            catch (Exception e)
            {
                if (closed == true)
                    return SetErrorValidation(node, Validation.Save_PopUp_Closed, e);

                return SetErrorValidation(node, Validation.Save_PopUp_Opened, e);
            }
        }

        public KeyValuePair<string, bool> ValidateMessageDisplayCorrect(string expectedMessage)
        {
            var node = StepNode();
            try
            {
                string actualMessage = SaveMessage.Text.Trim();
                if (actualMessage == expectedMessage)
                    return SetPassValidation(node, Validation.Message_Display_Correct);
                else
                    return SetFailValidation(node, Validation.Message_Display_Correct, expectedMessage, actualMessage);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Message_Display_Correct, e);
            }
        }

        public KeyValuePair<string, bool> ValidateHeaderIsCorrect(string expectedHeader)
        {
            var node = StepNode();
            try
            {
                string actualHeader = HeaderLabel.Text.Trim();
                if (actualHeader == expectedHeader)
                    return SetPassValidation(node, Validation.Header_Is_Correct);
                else
                    return SetFailValidation(node, Validation.Header_Is_Correct, expectedHeader, actualHeader);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Header_Is_Correct, e);
            }
        }

        private static class Validation
        {
            public static string Required_Fields_Are_Marked_Red_Asterisk = "Validate that the Required Fields are marked red asterisk: - ";
            public static string Item_Dropdown_Is_Highlighted = "Validate that the item is highlighted when hovered or scrolled over in the dropdown: ";
            public static string Save_PopUp_Closed = "Validate that the save popup is closed";
            public static string Save_PopUp_Opened = "Validate that the save popup is opened";
            public static string Message_Display_Correct = "Validate that the message is displayed correctly";
            public static string Header_Is_Correct = "Validate that the header is correct";
        }
        #endregion
    }
}
