using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common
{
    public class DigikeyEnum
    {
        public enum Location
        {
            [Description("United States")]
            USA,
            [Description("Vietnam")]
            VietNam
        }
    }
}
