using System;

namespace Connect.Models {

    public class Milestone {

        public string projectId {
            get;
            set;
        }

        public string milestoneName {
            get;
            set;
        }

        public DateTime plannedDateTime {
            get {
                return Convert.ToDateTime(plannedDate);
            }
            set {
            }
        }

        public string plannedDate {
            get;
            set;
        }

        public DateTime actualDateTime {
            get {
                return Convert.ToDateTime(actualDate);
            }
            set { }
        }

        public string actualDate {
            get;
            set;
        }

        public string status {
            get;
            set;
        }

        public int sortIndex {
            get;
            set;
        }

        public Milestone() { }
    }
}