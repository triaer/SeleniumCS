using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.PopupWindows;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using System.Windows.Forms;

namespace KiewitTeamBinder.UI.Pages.Dialogs
{
    public class GenerateHyperlinkDialog : StandardReports
    {
        #region Entities
        private static By _dialog => By.XPath("//div[@id= 'RadWindowWrapper_MicrosoftReportViewer_GenerateDocLink_RadWndGenerateLink']");
        private static By _closeButton => By.XPath("//input[contains(@id, 'btnClose_input')]");
        private static By _hyperlink => By.Id("lnkCopy");
        private static By _dialogTitle => By.XPath(".//tr[@class='rwTitleRow']//tr//em");
        private static By _generatedLinkTextbox => By.Id("divGeneratedLink");
        
        //public IWebElement Dialog { get { return StableFindElement(_dialog); } }
        public IWebElement CloseButton { get { return StableFindElement(_closeButton); } }
        public IWebElement Hyperlink { get { return StableFindElement(_hyperlink); } }
        public IWebElement GeneratedLinkTextbox { get { return StableFindElement(_generatedLinkTextbox); } }
        
        #endregion

        #region Actions
        public GenerateHyperlinkDialog(IWebDriver webDriver) : base(webDriver)
        {
            //webDriver.SwitchTo().ActiveElement();
            var node = StepNode();
            try
            {
                IWebElement Title = StableFindElement(_dialog).FindElement(_dialogTitle);
                node.Info("Dialog shows title: " + Title.Text);
            }
            catch
            {
                node.Info("Generate Hyperlink dialog not display");
            }
        }

        public StandardReports ClickCloseButton(ref By currentIframe, ref List<KeyValuePair<string, bool>> methodValidation)
        {
            var node = StepNode();
            node.Info("Click Close in the Generate Hyperlink Dialog box");
            CloseButton.Click();
            methodValidation.Add(ValidateGenerateHyperlinkDialogIsClosed());
            SwitchToFrame(ref currentIframe, null);
            SwitchToFrame(ref currentIframe, reportViewIframe);
            return new StandardReports(WebDriver);
        }

        public string CopyHyperlink()
        {
            var node = StepNode();
            if (WaitUntil(driver => GeneratedLinkTextbox.Text != ""))
            {
                node.Info("Copy the report link in the textbox to clipboard. Text is in textbox: " + GeneratedLinkTextbox.Text);
                Hyperlink.HoverAndClickWithJS();
                //Wait(shortTimeout);
                //SendKeys.SendWait(@"{Tab}");
                //Wait(shortTimeout / 2);
                //SendKeys.SendWait(@"{Enter}");
                //Wait(shortTimeout / 2);
                node.Debug("After clicking the Copy button ", AttachScreenshot(GetCaptureScreenshot()));
                var link = System.Windows.Clipboard.GetText();
                node.Info("Coppied Hyperlink: " + link);

                return (link != "") ? link : GeneratedLinkTextbox.Text;
            }
            else
            {
                node.Info("The Generated Link Textbox has not value");
                return "";
            }
        }

        private KeyValuePair<string, bool> ValidateGenerateHyperlinkDialogIsClosed()
        {
            var node = StepNode();
            try
            {
                var Dialog = FindElement(_dialog);
                if (!Dialog.IsDisplayed())
                    return SetPassValidation(node, Validation.Generate_Hyperlink_Dialog_Is_Closed);

                return SetFailValidation(node, Validation.Generate_Hyperlink_Dialog_Is_Closed);               
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Generate_Hyperlink_Dialog_Is_Closed, e);
            }
        }
        
        private static class Validation
        {
            public static string Generate_Hyperlink_Dialog_Is_Closed = "Validate that the generate hyperlink dialog is closed";
        }
        #endregion
    }
}
