using System;
namespace Connect.Models
{
    public class ProjectDetails
    {
		public string projectId
        {
            get;
            set;
        }
       
        public string projectName
        {
            get;
            set;
        }

        public string projectDescription
        {
            get;
            set;
        }

        public string customerName
        {
            get;
            set;
        }

        public string protocolId
        {
            get;
            set;
        }

        public string protocolDesc
        {
            get;
            set;
        }

        public string phase
        {
            get;
            set;
        }

        public string projectDirector
        {
            get;
            set;
        }

        public string directorEmail
        {
            get;
            set;
        }

        public string directorPhone
        {
            get;
            set;
        }

        public string projectLead
        {
            get;
            set;
        }

        public string leadEmail
        {
            get;
            set;
        }

        public string leadPhone
        {
            get;
            set;
        }

        public string leadDataManager
        {
            get;
            set;
        }

        public string leadDmEmail
        {
            get;
            set;
        }

        public string leadDmPhone
        {
            get;
            set;
        }

        public string projectLifecycleStage
        {
            get;
            set;
        }

        public string projectStartDate
        {
            get;
            set;
        }

        public string projectEndDate
        {
            get;
            set;
        }

        public string primaryTherapeuticArea
        {
            get;
            set;
        }

        public string primaryIndication
        {
            get;
            set;
        }

        public string owningBu
        {
            get;
            set;
        }

        public int totalDirectBudgetAmt
        {
            get;
            set;
        }

        public int totalIndirectBudgetAmt
        {
            get;
            set;
        }


        public ProjectDetails()
        {
        }
    }
}
