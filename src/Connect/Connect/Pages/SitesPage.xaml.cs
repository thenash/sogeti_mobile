using Connect.Models;
using Connect.ViewModels;
using Xamarin.Forms;

namespace Connect.Pages
{
    public partial class SitesPage : ContentPage
    {
        SitesViewModel _viewModel;
        private string _projectID = "Project 1";


        public SitesPage() : this(null) {}

        public SitesPage(Milestone project)
        {
            InitializeComponent();

            BindingContext = _viewModel = new SitesViewModel(_projectID);

            if(project != null)
                _projectID = project.projectId;
        }

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (App.LoggedIn)
			{
				//if (_viewModel.IsInitialized)
				//	return;

				//_viewModel.IsInitialized = true;
				//_viewModel.LoadCommand.Execute(null);
			}
		}

    }



}
