using System;
using Xamarin.Forms;

namespace Connect.Pages
{
    public partial class MainPage : MasterDetailPage {

        public MainPage() {
            Master = new MenuPage();

            Detail = new NavigationPage(new ProjectsPage { Title = "Project Selection" }) {
                Title = "Project Selection",
                Style = (Style)Application.Current.Resources["NavigationPageStyle"]
            };
		}

        protected override async void OnAppearing() {
            base.OnAppearing();

            ((MenuPage)Master).PageNavigated += OnPageNavigated;

            if(!App.LoggedIn) {
                await Navigation.PushModalAsync(new LoginPage(), true);
            }
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();

            ((MenuPage)Master).PageNavigated -= OnPageNavigated;
        }

        private void OnPageNavigated(object o, PageNavigationEventArgs e) {
            Detail      = new NavigationPage((Page)Activator.CreateInstance(e.TargetType)) { Title = e.Title, Style = (Style)Application.Current.Resources["NavigationPageStyle"] };
            IsPresented = false;
        }
    }
}