using Newtonsoft.Json;

namespace Connect.Models {

    public class VisitMetrics {

        [JsonProperty("projectId")]
        public string ProjectId {
            get;
            set;
        }

        [JsonProperty("numSites")]
        public int NumSites {
            get;
            set;
        }

        [JsonProperty("numVisits")]
        public int NumVisits {
            get;
            set;
        }

        [JsonProperty("reportsCompleted")]
        public int ReportsCompleted {
            get;
            set;
        }

        [JsonProperty("compliance")]
        public string Compliance {
            get;
            set;
        }

        [JsonProperty("eventType")]
        public string EventType {
            get;
            set;
        }

        public VisitMetrics() { }
    }
}