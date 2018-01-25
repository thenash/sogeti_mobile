namespace Connect.Models {

    public class BusinessUnitFilterItem {

        /// <summary>
        /// The name of the Business Unit.
        /// </summary>
        public int BusinessUnitId {
            get;
            set;
        }

        /// <summary>
        /// The name of the Business Unit.
        /// </summary>
        public string BusinessUnitName {
            get;
            set;
        }

        /// <summary>
        /// The Business Unit that the item belongs to.
        /// </summary>
        public string IdAndName => BusinessUnitId == -1 ? BusinessUnitName : BusinessUnitId + " - " + BusinessUnitName;

        public BusinessUnitFilterItem() { }
    }
}