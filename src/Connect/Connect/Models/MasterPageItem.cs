using System;

namespace Connect.Models {

	public class MasterPageItem {

		public string Title { get; set; }

		public string IconSource { get; set; }

		public Type TargetType { get; set; }

		public bool IsSelected { get; set; }

		public bool IsFirst { get; set; }
	}
}