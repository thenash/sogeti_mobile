using System;
using Connect.Helpers;

namespace Connect.Models {

    public class SubjectStats {

        public string projectId {
            get;
            set;
        }

        public DateTime isoDateTime {
            get;
            set;
        }

        public string isoDate {
            get;
            set;
        }

        [ChartXAxis(3, "Complete")]
        public int complete {
            get;
            set;
        }

        [ChartXAxis(1, "Enrolled")]
        public int enrolled {
            get;
            set;
        }

        [ChartXAxis(2, "Early Term")]
        public int early_Term {
            get;
            set;
        }

        [ChartXAxis(0, "Screened")]
        public int screened {
            get;
            set;
        }

        public int screenFail {
            get;
            set;
        }

        public int safetyPd {
            get;
            set;
        }

        public int safetySae {
            get;
            set;
        }

        public SubjectStats() { }
    }
}