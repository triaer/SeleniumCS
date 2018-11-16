using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.UI.Pages.Global.PageBase;

namespace KiewitTeamBinder.UI
{
    public static class IWebElementExtensions
    {
        public static void InputText(this IWebElement Element, string text)
        {
            Element.Clear();
            Element.SendKeys(text);
        }

        public static void Check(this IWebElement Element)
        {
            bool isChecked = Element.Selected;
            if (isChecked == false)
            {
                Element.Click();
            }
        }

        public static void UnCheck(this IWebElement Element)
        {
            bool isChecked = Element.Selected;
            if (isChecked == true)
            {
                Element.Click();
            }
        }

        public static bool IsDisplayed(this IWebElement Element)
        {
            try
            {
                return Element.Displayed;
            }
            catch
            {
                return false;
            }
        }

        public static IWebElement StableFindElement(this IWebElement Element, By by, long timeout = longTimeout)
        {
            IWebElement Ele = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    var wait = Browser.Wait(shortTimeout);
                    wait.Until(d => Element.FindElement(by).Displayed);
                    Ele = Element.FindElement(by);
                    ScrollToElement(Ele);
                    break;
                }

                catch (StaleElementReferenceException)
                {
                    if (FindElement(by) != null)
                        if (FindElement(by) != null)
                            try
                            {
                                if (!Element.FindElement(by).IsDisplayed())
                                    ScrollToElement(Element.FindElement(by));
                            }
                            catch
                            {
                                return Ele;
                            }
                }

                catch (WebDriverTimeoutException)
                {
                    if (FindElement(by) != null)
                        try
                        {
                            if (!Element.FindElement(by).IsDisplayed())
                                ScrollToElement(Element.FindElement(by));
                        }
                        catch
                        {
                            return Ele;
                        }     
                }

                catch (Exception)
                {
                    //skip remain exceptions
                }
            } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);

            stopwatch.Stop();
            return Ele;
        }

        public static ReadOnlyCollection<IWebElement> StableFindElements(this IWebElement Element, By by, long timeout = longTimeout)
        {
            ReadOnlyCollection<IWebElement> Eles = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                try
                {
                    var wait = Browser.Wait(shortTimeout);
                    wait.Until(d => Element.FindElements(by).Count > 0);
                    Eles = Element.FindElements(by);
                    break;
                }

                catch
                {
                    //skip remain exceptions
                }
            } while (stopwatch.ElapsedMilliseconds <= timeout * 1000);

            stopwatch.Stop();
            return Eles;
        }

        public static string GetValue(this IWebElement Element)
        {
            return Element.GetAttribute("value");
        }

        public static IWebDriver SwitchToIFrameBySelector(this IWebDriver driver, By bySelector, int seconds = sapLongTimeout)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            do
            {
                try
                {
                    WaitForElement(bySelector);
                    driver = Browser.Wait(longTimeout).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt(bySelector));
                    break;
                }
                catch (Exception)
                {
                    //Skip the exception
                }

            } while (stopwatch.ElapsedMilliseconds <= seconds * 1000);
            stopwatch.Stop();
            return driver;
        }
        public static void SwitchOutOfIFrame(this IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }

        public static void ActionsClick(this IWebElement Element)
        {
            Actions actions = new Actions(WebDriver);
            actions.MoveToElement(Element).Click(Element).Build().Perform();
        }
        public static void ClickTwice(this IWebElement Element)
        {
            Actions actions = new Actions(WebDriver);
            actions.Click(Element).Click(Element).Perform();
        }

        public static void DoubleClick(this IWebElement Element)
        {
            Actions actions = new Actions(WebDriver);
            actions.DoubleClick(Element).Perform();
        }
        
        public static void ClickOnIE(this IWebElement Element)
        {
            ScrollIntoView(Element);
            Element.Click();
        }
        public static void HoverAndClickWithJS(this IWebElement Element)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)WebDriver;
            Element.hoverWithJS();
            jse.ExecuteScript("arguments[0].click();", Element);
        }
        private static void hoverWithJS(this IWebElement Element)
        {
            var mouseOverScript = "if(document.createEvent){var evObj = document.createEvent('MouseEvents');"
                + "evObj.initEvent('mouseover', true, false);" 
                +"arguments[0].dispatchEvent(evObj); }"
                + "else if(document.createEventObject) { arguments[0].fireEvent('onmouseover'); }";
            ((IJavaScriptExecutor)WebDriver).ExecuteScript(mouseOverScript, Element);
        }
        public static void enableWithJS(this IWebElement Element)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript("arguments[0].style.display='block';",Element);
        }
    }
}
