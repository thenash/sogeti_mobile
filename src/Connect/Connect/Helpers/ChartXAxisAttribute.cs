using System;

namespace Connect.Helpers {

    /// <summary>
    /// When found, marks the property name as being available to be put in the X-axis of the chart.
    /// </summary>
    public class ChartXAxisAttribute : Attribute {

        /// <summary>
        /// 0-based index for ordering the various properties.
        /// </summary>
        public int Priority {
            get; set;
        }

        public string DisplayName {
            get; set;
        }

        public ChartXAxisAttribute(int priority, string displayName) {
            Priority    = priority;
            DisplayName = displayName;
        }
    }
}