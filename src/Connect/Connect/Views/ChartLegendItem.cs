using Connect.Helpers;
using Xamarin.Forms;

namespace Connect.Views {

    public class ChartLegendItem : StackLayout {

        private readonly Label   _label;
        private readonly BoxView _boxView;

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(ButtonLabelContentView), string.Empty);

        public string LabelText {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public static readonly BindableProperty LabelTextSizeProperty = BindableProperty.Create(nameof(LabelTextSize), typeof(NamedSize), typeof(ButtonLabelContentView), NamedSize.Small);

        public NamedSize LabelTextSize {
            get => (NamedSize)GetValue(LabelTextSizeProperty);
            set => SetValue(LabelTextSizeProperty, value);
        }

        public static readonly BindableProperty ItemColorProperty = BindableProperty.Create(nameof(ItemColor), typeof(Color), typeof(ButtonLabelContentView), Color.Default);

        public Color ItemColor {
            get => (Color)GetValue(ItemColorProperty);
            set => SetValue(ItemColorProperty, value);
        }

        public ChartLegendItem() {

            Orientation = StackOrientation.Horizontal;
            Padding     = new Thickness(10,5,0,5);

            _label = new Label {
                TextColor             = Utility.GetResource<Color>("DarkGray"),
                FontSize              = Device.GetNamedSize(LabelTextSize, typeof(Label)),
                VerticalTextAlignment = TextAlignment.Center
            };

            _boxView = new BoxView {
                WidthRequest    = 20,
                HeightRequest   = 10,
                VerticalOptions = LayoutOptions.Center
            };

            Children.Add(_boxView);
            Children.Add(_label);
        }

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if(propertyName == null) {
                return;
            }

            switch(propertyName) {
                case nameof(LabelText):
                    _label.Text = LabelText;
                    break;

                case nameof(LabelTextSize):
                    _label.FontSize = Device.GetNamedSize(LabelTextSize, typeof(Label));
                    break;

                case nameof(ItemColor):
                    _boxView.BackgroundColor = ItemColor;
                    break;
            }
        }
    }
}