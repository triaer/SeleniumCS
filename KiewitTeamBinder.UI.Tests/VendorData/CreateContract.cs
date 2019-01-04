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
        public void VendorDataRegister_CreateNewDeliverable_UI()
        {
            try
            {
                // given 
                var teambinderTestAccount = GetTestAccount("AdminAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var createNewDeliverableData = new CreateNewDeliverableSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + createNewDeliverableData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(createNewDeliverableData.ProjectName);

                //when
                string parrentWindow;
                string currentWindow;
                VendorDataRegister vendorDataRegister = projectDashBoard.SelectModuleMenuItem<VendorDataRegister>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription(), subMenuItem: ModuleSubMenuInLeftNav.VENDODATAREGISTER.ToDescription());
                
                //UserStory 121992 - 120796 - Create New Deliverable
                test = LogTest("Create New Deliverable");
                vendorDataRegister.ClickHeaderButton<VendorDataRegister>(MainPaneTableHeaderButton.New)
                                  .LogValidation<VendorDataRegister>(ref validations, projectDashBoard.ValidateDisplayedSubItemLinks(createNewDeliverableData.SubItemMenus));
                DeliverableItemDetail deliverableItemDetail = vendorDataRegister.OpenDeliverableLineItemTemplate(out parrentWindow);
                deliverableItemDetail.LogValidation<DeliverableItemDetail>(ref validations, deliverableItemDetail.ValidateWindowIsOpened(createNewDeliverableData.DeliverableWindowTitle))
                                     .LogValidation<DeliverableItemDetail>(ref validations, deliverableItemDetail.ValidateRequiredFieldsWithRedAsterisk(createNewDeliverableData.RequiredField))
                                     .EnterDeliverableItemInfo(createNewDeliverableData.DeliverableItemInfo, ref methodValidations)
                                     .LogValidation<DeliverableItemDetail>(ref validations, deliverableItemDetail.ValidateSelectedItemShowInDropdownBoxesCorrect(createNewDeliverableData.DeliverableItemInfo));
                parrentWindow = deliverableItemDetail.GetCurrentWindow();
                AlertDialog alertDialog = deliverableItemDetail.ClickToolbarButton<AlertDialog>(ToolbarButton.Save);
                alertDialog.LogValidation<AlertDialog>(ref validations, deliverableItemDetail.ValidateMessageDisplayCorrect(createNewDeliverableData.SaveMessage))
                           .ClickOKButton<DeliverableItemDetail>();
                deliverableItemDetail.LogValidation<DeliverableItemDetail>(ref validations, deliverableItemDetail.ValidateSaveDialogStatus(closed: true))
                                     .ClickToolbarButton<DeliverableItemDetail>(ToolbarButton.More, checkProgressPopup: false, isDisappear: true)
                                     .LogValidation<DeliverableItemDetail>(ref validations, deliverableItemDetail.ValidateDisplayedSubItemLinks(createNewDeliverableData.SubItemOfMoreFunction));
                LinkItems linkItem = deliverableItemDetail.ClickHeaderDropdownItem<LinkItems>(MainPaneHeaderDropdownItem.LinkItems, true);

                currentWindow = linkItem.GetCurrentWindow();
                linkItem.LogValidation<LinkItems>(ref validations, linkItem.ValidateWindowIsOpened(createNewDeliverableData.LinkItemsWindowTitle))
                        .ClickToolbarButton<DeliverableItemDetail>(ToolbarButton.Add)
                        .LogValidation<DeliverableItemDetail>(ref validations, linkItem.ValidateDisplayedSubItemLinks(createNewDeliverableData.SubItemOfAddFunction));
                AddDocument addDocument = linkItem.ClickHeaderDropdownItem<AddDocument>(MainPaneHeaderDropdownItem.Documents, false, true);
                addDocument.LogValidation<AddDocument>(ref validations, addDocument.ValidateDocumentSearchWindowStatus())
                           .SwitchToFrameOnAddDocument()
                           .EnterDocumentNo(createNewDeliverableData.DocumentNo)
                           .ClickToobarBottomButton<AddDocument>(ToolbarButton.Search.ToDescription(), createNewDeliverableData.GridViewAddDocName)
                           .SelectItemByDocumentNo(createNewDeliverableData.DocumentNo)
                           .LogValidation<AddDocument>(ref validations, addDocument.ValidateDocumentIsHighlighted(createNewDeliverableData.DocumentNo))
                           .ClickToobarBottomButton<LinkItems>(ToolbarButton.OK.ToDescription(), createNewDeliverableData.GridViewLinkItemsName, true);
                linkItem.LogValidation<LinkItems>(ref validations, addDocument.ValidateDocumentSearchWindowStatus(true))
                        .LogValidation<LinkItems>(ref validations, linkItem.ValidateDocumentIsAttached(createNewDeliverableData.DocumentNo))
                        .ClickToolbarButton<AlertDialog>(ToolbarButton.Save);
                alertDialog.LogValidation<AlertDialog>(ref validations, linkItem.ValidateMessageDisplayCorrect(createNewDeliverableData.SaveMessageOnLinkItem))
                           .ClickOKButton<LinkItems>()
                           .SwitchToWindow(parrentWindow);
                deliverableItemDetail.LogValidation<DeliverableItemDetail>(ref validations, deliverableItemDetail.ValidateSaveDialogStatus(true))
                                     .ClickToolbarButton<AlertDialog>(ToolbarButton.Save, checkProgressPopup: false, isDisappear: true);
                alertDialog.LogValidation<AlertDialog>(ref validations, deliverableItemDetail.ValidateMessageDisplayCorrect(createNewDeliverableData.SaveMessage))
                           .ClickOKButton<DeliverableItemDetail>()
                           .SwitchToWindow(currentWindow);

                int countWindow = linkItem.GetCountWindow();
                linkItem.LogValidation<LinkItems>(ref validations, deliverableItemDetail.ValidateSaveDialogStatus(true))
                        .ClickToolbarButton<DeliverableItemDetail>(ToolbarButton.Close)
                        .SwitchToWindow(parrentWindow);
                deliverableItemDetail.LogValidation<LinkItems>(ref validations, linkItem.ValidateLinkItemsWindowIsClosed(countWindow));

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
