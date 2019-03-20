using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models
{
    public class TAPage
    {
        public string PageName { get; set; }
        public string ParentPage { get; set; }
        public int NumberOfColumns { get; set; }
        public string DisplayAfter { get; set; }
        public bool IsPublic { get; set; }
    }
}
