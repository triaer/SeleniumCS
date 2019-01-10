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

        private static By _moreLessButton(string widgetName) => By.XPath($"//div[@widgetuniquename = '{widgetName}']//a[contains(@id,'More')]");
        private static By _rowInWidget(string widgetName, string rowName) => By.XPath($"//div[@widgetuniquename = '{widgetName}']//td[contains(@id,'tdCaption')][span = '{rowName}']");
        private static By _numberOnRowInWidget(string widgetName, string rowName) => By.XPath($"//div[@widgetuniquename = '{widgetName}']//td[contains(@id,'tdCaption')][span = '{rowName}']/preceding-sibling::td[contains(@id,'tdCount')]");

        public IWebElement MoreLessButton(string widgetName) => StableFindElement(_moreLessButton(widgetName));
        public IWebElement RowInWidget(string widgetName, string rowName) => StableFindElement(_rowInWidget(widgetName, rowName));
        public IWebElement NumberOnRowInWidget(string widgetName, string rowName) => StableFindElement(_numberOnRowInWidget(widgetName, rowName));
        #endregion

        #region Actions
        public Dashboard(IWebDriver webDriver) : base(webDriver)
        {

        }

        /// <summary>
        /// Click More button or Less button at the bottom left of the widget
        /// </summary>
        /// <param name="widgetName">Widget name</param>
        /// <param name="clickMoreButton">Set true if want to click More button, fasle if want to click Less button</param>
        /// <returns>Dashboard object</returns>
        public Dashboard ClickMoreOrLessButton(string widgetName, bool clickMoreButton)
        {
            var moreLessButton = MoreLessButton(widgetName);
            ScrollIntoView(moreLessButton);
            if ((moreLessButton.Text == "More") == clickMoreButton)
                moreLessButton.Click();
            return this;
        }

        private int GetCountValueFromRow(string widgetName, string rowName)
        {
            return int.Parse(RowInWidget(widgetName, rowName).GetAttribute("count"));
        }

        public T ClickNumberOnRow<T>(string widgetName, string rowName)
        {
            var NumberOnRow = NumberOnRowInWidget(widgetName, rowName);
            ScrollIntoView(NumberOnRow);
            NumberOnRow.Click();
            return (T)Activator.CreateInstance(typeof(T), WebDriver);
        }

        public KeyValuePair<string, bool> ValidateWidgetsOfDashboardDisplayed(string[] widgets)
        {
            var node = StepNode();

            try
            {
                if (widgets.Length >= 0)
                {
                    for (int i = 0; i < widgets.Length; i++)
                    {
                        if (StableFindElement(By.XPath(string.Format(_widgetLabelXpath, widgets[i]))) != null)
                            return SetPassValidation(node, Validation.Widget_Dashboard_Dispalyed + widgets[i]);
                        else
                            return SetFailValidation(node, Validation.Widget_Dashboard_Dispalyed + widgets[i]);
                    }
                }
                return SetFailValidation(node, Validation.Widget_Dashboard_Dispalyed);
            }
            catch (Exception e)
            {
                return SetErrorValidation(node, Validation.Widget_Dashboard_Dispalyed, e);
            }
        }

        public KeyValuePair<string, bool> ValidateCountValueIsCorrect(string widgetName, string rowName, int expectedCountValue)
        {
            var node = StepNode();            
            try
            {
                int actualCountValue = GetCountValueFromRow(widgetName, rowName);
                if (actualCountValue == expectedCountValue)
                    return SetPassValidation(node, Validation.Count_Value_Is_Correct);
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
