using KiewitTeamBinder.UI.Pages.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Threading;
using static KiewitTeamBinder.UI.ExtentReportsHelper;
using OpenQA.Selenium.Interactions;

namespace KiewitTeamBinder.UI.Pages
{
    public class AgodaResults : LoggedInLanding
    {
        #region Locators
        private By _hotel(string name) => By.XPath($"//h3[contains(text(),'{name}')]");
        private By _filterButton(string filter) => By.XPath($"//button[@class='btn PillDropdown__Button']//span[contains(text(),'{filter}')]");
        private By _leftSlider => By.XPath("//div[@class='rc-slider-handle rc-slider-handle-1']");
        private By _rightSlider => By.XPath("//div[@class='rc-slider-handle rc-slider-handle-2']");
        public IWebElement Hotel(string name) => StableFindElement(_hotel(name));
        public IWebElement FilterButton(string filter) => StableFindElement(_filterButton(filter));
        public IWebElement LeftSlider { get { return StableFindElement(_leftSlider); } }
        public IWebElement RightSlider { get { return StableFindElement(_rightSlider); } }
        #endregion

        #region Element
        #endregion

        #region Methods
        public AgodaBookingRoom SelectHotel(string name)
        {
            var node = CreateStepNode();
            node.Info("Select Hotel");
            IJavaScriptExecutor jse = (IJavaScriptExecutor)WebDriver;
            var hieght = jse.ExecuteScript("return document.body.scrollHeight");
            var scrollLocate = 600;
            //jse.ExecuteScript($"window.scrollTo(0, {scrollLocate})");
            while (FindElement(_hotel(name), 1) == null)
            {
                jse.ExecuteScript($"window.scrollTo(0, {scrollLocate})");
                scrollLocate += 300;
            }
            Hotel(name).Click();
            //Thread.Sleep(3000);
            EndStepNode(node);
            return new AgodaBookingRoom(WebDriver);
            //ScrollIntoView(Hotel(name));
        }
        
        public AgodaResults SelectPriceRange(string filter, double leftPercent = 0, double rightPercent = 0)
        {
            FilterButton(filter).Click();
            WaitForElement(_rightSlider);
            Actions move = new Actions(WebDriver);
            if (leftPercent != 0)
            {
                int leftPoint = Convert.ToInt32(leftPercent * 2.245);
                move.DragAndDropToOffset(LeftSlider, leftPoint, 0).Build().Perform();            
            }
            if (rightPercent != 0)
            {
                int rightPoint = Convert.ToInt32((rightPercent - 100) * 2.245);
                move.DragAndDropToOffset(RightSlider, rightPoint, 0).Build().Perform();
            }
          
            return this;
        }
        #endregion
        public AgodaResults(IWebDriver webDriver) : base(webDriver)
        {

        }
    }
}
