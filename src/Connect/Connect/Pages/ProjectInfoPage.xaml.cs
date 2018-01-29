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
        private bool _isContactOpen;

        private readonly ProjectInfoViewModel _viewModel;

        private readonly ProjectCommentsPopup _commentsPopup;
        private readonly ContactInfoPopup     _contactInfoPopup;

        public ProjectInfoPage() {

            BindingContext = _viewModel = new ProjectInfoViewModel(App.SelectedProject);

            InitializeComponent();

            _commentsPopup    = new ProjectCommentsPopup();
            _contactInfoPopup = new ContactInfoPopup();

            #region Gesture Recognizers

            ProjectCommentBackgroundBoxView.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(async () => {
                    ContactInfoBackgroundBoxView.BackgroundColor    = Color.Default;
                    ProjectCommentBackgroundBoxView.BackgroundColor = Utility.GetResource<Color>("OrangeYellow");

                    if(_isCommentsOpen) {
                        OnCommentsPopupDisappearing(null, null);
                        return;
                    }

                    _isCommentsOpen = true;

                    //_commentsPopup.Comments = //TODO: Set project comment data

                    _commentsPopup.ProjectCode  = _viewModel.Project.projectId;
                    _commentsPopup.CustomerName = _viewModel.Project.customerName;
                    _commentsPopup.ProtocolId   = _viewModel.Project.protocolId;

                    await Navigation.PushPopupAsync(_commentsPopup);
                })
            });

            ContactInfoBackgroundBoxView.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(async () => {
                    ProjectCommentBackgroundBoxView.BackgroundColor = Color.Default;
                    ContactInfoBackgroundBoxView.BackgroundColor    = Utility.GetResource<Color>("OrangeYellow");

                    if(_isContactOpen) {
                        OnContactInfoPopupDisappearing(null, null);
                        return;
                    }

                    if(_viewModel.Contacts.IsNullOrEmpty()) {
                        await DisplayAlert("", "No contact data available.", "OK");
                        OnContactInfoPopupDisappearing(null, null);
                        return;
                    }

                    _isContactOpen = true;

                    _contactInfoPopup.Contacts     = _viewModel.Contacts;
                    _contactInfoPopup.ProjectCode  = _viewModel.Project.projectId;
                    _contactInfoPopup.CustomerName = _viewModel.Project.customerName;
                    _contactInfoPopup.ProtocolId   = _viewModel.Project.protocolId;

                    await Navigation.PushPopupAsync(_contactInfoPopup);
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

            _contactInfoPopup.Disappearing -= OnContactInfoPopupDisappearing;
            _contactInfoPopup.Disappearing += OnContactInfoPopupDisappearing;

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
            double height = (Height - Height * 0.8) / 2;

            _commentsPopup.Padding    = new Thickness(0, height);
            _contactInfoPopup.Padding = new Thickness(0, height);
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();

            SizeChanged -= OnSizeChanged;

            _commentsPopup.Disappearing    -= OnCommentsPopupDisappearing;
            _contactInfoPopup.Disappearing -= OnContactInfoPopupDisappearing;
        }

        #endregion

        private void OnCommentsPopupDisappearing(object sender, EventArgs eventArgs) {
            _isCommentsOpen = false;

            ProjectCommentBackgroundBoxView.BackgroundColor = Color.Default;
        }

        private void OnContactInfoPopupDisappearing(object sender, EventArgs eventArgs) {
            _isContactOpen = false;

            ContactInfoBackgroundBoxView.BackgroundColor = Color.Default;
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
                card.ForceLayout();
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
            //OnPropertyChanged(nameof(_viewModel.BackgroundColorReset));//BUG: The VarianceViewCard.BackgroundColorReset binding is not working, so using the workaround below instead

            MessagingCenter.Send(this, ConstantKeys.ChangeBackground);
        }
    }
}