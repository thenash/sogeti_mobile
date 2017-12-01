using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Connect.Helpers;
using System.Collections.Generic;

namespace Connect.ViewModels
{
    public class ProjectInfoViewModel : BaseViewModel
    {
        public ProjectInfoViewModel(Project project)
		{
			this.Title = "Project Information";
            Project = project;
			Milestones = new ObservableCollection<Milestone>();

            refreshData();
		}

        private async void refreshData()
		{
			await ExecuteLoadProjectDetailsCommand();
			await ExecuteLoadMilestonesCommand();
		}

		private ProjectDetails _projectDetails;

		public ProjectDetails ProjectDetails
		{
			get { return _projectDetails; }
			set { _projectDetails = value; OnPropertyChanged("ProjectDetails"); }
		}

		private Project _project;

		public Project Project
		{
			get { return _project; }
			set { _project = value; OnPropertyChanged("Project"); }
		}

		public ObservableCollection<Milestone> Milestones
		{
			get;
			private set;
		}

		private Command loadProjectDetails;
		/// <summary>
		/// Command to load/refresh artitists
		/// </summary>
		public Command LoadProjectDetailsCommand
		{
			get { return loadProjectDetails ?? (loadProjectDetails = new Command(async () => await ExecuteLoadProjectDetailsCommand())); }
		}

		private async Task ExecuteLoadProjectDetailsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				string url = $"https://ecs.incresearch.com/ECS/mobile/projectdetails/projectId/{Project.projectId}";

				var client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

				var response = await client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					var projectDetails = Utility.DeserializeResponse<List<ProjectDetails>>(content, "data/projects/project");
                    ProjectDetails = projectDetails[0];
				}
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", "Unable to load project milestones.", "OK");
			}

			IsBusy = false;
		}

		private Command loadMilestones;
		/// <summary>
		/// Command to load/refresh artitists
		/// </summary>
        public Command LoadMilestonesCommand
		{
			get { return loadMilestones ?? (loadMilestones = new Command(async () => await ExecuteLoadMilestonesCommand())); }
		}

        private async Task ExecuteLoadMilestonesCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				string url = $"https://ecs.incresearch.com/ECS/mobile/milestones/projectId/{Project.projectId}";

				var client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

				var response = await client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var miletones = Utility.DeserializeResponse<List<Milestone>>(content, "data/project/milestone");

                    Milestones.Clear();

					foreach (var milestone in miletones)
					{
						Milestones.Add(milestone);
					}
				}

				
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", "Unable to load project milestones.", "OK");
			}

			IsBusy = false;
		}
    }
}
