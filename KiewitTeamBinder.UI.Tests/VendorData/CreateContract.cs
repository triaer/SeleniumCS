using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages.Dialogs;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.PopupWindows;
using KiewitTeamBinder.UI.Pages.VendorDataModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Tests.VendorData
{
    [TestClass]
    public class CreateContract : UITestBase
    {
        [TestMethod]
        public void CreateNewDeliverable()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var createNewDeliverable = new CreateNewDeliverableSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + createNewDeliverable.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(createNewDeliverable.ProjectName);

                //when
                //UserStory 121992 - 120796 - Create New Deliverable
                test = LogTest("Create New Deliverable");
                string parrentWindow;
                VendorDataRegister vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                vendorDataRegister.ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.New)
                                  .LogValidation<VendorDataRegister>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(createNewDeliverable.SubItemMenus));

                DeliverableItemDetail deliverableItemDetail = vendorDataRegister.OpenDeliverableLineItemTemplate(out parrentWindow);
                deliverableItemDetail.LogValidation<DeliverableItemDetail>(ref validations, deliverableItemDetail.ValidateWindowIsOpened(createNewDeliverable.DeliverableWindowTitle))
                                     .LogValidation<DeliverableItemDetail>(ref validations, deliverableItemDetail.ValidateRequiredFieldsWithRedAsterisk(createNewDeliverable.RequiredField))
                                     .EnterDeliverableItemInfo(createNewDeliverable.DeliverableItemInfo, ref methodValidations)
                                     .LogValidation<DeliverableItemDetail>(ref validations, deliverableItemDetail.ValidateSelectedItemShowInDropdownBoxesCorrect(createNewDeliverable.DeliverableItemInfo));
                AlertDialog alertDialog = deliverableItemDetail.ClickToolbarButton<AlertDialog>(ToolbarButton.Save);
                alertDialog.LogValidation<AlertDialog>(ref validations, deliverableItemDetail.ValidateMessageDisplayCorrect(createNewDeliverable.SaveMessage))
                           .ClickOKButton<DeliverableItemDetail>();

                deliverableItemDetail.ClickToolbarButton<DeliverableItemDetail>(ToolbarButton.More, checkProgressPopup: false, isDisappear: true)
                                     .LogValidation<DeliverableItemDetail>(ref validations, deliverableItemDetail.ValidateDisplayedSubItemLinks(createNewDeliverable.SubItemOfMoreFunction));
                                     
                LinkItem linkItem = deliverableItemDetail.ClickHeaderDropdownItem<LinkItem>(MainPaneHeaderDropdownItem.LinkItems, true);
                linkItem.LogValidation<LinkItem>(ref validations, linkItem.ValidateWindowIsOpened(createNewDeliverable.LinkItemsWindowTitle))
                        .ClickToolbarButton<DeliverableItemDetail>(ToolbarButton.Add, checkProgressPopup: false)
                        .LogValidation<DeliverableItemDetail>(ref validations, linkItem.ValidateDisplayedSubItemLinks(createNewDeliverable.SubItemOfAddFunction));

                AddDocument addDocument = linkItem.ClickHeaderDropdownItem<AddDocument>(MainPaneHeaderDropdownItem.Documents, false);
                addDocument.EnterAltDocumentNo(createNewDeliverable.DocumentNo)
                           .ClickToobarBottomButton<AddDocument>(ToolbarButton.Search.ToDescription(), createNewDeliverable.GridViewAddDocName)
                           //Switch Frame
                           .SelectItemByDocumentNo<AddDocument>(createNewDeliverable.DocumentNo)
                           ;

                // then
                Utils.AddCollectionToCollection(validations, methodValidations);
                Console.WriteLine(string.Join(System.Environment.NewLine, validations.ToArray()));
                validations.Should().OnlyContain(validations => validations.Value).Equals(bool.TrueString);
            }
            catch (Exception e)
            {
                lastException = e;
                validations = Utils.AddCollectionToCollection(validations, methodValidations);
                throw;
            }
        }
    }
}
