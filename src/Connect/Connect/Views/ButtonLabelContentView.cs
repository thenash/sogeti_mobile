using System;
using Connect.Helpers;
using Xamarin.Forms;

namespace Connect.Views {
    public class ButtonLabelContentView : ContentView {

        private TapGestureRecognizer _tapGesture;

        private readonly Label _label;
        private readonly Frame _frame;

        public event EventHandler<EventArgs> Tapped;

        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(ButtonLabelContentView), string.Empty);

        public string LabelText {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public new static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(ButtonLabelContentView), 0D);

        public new double HeightRequest {
            get => (double)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        public new static readonly BindableProperty WidthRequestProperty = BindableProperty.Create(nameof(WidthRequest), typeof(double), typeof(ButtonLabelContentView), 0D);

        public new double WidthRequest {
            get => (double)GetValue(WidthRequestProperty);
            set => SetValue(WidthRequestProperty, value);
        }

        public ButtonLabelContentView() {

            _label = new Label {
                TextColor               = Color.White,
                FontAttributes          = FontAttributes.Bold,
                FontSize                = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment   = TextAlignment.Center,
                InputTransparent        = false,
                Style                   = Utility.GetResource<Style>("BaseLabelStyle")
            };

            _frame = new Frame {
                HasShadow         = false,
                CornerRadius      = 2,
                Padding           = 0,
                BackgroundColor   = Utility.GetResource<Color>("DarkBlue"),
                HorizontalOptions = LayoutOptions.Center,
                Content           = _label
            };

            Content = _frame;
        }

        protected override void OnParentSet() {
            base.OnParentSet();

            if(Parent == null) {
                _tapGesture.Tapped -= OnTapped;

                _frame.GestureRecognizers.Remove(_tapGesture);
            } else {
                _tapGesture = new TapGestureRecognizer();
                _tapGesture.Tapped += OnTapped;

                _frame.GestureRecognizers.Add(_tapGesture);
            }
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

                case nameof(WidthRequest):
                    _frame.WidthRequest = WidthRequest;
                    break;

                case nameof(HeightRequest):
                    _frame.HeightRequest = HeightRequest;
                    break;
            }
        }

        private void OnTapped(object sender, EventArgs eventArgs) {
            Tapped?.Invoke(sender, eventArgs);
        }
    }
}