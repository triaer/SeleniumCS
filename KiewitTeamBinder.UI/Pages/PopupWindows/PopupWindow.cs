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
        private static By _textField(string fieldLabel) => By.XPath($"//*[span[(text()= '{fieldLabel}')]]/following-sibling::*[*]//*[contains(@class,'Text')]");
        private static By _dropdownList(string fieldLabel, string type) => By.XPath($"//*[span[(text()= '{fieldLabel}')]]/following-sibling::*[*]//input[contains(@id, '{type}')]");
        private static By _itemDropdown(string dropdownListName) => By.XPath($"//ul/li[starts-with(text(),'{dropdownListName}')]");
        private static By _saveDocButton => By.Id("SaveDocToolBar");

        public IWebElement HeaderLabel { get { return StableFindElement(_headerLabel); } }
        public IWebElement DropdownListInput(string fieldLabel, string idType = "") => StableFindElement(_dropdownList(fieldLabel, idType + "_Input"));
        public IWebElement DropdownListClientState(string fieldLabel, string idType = "") => FindElement(_dropdownList(fieldLabel, idType + "_ClientState"));
        public IWebElement TextField(string fieldLabel) => StableFindElement(_textField(fieldLabel));
        public IWebElement ToolBarButton(string buttonName) => StableFindElement(_toolBarButton(buttonName));
        public IWebElement AsteriskLabel(string fieldLabel) => StableFindElement(_asteriskLabel(fieldLabel));
        public IWebElement ItemDropdown(string dropdownListName) => StableFindElement(_itemDropdown(dropdownListName));
        public IWebElement SaveDocButton { get { return StableFindElement(_saveDocButton); } }
        #endregion

        #region Actions
        public PopupWindow(IWebDriver webDriver) : base(webDriver)
        { }
        public string GetCurrentWindow()
        {
            return WebDriver.CurrentWindowHandle;
        }

        public T ClickToolbarButtonOnWinPopup<T>(ToolbarButton buttonName, bool checkProgressPopup = false, bool isDisappear = false)
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

        public T EnterTextField<T>(string fieldLabel, string content)
        {
            var node = StepNode();
            node.Info($"Enter {content} in {fieldLabel} Field.");
            WaitForElementEnable(_textField(fieldLabel));
            TextField(fieldLabel).InputText(content);
            WaitForJQueryLoad();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public T SelectItemInDropdown<T>(string fieldLabel, string selectedValue, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            IWebElement DropdownList = DropdownListInput(fieldLabel);
            DropdownList.Click();
            WaitForJQueryLoad();
            methodValidation.Add(ValidateItemDropdownIsHighlighted(selectedValue, fieldLabel));
            ItemDropdown(selectedValue).Click();
            DropdownList.SendKeys(OpenQA.Selenium.Keys.Tab);
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }


        public AlertDialog ClickSaveInToolbarHeader(bool checkProgressPopup = false)
        {
            ClickToolbarButtonOnWinPopup<PopupWindow>(ToolbarButton.Save, checkProgressPopup);
            
            return new AlertDialog(WebDriver);
        }

        public AlertDialog ClickValidateDocumentDetails(ToolbarButton buttonName, ref List<KeyValuePair<string, bool>> methodValidation, string processMessage)
        {
            var dialog = ClickToolbarButtonOnWinPopup<AlertDialog>(buttonName, true);
            methodValidation.Add(ValidateProgressContentMessage(processMessage));
            return dialog;
        }

        public T ClickCloseButtonOnPopUp<T>()
        {
            ClickToolbarButtonOnWinPopup<T>(ToolbarButton.Close);
            WebDriver.SwitchTo().Window(WebDriver.WindowHandles.Last());
            WaitForJQueryLoad();
            WaitForLoadingPanel();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public KeyValuePair<string, bool> ValidateItemDropdownIsHighlighted(string value, string idDropdown)
        {
            var node = StepNode();
            try
            {
                node.Info("The Dropdown: " + idDropdown);
                string actual;
                int i = 0;
                if (idDropdown.Contains("Contract Number") ||  idDropdown.Contains("Rev") || idDropdown.Contains("Category"))
                {
                    ScrollIntoView(ItemDropdown(value));
                    WaitForElementDisplay(_itemDropdown(value));
                }
                do
                {
                    ItemDropdown(value).HoverElement();
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
        public KeyValuePair<string, bool> ValidateItemDropdownIsSelected(string value, string idDropdownButton)
        {
            var node = StepNode();
            string selectedText = "";
            string message = string.Format(Validation.Item_Dropdown_Is_Selected, idDropdownButton.Split('_')[0], value);
            try
            {
                if (idDropdownButton.Contains("Criticality"))
                {
                    string actual = FindElement(By.Id(idDropdownButton)).GetAttribute("value");
                    if (actual.Split('-')[0].Trim() == value)
                        return SetPassValidation(node, message);
                    return SetFailValidation(node, message, value, actual);
                }
                else
                {
                    string id = idDropdownButton.Replace("Input", "ClientState");
                    node.Info("The Dropdown: " + id);
                    string clientStateValue = FindElement(By.Id(id)).GetAttribute("value");
                    string[] attributeValues = clientStateValue.Split(',');
                    foreach (var attributeValue in attributeValues)
                    {
                        if (attributeValue.Contains("text"))
                        {
                            selectedText = attributeValue.Split(':')[1];
                            selectedText = selectedText.Replace("\"", "");

                            if (selectedText.Split('-')[0].Trim() == value)
                                return SetPassValidation(node, message);
                        }
                    }

                    return SetFailValidation(node, message, value, selectedText);
                }
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, message, e);
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

        public KeyValuePair<string, bool> ValidateHeaderIsCorrect(string expectedHeader)
        {
            var node = StepNode();
            try
            {
                string actualHeader = HeaderLabel.Text.Trim();
                if (actualHeader == expectedHeader)
                    return SetPassValidation(node, Validation.Header_Is_Correct + expectedHeader);
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
            public static string Message_Display_Correct = "Validate that the message is displayed correctly";
            public static string Header_Is_Correct = "Validate that the header is correct: ";
            public static string Item_Dropdown_Is_Selected = "Validate that the {0} dropdown selected item '{1}' ";
        }
        #endregion
    }
}
