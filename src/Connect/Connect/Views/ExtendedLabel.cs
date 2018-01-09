using Xamarin.Forms;

namespace Connect.Views {

    public class ExtendedLabel : Label {

        public const string FontAwesomeName = "FontAwesome";

        public ExtendedLabel() {
            FontFamily = FontAwesomeName;
        }

        public ExtendedLabel(string fontAwesomeLabel = null) {
            FontFamily = FontAwesomeName;
            Text       = fontAwesomeLabel;
        }
    }

    // https://github.com/neilkennedy/FontAwesome.Xamarin/blob/master/FontAwesome.Xamarin/FontAwesome.cs
    // For a huge list of icon codes
    public static class Icon {
        public const string FAChartBar  = "\uf080";
        public const string FAMusic     = "\uf001";
        public const string FASearch    = "\uf002";
        public const string FAEnvelopeO = "\uf003";
    }
}