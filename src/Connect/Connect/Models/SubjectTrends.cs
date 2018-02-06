using System;

namespace Connect.Models {

    public class SubjectTrends {

        public string projectId {
            get;
            set;
        }

        public string month {
            get;
            set;
        }

        public string eventType {
            get;
            set;
        }

        public string high {
            get;
            set;
        }

        public string low {
            get;
            set;
        }

        public string ceiling {
            get;
            set;
        }

        public string actual {
            get;
            set;
        }

        public DateTime MonthDateTime {
            get {
                if(DateTime.TryParse(month, out DateTime dt)) {
                    return dt;
                }

                return DateTime.MinValue;
            }
        }

        public int HighInt {
            get {
                if(int.TryParse(high, out int h)) {
                    return h;
                }

                return 0;
            }
        }

        public int ActualInt {
            get {
                if(int.TryParse(actual, out int act)) {
                    return act;
                }

                return 0;
            }
        }

        public int CeilingInt {
            get {
                if(int.TryParse(ceiling, out int ceil)) {
                    return ceil;
                }

                return 0;
            }
        }

        public SubjectTrends() { }
    }
}