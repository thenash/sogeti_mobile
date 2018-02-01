using System;

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

        public int complete {
            get;
            set;
        }

        public int enrolled {
            get;
            set;
        }

        public int early_Term {
            get;
            set;
        }

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