using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages
{
    public class MainPage : PageBase
    {
        private IWebDriver _driverMainPage;

        #region Locators

        static readonly By _txtNewPageName = By.XPath("//div[@id='div_popup']//input[@class='page_txt_name']");
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
        static readonly By _addPageBtn = By.XPath("//a[text()='Add Page']");
        
        static readonly By _btnChoosePanels = By.XPath("//a[@id='btnChoosepanel']");
        //static string _lnkmainmenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]";
        //static string _lnksubmenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]/following::a[contains(.,'{1}')]";
        //static string _lnksettingitem = "//li[@class='mn-setting']//a[ .='{0}']";
        //static string _cbbname = "//td[contains(text(), '{0}')]/following-sibling::*/descendant::select";
        //static string _lnkpage = "//a[.='{0}']";

        #endregion

        #region Elements

        public IWebElement TxtNewPagePageName
        {
            get { return StableFindElement(_txtNewPageName); }
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

        public IWebElement AddPageButton
        {
            get { return StableFindElement(_addPageBtn); }
        }

        public IWebElement BtnChoosePanels
        {
            get { return StableFindElement(_btnChoosePanels); }
        }

        #endregion

        #region Methods

        public MainPage(IWebDriver driver) : base(driver)
        {
            this._driverMainPage = driver;
        }

        public Login LogOut()
        {
            HoverElement(_lnkAccount);
            LnkLogout.Click();
            return new Login(this._driverMainPage);
        }

        public MainPage OpenAddPageDialog()
        {
            HoverElement(_tabSetting);
            AddPageButton.Click();
            return this;
        }

        public MainPage AddNewPage(String pageName = "test", String parentPage = "Select parent", int noOfCol = 2, String displayAfter = "Overview", Boolean publicChbx = true)
        {
            TxtNewPagePageName.SendKeys(pageName);
            CmbParentPage.SendKeys(parentPage);
            CmbNumberOfColumns.SendKeys(noOfCol.ToString());
            CmbNewPageDisplayAfter.SendKeys(displayAfter);
            if (publicChbx)
            {
                ChbPublic.Check(); // true, set it as Public page
            }
            else
            {
                // just leave it blank
            }
            BtnPageOK.Click();
            return this;
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

        public KeyValuePair<string, bool> ValidateControlsAreLockedAndDisabled()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                HoverElement(_lnkAccount);
                bool controlEnable = LnkLogout.Enabled;
                if (!controlEnable)
                    validation = SetPassValidation(node, ValidationMessage.ValidateControlsAreLockedAndDisabled);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateControlsAreLockedAndDisabled);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateControlsAreLockedAndDisabled, e);
            }
            EndStepNode(node);
            return validation;
        }

        #endregion

        private static class ValidationMessage
        {
            public static string ValidateDashboardMainPageDisplayed = "Validate That TA Dashboard Main Page Displayed";
            public static string ValidateControlsAreLockedAndDisabled = "Validate That Controls On Main Page Are Locked And Dsiabled";
        }
    }
}
