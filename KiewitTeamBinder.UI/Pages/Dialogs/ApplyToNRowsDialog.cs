﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiewitTeamBinder.Common;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Windows.Forms;
using KiewitTeamBinder.UI.Pages.Global;
using AventStack.ExtentReports;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.Dialogs
{
    public class ApplyToNRowsDialog : LoggedInLanding
    {
        #region Entities
        public string IFrameName = "RadWindowConfirm";
        public static By _message => By.XPath("//*[@id ='tdMessageBody']");
        public static By _nTextbox => By.Id("txtInput");
        public static By _oKButton => By.XPath("//a[input[@value = 'OK']]");
        public static By _cancelButton => By.XPath("//a[input[@value = 'Cancel']]");

        public IWebElement Message { get { return StableFindElement(_message); } }
        public IWebElement NTextbox { get { return StableFindElement(_nTextbox); } }
        public IWebElement OKButton { get { return StableFindElement(_oKButton); } }
        public IWebElement CancelButton { get { return StableFindElement(_cancelButton); } }
        #endregion

        #region Actions
        public ApplyToNRowsDialog(IWebDriver webDriver) : base(webDriver)
        {

        }

        public ApplyToNRowsDialog EnterNumberOfRow(int numberOfRow)
        {
            NTextbox.InputText(numberOfRow.ToString());
            return this;
        }

        public T ClickOKButton<T>()
        {
            OKButton.Click();
            WebDriver.SwitchTo().DefaultContent();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public void ClickCancelButton()
        {
            CancelButton.HoverAndClickWithJS();
            WebDriver.SwitchTo().DefaultContent();
        }

        private string GetMessageOnDialog()
        {
            string numberOfRow = NTextbox.GetAttribute("value");
            string message = Message.Text.Replace("\r\n", " " +numberOfRow + " ");
            return message;
        }

        //public KeyValuePair<string, bool> ValidateMessageOnDialog(string expectedMessage)
        //{
        //    var node = StepNode();
        //    try
        //    {
        //        if (expectedMessage == GetMessageOnDialog())
        //            return SetPassValidation(node, Validation.Message_On_Dialog);
        //        else
        //            return SetFailValidation(node, Validation.Message_On_Dialog);
        //    }
        //    catch (Exception e)
        //    {
        //        return SetErrorValidation(node, Validation.Message_On_Dialog, e);
        //    }
        //}

        public List<KeyValuePair<string, bool>> ValidateApplyToNRowsDialogDisplaysCorrectly(string expectedMessage, bool checkOpened = true)
        {
            var node = StepNode();
            var validation = new List<KeyValuePair<string, bool>>();
            try
            {
                IWebElement Board = StableFindElement(By.XPath("//form[@id='form1']/table[@id='tblMessage']"));
                if (checkOpened)
                    validation.Add((Board != null) ? SetPassValidation(node, Validation.Dialog_Opens)
                                                   : SetFailValidation(node, Validation.Dialog_Opens));
                else
                {
                    validation.Add((Board == null) ? SetPassValidation(node, Validation.Dialog_Closes)
                                                   : SetFailValidation(node, Validation.Dialog_Closes));
                    return validation;
                }

                if (Board != null)
                {
                    string actualContent = GetMessageOnDialog();
                    if (expectedMessage == actualContent)
                        validation.Add(SetPassValidation(node, Validation.Message_On_Dialog + actualContent));
                    else
                        validation.Add(SetFailValidation(node, Validation.Message_On_Dialog,expectedMessage,actualContent));

                    if (FindElement(_nTextbox) != null)
                        validation.Add(SetPassValidation(node, Validation.Edit_Textbox_Displays));
                    else
                        validation.Add(SetFailValidation(node, Validation.Edit_Textbox_Displays));

                    if (FindElement(_oKButton) != null)
                        validation.Add(SetPassValidation(node, Validation.OK_Button_Displays));
                    else
                        validation.Add(SetFailValidation(node, Validation.OK_Button_Displays));

                    if (FindElement(_cancelButton) != null)
                        validation.Add(SetPassValidation(node, Validation.Cancel_Button_Displays));
                    else
                        validation.Add(SetFailValidation(node, Validation.Cancel_Button_Displays));
                }
                return validation;
            }
            catch (Exception e)
            {
                validation.Add(SetErrorValidation(node, Validation.Message_On_Dialog, e));
                return validation;
            }
        }

        public KeyValuePair<string, bool> ValidateDialogOpens(bool checkOpened)
        {
            var node = StepNode();
            try
            {
                IWebElement Board = StableFindElement(By.XPath("//form[@id='form1']/div[contains(@id,'_RadWindowConfirm')]"));
                if (Board.GetAttribute("id").ToLower().Contains("confirm") == checkOpened)
                    return SetPassValidation(node, Validation.Dialog_Opens);

                return SetFailValidation(node, Validation.Dialog_Opens);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Dialog_Opens, e);
            }
        }        

        private static class Validation
        {
            public static string Message_On_Dialog = "Validate The Content Of Message On Dialog Displays Correctly: ";
            public static string Dialog_Opens = "Validate That The Dialog Opens";
            public static string Dialog_Closes = "Validate That The Dialog Closes";
            public static string OK_Button_Displays = "Validate The OK Button Displays";
            public static string Cancel_Button_Displays = "Validate The Cancel Button Displays";
            public static string Edit_Textbox_Displays = "Validate The Edit Textbox Displays";

        }

        #endregion
    }
}
