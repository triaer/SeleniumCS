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

namespace KiewitTeamBinder.UI.Pages.VendorDataModule
{
    public class DocumentDetail : ProjectsDashboard
    {
        #region Entities
        private string _requiredFieldXpath = "//span[text()='{0}']/following::span[1]";
        private static By _itemDropdown(string dropdownListName) => By.XPath($"//ul/li[text()='{dropdownListName}']");
        private static By _dropdownButton(string idDropdownButton) => By.XPath($"//input[@id='{idDropdownButton}']/following::a[1]");
        private static By _documentNoTextBox => By.Id("txtDocumentNo");
        private static By _revStatusDropdown => By.Id("ComboDocumentRev_Input");
        private static By _statusDropdown => By.Id("ComboDocumentSts_Input");
        private static By _titleTextBox => By.Id("txtDocumentTitle");
        private static By _categoryDropdown => By.Id("ComboDocumentCategory_Input");
        private static By _disciplineDropdown => By.Id("ComboDocumentDisc_Input");
        private static By _typeDropdown => By.Id("ComboDocumentType_Input");
        private static By _attachFilesButton => By.Id("fileUpload");
        private static By _saveDocButton => By.Id("SaveDocToolBar");
        private static By _fromUserLabel => By.Id("txtUploadFromUser");
        private static By _saveSingleDocPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]");
        private static By _saveSingleDocMessage => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]//div[contains(@id, '_message')]");
        private static By _okButtonOnPopUp => By.XPath("//div[contains(@id,'RadWindowWrapper_alert')]//span[text()='OK']");
        //private static By _clientStateValue(string idDropdownButton) => By.XPath($"//input[contains(@id,'{idDropdownButton}_ClientState')]");

        public IWebElement ItemDropdown(string dropdownListName) => StableFindElement(_itemDropdown(dropdownListName));
        public IWebElement DropdownButton(string idDropdownButton) => StableFindElement(_dropdownButton(idDropdownButton));
        public IWebElement DocumentNoTextBox { get { return StableFindElement(_documentNoTextBox); } }
        public IWebElement RevStatusDropdown { get { return StableFindElement(_revStatusDropdown); } }
        public IWebElement StatusDropdown { get { return StableFindElement(_statusDropdown); } }
        public IWebElement TitleTextBox { get { return StableFindElement(_titleTextBox); } }
        public IWebElement CategoryDropdown { get { return StableFindElement(_categoryDropdown); } }
        public IWebElement DisciplineDropdown { get { return StableFindElement(_disciplineDropdown); } }
        public IWebElement TypeDropdown { get { return StableFindElement(_typeDropdown); } }
        public IWebElement AttachFilesButton { get { return StableFindElement(_attachFilesButton); } }
        public IWebElement SaveDocButton { get { return StableFindElement(_saveDocButton); } }
        public IWebElement FromUserLabel { get { return StableFindElement(_fromUserLabel); } }
        public IWebElement SaveSingleDocMessage { get { return StableFindElement(_saveSingleDocMessage); } }
        public IWebElement OkButtonOnPopUp { get { return StableFindElement(_okButtonOnPopUp); } }
        #endregion

        #region Actions
        public DocumentDetail(IWebDriver webDriver) : base(webDriver) { }

        public DocumentDetail EnterDocumentInformation(SingleDocumentInfo singleDocumentInfo, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            var node = StepNode();
            node.Info($"Enter {singleDocumentInfo.DocumentNo} in Document No Field.");
            FillDataForDocument(DocumentNoTextBox, singleDocumentInfo.DocumentNo);
            node.Info("Click Rev Status dropdown, and select: " + singleDocumentInfo.RevStatus);
            SelectItemInDropdown(RevStatusDropdown, singleDocumentInfo.RevStatus, ref methodValidation);
            node.Info("Click Status dropdown, and select: " + singleDocumentInfo.Status);
            SelectItemInDropdown(StatusDropdown, singleDocumentInfo.Status, ref methodValidation);
            node.Info($"Enter {singleDocumentInfo.Title} in Document Title  Field.");
            FillDataForDocument(TitleTextBox, singleDocumentInfo.Title);
            node.Info("Click Category Type dropdown, and select: " + singleDocumentInfo.Category);
            SelectItemInDropdown(CategoryDropdown, singleDocumentInfo.Category, ref methodValidation);
            node.Info("Click Discipline Type dropdown, and select: " + singleDocumentInfo.Discipline);
            SelectItemInDropdown(DisciplineDropdown, singleDocumentInfo.Discipline, ref methodValidation);
            node.Info("Click Type dropdown, and select: " + singleDocumentInfo.Type);
            SelectItemInDropdown(TypeDropdown, singleDocumentInfo.Type, ref methodValidation);
            return this;
        }

        public DocumentDetail SelectItemInDropdown(IWebElement Element, string value, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            string id = Element.GetAttribute("id");
            Element.Click();
            methodValidation.Add(ValidateItemDropdownIsHighlighted(value, id));
            ItemDropdown(value).Click();

            return this;
        }

        public DocumentDetail FillDataForDocument(IWebElement Element, string value)
        {
            Element.InputText(value);
            return this;
        }

        public DocumentDetail ClickAttachFilesButton(string filePath, string fileNames)
        {

            var node = StepNode();
            node.Info("Click Attach Files In the header");
            AttachFilesButton.ClickWithHandling();
            node.Info("Choose files from window explorer form");
            node.Info("Files name: " + fileNames);
            Wait(shortTimeout / 2);
            SendKeys.SendWait(@filePath);
            SendKeys.SendWait(@"{Enter}");
            Wait(shortTimeout / 2);
            SendKeys.SendWait(@fileNames);
            Wait(shortTimeout / 3);
            SendKeys.SendWait(@"{Enter}");
            Wait(shortTimeout / 3);
            
            return this;
        }

        public DocumentDetail ClickSaveButton()
        {
            SaveDocButton.Click();
            WebDriver.SwitchTo().ActiveElement();
            return this;
        }
        public DocumentDetail ClickOkButtonOnPopUp()
        {
            OkButtonOnPopUp.Click();
            return this;
        }

        public List<KeyValuePair<string, bool>> ValidateSelectedItemShowInDropdownBoxesCorrect(SingleDocumentInfo singleDocumentInfo)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();

            validation.Add(ValidateItemDropdownIsSelected(singleDocumentInfo.RevStatus, RevStatusDropdown.GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(singleDocumentInfo.Status, StatusDropdown.GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(singleDocumentInfo.Category, CategoryDropdown.GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(singleDocumentInfo.Discipline, DisciplineDropdown.GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(singleDocumentInfo.Type, TypeDropdown.GetAttribute("id")));
            return validation;
        }

        public KeyValuePair<string, bool> ValidateItemDropdownIsSelected(string value, string idDropdownButton)
        {
            var node = StepNode();
            string selectedText = "";
            try
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

                        if (selectedText.Trim() == value)
                            return SetPassValidation(node, Validation.Item_Dropdown_Is_Selected);
                    }
                }

                return SetFailValidation(node, Validation.Item_Dropdown_Is_Selected);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Item_Dropdown_Is_Selected, e);
            }
        }

        public KeyValuePair<string, bool> ValidateItemDropdownIsHighlighted(string value, string idDropdown)
        {
            var node = StepNode();
            try
            {
                node.Info("The Dropdown: " + idDropdown);
                string actual;

                if (idDropdown.Contains("Rev_Input") || idDropdown.Contains("Category_Input"))
                {
                    ScrollToElement(ItemDropdown(value));
                    ScrollToElement(ItemDropdown(value));
                }

                ScrollToElement(ItemDropdown(value));
                actual = ItemDropdown(value).GetAttribute("class");
                node.Info("Attribute: " + actual);

                if (actual.Contains("Hovered"))
                    return SetPassValidation(node, Validation.Item_Dropdown_Is_Highlighted);

                return SetFailValidation(node, Validation.Item_Dropdown_Is_Highlighted);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Item_Dropdown_Is_Highlighted, e);
            }
        }

        public List<KeyValuePair<string, bool>> ValidateFromUserFieldShowCorrectDataAndFormat(string usernameData, Color color)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            string expect = "";

            string[] user = usernameData.Split(' ');
            for (int i = 0; i < user.Length; i++)
            {
                expect = expect + user[i] + " ";
            }

            try
            {
                //data
                string actual = FromUserLabel.GetValue().Split(',')[0];
                node.Info("User field is display: " + actual);
                if (actual.Trim().Contains(expect.Trim()))
                    validation.Add(SetPassValidation(node, Validation.User_Field_Display_Correct));
                else
                    validation.Add(SetFailValidation(node, Validation.User_Field_Display_Correct, expect, actual));
                //color
                string expectColor = ConvertColorFromRGBToHex(color);
                node.Info("Color of User field: " + expectColor);
                string colorRGBA = FromUserLabel.GetCssValue("background-color");
                string actualColor = ConvertColorFromRGBAToHex(colorRGBA);
                node.Info("Color of User field: " + actualColor);
                if (actualColor == expectColor)
                    validation.Add(SetPassValidation(node, Validation.User_Field_Color_Correct));
                else
                    validation.Add(SetFailValidation(node, Validation.User_Field_Color_Correct, expectColor, actualColor));
                //not editable
                if (StableFindElement(By.XPath("//input[@readonly and @id='txtUploadFromUser']")) != null)
                    validation.Add(SetPassValidation(node, Validation.User_Fields_Cannot_Update));
                else
                    validation.Add(SetFailValidation(node, Validation.User_Fields_Cannot_Update));

                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.User_Field_Display_Correct, e));
                return validation;
            }
        }

        public string ConvertColorFromRGBToHex(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        public string ConvertColorFromRGBAToHex(string color)
        {
            string[] listValue = color.Substring(5).Replace(')', ' ').Split(',');
            int r = Int32.Parse(listValue[0].Trim());
            int g = Int32.Parse(listValue[1].Trim());
            int b = Int32.Parse(listValue[2].Trim());
            Color colorConvert = Color.FromArgb(r, g, b);
            return "#" + colorConvert.R.ToString("X2") + colorConvert.G.ToString("X2") + colorConvert.B.ToString("X2");
        }

        public List<KeyValuePair<string, bool>> ValidateRequiredFieldsWithRedAsterisk(string[] requiredFields)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                for (int i = 0; i < requiredFields.Length; i++)
                {
                    node.Info("Check " + requiredFields[i] + "field is marked red asterisk");
                    if (StableFindElement(By.XPath(string.Format(_requiredFieldXpath, requiredFields[i]))) != null)
                        validation.Add(SetPassValidation(node, Validation.Required_Fields_Are_Marked_Red_Asterisk));
                    else
                        validation.Add(SetFailValidation(node, Validation.Required_Fields_Are_Marked_Red_Asterisk));
                }

                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Required_Fields_Are_Marked_Red_Asterisk, e));
            }
            return validation;
        }

        public KeyValuePair<string, bool> ValidateDocumentNoIsLimitRetained(int maxLength)
        {
            var node = StepNode();

            try
            {
                string actual = DocumentNoTextBox.GetAttribute("maxlength");
                if (actual == maxLength.ToString())
                    return SetPassValidation(node, Validation.Document_No_Limit_Retained);

                return SetFailValidation(node, Validation.Document_No_Limit_Retained, maxLength.ToString(), actual);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Document_No_Limit_Retained, e);
            }
        }

        public KeyValuePair<string, bool> ValidateSaveSingleDocPopUpStatus(bool close = false)
        {
            var node = StepNode();

            try
            {
                if (close == true)
                {
                    if (StableFindElement(_saveSingleDocPopUp) == null)
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
                string message = "Document details saved successfully.";
                string actual = SaveSingleDocMessage.Text.Trim();
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
            public static string User_Field_Display_Correct = "Validate that the User Field is displayed correctly";
            public static string User_Field_Color_Correct = "Validate that the Color of User Field displayed correctly";
            public static string User_Fields_Cannot_Update = "Validate that the User Field is cannot updated";
            public static string Document_No_Limit_Retained = "Validate that the Document No is retained limited";
            public static string Required_Fields_Are_Marked_Red_Asterisk = "Validate that the Required Fields are marked red asterisk";
            public static string Item_Dropdown_Is_Highlighted = "Validate that the Item Dropdown is highlighted";
            public static string Item_Dropdown_Is_Selected = "Validate that the Item Dropdown is selected";
            public static string Save_SingleDoc_PopUp_Closed = "Validate that the Save Single Document PopUp is closed";
            public static string Save_SingleDoc_PopUp_Opened = "Validate that the Save Single Document PopUp is opened";
            public static string Message_Display_Correct = "Validate that the Message is displayed correctly";
        }
    }
}
