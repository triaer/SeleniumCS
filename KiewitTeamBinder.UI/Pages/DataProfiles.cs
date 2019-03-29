using KiewitTeamBinder.Common.Helper;
using KiewitTeamBinder.Common.TestData;
using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.KiewitTeamBinderENums;
using static KiewitTeamBinder.UI.ExtentReportsHelper;


namespace KiewitTeamBinder.UI.Pages
{
    public class DataProfiles : LoggedInLanding
    {

        #region Locators

        private By _addNewButton => By.XPath("//a[contains(text(),'Add New')]");
        private By _inputName => By.XPath("//input[@name='txtProfileName']");
        private By _itemTypeDropdown => By.XPath("//select[@id='cbbEntityType']");
        private By _relatedDataDropdown => By.XPath("//select[@id='cbbSubReport']");
        private By _nextButton => By.XPath("//input[@value='Next']");
        private By _generalSetting => By.XPath("//td[@class='profilesettingheader']");
        public By _displayFileds => By.XPath("//td[@class='profilesettingheader']");
        //public By _sortFields => By.XPath("//td[@class='profilesettingheader']")
        private By _nameCheckbox => By.XPath("//input[@value='name']");
        private By _filedDropdown => By.XPath("//select[@id='cbbFields']");
        private By _addButton => By.XPath("//button[@title='Add condition']");
        private By _ValueInput => By.XPath("//input[@id='txtSearchText']");
        private By _finishButton => By.XPath("//input[@value='Finish']");
        private By _addLevelButton => By.XPath("//input[@id='btnAddSortField']");
        private By _sortByLabel(string addedLevel) => By.XPath($"//span[@class='sortFieldName'][contains(text(), '{addedLevel}')]");
        private By _andOrDropdown => By.XPath("//select[@name='cbbAndOrCondition']");
        private By _filterFieldDropdown => By.XPath("//select[@id='cbbField']");
        private By _filterValue => By.XPath("//select[@id='listCondition']//option");
        private By _dataProfileMenu(string menu) => By.XPath($"//li[contains(text(),'{menu}')]");
        private By _checkAllLable(string checkOrUncheck) => By.XPath($"//a[contains(text(),'{checkOrUncheck}')]");
        private By _checkBoxes => By.XPath("//input[@type='checkbox']");
        private By _allCheckboxes(string checkbox) => By.XPath($"//input[@value='{checkbox}']");
        private By _FirstColumInTable => By.XPath("//table[@class='panel_indicator_table GridView']//tr//td[1]|//th[1]");
        public By _dataProfilesColumn => By.XPath("//th[contains(text(),'Data Profile')]");
        public By _dataProfiles => By.XPath("//div[@id='div_main_body']//tr//td[2]//a");
        
        #endregion

        #region Elements
        public IWebElement AddNewButton { get { return StableFindElement(_addNewButton); } }
        public IWebElement NameFiled { get { return StableFindElement(_inputName); } }
        public IWebElement NextButton { get { return StableFindElement(_nextButton); } }
        public IWebElement TypeDropdown { get { return StableFindElement(_itemTypeDropdown); } }
        public IWebElement RelatedDropdown { get { return StableFindElement(_relatedDataDropdown); } }
        public IWebElement DisplayField { get { return StableFindElement(_displayFileds); } }
        public IWebElement NameCheckBox { get { return StableFindElement(_nameCheckbox); } }
        public IWebElement FiledDropdown { get { return StableFindElement(_filedDropdown); } }
        public IWebElement ButtonAdd { get { return StableFindElement(_addButton); } }
        public IWebElement ValueInput { get { return StableFindElement(_ValueInput); } }
        public IWebElement FinishButton { get { return StableFindElement(_finishButton); } }
        public IWebElement AddLevelButton { get { return StableFindElement(_addLevelButton); } }
        public IWebElement SortByLable(string addedlevel) => StableFindElement(_sortByLabel(addedlevel));
        public IWebElement AndOrDropdown {get { return StableFindElement(_andOrDropdown); } }
        public IWebElement FilterFieldDropdown { get { return StableFindElement(_filterFieldDropdown); } }
        public IWebElement DataProfilesMenu(string menuName) => StableFindElement(_dataProfileMenu(menuName));
        public IWebElement CheckAllLable(string checkOrUncheck) => StableFindElement(_checkAllLable(checkOrUncheck));
        //public IWebElement AllCheckBoxes(string checkBoxName) => StableFindElement(_allCheckboxes(checkBoxName));
        //public IWebElement sortByLabel(string addedLevel) { return StableFindElement(_sortByLabel(addedLevel)); }
        //List<IWebElement> documentLines = HoldingAreaGridData.StableFindElements(_documentRowsVisiableOnGrid).ToList();
        public ReadOnlyCollection<IWebElement> DataProfilesName { get { return StableFindElements(_dataProfiles); } }
        public ReadOnlyCollection<IWebElement> FilterValue { get { return StableFindElements(_filterValue); } }
        public ReadOnlyCollection<IWebElement> CheckBoxes { get { return StableFindElements(_checkBoxes); } }
        #endregion

        #region Methods

        public DataProfiles(IWebDriver webDriver) : base(webDriver)
        {
        }


        public DataProfiles IpuntName(string name)
        {
            var node = CreateStepNode();
            node.Info("Input to 'Name * ' field");
            AddNewButton.Click();
            WaitForElement(_generalSetting);
            NameFiled.InputText(name);
            EndStepNode(node);
            return this;
        }

        public DataProfiles SelectItemType(string item)
        {
            var node = CreateStepNode();
            node.Info("Click 'Item Type' dropped down menu and select any item");
            SelectDropdownByText(TypeDropdown, item);
            EndStepNode(node);
            return this;
        }

        public DataProfiles SelectRelated(string item)
        {
            var node = CreateStepNode();
            node.Info("Click 'Related Data' dropped down menu and select any item");
            SelectDropdownByText(RelatedDropdown, item);
            EndStepNode(node);
            return this;
        }

        public DataProfiles ClickNextButton( By elementForwait)
        {
            var node = CreateStepNode();
            NextButton.Click();
       
            WaitForElement(elementForwait);
         
            EndStepNode(node);
            return this;
        }

        public DataProfiles CLickAddLevelButton()
        {
            AddLevelButton.Click();
            return this;
        }

        public DataProfiles ClickFinishBUtton( By elementForwait)
        {
            var node = CreateStepNode();
            FinishButton.Click();
           
            WaitForElement(elementForwait);
            
            EndStepNode(node);
            return this;
        }

        public DataProfiles CheckNameCheckBox()
        {
            var node = CreateStepNode();
            NameCheckBox.Check();
            EndStepNode(node);
            return this;
        }

        public DataProfiles SelectField(string item)
        {
            var node = CreateStepNode();
            SelectDropdownByText(FiledDropdown, item);
            EndStepNode(node);
            return this;
        }

        public DataProfiles AddCriteria(string value, string [] filterField, bool and = true)
        {
            var node = CreateStepNode();
            
            if (and == false)
            {
                SelectDropdownByText(AndOrDropdown, "or");
            }
            for (int i = 0; i < filterField.Length; i++)
            {
                SelectDropdownByText(FilterFieldDropdown, filterField[i]);
                ValueInput.InputText(value);
                ButtonAdd.Click();
            }
            EndStepNode(node);
            return this;
        }

        public DataProfiles SelectCreatedDataProfiles(string dataProfileName)
        {
            var node = CreateStepNode();
            foreach (var item in DataProfilesName)
            {
                if (item.Text == dataProfileName)
                {
                    item.Click();
                }
            }
            EndStepNode(node);
            return this;
        }

        public DataProfiles CheckAllFields()
        {
            CheckAllLable(CheckAllLablesEnum.CheckAll.ToDescription()).Click();
            return this;
        }

        public DataProfiles UnCheckAllField()
        {
            CheckAllLable(CheckAllLablesEnum.UnCheckAll.ToDescription()).Click();
            return this;
        }

        //public string getSelectedItemType()
        //{
        //    SelectElement selectElement = new SelectElement(TypeDropdown);
        //    string selected =  selectElement.SelectedOption.Text;
        //    return selected;
        //}

        public bool CheckAddedLevelIsExit(string addedLevel)
        {
            try
            {
                if (StableFindElement(_sortByLabel(addedLevel)) != null)
                {
                   return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<string> ListFiterValue(ReadOnlyCollection<IWebElement> listFiter)
        {
            List<string> listFitertext = new List<string>();
            foreach (var item in listFiter)
            {
                listFitertext.Add(item.Text);
            }
            return listFitertext;
        }

        public DataProfiles AddNewDataProfile(string name, string iTemType, string relatedDataItem, string filed, string valueDescription, string [] fillterField)
        {
            IpuntName(name);
            SelectItemType(iTemType);
            SelectRelated(relatedDataItem);
            ClickNextButton(_displayFileds);
            CheckNameCheckBox();
            ClickNextButton(_displayFileds);
            SelectField(filed);
            CLickAddLevelButton();
            ClickNextButton(_displayFileds);
            AddCriteria(valueDescription, fillterField);
            ClickNextButton( _displayFileds);
            CheckNameCheckBox();
            ClickFinishBUtton(_dataProfilesColumn);
            return this;
        }

        public DataProfiles NavigateToStaticPage()
        {
            DataProfilesMenu(DataProfileMenuTabs.Generalsettings.ToDescription()).Click();
            Thread.Sleep(1000);
            DataProfilesMenu(DataProfileMenuTabs.StaticField.ToDescription()).Click();
            WaitForElement(_displayFileds);
            return this;
        }

 

        public KeyValuePair<string, bool> ValidateItemType(string itemtype)
        {
            var node = CreateStepNode();
            string actText = GetTextSelectedDropDown(TypeDropdown).ToLower();
            //string expItemtype = itemtype.ToLower();
            try
            {
                if (actText == itemtype)
                {
                    return SetPassValidation(node, ValidationMessage.CheckAllSetting);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.CheckAllSetting);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.CheckAllSetting, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidateDataProfilename(string dataProfileName)
        {
            var node = CreateStepNode();
            try
            {
                //string abc = NameFiled.GetAttribute("value");
                if (NameFiled.GetAttribute("value") == dataProfileName)
                {
                    //EndStepNode(node);
                    return SetPassValidation(node, ValidationMessage.CheckAllSetting);
                }
                else
                {
                    //EndStepNode(node);
                    return  SetFailValidation(node, ValidationMessage.CheckAllSetting);
                }

            }
            catch (Exception e)
            {
                //EndStepNode(node);
                return SetErrorValidation(node, ValidationMessage.CheckAllSetting, e);
                
            }
            finally
            {
                EndStepNode(node);
            }
            
        }

        public KeyValuePair<string, bool> ValidateRelatedData(string relatedData)
        {
            var node = CreateStepNode();
            string ExpRelatedData = relatedData.ToLower();
            try
            {
                RelatedDropdown.Click();
                string abc = RelatedDropdown.GetAttribute("oldvalue");
                if (abc == ExpRelatedData)
                {
                    return SetPassValidation(node, ValidationMessage.CheckAllSetting);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.CheckAllSetting);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.CheckAllSetting, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidateNameisCheked()
        {
            var node = CreateStepNode();
            try
            {
                if (NameCheckBox.Selected)
                {
                    return SetPassValidation(node, ValidationMessage.CheckAllSetting);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.CheckAllSetting);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.CheckAllSetting, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidateAddedLevelIsExit(string addedLevel)
        {
            var node = CreateStepNode();
            try
            {
                if (CheckAddedLevelIsExit(addedLevel))
                {
                    return SetPassValidation(node, ValidationMessage.CheckAllSetting);
                }
                else
                {
                    return SetFailValidation(node, ValidationMessage.CheckAllSetting);
                }
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.CheckAllSetting, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public List<KeyValuePair<string, bool>> ValidateFilterValue(string [] field, string value, string andOr = null)
        {
            var node = CreateStepNode();
            string[] expValue = new string[field.Length];
            var validations = new List<KeyValuePair<string, bool>>();
            expValue[0] = field[0] + "=\"" + value + "\"";
            for (int i = 1; i < field.Length; i++)
            {
                expValue[i] = andOr + " " +field[i] + "=\"" + value + "\"";
            }
            //string actValue  = ListFiterValue(FilterValue)[0];
            try
            {
                for (int i = 0; i < field.Length; i++)
                {

                    if (expValue[i] == ListFiterValue(FilterValue)[i])
                    {
                        validations.Add(SetPassValidation(node, ValidationMessage.CheckAllSetting));
                        //return validations;
                    }
                    else
                    {
                        validations.Add(SetFailValidation(node, ValidationMessage.CheckAllSetting));
                        //return validations;
                    }
                }
            }
            catch (Exception e)
            {
                validations.Add(SetErrorValidation(node, ValidationMessage.CheckAllSetting, e));
                //return validations;
            }
            EndStepNode(node);
            return validations;
            //finally
            //{
            //    //return validations;
            //    EndStepNode(node);
            //}
        }

        public KeyValuePair<string, bool> ValidateAllCheckBoxesAreChecked()
        {
            var node = CreateStepNode();
            try
            {
                foreach (var item in CheckBoxes)
                {
                    if (!item.Selected)
                    {
                        return SetFailValidation(node, ValidationMessage.ValidationAllCheckBoxes);
                    }
                }
                return SetPassValidation(node, ValidationMessage.ValidationAllCheckBoxes);
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidationAllCheckBoxes, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidationAllCheckBoxesAreUncheked()
        {
            var node = CreateStepNode();
            try
            {
                foreach (var item in CheckBoxes)
                {
                    if (item.Selected)
                    {
                        return SetFailValidation(node, ValidationMessage.ValidationAllCheckBoxes);
                    }
                }
                return SetPassValidation(node, ValidationMessage.ValidationAllCheckBoxes);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, ValidationMessage.ValidationAllCheckBoxes, e);
            }
            finally
            {
                EndStepNode(node);
            }
        }

        public KeyValuePair<string, bool> ValidateAllFieldsArePopulated()
        {
            var node = CreateStepNode();
            SignOnTestsSmoke singOnData = new SignOnTestsSmoke();
            try
            {
                for (int i = 0; i < singOnData.NameOfCheckbox.Length; i++)
                {
                    if (StableFindElement(_allCheckboxes(singOnData.NameOfCheckbox[i]))==null)
                    {
                        return SetPassValidation(node, ValidationMessage.ValidateAllFieldPopulated);
                    }
                }
                return SetPassValidation(node, ValidationMessage.ValidateAllFieldPopulated);
            }
            catch (Exception e)
            {

                return SetErrorValidation(node, ValidationMessage.ValidateAllFieldPopulated, e);
            }
        }

        private static class ValidationMessage
        {
            public static string CheckAllSetting = "Check all settings done above are saved correctly";
            public static string ValidationAllCheckBoxes = "Validate all Checkboxes are checked or unchecked";
            public static string ValidateAllFieldPopulated = "Validate all field are populated";
        }

        #endregion
    }
}
