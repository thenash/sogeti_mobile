using Connect.Interfaces;
using Xamarin.Forms;

namespace Connect.Services {

    public class NavigationService : INavigationService {

        /// <inheritdoc />
        public void Push(Page page) {
            App.NavPage.MenuPageItem.Navigate(page);
        }
    }
}
