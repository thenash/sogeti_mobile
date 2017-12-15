using System;
using Connect.Helpers;

namespace Connect.Models {

    public class SiteStats {

        public string projectId {
            get;
            set;
        }

        public string isoDate {
            get;
            set;
        }

        public DateTime isoDateTime { get; set; }

        [ChartXAxis(0, "Selected")]
        public int selected {
            get;
            set;
        }

        [ChartXAxis(1, "Activated")]
        public int activated {
            get;
            set;
        }

        [ChartXAxis(2, "Enrolling")]
        public int enrolling {
            get;
            set;
        }

        public int nonEnrolling {
            get;
            set;
        }

        [ChartXAxis(3, "Dormant")]
        public int inactive {
            get;
            set;
        }

        [ChartXAxis(4, "Closed")]
        public int closed {
            get;
            set;
        }

        public int siv {
            get;
            set;
        }

        public int pssv {
            get;
            set;
        }


        public SiteStats() { }
    }
}
