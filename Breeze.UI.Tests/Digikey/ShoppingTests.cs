﻿using Breeze.Common.Helper;
using Breeze.Common.TestData;
using Breeze.UI.Pages;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Breeze.UI.ExtentReportsHelper;

namespace Breeze.UI.Tests.Digikey
{
    [TestClass]
    public class ShoppingTests : UITestBase
    {

        [TestMethod]
        public void TC001()
        {
            try
            {
                //Given
                //0. Prepare data
                DigiBookingTestSmoke testData = new DigiBookingTestSmoke();

                //1. Navigate to www.digikey.com.
                //2. Select Products on top menu
                //3. Select Accessories under Battery Products section
                test.Info("Navigate to Digikey home page.");
                Browser.Open("chrome", Constant.DigikeyHomePage);

                test = LogTest("DIGIKEY_SHOPPING_TC001 - Verify that user can add, edit, delete product in cart successfully on multi browsers.");
                DigikeyHomePage homePage = new DigikeyHomePage();
                DigikeyProductListPage productListPage = homePage.SelectProductMenu()
                                                                 .SelectTargetProductCategory(testData.Category, testData.SubCategory);

                //When
                //4. Select any 3 different products
                //5. Click Compare Selected
                DigikeyProductComparisonPage comparePage = productListPage.SelectProductsAndCompare(testData.Products);

                Browser.Open("firefox", Constant.DigikeyHomePage);
                homePage.SelectProductMenu().SelectTargetProductCategory(testData.Category, testData.SubCategory);

                Browser.SwitchToDefaultBrowser();

                //Then
                //VP: Verify Digi-Key Part Number & Manufacturer Part Number is correct
                comparePage.LogValidation<DigikeyProductComparisonPage>(ref validations, comparePage.ValidateComparedProductInfos(testData.Products));

                //6. Click Back to Search Results link
                comparePage.BackToSearchResultsPage();

                Browser.SwitchToTargetBrowser("firefox");

                productListPage.SelectProductsAndCompare(testData.Products);
                comparePage.LogValidation<DigikeyProductComparisonPage>(ref validations, comparePage.ValidateComparedProductInfos(testData.Products));

                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);

            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void TC002()
        {
            try
            {
                //Given
                //0. Prepare data
                DigiBookingTestSmoke testData = new DigiBookingTestSmoke();

                //1. Navigate to www.digikey.com.
                //2. Select Products on top menu
                //3. Select Accessories under Battery Products section
                test.Info("Navigate to Digikey home page.");
                Browser.Open(browser, Constant.DigikeyHomePage);

                test = LogTest("DIGIKEY_SHOPPING_TC002 - Verify that user can compare product successfully.");
                DigikeyHomePage homePage = new DigikeyHomePage();
                DigikeyProductComparisonPage comparePage = homePage.SelectProductMenu()
                                                                 .SelectTargetProductCategory(testData.Category, testData.SubCategory)
                                                                 .SelectProductsAndCompare(testData.Products);
                //Then
                //VP: Verify Digi-Key Part Number & Manufacturer Part Number is correct
                comparePage.LogValidation<DigikeyProductComparisonPage>(ref validations, comparePage.ValidateComparedProductInfos(testData.Products));

                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                throw;
            }
        }

        [TestMethod]
        public void TC003()
        {
            try
            {
                //Given
                //0. Prepare data
                DigiBookingTestSmoke testData = new DigiBookingTestSmoke();

                //1. Navigate to www.digikey.com.
                //2. Select Products on top menu
                //3. Select Accessories under Battery Products section
                test.Info("Navigate to Digikey home page.");
                Browser.Open(browser, Constant.DigikeyHomePage);

                test = LogTest("DIGIKEY_SHOPPING_TC003 - Verify that user can add, edit, delete product in cart successfully.");
                DigikeyHomePage homePage = new DigikeyHomePage();
                DigikeyProductListPage productListPage = homePage.SelectProductMenu()
                                                                 .SelectTargetProductCategory(testData.Category, testData.SubCategory);

                //When
                //4. Select any 3 different products
                //5. Click Compare Selected
                DigikeyProductComparisonPage comparePage = productListPage.SelectProductsAndCompare(testData.Products);

                //Then
                //VP: Verify Digi-Key Part Number & Manufacturer Part Number is correct
                comparePage.LogValidation<DigikeyProductComparisonPage>(ref validations, comparePage.ValidateComparedProductInfos(testData.Products));

                //6. Click Back to Search Results link
                comparePage.BackToSearchResultsPage();

                //When
                //7. Select any product
                //8. Enter Quantity and Customer Reference
                //9. Click Add to Cart
                //10. Repeat step 7 - 9 for 2 others products
                //Then
                //VP: Verify selected products are in cart
                //When
                //12. Modify Customer Reference and quantity as 3 for all products
                //Then
                //VP: Verify just modified customer name and quantity is correct
                //When
                //14. Delete any 2 products
                //Then
                //VP: Verify those 2 products are deleted
                foreach (var product in testData.Products)
                {
                    productListPage.SelectProductMenu()
                                   .SelectTargetProductCategory(testData.Category, testData.SubCategory)
                                   .SelectTargetProduct(product)
                                   .EnterShoppingInfoAndAddToCart(product);
                }
                DigikeyShoppingCartPage shoppingCartPage = new DigikeyShoppingCartPage();
                shoppingCartPage.LogValidation<DigikeyShoppingCartPage>(ref validations, shoppingCartPage.ValidateProductsExistInCart(testData.Products))
                                .EditShoppingInfos(testData.EditedProducts)
                                .LogValidation<DigikeyShoppingCartPage>(ref validations, shoppingCartPage.ValidateModifiedCustomerNameAndQuantityAreCorrect(testData.EditedProducts))
                                .DeleteProducts(testData.DeletedProducts)
                                .LogValidation<DigikeyShoppingCartPage>(ref validations, shoppingCartPage.ValidateDeletedProductsNotExistInCart(testData.DeletedProducts));

                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
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
