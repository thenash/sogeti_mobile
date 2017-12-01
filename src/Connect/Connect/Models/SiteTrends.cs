using System;
namespace Connect.Models
{
    public class SiteTrends
    {
		public string projectId
        {
            get;
            set;
        }

        public string eventType
        {
            get;
            set;
        }

        public string month
        {
            get;
            set;
        }

        public string high
        {
            get;
            set;
        }

        public string low
        {
            get;
            set;
        }

        public string ceiling
        {
            get;
            set;
        }

        public string actual
        {
            get;
            set;
        }

		


		public SiteTrends()
        {
        }
    }
}
