﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;
using System.Runtime.Remoting;
using OpenQA.Selenium.Interactions;
using System.Windows.Forms;
using KiewitTeamBinder.Common.Helper;
using AventStack.ExtentReports;

namespace KiewitTeamBinder.UI.Pages
{
    public abstract class PageBase
    {
        #region Entities


        public string Title { get; set; }
        internal static string Url { get; set; }
        internal static IWebDriver WebDriver { get; set; }

        internal static By loadingIcon = By.XPath("//div[@class = 'k-loading-mask']");
        internal static string loadingIconXpath = "//div[@class = 'k-loading-mask']";
        internal static By overlayWindow = By.XPath("//div[@class = 'k-overlay']");
        internal const int longTimeout = 30;
        internal const int mediumTimeout = 15;
        internal const int shortTimeout = 5;
        internal const int sapLongTimeout = 240;
        internal const int sapShortTimeout = 60;
        #endregion

        #region Actions
        protected PageBase(IWebDriver webDriver)
        {
            WebDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
            Url = "";
            Title = "";
        }
        protected PageBase(string url, string title)
        {
            Url = url;
            Title = title;
        }

        internal void WaitForLoad()
        {
            var wait = Browser.Wait();

            try
            {
                wait.Until(p => p.Title == Title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //TakeScreenshot();
                throw;
            }

        }

        /// <summary>
        /// Using for special textbox which includes two input elements
        /// </summary>
        /// <param name="Box"></param>
        /// <param name="textbox"></param>
        /// <param name="value"></param>
        internal void FillTextbox(IWebElement Box, By textbox, string value)
        {
            ScrollIntoView(Box);
            Box.Click();
            for (int i = 0; i < 10; i++)
                if (StableFindElement(textbox, shortTimeout) != null)
                    break;
                else
                    Box.Click();
            
            StableFindElement(textbox).InputText(value);
        }

        internal static IWebElement StableFindElement(By by, long timeout = longTimeout)
        {
            IWebElement Element = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    var wait = Browser.Wait(shortTimeout);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
                    wait.Until(d => d.FindElement(by).Displayed);
                    Element = WebDriver.FindElement(by);
                    break;
                }

                catch (Exception)
                {
                    // Skip exception
                }
            } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);

            stopwatch.Stop();
            return Element;
        }

        internal static ReadOnlyCollection<IWebElement> StableFindElements(By by, long timeout = longTimeout)
        {
            ReadOnlyCollection<IWebElement> Elements = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    var wait = Browser.Wait(shortTimeout);
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
                    Elements = WebDriver.FindElements(by);
                    break;
                }

                catch (Exception)
                {
                    // Skip exception
                }
            } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);

            stopwatch.Stop();
            return Elements;
        }

        internal static void Wait(int seconds = longTimeout)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
            //WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds));
            //wait.
        }
        internal static T WaitUntil<T>(Func<IWebDriver, T> condition, int seconds = longTimeout)
        {
            var wait = Browser.Wait(seconds);
            var ignoredExceptions = new List<Type>() { typeof(StaleElementReferenceException) };

            wait.IgnoreExceptionTypes(ignoredExceptions.ToArray());
            return wait.Until(condition);
        }

        internal static void WaitForElementUntil(By elementDescription, int seconds = longTimeout)
        {
            IWebElement myDynamicElement = (new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds))).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementDescription));
        }

        internal static void WaitFor(By elementDescription)

        {
            var wait = Browser.Wait();

            wait.Until(e => e.FindElement(elementDescription));
        }

        internal static void WaitForElementClickable(By elementDescription, int seconds = mediumTimeout)
        {
            IWebElement myDynamicElement = (new WebDriverWait(WebDriver, TimeSpan.FromSeconds(seconds))).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementDescription));
        }

        internal static void WaitForElementEnable(By elementDescription, int seconds = mediumTimeout)
        {
            var wait = Browser.Wait(seconds);
            wait.Until(WebDriver => WebDriver.FindElement(elementDescription).Enabled);
        }


        internal static void WaitForElementDisplay(By elementDescription, int seconds = longTimeout)
        {
            var wait = Browser.Wait(seconds);

            wait.Until(e => e.FindElement(elementDescription).Displayed);
        }

        internal static void WaitForElementDisappear(By elementDescription, int seconds = mediumTimeout)
        {
            var wait = Browser.Wait(seconds);

            wait.Until(WebDriver => !WebDriver.FindElement(elementDescription).Displayed);
        }

        internal static void WaitForElementNotExist(IWebElement Element, int seconds = mediumTimeout)
        {
            var wait = Browser.Wait(seconds);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(Element));
        }

        internal static IWebElement FindElement(By by, int timeout = shortTimeout)
        {
            try
            {
                return Browser.Wait(timeout).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            }
            catch (Exception)
            {
                return null;
            }
        }
        internal static void WaitForPageComplete(int timeoutSec = mediumTimeout)
        {
            var wait = Browser.Wait(timeoutSec);
            IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriver;
            // Check if document is ready
            wait.Until(wd => js.ExecuteScript("return document.readyState")).Equals("complete");

        }
        internal static void ScrollToElement(IWebElement Element)
        {
            Actions action = new Actions(WebDriver);
            action.MoveToElement(Element);
            action.Perform();
        }
        internal static void ScrollToElement(By by, int timeout = longTimeout)
        {
            var element = StableFindElement(by, timeout);

            Actions action = new Actions(WebDriver);
            action.MoveToElement(element);
            action.Perform();
        }

        internal static void ScrollIntoView(IWebElement Element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)WebDriver;
            jse.ExecuteScript("arguments[0].scrollIntoView();", Element);
        }
        internal static void WaitForElementCSSAttribute(IWebElement Element, string cssAttribute, string cssAttributeValue, long seconds = mediumTimeout)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    Wait(1);
                    if (Element.GetCssValue(cssAttribute).Contains(cssAttributeValue))
                        break;
                    else
                        Wait(1);
                }

                catch (Exception)
                {
                    // Skip exception
                }
            } while (stopwatch.ElapsedMilliseconds <= seconds * 1000);

            stopwatch.Stop();
        }

        internal static void WaitForElementAttribute(IWebElement Element, string attribute, string attributeValue, long seconds = 15)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    Wait(1);
                    if (Element.GetAttribute(attribute).Contains(attributeValue))
                        break;
                    else
                        Wait(1);
                }
                catch (Exception)
                {
                    // Skip exception
                }
            } while (stopwatch.ElapsedMilliseconds <= seconds * 1000);
        }

        internal static void WaitForElement(By elementDescription, long timeout = longTimeout)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(shortTimeout));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                try
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(elementDescription));
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elementDescription));

                    break;
                }
                catch (Exception)
                {
                    // Skip exception
                }
            } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);

            stopwatch.Stop();
        }

        /// <summary>
        /// Get index of table cell value
        /// </summary>
        /// <param name="TableElement"></param>
        /// <param name="cellValue"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        internal static void GetTableCellValueIndex(IWebElement TableElement, string cellValue, out int rowIndex, out int columnIndex, string colType = "td", bool equalComparison = true)
        {
            rowIndex = columnIndex = -1;
            ReadOnlyCollection<IWebElement> rowCollection = TableElement.StableFindElements(By.TagName("tr"));
            foreach (var rowItem in rowCollection)
            {
                bool exitLoop = false;
                bool valueComparison = false;
                ReadOnlyCollection<IWebElement> colCollection = rowItem.StableFindElements(By.TagName(colType));
                foreach (var colItem in colCollection)
                {
                    if (equalComparison)
                        valueComparison = colItem.GetAttribute("innerText").Trim() == cellValue;
                    else
                        valueComparison = colItem.GetAttribute("innerText").Trim().Contains(cellValue);
                    if (valueComparison)
                    {
                        rowIndex = rowCollection.IndexOf(rowItem) + 1;
                        columnIndex = colCollection.IndexOf(colItem) + 1;
                        exitLoop = true;
                        break;
                    }
                }
                if (exitLoop == true)
                    break;
            }
        }

        /// <summary>
        /// Get value of table cell by index
        /// </summary>
        /// <param name="TableElement"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="colType"></param>
        /// <returns></returns>
        internal static string GetTableCellValueByIndex(IWebElement TableElement, int rowIndex, int colIndex, string colType = "td")
        {
            return TableElement.FindElement(By.XPath(".//tr[" + rowIndex + "]/" + colType + "[" + colIndex + "]")).Text;
        }

        /// <summary>
        /// Identify TableCell by cell value
        /// </summary>
        /// <param name="TableElement"></param>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        internal static IWebElement TableCell(IWebElement TableElement, string cellValue, string colType = "td", long timeout = longTimeout)
        {
            int i_RowNum = -1;
            int i_ColNum = -1;

            GetTableCellValueIndex(TableElement, cellValue, out i_RowNum, out i_ColNum);
            if (i_RowNum == -1 || i_ColNum == -1)
                return null;
            else
                return TableElement.StableFindElement(By.XPath(".//tr[" + i_RowNum + "]/" + colType + "[" + i_ColNum + "]"), timeout);
        }

        /// <summary>
        /// Identify TableCell by index
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        internal static IWebElement TableCell(IWebElement TableElement, int rowIndex, int colIndex, string colType = "td", long timeout = longTimeout)
        {
            try
            {
                return TableElement.StableFindElement(By.XPath(".//tr[" + rowIndex + "]/" + colType + "[" + colIndex + "]"), timeout);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get number of rows of a table 
        /// </summary>
        /// <param name="TableElement"></param>
        /// <returns></returns>
        internal static int GetTableRowNumber(IWebElement TableElement)
        {
            try
            {
                return TableElement.StableFindElements(By.TagName("tr")).Count;
            }

            catch
            {
                return 0;
            }
        }

        internal static int GetTableColumnNumber(IWebElement TableElement, string colType = "td")
        {
            return TableElement.StableFindElements(By.TagName(colType)).Count;

        }

        /// <summary>
        /// Select an item from a customized combobox by text value
        /// </summary>
        /// <param name="Combobox">Combobox element</param>
        /// <param name="data">Dataset locator, usually is "//ul/li"</param>
        /// <param name="value">text value of item</param>
        internal static void SelectComboboxByText(IWebElement Combobox, By data, string value, int timeout = mediumTimeout, bool isEqual = true)
        {
            Combobox.Click();
            WaitForElement(data, timeout);
            Wait(1);
            List<IWebElement> items = GetElementsWithSize(data, 1);

            if (isEqual)
            {
                foreach (IWebElement item in items)
                {
                    if (item.Text.Trim().Equals(value) || item.GetAttribute("innerHTML").Trim().Equals(value))
                    {
                        item.Click();
                        break;
                    }
                }
            }
            else
                foreach (IWebElement item in items)
                {
                    if (item.Text.Trim().Contains(value) || item.GetAttribute("innerHTML").Trim().Contains(value))
                    {
                        item.Click();
                        break;
                    }
                }
        }

        /// <summary>
        /// Select random an item from a customized combobox by text value
        /// </summary>
        /// <param name="Combobox">Combobox element</param>
        /// <param name="data">Dataset locator, usually is "//ul/li"</param>
        /// <param name="value">text value of item</param>
        public string SelectRandomComboboxByText(IWebElement Combobox, By data, int timeout = mediumTimeout)
        {
            Combobox.Click();
            WaitForElement(data, timeout);
            Wait(1);
            List<IWebElement> Items = GetElementsWithSize(data, 1);

            IWebElement ElementRandom = Items[Utils.GetRandomNumber(0, Items.Count())];
            string selectedItem = ElementRandom.GetAttribute("innerHTML").Trim();

            ElementRandom.Click();

            return selectedItem;
        }


        /// <summary>
        /// Get elements by locator whenever elements' size equals or larger than the input size
        /// </summary>
        /// <param name="ElementLocator"></param>
        /// <param name="size">Size expected</param>
        /// <param name="timeoutSecond"></param>
        /// <returns>List of Elements that has size as expected</returns>
        internal static List<IWebElement> GetElementsWithSize(By ElementLocator, int size, int timeoutSecond = 30)
        {
            List<IWebElement> eles;
            Stopwatch sw = new Stopwatch();
            long timeout = timeoutSecond * 1000;
            sw.Start();
            do
            {
                eles = StableFindElements(ElementLocator).ToList();
                if (eles.Count >= size)
                    break;
            } while (sw.ElapsedMilliseconds <= timeout);
            sw.Stop();
            return eles;
        }

        /// <summary>
        /// Wait for spinner appear and disappear
        /// </summary>
        /// <param name="elementDescription"></param>
        /// <param name="timeout"></param>
        internal static void WaitForLoading(By elementDescription, int timeout = mediumTimeout)
        {
            var wait = Browser.Wait(timeout);
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(elementDescription));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(elementDescription));
            }
            catch (Exception)
            {
                //skip the action
            }
        }

        internal static void WaitForAngularJSLoad()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)WebDriver;
            bool isDataLoaded = false;
            int numOfWait = 1;
            while (isDataLoaded == false)
            {
                isDataLoaded = (bool)js.ExecuteScript("return (window.angular !== undefined) && (angular.element(document).injector() !== undefined) && (angular.element(document).injector().get('$http').pendingRequests.length === 0)");
                Wait(1);
                numOfWait++;
                if (numOfWait > 60)
                    break;
            }
        }

        internal static void WaitForAjaxComplete(int timeout = 30)
        {
            var wait = Browser.Wait(timeout);
            wait.Until(d => (bool)(d as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
        }

        internal static void Reload()
        {
            WebDriver.Navigate().Refresh();
        }

        public bool IsPageLoaded(By by)
        {
            try
            {
                WaitForElement(by);
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static void CopyDataFromExcelFileToExcelFile(string excelFileContainPath, string excelFileToBeCopiedPath, string excelFileContainSheetName, string excelFileToBeCopiedSheetName, int rowToBeCopied, int rowContain)
        {
            var excelDriver1 = ExcelInterop.ExcelDriver.getExcelHelper(excelFileToBeCopiedPath);
            excelDriver1.Open(excelFileToBeCopiedPath, excelFileToBeCopiedSheetName);
            excelDriver1.GetAllExcelRowsValue(rowToBeCopied);
            string[] cellValue = excelDriver1.GetAllExcelRowsValue(rowToBeCopied).Split(',');
            var excelDriver2 = ExcelInterop.ExcelDriver.getExcelHelper(excelFileContainPath);

            for (int colIndex = 1; colIndex <= cellValue.Length; colIndex++)
            {
                excelDriver2.WriteDataToExcelFile(excelFileContainPath, excelFileContainSheetName, rowContain, colIndex, cellValue[colIndex - 1]);
            }
            //excelDriver2.OpenExcelfileToView(excelFileContainPath, excelFileContainSheetName, 5);
            excelDriver2.Close();
        }

        internal static void EditCellValueInExcelFile(string filePath, string sheetName, string cellValueBeforeEdit, string cellValueAfterEdit)
        {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.Open(filePath, sheetName);
            excelDriver.Search(cellValueBeforeEdit);
            excelDriver.WriteDataToExcelFile(filePath, sheetName, excelDriver.Search(cellValueBeforeEdit)[0], excelDriver.Search(cellValueBeforeEdit)[1], cellValueAfterEdit);
            excelDriver.Close();
        }

        internal static void OpenExcelFiletoView(string filePath, string sheetName, int timeout)
        {
            var excelDriver = ExcelInterop.ExcelDriver.getExcelHelper(filePath);
            excelDriver.OpenExcelfileToView(filePath, sheetName, timeout);
            excelDriver.Close();
        }

        internal static void SwitchToPopUpWindow(IWebElement ElementToBeClicked, out string parentWindow, bool closePreviousWindow = false, int timeout = 30)
        {
            parentWindow = WebDriver.CurrentWindowHandle;
            ReadOnlyCollection<string> originalHandles = WebDriver.WindowHandles;

            //WaitForElementClickable(GetByOfElement(ElementToBeClicked));
            ElementToBeClicked.Click();
            string popUpWindowHandle = Browser.Wait(timeout).Until<string>((d) =>
            {
                string foundHandle = null;

                // Subtract out the list of known handles. In the case of a single
                // popup, the newHandles list will only have one value.
                List<string> newHandles = WebDriver.WindowHandles.Except(originalHandles).ToList();
                if (newHandles.Count > 0)
                {
                    foundHandle = newHandles[0];
                }

                return foundHandle;
            });
            if (closePreviousWindow == true)
            {
                Browser.Close();
            }
            WebDriver.SwitchTo().Window(popUpWindowHandle);
            WebDriver.Manage().Window.Maximize();
        }

        internal static void OpenURLInNewTab(string url, out string parentWindow, int timeout = 30)
        {
            parentWindow = WebDriver.CurrentWindowHandle;
            ((IJavaScriptExecutor)WebDriver).ExecuteScript("window.open('" + url + "','_blank');");

            string[] listTabs = WebDriver.WindowHandles.ToArray();
            WebDriver.SwitchTo().Window(listTabs[listTabs.Count() - 1]);
            WebDriver.Manage().Window.Maximize();
        }

        internal static string GetIDValueOfDivElementByText(IWebElement parent, string elementXPath, string content)
        {
            By selector = By.XPath(string.Format(elementXPath, content));
            WaitForElement(selector);
            return parent.StableFindElement(selector).GetAttribute("id");
        }
        internal static IWebElement GetDivCellByRowAndColumnIDNumber(IWebElement elementParent, string rowIndx, string colIndx)
        {
            string partID = rowIndx + ":" + colIndx;
            return elementParent.StableFindElement(By.CssSelector(string.Format("div[id$='{0}']", partID)));
        }

        internal static void SelectDateOnCalendar(string dateString, IWebElement DatePicker, By calendarLocator)
        {
            //Format of dateString: "MM/dd/yyyy"
            string[] monthList = { "JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER" };
            string _dayButtonXPath = ".//td[not(contains(@class,'k-other-month'))]/a[text()='{0}']";
            By _displayMonthYear = By.XPath(".//div[@class='k-header']/a[contains(@class,'k-nav-fast')]");

            string calMonth = null;
            string calYear = null;
            int jumMonthBy = 0;

            //parse the target date
            string[] parts = dateString.Split('/');
            int expMonth = int.Parse(parts[0]);
            int expDay = int.Parse(parts[1]);
            int expYear = int.Parse(parts[2]);

            //open the calendar
            DatePicker.Click();
            IWebElement CalendarPopup = StableFindElement(calendarLocator);
            WaitForElementAttribute(CalendarPopup, "style", "block");

            //Retrieve current selected month / year name from date picker popup
            IWebElement DispMonthYear = CalendarPopup.StableFindElement(_displayMonthYear);
            calMonth = DispMonthYear.Text.Split(' ')[0];
            calYear = DispMonthYear.Text.Split(' ')[1];

            //Calculate the differ month
            jumMonthBy = ((expYear - int.Parse(calYear)) * 12) + (expMonth - (monthList.ToList().IndexOf(calMonth) + 1));

            if (jumMonthBy > 0)
            {
                while (jumMonthBy-- > 0) CalendarPopup.FindElement(By.XPath(".//div[@class='k-header']/a[contains(@class,'k-nav-next')]")).Click();
            }
            else if (jumMonthBy < 0)
            {
                while (jumMonthBy++ < 0) CalendarPopup.FindElement(By.XPath(".//div[@class='k-header']/a[contains(@class,'k-nav-prev')]")).Click();
            }

            WaitUntil(dr => CalendarPopup.StableFindElement(_displayMonthYear).Text.Contains(monthList[expMonth - 1]));
            //click on the target day 
            By dayElement = By.XPath(string.Format(_dayButtonXPath, expDay));
            WaitForAngularJSLoad();
            IWebElement DayButton = CalendarPopup.StableFindElement(dayElement);
            DayButton.Click();

        }

        internal static void ClickElement(By by)
        {
            WaitForElementClickable(by);
            StableFindElement(by).Click();
        }

        public void GlobalSearchByName(string valueSearch, IWebElement SearchTextbox, By gridRows)
        {
            WaitForElement(gridRows);
            SearchTextbox.InputText(valueSearch);
            SearchTextbox.SendKeys(OpenQA.Selenium.Keys.Enter);

            if (FindElement(loadingIcon) != null)
                WaitForLoading(loadingIcon);
            WaitForElement(gridRows);
            WaitForAngularJSLoad();
        }
        internal static KeyValuePair<string, bool> SetPassValidation(ExtentTest test, string testInfo)
        {
            test.Pass(testInfo);
            return new KeyValuePair<string, bool>(testInfo, true);
        }

        internal static KeyValuePair<string, bool> SetFailValidation(ExtentTest test, string testInfo, string expectedValue = null, string actualValue = null)
        {
            if (expectedValue == null)
            {
                test.Fail(testInfo, AttachScreenshot(GetCaptureScreenshot()));
                return new KeyValuePair<string, bool>(testInfo, false);
            }
            else
            {
                test.Fail(Utils.ReportFailureOfValidationPoints(testInfo, expectedValue, actualValue), AttachScreenshot(GetCaptureScreenshot()));
                return new KeyValuePair<string, bool>(Utils.ReportFailureOfValidationPoints(testInfo, expectedValue, actualValue), false);
            }
        }

        internal static KeyValuePair<string, bool> SetErrorValidation(ExtentTest test, string testInfo, Exception exception)
        {
            test.Error(Utils.ReportExceptionInValidation(testInfo, exception), AttachScreenshot(GetCaptureScreenshot()));
            return new KeyValuePair<string, bool>(Utils.ReportExceptionInValidation(testInfo, exception), false);
        }

        internal static MediaEntityModelProvider AttachScreenshot(string imagePath)
        {
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(Utils.ImageToBase64(imagePath)).Build();
        }

        internal static string GetCaptureScreenshot(IWebElement HightLightElement = null)
        {
            string screenshotFilePath;
            TakeScreenshot(out screenshotFilePath, HightLightElement);
            return screenshotFilePath;
        }

        internal static void TakeScreenshot(out string filePath, IWebElement HightLightElement = null, string captureLocation = "c:\\temp\\testresults\\")
        {

            string timeStamp = DateTime.Now.ToString("ddMMyyyyHHmmss");

            filePath = captureLocation + "ErrorCapture" + timeStamp + ".png";

            if (HightLightElement != null)
                ((IJavaScriptExecutor)Browser.Driver).ExecuteScript("arguments[0].style.border='3px solid red'", HightLightElement);
            try

            {

                //TODO : Add more info to the filename and suitable location to save to 

                Screenshot screenshot = ((ITakesScreenshot)Browser.Driver).GetScreenshot();
                screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);

            }

            catch (Exception e)

            {

                Console.WriteLine("TakeScreenshot encountered an error. " + e.Message);

                throw;

            }



            //Console.WriteLine(callingClassName + "." + callingMethodName + " generated an error. A ScreenShot of the browser has been saved. " + filePath);

        }
    }
    #endregion
}
