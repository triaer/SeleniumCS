using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        static readonly By _deletePageBtn = By.XPath("//a[text()='Delete']");
        static readonly By _editPageBtn = By.XPath("//a[text()='Edit']");

        static readonly By _btnChoosePanels = By.XPath("//a[@id='btnChoosepanel']");

        static string _lnkPage = "//div[@id='main-menu']//a[text()='{0}']";
        static string _nextLnkPage = "//div[@id='main-menu']//a[text()='{0}']/parent::li/following-sibling::li/a";
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

        public IWebElement DeletePageButton
        {
            get { return StableFindElement(_deletePageBtn); }
        }

        public IWebElement EditPageButton
        {
            get { return StableFindElement(_editPageBtn); }
        }

        public IWebElement BtnChoosePanels
        {
            get { return StableFindElement(_btnChoosePanels); }
        }

        public IWebElement GeneralPage(String pageName)
        {
            By _generalPage = By.XPath(String.Format(_lnkPage, pageName));
            
            return StableFindElement(_generalPage);
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
            WaitForElementDisplay(_tabSetting);
            HoverElement(_tabSetting);
            WaitForElementClickable(_addPageBtn);
            AddPageButton.Click();
            return this;
        }

        public MainPage AddNewPage(String pageName = "Test", string parentPage = null, int noOfCol = 0, String displayAfterPage = null, Boolean publicChbx = false)
        {
            this.OpenAddPageDialog();
            
            TxtNewPagePageName.SendKeys(pageName);
            if (parentPage!=null)
            {
                CmbParentPage.SelectItem(parentPage, "Text"); 
            }
            if (noOfCol != 0)
            {
                CmbNumberOfColumns.SelectItem(noOfCol.ToString(), "Text"); 
            }

            if (displayAfterPage != null)
            {
                CmbNewPageDisplayAfter.SelectItem(displayAfterPage, "Text"); 
            }

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

        public MainPage deletePage()
        {
            WaitForElementDisplay(_tabSetting);
            HoverElement(_tabSetting);
            WaitForElementClickable(_deletePageBtn);
            DeletePageButton.Click();
            //Thread.Sleep(5000);
            
            return this;
        }

        public MainPage editPage(String newPageName, string parentPage = null, int noOfCol = 0, String displayAfterPage = null, Boolean publicChbx = false)
        {
            WaitForElementDisplay(_tabSetting);
            HoverElement(_tabSetting);
            WaitForElementClickable(_editPageBtn);

            if (newPageName.Equals(TxtNewPagePageName.GetValue().ToString()))
            {
                TxtNewPagePageName.Clear();
                TxtNewPagePageName.SendKeys(newPageName); 
            }
            if (parentPage != null)
            {
                CmbParentPage.SelectItem(parentPage, "Text");
            }
            if (noOfCol != 0)
            {
                CmbNumberOfColumns.SelectItem(noOfCol.ToString(), "Text");
            }

            if (displayAfterPage != null)
            {
                CmbNewPageDisplayAfter.SelectItem(displayAfterPage, "Text");
            }

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

            return this;
        }

        public MainPage confirmDeletePage()
        {
            WaitForAlertPresent();
            this._driverMainPage.SwitchTo().Alert().Accept();
            return this;
        }

        public MainPage selectPage (String pageName = null)
        {
            if (pageName.Equals(null))
            {
                // Warning something here
                return this;
            }
            if (pageName.Contains(" "))
            {
                pageName = pageName.Replace(" ", "&nbsp;");
            }
            //WaitForElementClickable(By.XPath(string.Format(_lnkPage, pageName)));
            GeneralPage(pageName).Click();
            return this;
        }

        public string getAlertMessage()
        {
            WaitForAlertPresent();
            //Thread.Sleep(1000);
            IAlert alert = this._driverMainPage.SwitchTo().Alert();
            string alertMsg = alert.Text;
            alert.Accept();
            //Thread.Sleep(1000);
            return alertMsg;
        }

        public MainPage selectChildPage(string parentPageName, string childPageName)
        {
            By parent = By.XPath(String.Format(_lnkPage, parentPageName));
            By child = By.XPath(String.Format(_lnkPage, childPageName));
            WaitForElementDisplay(parent);
            HoverElement(parent);
            WaitForElementClickable(child);
            StableFindElement(child).Click();
            return this;
        }
        
        public KeyValuePair<string, bool> ValidateDashboardMainPageDisplayed()
        {
            // TC001 TC002 TC003 checkpoint
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
            // TC014 checkpoint
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

        public KeyValuePair<string, bool> CheckPageDisplayed(String pageName)
        {
            // TC015 checkpoint
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool newPageFounded = GeneralPage(pageName).IsDisplayed();
                
                if (newPageFounded)
                    validation = SetPassValidation(node, ValidationMessage.ValidateNewPageFound);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateNewPageFound);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateNewPageFound, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> CheckPagesOrder(string firstPage, string secondPage)
        {
            // TC016 checkpoint
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                _driverMainPage.Navigate().Refresh();
                string expectedName = StableFindElement(By.XPath(String.Format(_nextLnkPage, firstPage))).Text;
                if (expectedName.Equals(secondPage))
                    validation = SetPassValidation(node, ValidationMessage.ValidatePageOrder);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePageOrder);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageOrder, e);
            }
            EndStepNode(node);
            return validation;
        }
        
        public KeyValuePair<string, bool> CheckPageExisted(string pageName)
        {
            // TC017 checkpoint
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool pageFound = StableFindElement(By.XPath(String.Format(_lnkPage, pageName))).Displayed;
                if (pageFound)
                    validation = SetPassValidation(node, ValidationMessage.ValidatePageDisplayed);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePageDisplayed);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageDisplayed, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> CheckChildPageExisted(string pageName, string parentPageName)
        {
            // TC017 checkpoint
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                HoverElement(By.XPath(string.Format(_lnkPage, parentPageName)));
                bool pageFound = StableFindElement(By.XPath(String.Format(_lnkPage, pageName))).Displayed;
                if (pageFound)
                    validation = SetPassValidation(node, ValidationMessage.ValidatePageDisplayed);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePageDisplayed);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageDisplayed, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> CheckPageDeleted(string parentPage, string childPage = null)
        {
            // TC020 checkpoint
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool pageFound;
                By parent = By.XPath(String.Format(_lnkPage, parentPage));
                if (childPage != null)
                {
                    WaitForElementDisplay(parent);
                    HoverElement(parent);
                    pageFound = IsElementPresent(By.XPath(String.Format(_lnkPage, childPage)));
                }
                else
                {
                    pageFound = IsElementPresent(By.XPath(String.Format(_lnkPage, parentPage)));
                }
                
                if (!pageFound)
                    validation = SetPassValidation(node, ValidationMessage.ValidatePageDeleted);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePageDeleted);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageDeleted, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> CheckAlertMessage(string msg)
        {
            // TC020 checkpoint
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                string obsmsg = getAlertMessage();
                if (obsmsg.Equals(msg))
                    validation = SetPassValidation(node, ValidationMessage.ValidateAlertMessage);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateAlertMessage);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateAlertMessage, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> CheckDeleteButtonDisappeared()
        {
            // TC020 checkpoint
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                WaitForElementDisplay(_tabSetting);
                HoverElement(_tabSetting);
                bool deleteBtnFound = IsElementPresent(_deletePageBtn);

                if (!deleteBtnFound)
                    validation = SetPassValidation(node, ValidationMessage.ValidateAlertMessage);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateAlertMessage);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateAlertMessage, e);
            }
            EndStepNode(node);
            return validation;
        }

        #endregion

        private static class ValidationMessage
        {
            public static string ValidateDashboardMainPageDisplayed = "Validate That TA Dashboard Main Page Displayed.";
            public static string ValidateControlsAreLockedAndDisabled = "Validate That Controls On Main Page Are Locked And Disabled.";
            public static string ValidateNewPageFound = "Validate That New Page Created And Displayed.";
            public static string ValidatePageOrder = "Validate Pages In Proper Order";
            public static string ValidatePageDisplayed = "Page Does Display";
            public static string ValidateAlertMessage = "Validate Alert Message";
            public static string ValidatePageDeleted = "Validate Page Deleted";
        }
    }
}
