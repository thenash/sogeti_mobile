using System;
using Xamarin.Forms;
using Connect.ViewModels;
using Connect.Views;

namespace Connect.Pages {

    public partial class ProjectInfoPage : ContentPage {

        private readonly ProjectInfoViewModel _viewModel;

        public ProjectInfoPage() {

            BindingContext = _viewModel = new ProjectInfoViewModel(App.SelectedProject);

            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            if(App.LoggedIn) {
                if(_viewModel.IsInitialized) {
                    return;
                }

                _viewModel.IsInitialized = true;

                await _viewModel.ExecuteLoadProjectDetailsCommand();
                await _viewModel.ExecuteLoadMilestonesCommand();
            }
        }

        private void OnVarianceFilterTapped(object sender, EventArgs e) {
            ResetVarianceFilterButtons();

            if(sender is VarianceViewCard card) {
                card.BackgroundColor = (Color)Application.Current.Resources["SkyBlue"];

                _viewModel.FilterMilestonesByVariance(card.Variance);
            }
        }

        private void ResetVarianceFilterButtons() {
            WhiteVarianceViewCard.BackgroundColor  = Color.Default;
            RedVarianceViewCard.BackgroundColor    = Color.Default;
            YellowVarianceViewCard.BackgroundColor = Color.Default;
            GreenVarianceViewCard.BackgroundColor  = Color.Default;
            GrayVarianceViewCard.BackgroundColor   = Color.Default;
        }
    }
}