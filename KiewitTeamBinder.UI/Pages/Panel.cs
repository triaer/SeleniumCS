using Agoda.UI.Pages.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Agoda.UI.ExtentReportsHelper;

namespace Agoda.UI.Pages
{
    public class Panel : PageBase
    {
        private IWebDriver _driver;

        #region Locators
        static readonly By _lnkAddNew = By.XPath("//a[text()='Add New']");
        static readonly By _lnkDelete = By.XPath("//a[text()='Delete']");
        static readonly By _lnkCheckAll = By.XPath("//a[text()='Check All']");
        static readonly By _lnkUnCheckAll = By.XPath("//a[text()='UnCheck All']");

        static private string _panelLnk = "//a[text()='{0}']";
        static private string _chkPanel = "//a[text()='{0}']/ancestor::tr//input[@id='chkDelPanel']";
        static private string _editPanel = "//a[text()='{0}']/ancestor::tr//a[text()='Edit']";
        static private string _deletePanel = "//a[text()='{0}']/ancestor::tr//a[text()='Delete']";

        static readonly By _txtDisplayName = By.Id("txtDisplayName");
        static readonly By _slSeries = By.Id("cbbSeriesField");
        static readonly By _btnOK = By.XPath("//input[@id='OK']");
        static readonly By _btnCancel = By.XPath("//input[@id='Cancel']");

        #endregion

        #region Elements
        public IWebElement LinkAddNewPanel
        {
            get { return StableFindElement(_lnkAddNew); }
        }

        public IWebElement LinkDeletePanels
        {
            get { return StableFindElement(_lnkDelete); }
        }

        public IWebElement LinkCheckAll
        {
            get { return StableFindElement(_lnkCheckAll); }
        }

        public IWebElement LinkUnCheckAll
        {
            get { return StableFindElement(_lnkUnCheckAll); }
        }

        public IWebElement PanelLink (string panelName)
        {
            return StableFindElement(By.XPath(string.Format(_panelLnk, panelName))); 
        }

        public IWebElement PanelCheckBox(string panelName)
        {
            return StableFindElement(By.XPath(string.Format(_chkPanel, panelName)));
        }

        public IWebElement LinkEditPanel(string panelName)
        {
            return StableFindElement(By.XPath(string.Format(_editPanel, panelName)));
        }

        public IWebElement LinkDeletePanel(string panelName)
        {
            return StableFindElement(By.XPath(string.Format(_deletePanel, panelName)));
        }

        public IWebElement TxtDisplayName
        {
            get { return StableFindElement(_txtDisplayName); }
        }

        public IWebElement CbbSeries
        {
            get { return StableFindElement(_slSeries); }
        }

        public IWebElement BtnOK
        {
            get { return StableFindElement(_btnOK); }
        }

        public IWebElement BtnCancel
        {
            get { return StableFindElement(_btnCancel); }
        }
        #endregion

        #region Methods

        public Panel(IWebDriver driver) : base(driver)
        {
            this._driver = driver;
        }

        public Panel SelectPanel (string panelName)
        {
            PanelCheckBox(panelName).Check();
            return this;
        }

        private Panel OpenAddNewPanelDialog()
        {
            LinkAddNewPanel.Click();
            return this;
        }

        public Panel AddNewPanel(string displayName = null, string seriesValue = "name")
        {
            this.OpenAddNewPanelDialog();
            TxtDisplayName.SendKeys(displayName);
            CbbSeries.SelectItem(seriesValue, "Value");
            BtnOK.Click();
            return this;
        }

        public Panel DismissPanelDialog()
        {
            BtnCancel.Click();
            return this;
        }

        public Panel EditPanel (string panelName)
        {
            LinkEditPanel(panelName).Click();
            return this;
        }

        public Panel DeletePanel (string panelName)
        {
            LinkDeletePanel(panelName).Click();
            return this;
        }

        public Panel DeleteSelectedPanels(string panelName)
        {
            SelectPanel(panelName);
            LinkDeletePanels.Click();
            return this;
        }

        private string getAlertMessage()
        {
            return this._driver.SwitchTo().Alert().Text;
        }
        
        public KeyValuePair<string, bool> ValidateErrorMessage(string expectedMsg)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                var foundErrorMessage = getAlertMessage();
                Console.WriteLine(expectedMsg + "\r" + foundErrorMessage);
                
                if (foundErrorMessage.Equals(expectedMsg))
                    validation = SetPassValidation(node, ValidationMessage.ValidateErrorMessage);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateErrorMessage);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateErrorMessage, e);
            }

            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> ValidatePanelExisted(string panelName)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool isPanelDisplayed = PanelLink(panelName).Displayed;
                if (isPanelDisplayed.Equals(true))
                    validation = SetPassValidation(node, ValidationMessage.ValidatePanelExisted);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePanelExisted);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePanelExisted, e);
            }

            EndStepNode(node);
            return validation;
        }

        private static class ValidationMessage
        {
            public static string ValidateErrorMessage = "Validate That Error Message Is Correct";
            public static string ValidatePanelExisted = "Validate That The Panel Is Existed";
        }

        #endregion
    }
}
