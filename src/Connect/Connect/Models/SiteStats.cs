using System;
namespace Connect.Models
{
    public class SiteStats
    {
        public string projectId
        {
            get;
            set;
        }
		
        public string isoDate
        {
            get;
            set;
        }

        public DateTime isoDateTime
        {
            get{
                return Convert.ToDateTime(isoDate);
            }
            set{}
        }

        public int selected
        {
            get;
            set;
        }

        public int activated
        {
            get;
            set;
        }

        public int enrolling
        {
            get;
            set;
        }

        public int nonEnrolling
        {
            get;
            set;
        }

        public int inactive
        {
            get;
            set;
        }

        public int closed
        {
            get;
            set;
        }

        public int siv
        {
            get;
            set;
        }

        public int pssv
        {
            get;
            set;
        }


		public SiteStats()
        {
        }
    }
}
