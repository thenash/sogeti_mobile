using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Connect.Views
{
    public partial class LegendValueCell : ContentView
    {
        public Color LegendColor {
            get { return legendColor.BackgroundColor; }
            set { legendColor.BackgroundColor = value; }
        }

        public string LegendLabel {
            get { return legendTitle.Text; }
            set { legendTitle.Text = value; }
        }

        public LegendValueCell()
        {
            InitializeComponent();
        }
    }
}
