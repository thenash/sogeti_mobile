using Xamarin.Forms;

namespace Connect.Views {

    public partial class CardCellView : ContentView {

        public new Color BackgroundColor {
            get => ContainerGrid.BackgroundColor;
            set => ContainerGrid.BackgroundColor = value;
        }

        public string Title {
            get => CellTitle.Text;
            set => CellTitle.Text = value;
        }

        public Color TitleColor {
            get => CellTitle.TextColor;
            set => CellTitle.TextColor = value;
        }

        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(CardCellView), default(string));

        public string Description {
            get => GetValue(DescriptionProperty).ToString();
            set => SetValue(DescriptionProperty, value);
        }

        public LayoutOptions TextAlignment {
            set {
                CellTitle.HorizontalOptions = value;
                CellDescription.HorizontalOptions = value;
            }
        }

        public CardCellView() {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(string propertyName) {
            base.OnPropertyChanged(propertyName);

            switch(propertyName) {
                case nameof(Description):
                    CellDescription.Text = GetValue(DescriptionProperty).ToString();
                    break;
            }
        }
    }
}