using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using static KiewitTeamBinder.Common.DashBoardENums;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages;
using KiewitTeamBinder.UI.Tests.User;

namespace KiewitTeamBinder.UI.Pages
{
    public class Panel : DataProfilesAndPanelTable
    {
        public Panel(IWebDriver driver) : base(driver)
        {
        }

        #region Locators
        static By _settingsFormElement(string panelType) => By.XPath($"//td[@class='firstCol' and contains(text(),'Display Name')]/ancestor::table[@id='infoSettings']/following-sibling::div//legend[text()='{panelType}']");
        static By _radTypePanelInPanelDialog(string panelType) => By.XPath($"//label[normalize-space()='{panelType}']/input");

        #endregion

        #region Elements
        public IWebElement SettingsFormElement(string panelType) => StableFindElement(_settingsFormElement(panelType));
        public IWebElement RadTypePanelInPanelDialog(string panelType) => StableFindElement(_radTypePanelInPanelDialog(panelType));
        #endregion

        #region Methods

        public KeyValuePair<string, bool> ValidatePanelSettingDisplayAfterDisplayNameField(string panelType)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool isDisplayAfter = SettingsFormElement(panelType).IsDisplayed();
                if (isDisplayAfter)
                    validation = SetPassValidation(node, ValidationMessage.ValidatePanelSettingDisplayAfterDisplayNameField);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePanelSettingDisplayAfterDisplayNameField);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePanelSettingDisplayAfterDisplayNameField, e);
            }
            EndStepNode(node);
            return validation;
        }

        public Panel ClickPanelRadioButton(string panelType)
        {
            var node = CreateStepNode();
            node.Info("Click the radio button: " + panelType);
            RadTypePanelInPanelDialog(panelType).Click();
            return this;
        }

        public Panel EnterInfoInGeneralFieldOfPanelDialog(string type = "Chart", string dataProfile = null, string displayName = null)
        {
            var node = CreateStepNode();
            node.Info("Enter information in Panel dialog: " + type + ", " + dataProfile + ", " + displayName);
            if(type != "Chart")
            {
                ClickPanelRadioButton(type);
            }
            if(dataProfile != null)
            {
                CboElement(ComboBox.DataProfiles.ToDescription()).SelectItem(dataProfile);
            }
            if(displayName != null)
            {
                TxtElement(Textbox.DisplayName.ToDescription()).InputText(displayName);
            }
            EndStepNode(node);
            return this;
        }

        public Panel EnterInfoChartPanelInPanelDialog(ChartPanel chartPanel)
        {
            var node = CreateStepNode();
            node.Info("Enter information for Chart Panel");
            EnterInfoInGeneralFieldOfPanelDialog(chartPanel.Type, chartPanel.DataProfile, chartPanel.DisplayName);
            if(chartPanel.ChartTitle != null && chartPanel.ChartTitle != TxtElement(Textbox.ChartTitle.ToDescription()).Text)
                TxtElement(Textbox.ChartTitle.ToDescription()).InputText(chartPanel.ChartTitle);
            if (chartPanel.ShowTitle)
                ChkElement(Checkbox.ShowTitle.ToDescription()).Check();
            else
                ChkElement(Checkbox.ShowTitle.ToDescription()).UnCheck();
            if (chartPanel.ChartType != null)
                CboElement(ComboBox.ChartType.ToDescription()).SelectItem(chartPanel.ChartType);

            EndStepNode(node);
            return this;
        }

        #endregion

        private static class ValidationMessage
        {
            public static string ValidatePanelSettingDisplayAfterDisplayNameField = "Validate Panel Setting form display after Display Name field.";
        }

        
    }
}
