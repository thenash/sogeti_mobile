using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Connect.Views
{
    public partial class VarianceViewCard : ContentView
	{
		public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create("IndicatorColor", typeof(Color), typeof(VarianceViewCard), Color.White);

		public Color IndicatorColor
		{
			get
			{
				return (Color)GetValue(IndicatorColorProperty);
			}
			set
			{
				SetValue(IndicatorColorProperty, value);
			}
		}

		public static readonly BindableProperty DescriptionProperty = BindableProperty.Create("Description", typeof(string), typeof(VarianceViewCard), default(string));

		public string Description
		{
			get
			{
				return GetValue(DescriptionProperty).ToString();
			}
			set
			{
				SetValue(DescriptionProperty, value);
			}
		}

		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged(propertyName);

			switch (propertyName)
			{
				case "Description":
					varianceCardStatus.Text = GetValue(DescriptionProperty).ToString();
					break;
				case "IndicatorColor":
                    statusIndicator.Color = (Color)GetValue(IndicatorColorProperty);
					break;
			}
		}

        public VarianceViewCard()
        {
            InitializeComponent();
        }
    }
}
