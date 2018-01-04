using System;
using System.Linq;
using Connect.Helpers;
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

        private void OnShowMoreTapped(object sender, EventArgs e) {
            _viewModel.ExecuteShowMoreMilestones();

            if(_viewModel.DisplayMilestones != null && _viewModel.DisplayMilestones.Count > 0) {
                MilestoneListView.ScrollTo(_viewModel.DisplayMilestones.Last(), ScrollToPosition.End, true);
            }
        }

        private void OnShowLessTapped(object sender, EventArgs e) {
            _viewModel.ExecuteShowLessMilestones();

            if(_viewModel.DisplayMilestones != null && _viewModel.DisplayMilestones.Count > 0) {
                MilestoneListView.ScrollTo(_viewModel.DisplayMilestones.Last(), ScrollToPosition.End, true);
            }
        }

        private void OnProjectCommentExecuteTapped(object sender, EventArgs e) {

        }

        private void OnProjectCommentAnimateTapped(object sender, EventArgs e) {
            ContactInfoBackgroundBoxView.BackgroundColor    = Color.Default;
            ProjectCommentBackgroundBoxView.BackgroundColor = Utility.GetResource<Color>("OrangeYellow");
        }

        private void OnContactInfoExecuteTapped(object sender, EventArgs e) {

        }

        private void OnContactInfoAnimateTapped(object sender, EventArgs e) {
            ProjectCommentBackgroundBoxView.BackgroundColor = Color.Default;
            ContactInfoBackgroundBoxView.BackgroundColor    = Utility.GetResource<Color>("OrangeYellow");
        }

        private void OnVarianceFilterExecuteTapped(object sender, EventArgs e) {
            if(!(sender is VarianceViewCard card)) {
                return;
            }

            ResetVarianceFilterButtons();

            _viewModel.FilterMilestonesByVariance(card.Variance);
        }

        private void OnVarianceFilterAnimateTapped(object sender, EventArgs e) {
            if(!(sender is VarianceViewCard card)) {
                return;
            }

            Device.BeginInvokeOnMainThread(() => {
                card.BackgroundColor = Utility.GetResource<Color>("SkyBlue");
            });
        }

        private void ResetVarianceFilterButtons() {

            //_viewModel.BackgroundColorReset = Color.Default;
            //OnPropertyChanged(nameof(_viewModel.BackgroundColorReset));//BUG: The VarianceViewCard.BackgroundColorReset binding is not working, so using this workaround instead

            MessagingCenter.Send(this, ConstantKeys.ChangeBackground);
        }
    }
}