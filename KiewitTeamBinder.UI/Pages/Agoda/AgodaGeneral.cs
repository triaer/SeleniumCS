using KiewitTeamBinder.UI.Pages.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace KiewitTeamBinder.UI.Pages.Agoda
{
    public class AgodaGeneral : LoggedInLanding
    {
        public AgodaGeneral(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}
