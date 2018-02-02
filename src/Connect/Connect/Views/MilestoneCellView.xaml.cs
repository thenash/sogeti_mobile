using System;
using System.Globalization;
using Xamarin.Forms;

namespace Connect.Views {

    public partial class MilestoneCellView : ViewCell {

        public static readonly BindableProperty ProjectCodeProperty = BindableProperty.Create(nameof(ProjectCode), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

        public string ProjectCode {
            get => (string)GetValue(ProjectCodeProperty);
            set => SetValue(ProjectCodeProperty, value);
        }

        public static readonly BindableProperty VarianceProperty = BindableProperty.Create(nameof(Variance), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

        public string Variance {
            get => (string)GetValue(VarianceProperty);
            set => SetValue(VarianceProperty, value);
        }

        public static readonly BindableProperty PlannedDateProperty = BindableProperty.Create(nameof(PlannedDate), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

        public string PlannedDate {
            get => (string)GetValue(PlannedDateProperty);
            set => SetValue(PlannedDateProperty, value);
        }

        public static readonly BindableProperty ActualDateProperty = BindableProperty.Create(nameof(ActualDate), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

        public string ActualDate {
            get => (string)GetValue(ActualDateProperty);
            set => SetValue(ActualDateProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if(propertyName == null) {
                return;
            }

            switch(propertyName) {
                case nameof(ActualDate):
                    actualDateCell.Description = ActualDate;
                    break;

                case "ProtocolId":
                    varianceCell.Description = Variance;   //TODO: Fix this
                    break;

                case nameof(Variance):
                    varianceCell.Description = Variance;
                    break;

                case nameof(PlannedDate):
                    plannedDateCell.Description = PlannedDate;
                    break;

                case nameof(ProjectCode):
                    milestoneCell.Description = ProjectCode;
                    break;
            }
        }

        public MilestoneCellView() {
            InitializeComponent();
        }
    }
}