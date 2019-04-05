using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using KiewitTeamBinder.Common.TestData;

namespace KiewitTeamBinder.UI.Pages
{
    public class SubCategoryDigiKey : LoggedInLanding
    {
        #region Locators
        private By _input(int quality) => By.XPath($"//tr[position()<={quality}]//input[@name='part']");
        private By _compareButton => By.XPath("//input[@value='Compare Selected']");
        private By _searchIcon => By.XPath("//button[@id='header-search-button']");
        private By _product(int quality) => By.XPath($"//tbody[@id='lnkPart']//tr[{quality}]//td[4]/a");
        private By _quantity => By.XPath("//input[@name='qty']");
        private By _referrence => By.XPath("//input[@id='cref']");
        private By _addtoCart => By.XPath("//input[@id='addtoorderbutton']");
        private By _cart => By.XPath("//a[@class='header__resource resource--cart']");
        private By _viewCart => By.XPath("//a[contains(text(),'View Cart')]");
        #endregion

        #region Elements
        public IReadOnlyCollection<IWebElement> Input(int quality) => StableFindElements(_input(quality));
        public IWebElement CompareButton { get { return StableFindElement(_compareButton); } }
        public IWebElement Product(int quality) => StableFindElement(_product(quality));
        public IWebElement Quantity { get { return StableFindElement(_quantity); } }
        public IWebElement Reference { get { return StableFindElement(_referrence); } }
        public IWebElement AddToCart { get { return StableFindElement(_addtoCart); } }
        public IWebElement Cart { get { return StableFindElement(_cart); } }
        public IWebElement ViewCart { get { return StableFindElement(_viewCart); } }
        #endregion

        #region Methods
        public CompareDigiKey SelectAndCompareProducts(int quality)
        {
            foreach (var item in Input(quality))
            {
                item.Check();
            }
            CompareButton.Click();
            WaitForElement(_searchIcon);
            return new CompareDigiKey(WebDriver);
        }
        
        public CartDigiKey SelectProductAndAddtoCart(int quality, string productQuantity, string [] reference)
        {
            MainDigiKey mainDigiKey = new MainDigiKey(WebDriver);
            ProductDetailsDigiKey pDD = new ProductDetailsDigiKey(WebDriver);
            DigiKeyTestsSmoke digiKeyData = new DigiKeyTestsSmoke();
            for (int i = 0; i < quality; i++)
            {
                Product(i+1).Click();
                WaitForElement(_searchIcon);
                pDD.EnterQuantityAndReference(productQuantity, reference[i]);
                mainDigiKey.SelectMenu<ProductsDigiKey>(TopMenuDigiKey.Products.ToDescription())
                           .SelectSubCategory<SubCategoryDigiKey>(digiKeyData.Category, digiKeyData.subCategory);
            }
            Cart.Click();
            ViewCart.Click();
            WaitForElement(_searchIcon);
            return new CartDigiKey(WebDriver);
        }

        #endregion
        public SubCategoryDigiKey(IWebDriver webDriver) : base(webDriver)
        {

        }
    }
}
