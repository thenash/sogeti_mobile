using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Models;
using Xamarin.Forms;
using Connect.Helpers;

namespace Connect.ViewModels {

    public class ProjectsViewModel : BaseViewModel {

        public ProjectsViewModel() {
			Title = "Projects";
			Projects = new ObservableCollection<Project>();
        }

		/// <summary>
		/// gets or sets the feed items.
		/// </summary>
		public ObservableCollection<Project> Projects { get; private set; }

		private Command _loadCommand;
		/// <summary>
		/// Command to load/refresh projects.
		/// </summary>
		public Command LoadCommand => _loadCommand ?? (_loadCommand = new Command(async () => await ExecuteLoadCommand()));

        private Command<Project> _projectSelectedCommand;
        /// <summary>
        /// Command to handle a project being selected.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public Command<Project> ProjectSelectedCommand => _projectSelectedCommand ?? (_projectSelectedCommand = new Command<Project>(OnProjectSelectedCommand));

        private void OnProjectSelectedCommand(Project project) {
            MessagingCenter.Send(this, ConstantKeys.ProjectSelected, project);
        }

		private async Task ExecuteLoadCommand() {
		    if(IsBusy) {
		        return;
		    }

			IsBusy = true;

#if DEBUG
            Projects.Add(new Project {
                customerName = "Generic Customer",
                owningBu = "9500 Biometrics",
                phase = 4,
                primaryIndication = "Dry Eye",
                primaryTherapeuticArea = "Psychiatry",
                projectDirector = "Sally Smith",
                projectId = "1001234",
                protocolId = "9083E1-ES3"
            });

            Projects.Add(new Project {
                customerName = "Generic Customer",
                owningBu = "9500 Biometrics",
                phase = 4,
                primaryIndication = "Dry Eye",
                primaryTherapeuticArea = "Psychiatry",
                projectDirector = "Sally Smith",
                projectId = "1001234",
                protocolId = "9083E1-ES3"
            });

            Projects.Add(new Project {
                customerName = "Generic Customer",
                owningBu = "9500 Biometrics",
                phase = 4,
                primaryIndication = "Dry Eye",
                primaryTherapeuticArea = "Psychiatry",
                projectDirector = "Sally Smith",
                projectId = "1001234",
                protocolId = "9083E1-ES3"
            });

            Projects.Add(new Project {
                customerName = "Generic Customer",
                owningBu = "9500 Biometrics",
                phase = 4,
                primaryIndication = "Dry Eye",
                primaryTherapeuticArea = "Psychiatry",
                projectDirector = "Sally Smith",
                projectId = "1001234",
                protocolId = "9083E1-ES3"
            });
#endif

            try {
				string url = "https://ecs.incresearch.com/ECS/mobile/project";

				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

				HttpResponseMessage response = await client.GetAsync(url);

				if (response.IsSuccessStatusCode) {
					string content = await response.Content.ReadAsStringAsync();
					List<Project> projects = Utility.DeserializeResponse<List<Project>>(content, "data/projects/projectinfo");

                    Projects.Clear();

					foreach (Project project in projects) {
						Projects.Add(project);
					}
				}

			} catch (Exception ex) {
				ContentPage page = new ContentPage();
				await page.DisplayAlert("Error", "Unable to load projects.", "OK");
			}

			IsBusy = false;
		}
    }
}
