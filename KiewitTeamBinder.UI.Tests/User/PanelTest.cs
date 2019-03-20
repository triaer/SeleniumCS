using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.Models;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static KiewitTeamBinder.Common.DashBoardENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.User
{
    [TestClass]
    public class PanelTest : UITestBase
    {

        [TestMethod]
        public void TC030()
        {
            try
            {
                //Given
                test.Info("Navigate to Dashboard login page");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                var panelData = new PanelData();

                test = LogTest("DA_PANEL_TR030 - Verify when \"Choose panels\" form is expanded all pre-set panels are populated and sorted correctly");
                Login loginPage = new Login(driver);
                MainPage mainPage = new MainPage(driver);
                loginPage.SignOn(panelData.user1)
                    .AddPage(panelData.taPage1)
                    .ClickSymbolMenu(MainMenu.ChoosePanels.ToDescription())
                    .LogValidation<MainPage>(ref validations, mainPage.ValidateInformationInChoosePanels("Charts", panelData.chartInfo))
                    .LogValidation<MainPage>(ref validations, mainPage.ValidateInformationInChoosePanels("Indicators", panelData.indicatorsInfo))
                    .DeleteOnePage(panelData.taPage1.PageName);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void TC034()
        {
            try
            {
                test.Info("Navigate to Dashboard login page");
                var driver = Browser.Open(Constant.HomePage, "chrome");
                var panelData = new PanelData();

                test = LogTest("DA_PANEL_TC034 - Verify correct panel setting form is displayed with corresponding panel type selecteted");
                Login loginPage = new Login(driver);
                Panel panelPage = new Panel(driver);
                loginPage.SignOn(panelData.user1)
                    .ClickSubMenu(MainMenu.Administer.ToDescription(), SubMenu.Panels.ToDescription())
                    .ClickLinkButton(LinkButton.AddNew.ToDescription())
                    .LogValidation<Panel>(ref validations, panelPage.ValidatePanelSettingDisplayAfterDisplayNameField(panelData.ChartSettings))
                    .ClickPanelRadioButton(RadioButton.Indicator.ToDescription())
                    .LogValidation<Panel>(ref validations, panelPage.ValidatePanelSettingDisplayAfterDisplayNameField(panelData.IndicatorSettings));
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void TC036()
        {
            try
            {
                test.Info("Navigate to Dashboard login page");
                var driver = Browser.Open(Constant.HomePage, "chorome");
                var panelData = new PanelData();

                test = LogTest("DA_PANEL_TC036 - Verify \"Data Profile\" listing of \"Add New Panel\" and \"Edit Panel\" control/form are in alphabetical order");
                Login loginPage = new Login(driver);
                Panel panelPage = new Panel(driver);
                loginPage.SignOn(panelData.user1)
                    .ClickSubMenu(MainMenu.Administer.ToDescription(), SubMenu.Panels.ToDescription())
                    .ClickLinkButton(LinkButton.AddNew.ToDescription())
                    .LogValidation<Panel>(ref validations, panelPage.ValidateListInComboboxIsInAlphabeticalOrder(ComboBox.DataProfiles.ToDescription()))
                    .AddPage();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
