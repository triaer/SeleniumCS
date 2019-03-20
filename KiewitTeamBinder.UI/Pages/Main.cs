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
        static By _lnkMainMenu(string menuText) => By.XPath($"//li[@class='sep']/parent::*/../a[contains(.,'{menuText}')]");
        static By _lnkSubMenu(string mainMenu, string subMenu) => By.XPath($"//li[@class='sep']/parent::*/../a[contains(.,'{mainMenu}')]/following::a[contains(.,'{subMenu}')]");
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
        static By _subMenuElement(string subMenuText) => By.XPath($"//a[.='{subMenuText}']");
        static By _lnkPanelElement(string tableName) => By.XPath($"//div[div[.='{tableName}']]//a");
        static By _lnkButtonElement(string lnkButton) => By.XPath($"//a[.='{lnkButton}']");
        static By _cboElement(string cboId) => By.XPath($"//select[@id='{cboId}']");
        static By _cboElementValue(string cboId) => By.XPath($"//select[@id='{cboId}']//option");
        static By _txtElement(string txtName) => By.XPath($"//input[@name='{txtName}']");
        static By _chkElement(string chkId) => By.XPath($"//input[@id='{chkId}']");
        static By _radElement(string radId) => By.XPath($"//input[@id='{radId}']");

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
        public IWebElement SubMenuElement(string subMenuText) => StableFindElement(_subMenuElement(subMenuText));

        public IReadOnlyCollection<IWebElement> LnkPanelElement(string tableName) => StableFindElements(_lnkPanelElement(tableName));
        public IWebElement LnkMainMenu(string menuText) => StableFindElement(_lnkMainMenu(menuText));
        public IWebElement LnkSubMenu(string mainMenu, string subMenu) => StableFindElement(_lnkSubMenu(mainMenu, subMenu));
        public IWebElement LnkButtonElement(string lnkButton) => StableFindElement(_lnkButtonElement(lnkButton));
        public IWebElement CboElement(string cboId) => StableFindElement(_cboElement(cboId));
        public IReadOnlyCollection<IWebElement> CboElementValue(string cboId) => StableFindElements(_cboElementValue(cboId));
        public IWebElement TxtElement(string txtName) => StableFindElement(_txtElement(txtName));
        public IWebElement ChkElement(string chkId) => StableFindElement(_chkElement(chkId));

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
                WaitUntil(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
                string errorText = WebDriver.SwitchTo().Alert().Text;
                
                if (errorText == text)
                    validation = SetPassValidation(node, ValidationMessage.ValidateTextInAlertPopup);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateTextInAlertPopup);
            }
            catch (UnhandledAlertException e)
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

        public List <KeyValuePair<string, bool>> ValidateInformationInChoosePanels(string tableName, string[] validateValue)
        {
            var node = CreateStepNode();
            var validations = new List <KeyValuePair<string, bool>>();
            List<string> tempActualValue = new List<string>();
            try
            {
                foreach (var item in LnkPanelElement(tableName))
                {
                    tempActualValue.Add(item.Text);
                }
                string[] actualValue = tempActualValue.ToArray();
                foreach (var item in validateValue)
                {
                    if (actualValue.Contains(item))
                        validations.Add(SetPassValidation(node, ValidationMessage.ValidateInformationInChoosePanels));
                    else
                        validations.Add(SetFailValidation(node, ValidationMessage.ValidateInformationInChoosePanels, item));
                }
            }
            catch (Exception e)
            {
                validations.Add(SetErrorValidation(node, ValidationMessage.ValidateInformationInChoosePanels, e));
            }
            EndStepNode(node);
            return validations;
        }

        public MainPage ClickGlobalSettingsSubMenu(string subSymbolMenuText)
        {
            var node = CreateStepNode();
            node.Info("From Global Settings, click " + subSymbolMenuText);
            ClickSymbolMenu(MainMenu.GlobalSettings.ToDescription());
            SubSymbolMenuElement(MainMenu.GlobalSettings.ToDescription(), subSymbolMenuText).WaitAndClick();
            EndStepNode(node);
            return this;
        }

        public MainPage ClickSymbolMenu(string symbolMenu)
        {
            SymbolMenuElement(symbolMenu).WaitAndClick();
            return this;
        }

        public MainPage ClickSubMenu(string mainMenu, String subMenu)
        {
            LnkMainMenu(mainMenu).Click();
            LnkSubMenu(mainMenu, subMenu).Click();
            return this;
        }

        public MainPage ClickLinkButton(string lnkButton)
        {
            var node = CreateStepNode();
            node.Info("Click the link button: " + lnkButton);
            LnkButtonElement(lnkButton).Click();
            EndStepNode(node);
            return this;
        }

        /// <param "isPublic"> If value is "yes", check the checkbox. Else the value is "no", uncheck the checkbox </param>
        /// 

        public MainPage updatePage(TAPage page)
        {
            page.IsPublic = true;
            this.FillInfoInPageDiaglog(page);
            return this;
        }

        public MainPage FillInfoInPageDiaglog(TAPage taPage)
        {
            var node = CreateStepNode();
            node.Info("Fill information the Add New Page or Edit Page dialog: " + taPage.PageName + ", " + taPage.ParentPage + ", " + taPage.NumberOfColumns + ", " + taPage.DisplayAfter + ", " + taPage.IsPublic);
            
            if (taPage.PageName != null && taPage.PageName != TxtNewPagePageName.Text)
            {
                TxtNewPagePageName.InputText(taPage.PageName);
            }
            if (taPage.ParentPage != null)
            {
                CmbParentPage.SelectItem(taPage.ParentPage);
            }
            if (taPage.NumberOfColumns != 2)
            {
                CmbNumberOfColumns.SelectItem(taPage.NumberOfColumns.ToString());
            }
            if (taPage.DisplayAfter != null)
            {
                CmbNewPageDisplayAfter.SelectItem(taPage.DisplayAfter);
            }
            if (taPage.IsPublic)
                ChbPublic.Check();
            else 
                ChbPublic.UnCheck();
            string txtNameValue = TxtNewPagePageName.Text;
            BtnPageOK.Click();
            //WaitForPage(txtNameValue, taPage.PageName);
            string locator;
            if (taPage.PageName.Contains(" "))
            {
                string newPageName = taPage.PageName.Replace(" ", "\u00a0");
                locator = string.Format(_lnkPage, newPageName);
            }
            else
            {
                locator = string.Format(_lnkPage, taPage.PageName);
            }
            WaitForElementExisted(By.XPath(locator));
            EndStepNode(node);
            return this;
        }

        public void WaitForPage(string txtNameValue, string pageName = null)
        {
            string locator;
            if (pageName != null && pageName.Contains(" "))
            {
                string newPageName = pageName.Replace(" ", "\u00a0");
                locator = string.Format(_lnkPage, newPageName);
            }
            //else if (pageName == null && txtNameValue.Contains(" "))
            //{
            //    string newPageName = txtNameValue.Replace(" ", "\u00a0");
            //    locator = string.Format(_lnkPage, newPageName);
            //}
            else
            {
                locator = string.Format(_lnkPage, txtNameValue);
            }
            WaitForElementDisplay(By.XPath(locator));
        }

        public MainPage AddPage(TAPage taPage)
        {
            var node = CreateStepNode();
            node.Info("Add a new page.");
            ClickGlobalSettingsSubMenu(SubMenu.AddPage.ToDescription());
            FillInfoInPageDiaglog(taPage);
            EndStepNode(node);
            return this;
        }

        public MainPage EditPage(TAPage taPage)
        {
            var node = CreateStepNode();
            node.Info("Edit a page.");
            ClickGlobalSettingsSubMenu(SubMenu.Edit.ToDescription());
            FillInfoInPageDiaglog(taPage);
            EndStepNode(node);
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

        //public Login Logout()
        //{
        //    LnkAccount.Click();
        //    LnkLogout.Click();
        //    return new Login(WebDriver);
        //}

        public MainPage DeleteOnePage(string pageName)
        {
            var node = CreateStepNode();
            node.Info("Delete a page: " + pageName);
            ClickPage(pageName);
            ClickGlobalSettingsSubMenu(SubMenu.Delete.ToDescription());
            AcceptAlert<MainPage>();
            WaitForElementNotExist(LnkPage(pageName));
            EndStepNode(node);
            return this;
        }

        /// <param "pageName"> Sub page go first, then page go after </param>
        public MainPage DeletePage(string[] pageName, bool isSubPage = false)
        {
            //int lastPage = pageName.Count();
            if (isSubPage)
            {
                ClickSubPage(pageName);
                ClickGlobalSettingsSubMenu(SubMenu.Delete.ToDescription());
                AcceptAlert<MainPage>();
                DeleteOnePage(pageName[0]);
            }
            else
            {
                foreach (var item in pageName)
                {
                    DeleteOnePage(item);
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
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValiateDialogDisplayed, e);
            }
            EndStepNode(node);
            return validation;
        }

        public MainPage ClickPage(string pageName)
        {
            var node = CreateStepNode();
            node.Info("Click page: " + pageName);
            LnkPage(pageName).WaitAndClick();
            EndStepNode(node);
            return this;
        }

        public MainPage ClickSubPage(string[] pageName)
        {
            foreach (var item in pageName.ToList())
            {
                LnkPage(item).HoverElement();
            }
            ClickPage(pageName[pageName.Count()-1]);
            return this;
        }

        public KeyValuePair<string, bool> ValidatePageExisted(string pageName)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool isDisplayed = LnkPage(pageName).IsDisplayed();
                if (isDisplayed)
                    validation = SetPassValidation(node, ValidationMessage.ValidatePageIsExisted);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePageIsExisted);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageIsExisted, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> ValidatePageNotExisted(string pageName)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool isDisplayed = LnkPage(pageName).IsDisplayed();
                if (!isDisplayed)
                    validation = SetPassValidation(node, ValidationMessage.ValidatePageIsNotExisted);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePageIsNotExisted);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageIsNotExisted, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> ValidatePageVisible(string pageName)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
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
                bool isVisible = IsVisible(By.XPath(locator));
                if (isVisible)
                    validation = SetPassValidation(node, ValidationMessage.ValidatePageVisible);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePageVisible);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageVisible, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> ValidatePageInvisible(string pageName)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
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
                bool isVisible = IsVisible(By.XPath(locator));
                if (!isVisible)
                    validation = SetPassValidation(node, ValidationMessage.ValidatePageInvisible);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePageInvisible);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageInvisible, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> ValidatePageAccessed(string pageName)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool isAccessed = LnkPage(pageName).IsClickable();
                if (isAccessed)
                    validation = SetPassValidation(node, ValidationMessage.ValidatePageIsAccessed);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidatePageIsAccessed);
            }
            catch(Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidatePageIsAccessed, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> ValidateSubMenuNotExisted(string subMenu)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                bool isExisted = SubMenuElement(subMenu).IsDisplayed();
                if (isExisted)
                    validation = SetPassValidation(node, ValidationMessage.ValidateSubMenuNotExisted);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateSubMenuNotExisted);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateSubMenuNotExisted, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> ValidateListInComboboxIsInAlphabeticalOrder(string cboName)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            List<string> cboValue = new List<string>();
            List<string> sort = new List<string>();
            try
            {
                foreach (var item in CboElementValue(cboName))
                {
                    cboValue.Add(item.Text);
                }
                sort.AddRange(cboValue.OrderBy(o=>o));
                bool isSort = cboValue.SequenceEqual(sort);
                if (isSort)
                    validation = SetPassValidation(node, ValidationMessage.ValidateListInComboboxIsInAlphabeticalOrder);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateListInComboboxIsInAlphabeticalOrder);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateListInComboboxIsInAlphabeticalOrder, e);
            }
            EndStepNode(node);
            return validation;
        }

        public KeyValuePair<string, bool> ValidateSelectedDataIsDisplayOnCombobox(string cboName, string selectedData)
        {
            var node = CreateStepNode();
            var validation = new KeyValuePair<string, bool>();
            try
            {
                string actualValue = CboElement(cboName).Text;
                if (actualValue == selectedData)
                    validation = SetPassValidation(node, ValidationMessage.ValidateSelectedDataIsDisplayOnCombobox);
                else
                    validation = SetFailValidation(node, ValidationMessage.ValidateSelectedDataIsDisplayOnCombobox);
            }
            catch (Exception e)
            {
                validation = SetErrorValidation(node, ValidationMessage.ValidateSelectedDataIsDisplayOnCombobox, e);
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
            public static string ValidatePageIsExisted = "Validate that page is existed";
            public static string ValidatePageIsNotExisted = "Validate that page is not existed";
            public static string ValidateSubMenuNotExisted = "Validate that sub menu is not existed";
            public static string ValidateInformationInChoosePanels = "Validate that information in Choose Panels is correct.";
            public static string ValidateListInComboboxIsInAlphabeticalOrder = "Validate that the list in combobox is in alphabetical order.";
            public static string ValidateSelectedDataIsDisplayOnCombobox = "Validate the selected data is displayed correctly on combobox.";
        }
    }
}
