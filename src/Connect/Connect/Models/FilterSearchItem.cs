namespace Connect.Models {

    public class FilterSearchItem {

        /// <summary>
        /// The Protocol Id to filter on (either this or <see cref="ProjectId"/> should have a value at any one time, not both).
        /// </summary>
        public string ProtocolId {
            get;
            set;
        }

        /// <summary>
        /// The Project Id to filter on (either this or <see cref="ProtocolId"/> should have a value at any one time, not both).
        /// </summary>
        public string ProjectId {
            get;
            set;
        }

        /// <summary>
        /// The Project Id to filter on (either this or <see cref="ProtocolId"/> should have a value at any one time, not both).
        /// </summary>
        public string CustomerName {
            get;
            set;
        }

        /// <summary>
        /// The Business Unit that the item belongs to.
        /// </summary>
        public int BusinessUnitId {
            get;
            set;
        }

        /// <summary>
        /// The display name of the item.
        /// </summary>
        public string ItemText => (string.IsNullOrEmpty(ProjectId) ? ProtocolId : ProjectId) + " - " + CustomerName;

        public FilterSearchItem() { }
    }
}