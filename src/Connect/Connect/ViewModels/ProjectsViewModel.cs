using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Connect.Helpers;

namespace Connect.ViewModels
{
    public class ProjectsViewModel : BaseViewModel
    {
        public ProjectsViewModel()
		{
			this.Title = "Projects";
			Projects = new ObservableCollection<Project>();
        }

		/// <summary>
		/// gets or sets the feed items
		/// </summary>
		public ObservableCollection<Project> Projects
		{
			get;
			private set;
		}

		private Command loadCommand;
		/// <summary>
		/// Command to load/refresh artitists
		/// </summary>
		public Command LoadCommand
		{
			get { return loadCommand ?? (loadCommand = new Command(async () => await ExecuteLoadCommand())); }
		}

		private async Task ExecuteLoadCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				string url = "https://ecs.incresearch.com/ECS/mobile/project";

				var client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

				var response = await client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var projects = Utility.DeserializeResponse<System.Collections.Generic.List<Project>>(content, "data/projects/projectinfo");

                    Projects.Clear();

					foreach (var project in projects)
					{
						Projects.Add(project);
					}
				}
				
			}
			catch (Exception ex)
			{
				var page = new ContentPage();
				page.DisplayAlert("Error", "Unable to load projects.", "OK");
			}

			IsBusy = false;
		}
    }
}
