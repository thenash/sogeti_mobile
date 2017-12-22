using Connect.Pages;
using Xamarin.Forms;

namespace Connect.Views {

    public enum Variances {
        White  = 0,
        Red    = 1,
        Yellow = 2,
        Green  = 3,
        Gray   = 4
    }

    public partial class VarianceViewCard : ContentView {

        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(VarianceViewCard), Color.White);

        public Color IndicatorColor {
            get => (Color)GetValue(IndicatorColorProperty);
            set => SetValue(IndicatorColorProperty, value);
        }

        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(VarianceViewCard), default(string));

        public string Description {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        ///// <summary>
        ///// Gets or sets the color to override the background with.
        ///// </summary>
        ///// <value>The color to override with.</value>
        //public Color BackgroundColorReset {   //BUG: The VarianceViewCard.BackgroundColorReset binding is not working, so using this workaround instead
        //    get => (Color)GetValue(BackgroundColorResetProperty);
        //    set => SetValue(BackgroundColorResetProperty, value);
        //}

        ///// <summary>
        ///// The background override color property    //BUG: The VarianceViewCard.BackgroundColorReset binding is not working, so using this workaround instead
        ///// </summary>
        //public static readonly BindableProperty BackgroundColorResetProperty = BindableProperty.Create(nameof(BackgroundColorReset), typeof(Color), typeof(VarianceViewCard), Color.Default, BindingMode.TwoWay);

        public Variances Variance { get; set; }

        public VarianceViewCard() {
            InitializeComponent();
        }

        protected override void OnParentSet() {
            base.OnParentSet();

            if(Parent == null) {
                MessagingCenter.Unsubscribe<ProjectInfoPage>(this, ConstantKeys.ChangeBackground);
            } else {
                MessagingCenter.Unsubscribe<ProjectInfoPage>(this, ConstantKeys.ChangeBackground);
                MessagingCenter.Subscribe<ProjectInfoPage>(this, ConstantKeys.ChangeBackground, page => Device.BeginInvokeOnMainThread(() => BackgroundColor = Color.Default));
            }
        }

        protected override void OnPropertyChanged(string propertyName = null) {

            if(propertyName == null) {
                return;
            }

            if(propertyName == nameof(BackgroundColor)) {
                VarianceCardFrame.BackgroundColor = BackgroundColor;
                return;
            }

            //if(propertyName == nameof(BackgroundColorReset)) {    //BUG: The VarianceViewCard.BackgroundColorReset binding is not working, so using this workaround instead
            //    VarianceCardFrame.BackgroundColor = BackgroundColorReset;
            //    return;
            //}

            base.OnPropertyChanged(propertyName);

            switch(propertyName) {
                case nameof(Description):
                    VarianceCardStatus.Text = (string)GetValue(DescriptionProperty);
                    break;

                case nameof(IndicatorColor):
                    StatusIndicator.Color = (Color)GetValue(IndicatorColorProperty);
                    break;
            }
        }
    }
}