using System;
using Connect.ViewModels;
using Xamarin.Forms;

namespace Connect.Pages {

    public class BaseDetailPage<T> : MainBasePage where T : BaseViewModel, new() {

        private T _viewModel;
        protected T ViewModel {
            get => _viewModel ?? (_viewModel = new T());
            set => _viewModel = value;
        }

        public BaseDetailPage() {
            BindingContext = ViewModel;
        }

        ~BaseDetailPage() {
            _viewModel = null;
        }
    }

    public class MainBasePage : ContentPage {

        private enum NavDirection { Left = 0, Right }

        //private double _swipeDistance;
        //private const int MinSwipeDistance = 120;

        //private static readonly PanGestureRecognizer PanGesture = new PanGestureRecognizer {
        //    TouchPoints = 2
        //};

        //protected override void OnAppearing() {
        //    base.OnAppearing();

        //    PanGesture.PanUpdated -= OnPanUpdated;
        //    PanGesture.PanUpdated += OnPanUpdated;

        //    if(!Content.GestureRecognizers.Contains(PanGesture)) {
        //        Content.GestureRecognizers.Add(PanGesture);
        //    }
        //}

        //protected override void OnDisappearing() {
        //    base.OnDisappearing();

        //    PanGesture.PanUpdated -= OnPanUpdated;
        //}

        #region Event Handlers

        protected void OnLeftNavClicked(object sender, EventArgs e) => Navigate(NavDirection.Left);

        protected void OnRightNavClicked(object sender, EventArgs e) => Navigate(NavDirection.Right);

        #endregion

        #region Private Methods

        private void Navigate(NavDirection direction) {
            ContentPage navToPage = TranslateNavDirection(direction);

            if(navToPage == null) {
                return;
            }

            MessagingCenter.Send(this, ConstantKeys.SwipePage, navToPage);
        }

        private ContentPage TranslateNavDirection(NavDirection direction) {
            switch(GetType().Name) {
                case nameof(ProjectsPage):
                    if(direction == NavDirection.Left) {
                        return null;
                    } else {
                        return new ProjectInfoPage();
                    }

                case nameof(ProjectInfoPage):
                    if(direction == NavDirection.Left) {
                        return new ProjectsPage();
                    } else {
                        return new SitesPage();
                    }

                case nameof(SitesPage):
                    if(direction == NavDirection.Left) {
                        return new ProjectInfoPage();
                    } else {
                        return new SubjectsPage();
                    }

                case nameof(SubjectsPage):
                    if(direction == NavDirection.Left) {
                        return new SitesPage();
                    } else {
                        return new MonitoringPage();
                    }

                case nameof(MonitoringPage):
                    if(direction == NavDirection.Left) {
                        return new SubjectsPage();
                    } else {
                        return null;
                    }

                default:
#if DEBUG
                    throw new Exception("Unknown page type");
#else
                    return null;
#endif
            }
        }

        #endregion
    }
}
