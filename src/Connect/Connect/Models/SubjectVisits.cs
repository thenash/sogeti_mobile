using System;
using Newtonsoft.Json;

namespace Connect.Models {

    public class SubjectVisits {

        [JsonProperty("siteId")]
        public string SiteId {
            get;
            set;
        }

        [JsonProperty("piName")]
        public string PiName {
            get;
            set;
        }

        [JsonProperty("siteName")]
        public string SiteName {
            get;
            set;
        }

        [JsonProperty("countryId")]
        public string CountryId {
            get;
            set;
        }

        [JsonProperty("visitType")]
        public string VisitType {
            get;
            set;
        }

        [JsonProperty("monitor")]
        public string Monitor {
            get;
            set;
        }

        [JsonProperty("visitStartDate")]
        public DateTime VisitStartDate {
            get;
            set;
        }

        [JsonProperty("visitEndDate")]
        public DateTime VisitEndDate {
            get;
            set;
        }

        [JsonProperty("reportCreationDate")]
        public DateTime ReportCreationDate {
            get;
            set;
        }

        [JsonProperty("reportCompletionDate")]
        public DateTime ReportCompletionDate {
            get;
            set;
        }

        [JsonProperty("dataSource")]
        public string DataSource {
            get;
            set;
        }

        [JsonProperty("projectId")]
        public string ProjectId {
            get;
            set;
        }

        [JsonProperty("visitId")]
        public string VisitId {
            get;
            set;
        }

        public SubjectVisits() { }
    }
}