using System;
using System.Linq;
using Connect.Helpers;
using Xamarin.Forms;
using Connect.ViewModels;
using Connect.Views;
using Rg.Plugins.Popup.Extensions;

namespace Connect.Pages {

    public partial class ProjectInfoPage : ContentPage {

        private bool _isCommentsOpen;

        private readonly ProjectInfoViewModel _viewModel;

        private readonly ProjectCommentsPopup _commentsPopup;

        public ProjectInfoPage() {

            BindingContext = _viewModel = new ProjectInfoViewModel(App.SelectedProject);

            InitializeComponent();

            _commentsPopup = new ProjectCommentsPopup();

            #region Gesture Recognizers

            ProjectCommentBackgroundBoxView.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(async () => {
                    ContactInfoBackgroundBoxView.BackgroundColor    = Color.Default;
                    ProjectCommentBackgroundBoxView.BackgroundColor = Utility.GetResource<Color>("OrangeYellow");

                    if(_isCommentsOpen) {
                        return;
                    }

                    _isCommentsOpen = true;

                    await Navigation.PushPopupAsync(_commentsPopup);    //TODO: Set project comment data
                })
            });

            ContactInfoBackgroundBoxView.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(() => {
                    ProjectCommentBackgroundBoxView.BackgroundColor = Color.Default;
                    ContactInfoBackgroundBoxView.BackgroundColor    = Utility.GetResource<Color>("OrangeYellow");
                })
            });

            #endregion
        }

        #region Event Handler Overrides

        protected override async void OnAppearing() {
            base.OnAppearing();

            OnSizeChanged(null, null);
            SizeChanged += OnSizeChanged;

            _commentsPopup.Disappearing -= OnCommentsPopupDisappearing;
            _commentsPopup.Disappearing += OnCommentsPopupDisappearing;

            if(App.LoggedIn) {
                if(_viewModel.IsInitialized) {
                    return;
                }

                _viewModel.IsInitialized = true;

                await _viewModel.ExecuteLoadProjectDetailsCommand();
                await _viewModel.ExecuteLoadMilestonesCommand();
            }
        }

        private void OnSizeChanged(object sender, EventArgs eventArgs) {
            double width  = (Width  - Width  * 0.7) / 2;
            double height = (Height - Height * 0.7) / 2;

            _commentsPopup.Padding = new Thickness(width, height);
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();

            SizeChanged -= OnSizeChanged;

            _commentsPopup.Disappearing -= OnCommentsPopupDisappearing;
        }

        #endregion

        private void OnCommentsPopupDisappearing(object sender, EventArgs eventArgs) {
            _isCommentsOpen = false;

            ProjectCommentBackgroundBoxView.BackgroundColor = Color.Default;
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

        private void OnVarianceFilterExecuteTapped(object sender, EventArgs e) {
            if(!(sender is VarianceViewCard card)) {
                return;
            }

            ResetVarianceFilterButtons();

            Device.BeginInvokeOnMainThread(() => {
                card.BackgroundColor = Utility.GetResource<Color>("SkyBlue");
            });

            _viewModel.FilterMilestonesByVariance(card.Variance);
        }

        //private void OnVarianceFilterAnimateTapped(object sender, EventArgs e) {
        //    if(!(sender is VarianceViewCard card)) {
        //        return;
        //    }

        //    Device.BeginInvokeOnMainThread(() => {
        //        card.BackgroundColor = Utility.GetResource<Color>("SkyBlue");
        //    });
        //}

        private void ResetVarianceFilterButtons() {

            //_viewModel.BackgroundColorReset = Color.Default;
            //OnPropertyChanged(nameof(_viewModel.BackgroundColorReset));//BUG: The VarianceViewCard.BackgroundColorReset binding is not working, so using this workaround instead

            MessagingCenter.Send(this, ConstantKeys.ChangeBackground);
        }
    }
}