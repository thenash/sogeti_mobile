using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Helpers;
using Connect.Models;
using Xamarin.Forms;
using Connect.ViewModels;

namespace Connect.Pages
{
    public partial class ProjectInfoPage : ContentPage
    {
        HttpClient client;
        private string _projectID;
        List<Milestone> _miletones;
        List<ProjectDetails> _projectDetails;

        ProjectInfoViewModel viewModel;

        public ProjectInfoPage() : this(new Project()) {}

        public ProjectInfoPage(Project project)
        {
			InitializeComponent();

			this.BindingContext = viewModel = new ProjectInfoViewModel(project);

            if(project != null)
                _projectID = project.projectId;
        }

		//protected override async void OnAppearing()
		//{
		//	base.OnAppearing();

  //  //        milestonesList.ItemsSource = new List<Milestone>()
  //  //        {
		//		//new Milestone() { milestoneName = "Milestone 1", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" },
		//		//new Milestone() { milestoneName = "Milestone 2", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" }, 
		//		//new Milestone() { milestoneName = "Milestone 3", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" }, 
		//		//new Milestone() { milestoneName = "Milestone 4", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" }
  //          //};

		//	if (App.LoggedIn)
		//	{
  //              if (!string.IsNullOrEmpty((_projectID)))
  //              {
  //                  //await GetMilestones();
  //                  //await GetProjectDetails();

  //                  milestonesList.ItemsSource = _miletones;
  //              }
		//	}
		//}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (App.LoggedIn)
			{
				if (viewModel.IsInitialized)
					return;

				viewModel.IsInitialized = true;
			}
		}

		async Task GetMilestones()
		{
            string url = $"https://ecs.incresearch.com/ECS/mobile/milestones/projectId/{_projectID}";

			client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
                _miletones = Utility.DeserializeResponse<List<Milestone>>(content, "data/project/milestone");
			}
		}

		async Task GetProjectDetails()
		{
			string url = $"https://ecs.incresearch.com/ECS/mobile/projectdetails/projectId/{_projectID}";

			client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

                _projectDetails = Utility.DeserializeResponse<List<ProjectDetails>>(content, "data/projects/project");
			}

		}
    }
}
