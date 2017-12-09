using Xamarin.Forms;
using Connect.ViewModels;

namespace Connect.Pages {

    public partial class ProjectsPage : ContentPage {

        //HttpClient client;
        //List<Milestone> projects;

        private readonly ProjectsViewModel _viewModel;

        public ProjectsPage() {

            BindingContext = _viewModel = new ProjectsViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            if(App.LoggedIn) {
                if(_viewModel.IsInitialized) {
                    return;
                }

                _viewModel.IsInitialized = true;
                LoadProjects();
            }
        }

        public void LoadProjects() {
            _viewModel.LoadCommand?.Execute(null);
        }

        private void OnProjectSelected(object sender, SelectedItemChangedEventArgs e) {
            projectsList.SelectedItem = null;
        }
    }
}