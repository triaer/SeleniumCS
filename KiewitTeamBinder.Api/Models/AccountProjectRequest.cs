using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API.Models
{
    public class AccountProjectRequest
    {
        public int AccountId { get; set; }
        public int ProjectId { get; set; }
    }
}
