using Xamarin.Forms;

namespace Connect.Views {

    public partial class CardCellView : ContentView {

        public Color BackgroundColor {
            get => containerGrid.BackgroundColor;
            set => containerGrid.BackgroundColor = value;
        }

        public string Title {
            get => cellTitle.Text;
            set => cellTitle.Text = value;
        }

        public Color TitleColor {
            get => cellTitle.TextColor;
            set => cellTitle.TextColor = value;
        }

        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(CardCellView), default(string));

        public string Description {
            get => GetValue(DescriptionProperty).ToString();
            set => SetValue(DescriptionProperty, value);
        }

        public LayoutOptions TextAlignment {
            set {
                cellTitle.HorizontalOptions = value;
                cellDescription.HorizontalOptions = value;
            }
        }

        protected override void OnPropertyChanged(string propertyName) {
            base.OnPropertyChanged(propertyName);

            switch(propertyName) {
                case nameof(Description):
                    cellDescription.Text = GetValue(DescriptionProperty).ToString();
                    break;
            }
        }

        public CardCellView() {
            InitializeComponent();
        }
    }
}
