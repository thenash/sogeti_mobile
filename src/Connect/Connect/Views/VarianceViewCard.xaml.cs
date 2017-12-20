using System;
using System.Collections.Generic;
using System.Linq;
using Connect.Models;
using Xamarin.Forms;

namespace Connect.Views {

    public enum Variances {
        White = 0,
        Red = 1,
        Yellow = 2,
        Green = 3,
        Gray = 4
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

        public Variances Variance { get; set; }

        public VarianceViewCard() {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(string propertyName = null) {

            if(propertyName == null) {
                return;
            }

            if(propertyName == nameof(BackgroundColor)) {
                VarianceCardFrame.BackgroundColor = BackgroundColor;
                return;
            }

            base.OnPropertyChanged(propertyName);

            switch(propertyName) {
                case nameof(Description):
                    varianceCardStatus.Text = (string)GetValue(DescriptionProperty);
                    break;

                case nameof(IndicatorColor):
                    statusIndicator.Color = (Color)GetValue(IndicatorColorProperty);
                    break;
            }
        }

        public static List<Milestone> GetMilestonesByVariance(Variances variance, List<Milestone> milestones) {
            switch(variance) {
                case Variances.White:
                    return milestones;

                case Variances.Red:
                    return milestones.Where(mil => mil.variance >= 15).ToList();

                case Variances.Yellow:
                    return milestones.Where(mil => mil.variance >= 1 && mil.variance <= 14).ToList();

                case Variances.Green:
                    return milestones.Where(mil => mil.variance <= 0).ToList();

                case Variances.Gray:
                    return milestones.Where(mil => mil.plannedDateTime == DateTime.MinValue || mil.actualDateTime == DateTime.MinValue).ToList();   //TODO: Find out which date needs to be check for no value and what a no value date equals

                default:
#if DEBUG
                    throw new ArgumentOutOfRangeException(nameof(variance), variance, null);
#else
                    break;
#endif
            }
        }
    }
}