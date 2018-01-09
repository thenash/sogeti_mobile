using System;
using Connect.Helpers;
using Connect.ViewModels;
using Xamarin.Forms;

namespace Connect.Pages {

    public partial class SitesPage : ContentPage {

        private readonly SitesViewModel _viewModel;

        private readonly string _projectId;

        public SitesPage() : this(App.SelectedProject?.projectId) { }

        public SitesPage(string projectId) {

            BindingContext = _viewModel = new SitesViewModel(_projectId);

            InitializeComponent();

            if(projectId != null) {
                _projectId = projectId;
            }

            double microAmount        = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
            double smallToMicroAmount = Device.GetNamedSize(NamedSize.Small, typeof(Label)) - microAmount;

            double finalAxisFontSize = App.IsAndroid ? microAmount : microAmount - smallToMicroAmount;

            SiteStatusHorizontalAxis.LabelFontSize = finalAxisFontSize;
            SiteStatusVerticalAxis.LabelFontSize   = finalAxisFontSize;

            if(!App.IsAndroid && finalAxisFontSize > 9.8 && finalAxisFontSize <= 11) {    //BUG: Telerik Charts will not appear to overlap properly if the LabelFontSize is between these values, they have been notified about the issue
                finalAxisFontSize = 9.8;
            }

            BottomChartHorizontalAxis.LabelFontSize = finalAxisFontSize;
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            SizeChanged += OnSizeChanged;

            if(App.LoggedIn) {
                if(_viewModel.IsInitialized) {
                    return;
                }

                _viewModel.IsInitialized = true;

                await _viewModel.RefreshData(_projectId);
                InitGrid();
            }
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();

            SizeChanged -= OnSizeChanged;
        }

        private void OnSizeChanged(object sender, EventArgs eventArgs) {

            if(App.IsAndroid) { //BUG: On Android, the chart height is not being calculated correctly

                if(TopChart.Height < 250) {
                    TopChart.HeightRequest = 250;
                }

                if(BottomChart.Height < 250) {
                    BottomChart.HeightRequest = 250;
                }
            }
        }

        private void OnGridViewTapped(object sender, EventArgs e) {
            ChartViewButtonFrame.BackgroundColor = Utility.GetResource<Color>("Gray");
            GridViewButtonFrame.BackgroundColor  = Color.White;

            PlannedBottomLegendStackLayout.IsVisible = false;
            ActualBottomLegendStackLayout.IsVisible  = false;

            BottomChart.IsVisible = false;
            BottomGrid.IsVisible  = true;

            //TODO: Animate Grid
        }

        private void OnChartViewTapped(object sender, EventArgs e) {
            GridViewButtonFrame.BackgroundColor  = Utility.GetResource<Color>("Gray");
            ChartViewButtonFrame.BackgroundColor = Color.White;

            PlannedBottomLegendStackLayout.IsVisible = true;
            ActualBottomLegendStackLayout.IsVisible  = true;

            BottomGrid.IsVisible  = false;
            BottomChart.IsVisible = true;

            //TODO: Animate Chart
        }

        private void InitGrid() {
            BottomGrid.Children.Clear();

            BottomGrid.RowDefinitions = new RowDefinitionCollection {
                new RowDefinition {             //Create header row
                    Height = GridLength.Auto
                }, new RowDefinition {          //Create header separator row
                    Height = 1
                }
            };

            int plannedCount = _viewModel.PlannedBottomChartSiteStats.Count;

            for(int i = 0; i < plannedCount; i++) {
                BottomGrid.RowDefinitions.Add(new RowDefinition {
                    Height = GridLength.Auto
                });

                if(i != 0 && i != plannedCount - 1) {
                    BottomGrid.RowDefinitions.Add(new RowDefinition {   //Create separator rows
                        Height = GridLength.Auto
                    });
                }
            }

            //BottomGrid.RowDefinitions.Add(new RowDefinition {   //HACK: Prevents the Grid background color from being cut off to soon
            //    Height = 10
            //});

            double size = Device.GetNamedSize(NamedSize.Micro, typeof(Label));
            const double margin = 3;

            Color darkGray  = Utility.GetResource<Color>("DarkGray");
            Color lightGray = Utility.GetResource<Color>("LightGray");

            Style horizontalSeparatorStyle = Utility.GetResource<Style>("HorizontalSeparatorStyle");
            Style vertSeparatorStyle       = Utility.GetResource<Style>("VerticalSeparatorStyle");

            BottomGrid.Children.Add(new Label {
                Margin                = margin,
                Text                  = "Status",
                TextColor             = darkGray,
                FontSize              = size,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 0);

            BottomGrid.Children.Add(new BoxView {
                Style           = vertSeparatorStyle,
                BackgroundColor = darkGray
            }, 1, 0);

            BottomGrid.Children.Add(new Label {
                Margin                = margin,
                Text                  = "Planned to Date",
                TextColor             = darkGray,
                FontSize              = size,
                VerticalTextAlignment = TextAlignment.Center
            }, 2, 0);

            BottomGrid.Children.Add(new BoxView {
                Style           = vertSeparatorStyle,
                BackgroundColor = darkGray
            }, 3, 0);

            BottomGrid.Children.Add(new Label {
                Margin                = margin,
                Text                  = "Actual to Date",
                TextColor             = darkGray,
                FontSize              = size,
                VerticalTextAlignment = TextAlignment.Center
            }, 4, 0);

            BottomGrid.Children.Add(new BoxView {
                Style           = vertSeparatorStyle,
                BackgroundColor = darkGray
            }, 5, 0);

            BottomGrid.Children.Add(new Label {
                Margin                = margin,
                Text                  = "Total Contracted",
                TextColor             = darkGray,
                FontSize              = size,
                VerticalTextAlignment = TextAlignment.Center
            }, 6, 0);

            BottomGrid.Children.Add(new BoxView {
                Style           = horizontalSeparatorStyle,
                BackgroundColor = darkGray
            }, 0, 7, 1, 2);

            int rowSeparatorCount = 0;

            for(int index = 0; index < plannedCount; index++) {     //Create headers
                int separatorRow = index + rowSeparatorCount + 2;   //Add 2 for the header row and the header separator row

                Color backgroundColor = index % 2 == 0 ? Color.White : lightGray;

                #region Label Column

                BottomGrid.Children.Add(new Label {
                    Text                  = "  " + _viewModel.PlannedBottomChartSiteStats[index].Group,
                    TextColor             = darkGray,
                    FontSize              = size,
                    BackgroundColor       = backgroundColor,
                    VerticalTextAlignment = TextAlignment.Center
                }, 0, separatorRow);

                BottomGrid.Children.Add(new BoxView {
                    Style           = vertSeparatorStyle,
                    BackgroundColor = darkGray
                }, 1, separatorRow);

                if(index != plannedCount - 1) {
                    BottomGrid.Children.Add(new BoxView {
                        Style           = horizontalSeparatorStyle,
                        BackgroundColor = darkGray
                    }, 0, 7, separatorRow + 1, separatorRow + 2);

                    rowSeparatorCount++;
                }

                #endregion

                #region Planned To Date Column

                BottomGrid.Children.Add(new Label {
                    Text                    = _viewModel.PlannedBottomChartSiteStats[index].Value.ToString(),
                    TextColor               = darkGray,
                    FontSize                = size,
                    BackgroundColor         = backgroundColor,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment   = TextAlignment.Center
                }, 2, separatorRow);

                BottomGrid.Children.Add(new BoxView {
                    Style           = vertSeparatorStyle,
                    BackgroundColor = darkGray
                }, 3, separatorRow);

                #endregion

                #region Actual To Date Column

                BottomGrid.Children.Add(new Label {
                    Text                    = _viewModel.ActualBottomChartSiteStats[index].Value.ToString(),
                    TextColor               = darkGray,
                    FontSize                = size,
                    BackgroundColor         = backgroundColor,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment   = TextAlignment.Center
                }, 4, separatorRow);

                BottomGrid.Children.Add(new BoxView {
                    Style           = vertSeparatorStyle,
                    BackgroundColor = darkGray
                }, 5, separatorRow);

                #endregion

                #region Total Contracted Column

                BottomGrid.Children.Add(new Label {
                    Text                    = _viewModel.TotalBottomChartSiteStats[index].Value.ToString(),
                    TextColor               = darkGray,
                    FontSize                = size,
                    BackgroundColor         = backgroundColor,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment   = TextAlignment.Center
                }, 6, separatorRow);

                #endregion
            }

            BottomGrid.ForceLayout();
        }
    }
}