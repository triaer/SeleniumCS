using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.Common.Models
{
    public class ReportPanel
    {
        public string Type { get; set; }
        public string DataProfile { get; set; }
        public string DisplayName { get; set; }   
    }

    public class ChartPanel
    {
        public string Type { get; set; }
        public string DataProfile { get; set; }
        public string DisplayName { get; set; }
        public string ChartTitle { get; set; }
        public string ChartType { get; set; }
        public string Category { get; set; }
        public string Series { get; set; }
        public bool ShowTitle { get; set; }
        public string Style { get; set; }
        public string Legends { get; set; }
        public string[] DataLabels { get; set; }
    }

    public class IndicatorPanel
    {
        public string Type { get; set; }
        public string DataProfile { get; set; }
        public string DisplayName { get; set; }
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string StatisticField { get; set; }
        public string Value { get; set; }
        public bool Percentage { get; set; }
        public float[] From { get; set; }
        public string[] Color { get; set; }
    }

    public class HeatMapPanel
    {
        public string Type { get; set; }
        public string DataProfile { get; set; }
        public string DisplayName { get; set; }
        public string Title { get; set; }
        public bool ShowTitle { get; set; }
        public string Category { get; set; }
        public string Series { get; set; }
        public string SeriesValue { get; set; }
        public bool SetAsHeatValue { get; set; }
        public string Color { get; set; }
        public string Legends { get; set; }
    }
}
