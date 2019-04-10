using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;

namespace KiewitTeamBinder.UI.Tests.User
{
    [TestClass]
    public class DigiKeyTests : UITestBase
    {
        [TestMethod]
        public void TC02()
        {
            try
            {
                //given
                var driver = Browser.Open(Constant.DigiKey, "chrome");
                DigiKeyTestsSmoke digiKeyData = new DigiKeyTestsSmoke();


                //when
                MainDigiKey mainDigiKey = new MainDigiKey(driver);

                CompareDigiKey compareDigiKey = mainDigiKey.SelectMenu<ProductsDigiKey>(TopMenuDigiKey.Products.ToDescription())
                                                            .SelectSubCategory<SubCategoryDigiKey>(digiKeyData.Category, digiKeyData.subCategory)
                                                            .SelectAndCompareProducts(digiKeyData.Quantity);
                compareDigiKey.LogValidation<CompareDigiKey>(ref validations, compareDigiKey.ValidateNumber())
                                                                                                            .ClickBackButton<SubCategoryDigiKey>()
                                                                                                            .SelectProductAndAddtoCart(digiKeyData.Quantity, digiKeyData.QuantityOrder, digiKeyData.MultiReference)
                                                                                                            .ModifyCustomerRef(digiKeyData.ModifiedRef, digiKeyData.ModifiedQuantity, digiKeyData.Quantity)
                                                                                                            .DeleteProducts(digiKeyData.DeleteProducts);

                //then
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
