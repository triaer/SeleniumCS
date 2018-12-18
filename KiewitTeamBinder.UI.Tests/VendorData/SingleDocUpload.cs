using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.UI.Pages.Global;
using KiewitTeamBinder.UI.Pages.VendorDataModule;
using KiewitTeamBinder.Common.TestData;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using System.Threading;
using KiewitTeamBinder.UI;
using KiewitTeamBinder.UI.Pages.Dialogs;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;


namespace KiewitTeamBinder.UI.Tests.VendorData
{
    [TestClass]
    public class SingleDocUpload : UITestBase
    {
        [TestMethod]
        public void UploadSingleDoc()
        {
            try
            {
                // given
                var teambinderTestAccount = GetTestAccount("VendorAccount1", environment, "NonSSO");
                test.Info("Open TeamBinder Web Page: " + teambinderTestAccount.Url);
                var driver = Browser.Open(teambinderTestAccount.Url, browser);
                test.Info("Log on TeamBinder via Other User Login: " + teambinderTestAccount.Username);
                ProjectsList projectsList = new NonSsoSignOn(driver).Logon(teambinderTestAccount) as ProjectsList;

                var uploadSingleDocData = new UploadSingleDocSmoke();
                test.Info("Navigate to DashBoard Page of Project: " + uploadSingleDocData.ProjectName);
                ProjectsDashboard projectDashBoard = projectsList.NavigateToProjectDashboardPage(uploadSingleDocData.ProjectName);

                //when User Story 120221 - 120034 - Upload Single Doc
                test = LogTest("Upload Single Doc");
                string username = projectDashBoard.GetUserNameLogon();
                string currentWindow;
                projectDashBoard.SelectModuleMenuItem<ProjectsDashboard>(menuItem: ModuleNameInLeftNav.VENDORDATA.ToDescription());
                HoldingArea holdingArea = projectDashBoard.SelectModuleMenuItem<HoldingArea>(subMenuItem: ModuleSubMenuInLeftNav.HOLDINGAREA.ToDescription());

                DocumentDetail newDocument = holdingArea.ClickNewButton(out currentWindow);
                newDocument.LogValidation<DocumentDetail>(ref validations, newDocument.ValidateRequiredFieldsWithRedAsterisk(uploadSingleDocData.RequiredFields))
                                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateFromUserFieldShowCorrectDataAndFormat(username, uploadSingleDocData.ColorGrey))
                                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateDocumentNoIsLimitRetained(uploadSingleDocData.MaxLengthOfDocNo))
                                    .EnterDocumentInformation(uploadSingleDocData.SingleDocInformation, ref methodValidations)
                                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateSelectedItemShowInDropdownBoxesCorrect(uploadSingleDocData.SingleDocInformation))
                                    .ClickAttachFilesButton(Utils.GetInputFilesLocalPath(), uploadSingleDocData.FileNames)
                                    .ClickSaveButton()
                                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateSaveSingleDocPopUpStatus())
                                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateMessageDisplayCorrect())
                                    .ClickOkButtonOnPopUp()
                                    .LogValidation<DocumentDetail>(ref validations, newDocument.ValidateSaveSingleDocPopUpStatus(close: true));

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
