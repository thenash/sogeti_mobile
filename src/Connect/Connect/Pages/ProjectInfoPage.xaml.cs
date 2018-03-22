using System;
using System.Collections.Generic;
using System.Linq;
using Connect.Helpers;
using Xamarin.Forms;
using Connect.ViewModels;
using Connect.Views;
using Microsoft.AppCenter.Analytics;
using Rg.Plugins.Popup.Extensions;

namespace Connect.Pages {

    public partial class ProjectInfoPage : ProjectInfoPageXaml {

        private bool _isCommentsOpen;
        private bool _isContactOpen;

        private readonly ProjectCommentsPopup _commentsPopup;
        private readonly ContactInfoPopup     _contactInfoPopup;

        public ProjectInfoPage() {

            ViewModel.Project = App.SelectedProject;

            InitializeComponent();

            _commentsPopup    = new ProjectCommentsPopup();
            _contactInfoPopup = new ContactInfoPopup();

            #region Gesture Recognizers

            ProjectCommentBackgroundBoxView.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(async () => {
                    Analytics.TrackEvent("Button Clicked", new Dictionary<string, string> {
                        { "Page", nameof(ProjectInfoPage) },
                        { "Button", "ProjectCommentButton"}
                    });

                    ContactInfoBackgroundBoxView.BackgroundColor    = Color.Default;
                    ProjectCommentBackgroundBoxView.BackgroundColor = Utility.GetResource<Color>("Orange");

                    if(_isCommentsOpen) {
                        OnCommentsPopupDisappearing(null, null);
                        return;
                    }

                    _isCommentsOpen = true;

                    //_commentsPopup.Comments = //TODO: Set project comment data

                    _commentsPopup.ProjectCode  = ViewModel.Project.projectId;
                    _commentsPopup.CustomerName = ViewModel.Project.customerName;
                    _commentsPopup.ProtocolId   = ViewModel.Project.protocolId;

                    await Navigation.PushPopupAsync(_commentsPopup);
                })
            });

            ContactInfoBackgroundBoxView.GestureRecognizers.Add(new TapGestureRecognizer {
                Command = new Command(async () => {
                    Analytics.TrackEvent("Button Clicked", new Dictionary<string, string> {
                        { "Page", nameof(ProjectInfoPage) },
                        { "Button", "ContactInfoButton"}
                    });

                    ProjectCommentBackgroundBoxView.BackgroundColor = Color.Default;
                    ContactInfoBackgroundBoxView.BackgroundColor    = Utility.GetResource<Color>("Orange");

                    if(_isContactOpen) {
                        OnContactInfoPopupDisappearing(null, null);
                        return;
                    }

                    if(ViewModel.Contacts.IsNullOrEmpty()) {
                        await DisplayAlert(string.Empty, "No contact data available.", "OK");
                        OnContactInfoPopupDisappearing(null, null);
                        return;
                    }

                    _isContactOpen = true;

                    _contactInfoPopup.Contacts     = ViewModel.Contacts;
                    _contactInfoPopup.ProjectCode  = ViewModel.Project.projectId;
                    _contactInfoPopup.CustomerName = ViewModel.Project.customerName;
                    _contactInfoPopup.ProtocolId   = ViewModel.Project.protocolId;

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
                if(ViewModel.IsInitialized) {
                    return;
                }

                ViewModel.IsInitialized = true;

                await ViewModel.ExecuteLoadProjectDetailsCommand();
                await ViewModel.ExecuteLoadMilestonesCommand();
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
            ViewModel.ExecuteShowMoreMilestones();

            if(ViewModel.DisplayMilestones != null && ViewModel.DisplayMilestones.Count > 0) {
                MilestoneListView.ScrollTo(ViewModel.DisplayMilestones.Last(), ScrollToPosition.End, true);
            }
        }

        private void OnShowLessTapped(object sender, EventArgs e) {
            ViewModel.ExecuteShowLessMilestones();

            if(ViewModel.DisplayMilestones != null && ViewModel.DisplayMilestones.Count > 0) {
                MilestoneListView.ScrollTo(ViewModel.DisplayMilestones.Last(), ScrollToPosition.End, true);
            }
        }

        private void OnVarianceFilterExecuteTapped(object sender, EventArgs e) {
            if(!(sender is VarianceViewCard card)) {
                return;
            }

            Analytics.TrackEvent("Button Clicked", new Dictionary<string, string> {
                { "Page", nameof(ProjectsPage) },
                { "Button", "Variance" + Enum.GetName(typeof(Variances), card.Variance) + "Button"}
            });

            ResetVarianceFilterButtons();

            //Device.BeginInvokeOnMainThread(() => {
            //    card.BackgroundColor = Utility.GetResource<Color>("LightBlue");
            //    card.ForceLayout();
            //});

            ViewModel.FilterMilestonesByVariance(card.Variance);
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

            //ViewModel.BackgroundColorReset = Color.Default;
            //OnPropertyChanged(nameof(ViewModel.BackgroundColorReset));//BUG: The VarianceViewCard.BackgroundColorReset binding is not working, so using the workaround below instead

            MessagingCenter.Send(this, ConstantKeys.ChangeBackground);
        }
    }

    public class ProjectInfoPageXaml : BaseDetailPage<ProjectInfoViewModel> { }
}