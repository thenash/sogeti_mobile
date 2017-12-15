using System;
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
    }
}