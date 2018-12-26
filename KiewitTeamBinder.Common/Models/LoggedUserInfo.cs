using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models
{
    public class LoggedUserInfo
    {
        public class User
        {
            public string UserName { get; set; }
            public string CompanyName { get; set; }
            public string Description { get; set; }
        }
    }
}
