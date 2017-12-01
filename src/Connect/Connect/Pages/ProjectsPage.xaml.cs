using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Connect.Models;
using Connect.Helpers;
using Xamarin.Forms;
using Connect.ViewModels;

namespace Connect.Pages
{
    public partial class ProjectsPage : ContentPage
    {
        HttpClient client;
        List<Milestone> projects;

        ProjectsViewModel viewModel;

        public ProjectsPage()
        {
			InitializeComponent();

            this.BindingContext = viewModel = new ProjectsViewModel();

            projectsList.ItemSelected += (sender, e) => {
				if (projectsList.SelectedItem == null)
					return;

				Navigation.PushAsync(new ProjectInfoPage(projectsList.SelectedItem as Project));

				projectsList.SelectedItem = null;
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (App.LoggedIn)
			{
				if (viewModel.IsInitialized)
					return;

			      viewModel.IsInitialized = true;
			      viewModel.LoadCommand.Execute(null);
            }
        }
    }
}
