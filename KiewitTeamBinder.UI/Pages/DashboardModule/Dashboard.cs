﻿using KiewitTeamBinder.UI.Pages.Global;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.ExtentReportsHelper;

namespace KiewitTeamBinder.UI.Pages.DashboardModule
{
    public class Dashboard : ProjectsDashboard
    {
        #region Entities
        private static string _widgetLabelXpath = "//span[@class='Title' and text()='{0}']";

        private static By _moreLessButton(string widgetUniqueName) => By.XPath($"//div[@widgetuniquename = '{widgetUniqueName}']//a[contains(@id,'More')]");
        private static By _rowInWidget(string widgetUniqueName, string rowName) => By.XPath($"//div[@widgetuniquename = '{widgetUniqueName}']//td[contains(@id,'tdCaption')][span = '{rowName}']");
        private static By _numberOnRowInWidget(string widgetUniqueName, string rowName) => By.XPath($"//div[@widgetuniquename = '{widgetUniqueName}']//td[contains(@id,'tdCaption')][span = '{rowName}']/preceding-sibling::td[contains(@id,'tdCount')]");

        public IWebElement MoreLessButton(string widgetUniqueName) => StableFindElement(_moreLessButton(widgetUniqueName));
        public IWebElement RowInWidget(string widgetUniqueName, string rowName) => StableFindElement(_rowInWidget(widgetUniqueName, rowName));
        public IWebElement NumberOnRowInWidget(string widgetUniqueName, string rowName) => StableFindElement(_numberOnRowInWidget(widgetUniqueName, rowName));
        #endregion

        #region Actions
        public Dashboard(IWebDriver webDriver) : base(webDriver)
        {

        }

        public T ClickNumberOnRow<T>(string widgetUniqueName, string rowName)
        {
            var NumberOnRow = NumberOnRowInWidget(widgetUniqueName, rowName);
            ScrollIntoView(NumberOnRow);
            NumberOnRow.Click();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        /// <summary>
        /// Click More button or Less button at the bottom left of the widget
        /// </summary>
        /// <param name="widgetUniqueName">Widget name</param>
        /// <param name="clickMoreButton">Set true if want to click More button, fasle if want to click Less button</param>
        /// <returns>Dashboard object</returns>
        public Dashboard ClickMoreOrLessButton(string widgetUniqueName, bool clickMoreButton)
        {
            var moreLessButton = MoreLessButton(widgetUniqueName);
            ScrollIntoView(moreLessButton);
            if ((moreLessButton.Text == "More") == clickMoreButton)
            {
                moreLessButton.Click();
                WaitUntil(driver => MoreLessButton(widgetUniqueName).Text == "Less");
            }
                
            
            return this;
        }

        public int GetCountValueFromRow(string widgetUniqueName, string rowName)
        {
            return int.Parse(RowInWidget(widgetUniqueName, rowName).GetAttribute("count"));
        }

        public KeyValuePair<string, bool> ValidateWidgetsOfDashboardDisplayed(string[] widgetLabels)
        {
            var node = StepNode();

            try
            {
                if (widgetLabels.Length >= 0)
                {
                    for (int i = 0; i < widgetLabels.Length; i++)
                    {
                        if (StableFindElement(By.XPath(string.Format(_widgetLabelXpath, widgetLabels[i]))) == null)
                            return SetFailValidation(node, Validation.Widget_Dashboard_Dispalyed + widgetLabels[i]);
                    }
                    return SetPassValidation(node, Validation.Widget_Dashboard_Dispalyed);
                }
                return SetFailValidation(node, Validation.Widget_Dashboard_Dispalyed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Widget_Dashboard_Dispalyed, e);
            }
        }

        public KeyValuePair<string, bool> ValidateCountValueIsCorrect(string widgetUniqueName, string rowName, int expectedCountValue)
        {
            var node = StepNode();            
            try
            {
                int actualCountValue = GetCountValueFromRow(widgetUniqueName, rowName);
                if (actualCountValue == expectedCountValue)
                    return SetPassValidation(node, Validation.Count_Value_Is_Correct + " - " + expectedCountValue);
                else
                    return SetFailValidation(node, Validation.Count_Value_Is_Correct, expectedCountValue.ToString(), actualCountValue.ToString());  
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Count_Value_Is_Correct, e);
            }
        }        

        private static class Validation
        {
            public static string Widget_Dashboard_Dispalyed = "Validate that the widgit is displayed: ";
            public static string Count_Value_Is_Correct = "Validate that the count value is correct";
        }
        #endregion
    }
}
