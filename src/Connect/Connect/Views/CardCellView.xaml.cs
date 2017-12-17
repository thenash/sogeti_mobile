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
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public LayoutOptions TextAlignment {
            set {
                CellTitle.HorizontalOptions       = value;
                CellDescription.HorizontalOptions = value;
            }
        }

        /// <summary>
        /// The <see cref="LabelFontSize"/> property for the label text.
        /// </summary>
        public static readonly BindableProperty LabelFontSizeProperty = BindableProperty.Create(nameof(LabelFontSize), typeof(NamedSize), typeof(CardCellView), App.IsPhone ? NamedSize.Micro : NamedSize.Small);

        /// <summary>
        /// Gets or sets the <see cref="Label.FontSize"/>.
        /// </summary>
        public NamedSize LabelFontSize {
            get => (NamedSize)GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        /// <summary>
        /// The <see cref="DescriptionFontAttributes"/> property.
        /// </summary>
        public static readonly BindableProperty DescriptionFontAttributesProperty = BindableProperty.Create(nameof(DescriptionFontAttributes), typeof(FontAttributes), typeof(CardCellView), FontAttributes.Bold);

        /// <summary>
        /// Gets or sets the <see cref="Label.FontAttributes"/> for the description text.
        /// </summary>
        public FontAttributes DescriptionFontAttributes {
            get => (FontAttributes)GetValue(DescriptionFontAttributesProperty);
            set => SetValue(DescriptionFontAttributesProperty, value);
        }

        public CardCellView() {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if(propertyName == null) {
                return;
            }

            switch(propertyName) {
                case nameof(Description):
                    CellDescription.Text = (string)GetValue(DescriptionProperty);
                    break;

                case nameof(LabelFontSize):
                    CellTitle.FontSize = Device.GetNamedSize(LabelFontSize, typeof(Label));
                    break;

                case nameof(DescriptionFontAttributes):
                    CellDescription.FontAttributes = DescriptionFontAttributes;
                    break;
            }
        }
    }
}