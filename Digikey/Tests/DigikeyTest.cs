using System;
using FluentAssertions;
using static Digikey.Constants.Constants;
using Digikey.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Digikey.ExtentReportsHelper;

namespace Digikey.Tests
{
    [TestClass]
    public class AgodaTest : UITestBase
    {
        [TestMethod]
        public void DigiKey_Test()
        {
            try
            {
                test = LogTest("Verify information of selected items is correct.");

                test.Info("1. Navigate to www.digikey.com");
                var driver = Browser.Open(HomePage, "chrome");

                DigikeyMain digiMain = new DigikeyMain(driver);
                
                test.Info("2. Select Products on top menu.");
                test.Info("3. Select Accessories under Battery Products section");
                test.Info("4. Select any 3 different products");
                test.Info("5. Click Compare Selected");

                ProductComparePage productComparePage = digiMain
                    .GoToProductPage()
                    .GetProductList()
                    .SelectItemsToCompare(3);
                
                //VP:                
                validations.Add(productComparePage.ValidateSelectedItemsInfo());

                test = LogTest("Verify selected products are in Cart.");

                test.Info("6. Click Back to Search Results link");
                test.Info("7. Select any product");
                test.Info("8. Enter Quantity and Customer Reference");
                test.Info("9. Click Add to Cart");
                
                ProductsPage productPage = productComparePage.BackToProductPage();
                productPage.SelectItemByDigikey(1)
                    .AddProductToCart(quantity: 2, customerRef: "cusRef01")
                    .BackToProductsPage();
                
                test.Info("10. Repeat step 7-9 for 2 others products");

                productPage.GetProductList()
                    .SelectItemByDigikey(2)
                    .AddProductToCart(quantity: 2, customerRef: "cusRef02")
                    .BackToProductsPage();

                productPage.GetProductList()
                    .SelectItemByDigikey(3)
                    .AddProductToCart(quantity: 2, customerRef: "cusRef03")
                    .BackToProductsPage();

                CartPage cartPage = productPage.goToCartPage();

                // VP:
                validations.Add(cartPage.ValidateCartItems());



                Console.WriteLine(string.Join(Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

    }
}
