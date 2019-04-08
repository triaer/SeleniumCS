using KiewitTeamBinder.UI.Pages.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;


namespace KiewitTeamBinder.UI.Pages
{
    public class CartDigiKey : LoggedInLanding
    {
        #region Lcators
        private By _customerRefs(int elements) => By.XPath($"(//input[@class='customer-reference'])[{elements}]");
        private By _quantityLable => By.XPath("//th[@class='detailRow_quantity_th']");
        //private By _detailRows => By.XPath("//table[@class='DetailTable']//tr[contains(@class,'detailRow') and not(contains(@class,'sub'))]");
        private By _deleteCheckbox(int checkbox) => By.XPath($"(//input[@class ='deleteCheckbox'])[{checkbox}]");
        private By _deleteButton => By.XPath("//div[@id='cartViewButtons']//span[@class='trash-button icon-trash']");
        private By _okButton => By.XPath("//button[contains(text(),'OK')]");
        private By _quantityTextbox(int elements) => By.XPath($"(//input[@class='part-quantity text-box single-line'])[{elements}]");
        #endregion

        #region Elements
        public IWebElement CustomerRef(int elements) => StableFindElement(_customerRefs(elements));
        public IWebElement DeleteCheckbox(int checkbox) => StableFindElement(_deleteCheckbox(checkbox));
        public IWebElement DeleteButton { get { return StableFindElement(_deleteButton); } }
        public IWebElement OkBUtton { get { return StableFindElement(_okButton); } }
        public IWebElement QuantityTextbox(int elements) => StableFindElement(_quantityTextbox(elements));
        public IWebElement QuantityLable { get { return StableFindElement(_quantityLable); } }

        #endregion

        #region Methods
        public CartDigiKey ModifyCustomerRef(string [] modifiedRef, string [] modifiedQuantity, int elements)
        {
            for (int i = 1; i <= elements; i++)
            {
                CustomerRef(i).Clear();
                //WaitForElement(_quantityLable);
                Thread.Sleep(2000);
                CustomerRef(i).SendKeys(modifiedRef[i-1]);
                //QuantityTextbox(i).InputText(modifiedquantity, true);
                //Thread.Sleep(2000);
                //QuantityTextbox(i).SendKeys(Keys.Enter);
                if (i==elements)
                {
                    CustomerRef(elements).SendKeys(Keys.Enter);
                    Thread.Sleep(2000);
                }
                 
            }
            for (int i = 1; i <= elements; i++)
            {
             
                QuantityTextbox(i).InputText(modifiedQuantity[i-1], true);
                
                if (i == elements)
                {
                    System.Windows.Forms.SendKeys.SendWait(@"{Enter}");
                    //QuantityLable.Click();
                    Thread.Sleep(2000);
                }
            }
            //foreach (var item in CustomerRef)
            //{
            //    item.InputText(modifiedRef);
            //    Actions action = new Actions(WebDriver);

            //    action.SendKeys(Keys.Enter).Build().Perform();
            //    //item.SendKeys(Keys.Enter);
            //    Thread.Sleep(2000);

            //}
            return this;
        }

        public CartDigiKey DeleteProducts(int products)
        {
            for (int i = 1; i <= products; i++)
            {
                DeleteCheckbox(i).Check();
            }
            DeleteButton.Click();
            WaitForElement(_okButton);
            OkBUtton.Click();
            return this;
        }
        #endregion

        public CartDigiKey(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}
