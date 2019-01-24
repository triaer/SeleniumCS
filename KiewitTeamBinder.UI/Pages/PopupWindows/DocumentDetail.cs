using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models.VendorData;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using System.Drawing;


namespace KiewitTeamBinder.UI.Pages.PopupWindows
{
    public class DocumentDetail : PopupWindow
    {
        #region Entities
        
        private static By _documentNoTextBox => By.Id("txtDocumentNo");
        private static By _revStatusDropdown => By.Id("ComboDocumentRev_Input");
        private static By _statusDropdown => By.Id("ComboDocumentSts_Input");
        private static By _titleTextBox => By.Id("txtDocumentTitle");
        private static By _categoryDropdown => By.Id("ComboDocumentCategory_Input");
        private static By _disciplineDropdown => By.Id("ComboDocumentDisc_Input");
        private static By _typeDropdown => By.Id("ComboDocumentType_Input");
        private static By _attachFilesButton => By.Id("fileUpload");       
        private static By _fromUserLabel => By.Id("txtUploadFromUser");
        
        public IWebElement DocumentNoTextBox { get { return StableFindElement(_documentNoTextBox); } }
        public IWebElement RevStatusDropdown { get { return StableFindElement(_revStatusDropdown); } }
        public IWebElement StatusDropdown { get { return StableFindElement(_statusDropdown); } }
        public IWebElement CategoryDropdown { get { return StableFindElement(_categoryDropdown); } }
        public IWebElement DisciplineDropdown { get { return StableFindElement(_disciplineDropdown); } }
        public IWebElement TypeDropdown { get { return StableFindElement(_typeDropdown); } }
        public IWebElement AttachFilesButton { get { return StableFindElement(_attachFilesButton); } }
        public IWebElement FromUserLabel { get { return StableFindElement(_fromUserLabel); } }
        #endregion

        #region Actions
        public DocumentDetail(IWebDriver webDriver) : base(webDriver) { }

        public DocumentDetail EnterDocumentInformation(SingleDocumentInfo singleDocumentInfo, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            var node = StepNode();
            node.Info($"Enter {singleDocumentInfo.DocumentNo} in Document No Field.");
            EnterTextField<DocumentDetail>("Document No.", singleDocumentInfo.DocumentNo);
            node.Info("Click Rev Status dropdown, and select: " + singleDocumentInfo.RevStatus);
            SelectItemInDropdown<DocumentDetail>("Rev", singleDocumentInfo.RevStatus, ref methodValidation);
            node.Info("Click Status dropdown, and select: " + singleDocumentInfo.Status);
            SelectItemInDropdown<DocumentDetail>("Status", singleDocumentInfo.Status, ref methodValidation);
            node.Info($"Enter {singleDocumentInfo.Title} in Document Title  Field.");
            EnterTextField<DocumentDetail>("Title", singleDocumentInfo.Title);
            node.Info("Click Category Type dropdown, and select: " + singleDocumentInfo.Category);
            SelectItemInDropdown<DocumentDetail>("Category", singleDocumentInfo.Category, ref methodValidation);
            node.Info("Click Discipline Type dropdown, and select: " + singleDocumentInfo.Discipline);
            SelectItemInDropdown<DocumentDetail>("Discipline", singleDocumentInfo.Discipline, ref methodValidation);
            node.Info("Click Type dropdown, and select: " + singleDocumentInfo.Type);
            SelectItemInDropdown<DocumentDetail>("Type", singleDocumentInfo.Type, ref methodValidation);
            node.Debug("After entering value in fields", AttachScreenshot(GetCaptureScreenshot()));
            return this;
        }              

        public DocumentDetail ClickAttachFilesButton(string filePath, string fileNames)
        {
            var node = StepNode();
            node.Info("Click Attach Files In the header");
            AttachFilesButton.ClickWithHandleTimeout();
            node.Info("Choose files from window explorer form");
            node.Info("Files name: " + fileNames);
            UploadFiles(filePath, fileNames);
            WaitForJQueryLoad();
            return this;
        }        

        public List<KeyValuePair<string, bool>> ValidateSelectedItemShowInDropdownBoxesCorrect(SingleDocumentInfo singleDocumentInfo)
        {
            var validation = new List<KeyValuePair<string, bool>>();
            validation.Add(ValidateItemDropdownIsSelected(singleDocumentInfo.RevStatus, RevStatusDropdown.GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(singleDocumentInfo.Status, StatusDropdown.GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(singleDocumentInfo.Category, CategoryDropdown.GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(singleDocumentInfo.Discipline, DisciplineDropdown.GetAttribute("id")));
            validation.Add(ValidateItemDropdownIsSelected(singleDocumentInfo.Type, TypeDropdown.GetAttribute("id")));
            return validation;
        }


        //public override KeyValuePair<string, bool> ValidateItemDropdownIsHighlighted(string value, string idDropdown)
        //{
        //    var node = StepNode();
        //    try
        //    {
        //        node.Info("The Dropdown: " + idDropdown);
        //        string actual;

        //        if (idDropdown.Contains("Rev") || idDropdown.Contains("Category"))
        //        {
        //            ScrollIntoView(ItemDropdown(value));
        //            WaitForElementDisplay(_itemDropdown(value));
        //            ScrollToElement(ItemDropdown(value));
        //        }

        //        ScrollToElement(ItemDropdown(value));
        //        actual = ItemDropdown(value).GetAttribute("class");
        //        if (actual.Contains("Hovered"))
        //            return SetPassValidation(node, Validation.Item_Dropdown_Is_Highlighted + idDropdown);

        //        return SetFailValidation(node, Validation.Item_Dropdown_Is_Highlighted + idDropdown);
        //    }
        //    catch (Exception e)
        //    {
        //        return SetErrorValidation(node, Validation.Item_Dropdown_Is_Highlighted + idDropdown, e);
        //    }
        //}

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

        private string ConvertColorFromRGBToHex(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        private string ConvertColorFromRGBAToHex(string color)
        {
            string[] listValue = color.Substring(5).Replace(')', ' ').Split(',');
            int r = Int32.Parse(listValue[0].Trim());
            int g = Int32.Parse(listValue[1].Trim());
            int b = Int32.Parse(listValue[2].Trim());
            Color colorConvert = Color.FromArgb(r, g, b);
            return "#" + colorConvert.R.ToString("X2") + colorConvert.G.ToString("X2") + colorConvert.B.ToString("X2");
        }               

        public KeyValuePair<string, bool> ValidateDocumentNoIsLimitRetained(int maxLength)
        {
            var node = StepNode();
            try
            {
                string actual = DocumentNoTextBox.GetAttribute("maxlength");
                if (actual == maxLength.ToString())
                    return SetPassValidation(node, Validation.Document_No_Limit_Retained);
                else
                return SetFailValidation(node, Validation.Document_No_Limit_Retained, maxLength.ToString(), actual);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Document_No_Limit_Retained, e);
            }
        }
        
        private static class Validation
        {
            public static string User_Field_Display_Correct = "Validate that the User Field is displayed correctly";
            public static string User_Field_Color_Correct = "Validate that the Color of User Field displayed correctly";
            public static string User_Fields_Cannot_Update = "Validate that the User Field is cannot updated";
            public static string Document_No_Limit_Retained = "Validate that the Document No is retained limited";
            public static string Item_Dropdown_Is_Highlighted = "Validate that the item is highlighted when hovered or scrolled over in the dropdown: ";
        }
        #endregion
    }
}
