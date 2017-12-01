using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Connect.Views
{
    public partial class MilestoneCellView : ViewCell
	{
		public static readonly BindableProperty MilestoneProperty = BindableProperty.Create(nameof(ProjectCode), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

		public string ProjectCode
		{
			get { return GetValue(MilestoneProperty).ToString(); }
			set { SetValue(MilestoneProperty, value); }
		}

		public static readonly BindableProperty VarianceProperty = BindableProperty.Create(nameof(Variance), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

		public string Variance
		{
			get { return GetValue(VarianceProperty).ToString(); }
			set { SetValue(VarianceProperty, value); }
		}

		public static readonly BindableProperty PlannedDateProperty = BindableProperty.Create(nameof(PlannedDate), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

        public string PlannedDate
		{
			get { return GetValue(PlannedDateProperty).ToString(); }
			set { SetValue(PlannedDateProperty, value); }
		}

        public static readonly BindableProperty ActualDateProperty = BindableProperty.Create(nameof(ActualDate), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

        public string ActualDate
		{
			get { return GetValue(ActualDateProperty).ToString(); }
			set { SetValue(ActualDateProperty, value); }
		}

		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged(propertyName);

			switch (propertyName)
			{
				case "ActualDate":
					actualDateCell.Description = GetValue(ActualDateProperty).ToString();
					break;
				case "ProtocolId":
					varianceCell.Description = GetValue(VarianceProperty).ToString();
					break;
				case "PlannedDate":
					plannedDateCell.Description = GetValue(PlannedDateProperty).ToString();
					break;
				case "Milestone":
					milestoneCell.Description = GetValue(MilestoneProperty).ToString();
					break;
			}
		}

        public MilestoneCellView()
        {
            InitializeComponent();
        }
    }
}
