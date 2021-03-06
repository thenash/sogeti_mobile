﻿using System;
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

        public DateTime? plannedDateTime { get; set; }

        public DateTime? plannedDate {
            get;
            set;
        }

        public DateTime? actualDateTime { get; set; }

        public DateTime? actualDate {
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
                    return milestones.Where(mil => mil.status.Equals("r", StringComparison.OrdinalIgnoreCase)).ToList();

                case Variances.Yellow:
                    return milestones.Where(mil => mil.status.Equals("a", StringComparison.OrdinalIgnoreCase)).ToList();

                case Variances.Green:
                    return milestones.Where(mil => mil.status.Equals("g", StringComparison.OrdinalIgnoreCase)).ToList();

                case Variances.Gray:
                    return milestones.Where(mil => mil.status.Equals("u", StringComparison.OrdinalIgnoreCase)).ToList();

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