using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiewitTeamBinder.API.Models
{
    public class Value
    {
        public int ProjectId { get; set; }
        public int OrganizationId { get; set; }
        public string ProjectDisplay { get; set; }
        public string ProjectName { get; set; }
        public string SourceSystemId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string City { get; set; }
        public string CountryISOCode { get; set; }
        public string RegionISOCode { get; set; }
        public object ZipCode { get; set; }
        public object Address1 { get; set; }
        public object Address2 { get; set; }
        public object Latitude { get; set; }
        public object Longitude { get; set; }
        public object Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProjectStatusName { get; set; }
        public string OrganizationPath { get; set; }
        public string CreatedByFullName { get; set; }
        public string ModifiedByFullName { get; set; }
        public string TimeZoneName { get; set; }
    }

    public class ProjectsResponse
    {
        [JsonProperty(PropertyName = "@odata.context")]
        public string Context { get; set; }

        [JsonProperty(PropertyName = "@odata.count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "value")]
        public IList<Value> Value { get; set; }
    }
}
