using Newtonsoft.Json;

namespace Connect.Models {

    public class Project {

        public string projectId {
            get;
            set;
        }

        public string customerName {
            get;
            set;
        }

        public string protocolId {
            get;
            set;
        }

        public string owningBu {
            get;
            set;
        }

        public int phase {
            get;
            set;
        }

        public string projectDirector {
            get;
            set;
        }

        public string primaryTherapeuticArea {
            get;
            set;
        }

        public string primaryIndication {
            get;
            set;
        }

        [JsonIgnore]
        public bool IsSelected {
            get; set;
        }

        [JsonIgnore]
        public string PhaseString => phase.ToString();

        public Project() { }
    }
}
