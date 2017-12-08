using Xamarin.Forms;

namespace Connect.Interfaces {

    /// <summary>
    /// Handles in app navigation.
    /// </summary>
    public interface INavigationService {

        /// <summary>
        /// Changes the master detail's Detail page.
        /// </summary>
        /// <param name="page">The page to show.</param>
        void Push(Page page);
    }
}
