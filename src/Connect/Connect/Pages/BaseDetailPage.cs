using Connect.ViewModels;
using Xamarin.Forms;

namespace Connect.Pages {

    public class BaseDetailPage<T> : ContentPage where T : BaseViewModel, new() {

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
}
