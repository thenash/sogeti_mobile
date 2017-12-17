using Xamarin.Forms;
using Connect.ViewModels;

namespace Connect.Pages {

    public partial class ProjectInfoPage : ContentPage {

        private readonly ProjectInfoViewModel _viewModel;

        public ProjectInfoPage() {

            BindingContext = _viewModel = new ProjectInfoViewModel(App.SelectedProject);

            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            if(App.LoggedIn) {
                if(_viewModel.IsInitialized) {
                    return;
                }

                _viewModel.IsInitialized = true;

                await _viewModel.ExecuteLoadProjectDetailsCommand();
                await _viewModel.ExecuteLoadMilestonesCommand();
            }
        }
    }
}