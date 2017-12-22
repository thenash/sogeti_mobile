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
            if(!(sender is VarianceViewCard card)) {
                return;
            }

            ResetVarianceFilterButtons();

            Device.BeginInvokeOnMainThread(() => {
                card.BackgroundColor = (Color)Application.Current.Resources["SkyBlue"];
            });

            _viewModel.FilterMilestonesByVariance(card.Variance);
        }

        private void ResetVarianceFilterButtons() {

            //_viewModel.BackgroundColorReset = Color.Default;
            //OnPropertyChanged(nameof(_viewModel.BackgroundColorReset));//BUG: The VarianceViewCard.BackgroundColorReset binding is not working, so using this workaround instead

            MessagingCenter.Send(this, ConstantKeys.ChangeBackground);
        }
    }
}