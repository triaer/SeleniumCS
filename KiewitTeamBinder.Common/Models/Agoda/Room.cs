using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models.Agoda
{
    public class Room
    {
        public bool IsFreeBreakfast { get; set; }
        public bool IsFreeCancellation { get; set; }
        public bool IsNonSmoking { get; set; }
        public bool IsTwinBed { get; set; }
        public string RoomType { get; set; }
        public int RoomPosition { get; set; }
        public T FilterValue { get; set; }

    }
}
