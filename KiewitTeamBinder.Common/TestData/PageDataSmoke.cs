using KiewitTeamBinder.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiewitTeamBinder.Common.DashBoardENums;
using KiewitTeamBinder.Common.Helper;

namespace KiewitTeamBinder.Common.TestData
{
    public class PageDataSmoke
    {
        public string pageName = "Test";
        public string anotherPageName = "Another Test";
        public string subPageName = "Test Child";
        public string OverviewPage = "Overview";
        public string confirmDeletePageMessage = "Are you sure you want to delete this page?";
        public string warningHasChildPageMessage = "Can not delete page 'Test' since it has children page(s)";

        public TAPage taPage1 = new TAPage()
        {
            PageName = "Test",
            ParentPage = null,
            NumberOfColumns = 2,
            DisplayAfter = null,
            IsPublic = false,
        };

        public TAPage taPage2 = new TAPage()
        {
            PageName = "Another Test",
            ParentPage = null,
            NumberOfColumns = 2,
            DisplayAfter = null,
            IsPublic = true,
        };

        public TAPage taPage1Edited = new TAPage()
        {
            PageName = "Test",
            ParentPage = null,
            NumberOfColumns = 2,
            DisplayAfter = null,
            IsPublic = true,
        };

        public TAPage taPage2Edited = new TAPage()
        {
            PageName = "Another Test",
            ParentPage = null,
            NumberOfColumns = 2,
            DisplayAfter = null,
            IsPublic = false,
        };

        public TAPage taPage2DisplayAfter = new TAPage()
        {
            PageName = "Another Test",
            ParentPage = null,
            NumberOfColumns = 2,
            DisplayAfter = "Test",
            IsPublic = true,
        };

        public TAPage taChildPage = new TAPage()
        {
            PageName = "Test Child",
            ParentPage = "Test",
            NumberOfColumns = 2,
            DisplayAfter = null,
            IsPublic = false,
        };
    }
}
