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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

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
        static By _btnInPanelDialog(string btnValue) => By.XPath($"//input[@type='button' and @id='{btnValue}']");
        static By _radStyleChartPanel => By.XPath("//td/input[@type='radio']");

        #endregion

        #region Elements
        public IWebElement SettingsFormElement(string panelType) => StableFindElement(_settingsFormElement(panelType));
        public IWebElement RadTypePanelInPanelDialog(string panelType) => StableFindElement(_radTypePanelInPanelDialog(panelType));
        public IWebElement BtnInPanelDialog(string btnValue) => StableFindElement(_btnInPanelDialog(btnValue));
        public IReadOnlyCollection<IWebElement> RadStyleChartPanel => StableFindElements(_radStyleChartPanel);

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

        public Panel FillInfoChartPanelInPanelDialog(ChartPanel chartPanel)
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
            if (chartPanel.Style != null)
                RadElement(chartPanel.Style).Click();
            if (chartPanel.Series != null)
                CboElement(ComboBox.Series.ToDescription()).SelectItem(chartPanel.Series.ToLower(), "Value");
            if (chartPanel.Legends != null)
                RadElement(chartPanel.Legends).Click();
            if(chartPanel.DataLabels != null)
            {
                foreach (var item in chartPanel.DataLabels)
                {
                    ChkElement(item).Check();
                }
            }
            EndStepNode(node);
            return this;
        }

        public Panel AddChartPanel(ChartPanel chartPanel)
        {
            var node = CreateStepNode();
            node.Info("Add a new Chart panel.");
            ClickLinkButton(LinkButton.AddNew.ToDescription());
            FillInfoChartPanelInPanelDialog(chartPanel);
            ClickButtonInPanelDialog(Common.DashBoardENums.Button.OK.ToDescription());
            EndStepNode(node);
            return this;
        }

        public Panel ClickButtonInPanelDialog(string btnValue)
        {
            var node = CreateStepNode();
            node.Info("On Panel dialog, click button: " + btnValue);
            BtnInPanelDialog(btnValue).Click();
            EndStepNode(node);
            return this;
        }

        public ChartPanel UpdateChartPanel <T> (ChartPanel chartPanel, string updateField, T updateValue)
        {
            Type myType = typeof(ChartPanel);
            PropertyInfo pinfo = myType.GetProperty(updateField);
            pinfo.SetValue(chartPanel, updateValue);
            return chartPanel;
        }

        //public ChartPanel GetChartPanelDialogSettings()
        //{
        //    string dataProfilesValue = CboSelectedValue(ComboBox.DataProfiles.ToDescription()).Text;
        //    string displayNameValue = TxtElement(Textbox.DisplayName.ToDescription()).Text;
        //    string chartTitleValue = TxtElement(Textbox.ChartTitle.ToDescription()).Text;
        //    bool isShowTitle = ChkElement(Checkbox.ShowTitle.ToDescription()).Selected;
        //    string chartTypeValue = CboSelectedValue(ComboBox.ChartType.ToDescription()).Text;
        //    string stype = RadStyleChartPanel.Where(c => c.Selected).ToString();
        //}


        #endregion

        private static class ValidationMessage
        {
            public static string ValidatePanelSettingDisplayAfterDisplayNameField = "Validate Panel Setting form display after Display Name field.";
        }

        
    }
}
