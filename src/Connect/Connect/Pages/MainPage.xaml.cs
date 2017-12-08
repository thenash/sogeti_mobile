using Xamarin.Forms;

namespace Connect.Pages
{
    public partial class MainPage : MasterDetailPage {

        public MenuPage MenuPageItem => (MenuPage)Master;

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

        public void SetDetailPage(Page page) {
            Detail = new NavigationPage(page) {
                Title = page.Title,
                Style = (Style)Application.Current.Resources["NavigationPageStyle"]
            };

            IsPresented = false;
        }

        private void OnPageNavigated(object o, PageNavigationEventArgs e) {
            SetDetailPage(e.PageItem);
        }
    }
}