using Xamarin.Forms;

namespace Connect.Views {

    public partial class MilestoneCellView : ViewCell {

        public static readonly BindableProperty ProjectCodeProperty = BindableProperty.Create(nameof(ProjectCode), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

        public string ProjectCode {
            get => GetValue(ProjectCodeProperty).ToString();
            set => SetValue(ProjectCodeProperty, value);
        }

        public static readonly BindableProperty VarianceProperty = BindableProperty.Create(nameof(Variance), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

        public string Variance {
            get => GetValue(VarianceProperty).ToString();
            set => SetValue(VarianceProperty, value);
        }

        public static readonly BindableProperty PlannedDateProperty = BindableProperty.Create(nameof(PlannedDate), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

        public string PlannedDate {
            get => GetValue(PlannedDateProperty).ToString();
            set => SetValue(PlannedDateProperty, value);
        }

        public static readonly BindableProperty ActualDateProperty = BindableProperty.Create(nameof(ActualDate), typeof(string), typeof(MilestoneCellView), string.Empty, BindingMode.TwoWay);

        public string ActualDate {
            get => GetValue(ActualDateProperty).ToString();
            set => SetValue(ActualDateProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName) {
            base.OnPropertyChanged(propertyName);

            switch(propertyName) {
                case nameof(ActualDate):
                    actualDateCell.Description = GetValue(ActualDateProperty).ToString();
                    break;

                case "ProtocolId":
                    varianceCell.Description = GetValue(VarianceProperty).ToString();   //TODO: Fix this
                    break;

                case nameof(Variance):
                    varianceCell.Description = GetValue(VarianceProperty).ToString();
                    break;

                case nameof(PlannedDate):
                    plannedDateCell.Description = GetValue(PlannedDateProperty).ToString();
                    break;

                case nameof(ProjectCode):
                    milestoneCell.Description = GetValue(ProjectCodeProperty).ToString();
                    break;
            }
        }

        public MilestoneCellView() {
            InitializeComponent();
        }
    }
}
