using Xamarin.Forms;

namespace Connect.Views {

    public partial class VarianceViewCard : ContentView {

        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(VarianceViewCard), Color.White);

        public Color IndicatorColor {
            get => (Color)GetValue(IndicatorColorProperty);
            set => SetValue(IndicatorColorProperty, value);
        }

        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(VarianceViewCard), default(string));

        public string Description {
            get => GetValue(DescriptionProperty).ToString();
            set => SetValue(DescriptionProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName) {
            base.OnPropertyChanged(propertyName);

            switch(propertyName) {
                case nameof(Description):
                    varianceCardStatus.Text = GetValue(DescriptionProperty).ToString();
                    break;
                case nameof(IndicatorColor):
                    statusIndicator.Color = (Color)GetValue(IndicatorColorProperty);
                    break;
            }
        }

        public VarianceViewCard() {
            InitializeComponent();
        }
    }
}