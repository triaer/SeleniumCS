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
        static readonly By _lblRepositoryName = By.XPath("//a[@href='#Repository']/span");
        static readonly By _tabSetting = By.XPath("//li[@class='mn-setting']");
        static readonly By _btnChoosePanels = By.XPath("//a[@id='btnChoosepanel']");
        static string _lnkMainMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]";
        static string _lnkSubMenu = "//li[@class='sep']/parent::*/../a[contains(.,'{0}')]/following::a[contains(.,'{1}')]";
        static string _lnkSettingItem = "//li[@class='mn-setting']//a[ .='{0}']";
        static string _cbbName = "//td[contains(text(), '{0}')]/following-sibling::*/descendant::select";
        static string _lnkPage = "//a[.='{0}']";
        static By _lnkSubPage(string pageText, string subPageText) => By.XPath($"//a[.='{pageText}']/following-sibling::ul//a[.='{subPageText}']");
        static By _listTextMenu = By.XPath("//div[@id='main-menu']/div[@class='container']/ul/li/a");
        static string _symbolMenu = "//li[@class='{0}']";
        static string _symbolSubMenu = "//li[@class='{0}']//a[.='{1}']";
        static readonly By _pageLockedElement = By.XPath("//div[@id='div_Lock' and @class='transbox_show']");
        static By _dlgSmallElement(string dialogText) => By.XPath($"//tbody//h2[.='{dialogText}']");
        static By _dlgBigElement(string dialogText) => By.XPath($"//div[@class='ui-dialog-container']/div[@class='ui-dialog-titlebar']/span[.='{dialogText}']");
        static By _lnkOpenedPage(string pageName) => By.XPath($"//li[@class='active']/a[.='{pageName}']");

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

        public IReadOnlyCollection<IWebElement> ListTextMenu
        {
            get { return StableFindElements(_listTextMenu); }
        }

        public IWebElement SymbolMenuElement(string symbolMenuClass)
        {
            string locator = string.Format(_symbolMenu, symbolMenuClass);
            return StableFindElement(By.XPath(locator));
        }

        public IWebElement SubSymbolMenuElement(string symbolMenuClass, string subSymbolMenuText)
        {
            string locator = string.Format(_symbolSubMenu, symbolMenuClass, subSymbolMenuText);
            return StableFindElement(By.XPath(locator));
        }

        public IWebElement LnkPage(string pageName)
        {
            string locator;
            if (pageName.Contains(" "))
            {
                string newPageName = pageName.Replace(" ", "\u00a0");
                locator = string.Format(_lnkPage, newPageName);
            }
            else
            {
                locator = string.Format(_lnkPage, pageName);
            }
            return StableFindElement(By.XPath(locator));
        }

        public IWebElement PageLockedElement
        {
            get { return StableFindElement(_pageLockedElement); }
        }

        public IWebElement LnkSubPage(string pageText, string subPageText) => StableFindElement(_lnkSubPage(pageText, subPageText));

        public IWebElement DlgSmallElement(string dialogText) => StableFindElement(_dlgSmallElement(dialogText));

        public IWebElement DlgBigElement(string dialogText) => StableFindElement(_dlgBigElement(dialogText));

        public IWebElement LnkOpenedPage(string pageName) => StableFindElement(_lnkOpenedPage(pageName));

        #endregion

        #region Methods

        public MainPage(IWebDriver driver)
            : base(driver)
        {
            this._driverMainPage = driver;
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
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateDashboardMainPageDisplayed, e);
            }

            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> ValidateTextInAlertPopup(string text)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                string errorText = WebDriver.SwitchTo().Alert().Text;
                if (errorText == text)
                    validation = SetPassValidation(node, ValidationMessage.ValidateTextInAlertPopup);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateTextInAlertPopup, text, errorText);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateTextInAlertPopup, e);
            }
            EndStepNode(node);
            return validation;
        }

        /// <param "pageName"> The left page go first, then the right page go after </param>
        public KeyValuePair<string, bool> ValidatePageNextToAnotherPage(string[] pageName)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            List<string> tempActualPageName = new List<string>();
            try
            {
                foreach (var item in ListTextMenu)
                {
                    tempActualPageName.Add(item.Text);
                }
                string[] actualPageName = tempActualPageName.ToArray();
                int firstPageIndex = Array.IndexOf(actualPageName, pageName[0]);
                int k = 1;
                for (int i = 1; i < pageName.Count(); i++)
                {
                    bool isNextTo = (actualPageName[firstPageIndex + i] == pageName[k]);
                    if (isNextTo)
                        validation = SetPassValidation(node, ValidationMessage.ValidatePageNextToAnotherPage);
                    else
                        validation = SetFailValidation(node, ValidationMessage.ValidatePageNextToAnotherPage, pageName[k], actualPageName[firstPageIndex + i]);
                    k = k + 1;
                }
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageNextToAnotherPage, e);
            }
            EndStepNode(node);
            return validation;
        }

        public MainPage ClickSubSymbolMenu(string symbolMenuClass, string subSymbolMenuText)
        {
            SymbolMenuElement(symbolMenuClass).Click();
            SubSymbolMenuElement(symbolMenuClass, subSymbolMenuText).Click();
            return this;
        }

        /// <param "isPublic"> If value is "yes", check the checkbox. Else the value is "no", uncheck the checkbox </param>
        public MainPage FillInfoInPageDiaglog(string pageName = null, bool isWaitForPage = false, string parentPage = null, int numberOfColumns = 2, string displayAfter = null, string isPublic = "Default")
        {
            if(pageName != null)
            {
                TxtNewPagePageName.InputText(pageName);
            }
            if (parentPage != null)
            {
                CmbParentPage.SelectItem(parentPage);
            }
            if (numberOfColumns != 2)
            {
                CmbNumberOfColumns.SelectItem(numberOfColumns.ToString());
            }
            if (displayAfter != null)
            {
                CmbNewPageDisplayAfter.SelectItem(displayAfter);
            }
            if (isPublic != CheckValue.Default.ToDescription())
            {
                if (isPublic == CheckValue.Yes.ToDescription())
                    ChbPublic.Check();
                else if (isPublic == CheckValue.No.ToDescription())
                    ChbPublic.UnCheck();
            }
            if (isWaitForPage)
            {
                string locator;
                if (pageName != null && pageName.Contains(" "))
                {
                    string newPageName = pageName.Replace(" ", "\u00a0");
                    locator = string.Format(_lnkPage, newPageName);
                }
                else if(pageName == null && (TxtNewPagePageName.Text).Contains(" "))
                {
                    string newPageName = (TxtNewPagePageName.Text).Replace(" ", "\u00a0");
                    locator = string.Format(_lnkPage, newPageName);
                }
                else
                {
                    locator = string.Format(_lnkPage, pageName);
                }
                WaitForElementDisplay(By.XPath(locator));
            }
            BtnPageOK.Click();
            return this;
        }

        public MainPage AddPage(string pageName, bool isWaitForPage = false, string parentPage = null, int numberOfColumns = 2, string displayAfter = null, string isPublic = "Default")
        {
            ClickSubSymbolMenu(SymbolMenu.GlobalSettings.ToDescription(), SubMenu.AddPage.ToDescription());
            FillInfoInPageDiaglog(pageName, isWaitForPage, parentPage, numberOfColumns, displayAfter, isPublic);
            return this;
        }

        public MainPage EditPage(string pageName = null, bool isWaitForPage = false, string parentPage = null, int numberOfColumns = 2, string displayAfter = null, string isPublic = "Default")
        {
            ClickSubSymbolMenu(SymbolMenu.GlobalSettings.ToDescription(), SubMenu.Edit.ToDescription());
            FillInfoInPageDiaglog(pageName, isWaitForPage, parentPage, numberOfColumns, displayAfter, isPublic);
            return this;
        }

        public KeyValuePair<string, bool> ValidateAllControlInPageAreLocked()
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                if (PageLockedElement.IsDisplayed())
                    validation = SetPassValidation(node, ValidationMessage.ValidateAllControlInPageAreLocked);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateAllControlInPageAreLocked);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateAllControlInPageAreLocked, e);
            }
            EndStepNode(node);
            return validation;
        }

        public Login Logout()
        {
            LnkAccount.Click();
            LnkLogout.Click();
            return new Login(WebDriver);
        }

        public MainPage DeleteOnePage(string pageName, bool isWaitForDisappear = false)
        {
            ClickPage(pageName);
            ClickSubSymbolMenu(SymbolMenu.GlobalSettings.ToDescription(), SubMenu.Delete.ToDescription());
            WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            WebDriver.SwitchTo().Alert().Accept();
            if (isWaitForDisappear)
            {
                WaitForElementNotExist(LnkPage(pageName));
            }
            return this;
        }

        /// <param "pageName"> Sub page go first, then page go after </param>
        public MainPage DeletePage(string[] pageName, bool isSubPage = false)
        {
            //int lastPage = pageName.Count();
            if (isSubPage)
            {
                LnkSubPage(pageName[0], pageName[1]).Click();
                ClickSubSymbolMenu(SymbolMenu.GlobalSettings.ToDescription(), SubMenu.Delete.ToDescription());
                WebDriver.SwitchTo().Alert().Accept();
                DeleteOnePage(pageName[1]);
            }
            else
            {
                foreach (var item in pageName)
                {
                    DeleteOnePage(item, true);
                }
            }
            return this;
        }

        public KeyValuePair<string, bool> ValidateDialogDisplayed(string dialogText)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                if (dialogText == Dialog.AddNewPage.ToDescription() || dialogText == Dialog.EditPage.ToDescription())
                {
                    if (DlgSmallElement(dialogText).IsDisplayed())
                        validation = SetPassValidation(node, ValidationMessage.ValiateDialogDisplayed);
                    else
                        validation = SetFailValidation(node, ValidationMessage.ValiateDialogDisplayed);
                }
                else
                {
                    if (DlgBigElement(dialogText).IsDisplayed())
                        validation = SetPassValidation(node, ValidationMessage.ValiateDialogDisplayed);
                    else
                        validation = SetFailValidation(node, ValidationMessage.ValiateDialogDisplayed);
                }
            }
            catch(Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValiateDialogDisplayed, e);
            }
            EndStepNode(node);
            return validation;
        }

        public MainPage ClickPage(string pageName)
        {
            LnkPage(pageName).Click();
            return this;
        }

        public KeyValuePair<string, bool> ValidatePageVisible(string pageName, bool isInvisible = false, bool isAccessed = false)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool isDisplayed = LnkPage(pageName).IsDisplayed();
                if (isInvisible)
                {
                    if (!isDisplayed)
                        validation = SetPassValidation(node, ValidationMessage.ValidatePageInvisible);
                    else
                        validation = SetFailValidation(node, ValidationMessage.ValidatePageInvisible);
                }
                else
                {
                    if (isDisplayed)
                        validation = SetPassValidation(node, ValidationMessage.ValidatePageVisible);
                    else
                        validation = SetFailValidation(node, ValidationMessage.ValidatePageVisible);
                    if (isAccessed)
                    {
                        ClickPage(pageName);
                        bool isOpened = LnkOpenedPage(pageName).IsDisplayed();
                        if (isOpened)
                            validation = SetPassValidation(node, ValidationMessage.ValidatePageIsAccessed);
                        else
                            validation = SetFailValidation(node, ValidationMessage.ValidatePageIsAccessed);
                    }
                }
            }
            catch(Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageVisible, e);
            }
            EndStepNode(node);
            return validation;
        }

        #endregion

        private static class ValidationMessage
        {
            public static string ValidateDashboardMainPageDisplayed = "Validate that TA Dashboard main page displayed";
            public static string ValidateTextInAlertPopup = "Validate Dashboard error message appears";
            public static string ValidatePageNextToAnotherPage = "Validate page next to another page";
            public static string ValidateAllControlInPageAreLocked = "Validate all other controls within page are locked and disabled";
            public static string ValiateDialogDisplayed = "Valiate that dialog displayed";
            public static string ValidatePageVisible = "Validate that page is visible";
            public static string ValidatePageInvisible = "Validate that page is invisible";
            public static string ValidatePageIsAccessed = "Validate that page is accessed";
        }
    }
}
