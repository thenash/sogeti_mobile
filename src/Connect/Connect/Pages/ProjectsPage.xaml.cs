using System.Collections.Generic;
using System.Net.Http;
using Connect.Models;
using Xamarin.Forms;
using Connect.ViewModels;

namespace Connect.Pages {
    public partial class ProjectsPage : ContentPage {
        HttpClient client;
        List<Milestone> projects;

        ProjectsViewModel viewModel;

        public ProjectsPage() {

            BindingContext = viewModel = new ProjectsViewModel();

            InitializeComponent();
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            if(App.LoggedIn) {
                if(viewModel.IsInitialized) {
                    return;
                }

                viewModel.IsInitialized = true;
                viewModel.LoadCommand?.Execute(null);
            }
        }

        private void OnProjectSelected(object sender, SelectedItemChangedEventArgs e) {
            projectsList.SelectedItem = null;
        }
    }
}
