using System;
namespace Connect.Models
{
    public class VisitMetrics
    {
		public string projectId
        {
            get;
            set;
        }

        public int numSites
        {
            get;
            set;
        }

        public int numVisits
        {
            get;
            set;
        }

        public int reportsCompleted
        {
            get;
            set;
        }

        public string compliance
        {
            get;
            set;
        }

        public string eventType
        {
            get;
            set;
        }


		public VisitMetrics()
        {
        }
    }
}
