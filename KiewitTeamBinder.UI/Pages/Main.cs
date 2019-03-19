using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages
{
    public class MainPage : LoggedInLanding
    {
        private IWebDriver _driverMainPage;

        #region Locators

        static readonly By _txtNewPagePageName = By.XPath("//div[@id='div_popup']//input[@class='page_txt_name']");
        static readonly By _btnPageOK = By.XPath("//div[@id='div_popup']//input[contains(@onclick,'doAddPage')]");
        static readonly By _btnPageCancel = By.XPath("//div[@id='div_popup']//input[contains(@onclick,'closeWindow')]");
        static readonly By _cmbPageDisplayAfter = By.XPath("//div[@id='div_popup']//select[@id='afterpage']");
        static readonly By _cmbParentPage = By.XPath("//div[@id='div_popup']//select[@id='parent']");
        static readonly By _cbmNumberOfColumns = By.XPath("//div[@id='div_popup']//select[@id='columnnumber']");
        static readonly By _chbPublic = By.XPath("//input[@id='ispublic']");
        static readonly By _dlgPopupHeader = By.XPath("//div[@id='div_popup']//td[@class='ptc']/h2");
        static readonly By _lnkAccount = By.XPath("//a[@href='#Welcome']");
        static readonly By _lnkLogout = By.XPath("//a[@href='logout.do']");
        static readonly By _lblRepositoryName = By.XPath("//a[@href='#Repository']/span");
        static readonly By _tabSetting = By.XPath("//li[@class='mn-setting']");
        static readonly By _btnChoosePanels = By.XPath("//a[@id='btnChoosepanel']");


        static string _lnkMainMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]";
        static string _lnkSubMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]/following::a[contains(.,'{1}')]";
        static string _lnkSettingItem = "//li[@class='mn-setting']//a[ .='{0}']";
        static string _cbbName = "//td[contains(text(), '{0}')]/following-sibling::*/descendant::select";
        static string _lnkPage = "//a[.='{0}']";
        private static By _subGlobalSetting(string subtab) => By.XPath($"//a[contains(text(),'{subtab}')]");
        private static By _globalSettingIcon => By.XPath("//li[@class='mn-setting']");
        private static By _pageName => By.XPath("//input[@id='name']");
        private static By _popupForm => By.XPath("//div[@class='buildin_popup']");
        private static By _parentpageDropdown => By.XPath("//select[@name='parent']");
        private static By _parentPageItems => By.XPath("//select[@id='parent']//option");
        private static By _numberColumnDropdown => By.XPath("//select[@id='columnnumber']");
        private static By _displayAfter => By.XPath("//select[@id='afterpage']");
        private static By _okButton => By.XPath("//td[@align='right']//input[@value='OK']");
        private static By _administerMenu => By.XPath("//a[contains(text(),'Administer')]");
        private static By _administerItems(string item) => By.XPath($"//a[contains(text(),'{item}')]");
        
        #endregion

        #region Elements

        public IWebElement TxtNewPagePageName
        {
            get { return StableFindElement(_txtNewPagePageName); }
        }

        public IWebElement BtnPageOK
        {
            get { return StableFindElement(_btnPageOK); }
        }

        public IWebElement BtnPageCancel
        {
            get { return StableFindElement(_btnPageCancel); }
        }

        public IWebElement CmbNewPageDisplayAfter
        {
            get { return StableFindElement(_cmbPageDisplayAfter); }
        }

        public IWebElement CmbParentPage
        {
            get { return StableFindElement(_cmbParentPage); }
        }

        public IWebElement CmbNumberOfColumns
        {
            get { return StableFindElement(_cbmNumberOfColumns); }
        }

        public IWebElement ChbPublic
        {
            get { return StableFindElement(_chbPublic); }
        }

        public IWebElement DlgPopupHeader
        {
            get { return StableFindElement(_dlgPopupHeader); }
        }
        public IWebElement LnkAccount
        {
            get { return StableFindElement(_lnkAccount); }
        }

        public IWebElement LnkLogout
        {
            get { return StableFindElement(_lnkLogout); }
        }

        public IWebElement LblRepositoryName
        {
            get { return StableFindElement(_lblRepositoryName); }
        }

        public IWebElement TabSetting
        {
            get { return StableFindElement(_tabSetting); }
        }

        public IWebElement BtnChoosePanels
        {
            get { return StableFindElement(_btnChoosePanels); }
        }

        public IWebElement SubGlobalSettingMenu(string subTab) => StableFindElement(_subGlobalSetting(subTab));

        public IWebElement GlobalSettingIcon { get { return StableFindElement(_globalSettingIcon); } }
        public IWebElement PageName { get{ return StableFindElement(_pageName); } }
        public IWebElement ParentPageDropdown { get { return StableFindElement(_parentpageDropdown); } }
        public IWebElement NumberColumnDropdowm { get { return StableFindElement(_numberColumnDropdown); } }
        public IWebElement DisplayAfterDropdown { get { return StableFindElement(_displayAfter); } }
        public IWebElement ButtonOk { get { return StableFindElement(_okButton); } }
        public IWebElement AdministerMenu { get { return StableFindElement(_administerMenu); } }
        public IWebElement AdministerItem(string item) => StableFindElement(_administerItems(item));
        #endregion

        #region Methods

        public MainPage(IWebDriver driver)
            : base(driver)
        {
            this._driverMainPage = driver;
        }

        public MainPage SelectSubMenu(string subTab)
        {
            //string currentWindow;
            HoverElement(_globalSettingIcon);
            SubGlobalSettingMenu(subTab).Click();
            //PageName.InputText("sdasdsa");
            WaitForElement(_popupForm);
            //SwitchToNewPopUpWindow(SubGlobalSettingMenu(subTab), out currentWindow, false);

            return this;
        }


        public MainPage InputPageName(string pageName)
        {
            var node = CreateStepNode();
            node.Info("Input name of page");
            PageName.InputText(pageName);
            EndStepNode(node);
            return this;
        }

        public MainPage SelectParentPage(string parentPageName)
        {
            SelectDropdownByText(ParentPageDropdown, parentPageName);
            return this;
        }

        public MainPage SelectNumberCoumn(string columnNumber)
        {
            SelectDropdownByText(NumberColumnDropdowm, columnNumber);
            return this;
        }

        public MainPage SelectDisplayAfter(string displayApter)
        {
            SelectDropdownByText(DisplayAfterDropdown, displayApter);
            return this;
        }

        public MainPage ClickOKButton()
        {
            ButtonOk.Click();
            return this;
        }

        public DataProfiles NavigateToDataProfilesPage()
        {
            HoverElement(_administerMenu);
            AdministerItem(AdministerMenuENum.DataProfiles.ToDescription()).Click();
            DataProfiles dataProfile = new DataProfiles(WebDriver);
            WaitForElement(dataProfile._dataProfilesColumn);
            return dataProfile;

        }

        public KeyValuePair<string, bool> ValidateDashboardMainPageDisplayed()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool foundDashboardMainpage = LnkAccount.Displayed;
                if (foundDashboardMainpage)
                    validation = SetPassValidation(node, ValidationMessage.ValidateDashboardMainPageDisplayed);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateDashboardMainPageDisplayed);
            }
            catch(Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateDashboardMainPageDisplayed, e);
            }

            EndStepNode(node);
            return validation;           
        }

        public KeyValuePair<string, bool> VlidateTest()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                if (1 == 1)
                {
                    validation = SetPassValidation(node, ValidationMessage.ValidateTest);
                }
            }
            catch (Exception e)
            {

                validation = SetErrorValidation(node, ValidationMessage.ValidateTest, e);
            }
            EndStepNode(node);
            return validation;
        }

        #endregion

        private static class ValidationMessage
        {
            public static string ValidateDashboardMainPageDisplayed = "Validate That TA Dashboard Main Page Displayed";
            public static string ValidateTest = "TA";
        }
    }
}
