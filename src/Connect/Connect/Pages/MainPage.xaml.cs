using System;
using Connect.Helpers;
using Xamarin.Forms;

namespace Connect.Pages {

    public partial class MainPage : MasterDetailPage {

        #region Properties

        public MenuPage MenuPageItem => (MenuPage)Master;

        private readonly LoginPage _login;

        #endregion

        #region Constructors

        public MainPage() {
            Master = new MenuPage();

            Detail = new NavigationPage(new ProjectsPage { Title = "Project Selection" }) {
                Title = "Project Selection",
                Style = Utility.GetResource<Style>("NavigationPageStyle")
            };

            _login = new LoginPage();
        }

        #endregion

        #region Event Handler Overrides

        protected override async void OnAppearing() {
            base.OnAppearing();

            ((MenuPage)Master).PageNavigated += OnPageNavigated;

            if(!App.LoggedIn) {

                if(App.IsAndroid && ((NavigationPage)Detail).CurrentPage is ProjectsPage) {
                    _login.Disappearing += OnLoginDisappearing; //BUG: On Android, OnAppearing() does not get called when login page goes away because it is presented as a modal
                }

                await Navigation.PushModalAsync(_login, true);
            }
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            ((MenuPage)Master).PageNavigated -= OnPageNavigated;
        }

        #endregion

        #region Event Handlers

        private void OnPageNavigated(object o, PageNavigationEventArgs e) {
            SetDetailPage(e.PageItem);
        }

        private void OnLoginDisappearing(object sender, EventArgs eventArgs) {
            if(App.IsAndroid) {
                _login.Disappearing -= OnLoginDisappearing;

                if(((NavigationPage)Detail).CurrentPage is ProjectsPage projPage) {
                    projPage.LoadProjects();
                }
            }
        }

        #endregion

        #region Private Methods

        private void SetDetailPage(Page page) {
            Detail = new NavigationPage(page) {
                Title = page.Title,
                Style = Utility.GetResource<Style>("NavigationPageStyle")
            };

            IsPresented = false;
        }

        #endregion
    }
}