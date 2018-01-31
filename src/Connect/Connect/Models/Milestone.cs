using System;
using System.Collections.Generic;
using System.Linq;
using Connect.Views;

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
            get;
            set;
        }

        public string plannedDate {
            get;
            set;
        }

        public DateTime actualDateTime {
            get;
            set;
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

        public int variance { get; set; }

        public Milestone() { }

        public static IEnumerable<Milestone> GetMilestonesByVariance(Variances variance, IEnumerable<Milestone> milestones) {
            switch(variance) {
                case Variances.White:
                    return milestones.ToList();

                case Variances.Red:
                    return milestones.Where(mil => mil.variance >= 15).ToList();

                case Variances.Yellow:
                    return milestones.Where(mil => mil.variance >= 1 && mil.variance <= 14).ToList();

                case Variances.Green:
                    return milestones.Where(mil => mil.variance <= 0).ToList();

                case Variances.Gray:
                    return milestones.Where(mil => mil.plannedDateTime == DateTime.MinValue || mil.actualDateTime == DateTime.MinValue).ToList();   //TODO: Find out which date needs to be check for no value and what a no value date equals

                default:
#if DEBUG
                    throw new ArgumentOutOfRangeException(nameof(variance), variance, null);
#else
                    return null;
#endif
            }
        }
    }
}