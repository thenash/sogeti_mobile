using Xamarin.Forms;

namespace Connect.Views {

    public partial class LegendValueCell : ContentView {

        public Color LegendColor {
            get => legendColor.BackgroundColor;
            set => legendColor.BackgroundColor = value;
        }

        public string LegendLabel {
            get => legendTitle.Text;
            set => legendTitle.Text = value;
        }

        public LegendValueCell() {
            InitializeComponent();
        }
    }
}
