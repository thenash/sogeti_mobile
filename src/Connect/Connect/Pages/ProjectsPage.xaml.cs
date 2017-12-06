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

            projectsList.ItemSelected += async (sender, e) => {
                if(projectsList.SelectedItem == null) {
                    return;
                }

                await Navigation.PushAsync(new ProjectInfoPage(projectsList.SelectedItem as Project));

                projectsList.SelectedItem = null;
            };
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            if(App.LoggedIn) {
                if(viewModel.IsInitialized) {
                    return;
                }

                viewModel.IsInitialized = true;
                viewModel.LoadCommand?.Execute(null);
            }
        }
    }
}
