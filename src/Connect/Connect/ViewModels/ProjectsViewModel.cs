using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Connect.Models;
using Xamarin.Forms;

namespace Connect.ViewModels {

    public class ProjectsViewModel : BaseViewModel {

        public ProjectsViewModel() {
			Title    = "Project Selection";
			Projects = new ObservableCollection<Project>();
        }

        private ObservableCollection<Project> _projects;
        /// <summary>
        /// gets or sets the feed items.
        /// </summary>
        public ObservableCollection<Project> Projects {
            get => _projects;
            private set {
                if(_projects != value) {
                    _projects = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ProjectCount));
                }
            }
        }

        public int ProjectCount => Projects.Count;

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

            Project selectedProject = Projects.FirstOrDefault(proj => proj.IsSelected);

            if(selectedProject != null) {
                selectedProject.IsSelected = false;
            }

            Project projectToSelect = Projects.FirstOrDefault(proj => proj.projectId == project.projectId && proj.protocolId == project.protocolId);

            if(projectToSelect != null) {
                projectToSelect.IsSelected = true;
            }

            App.SelectedProject = projectToSelect;

            OnPropertyChanged(nameof(Projects));
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
                protocolId = "9083-E1-ES3"
            });

            Projects.Add(new Project {
                customerName = "Generic Customer",
                owningBu = "2000 Oncology",
                phase = 2,
                primaryIndication = "Hodgkin Lymphoma",
                primaryTherapeuticArea = "Oncology",
                projectDirector = "Lisa Jones",
                projectId = "1000249",
                protocolId = "7710-TM89-Y0"
            });

            Projects.Add(new Project {
                customerName = "Generic Customer",
                owningBu = "4000 General Medicine",
                phase = 4,
                primaryIndication = "Acne",
                primaryTherapeuticArea = "Dermatology",
                projectDirector = "James White",
                projectId = "200008",
                protocolId = "5404-Y5TT-U1"
            });

            Projects.Add(new Project {
                customerName = "Generic Customer",
                owningBu = "1000 Central Nervous System (CNS)",
                phase = 4,
                primaryIndication = "Eyelid Spasms",
                primaryTherapeuticArea = "Neurology",
                projectDirector = "Tom Johnson",
                projectId = "0963801",
                protocolId = "8008-1GG6-P3"
            });
#endif
            OnPropertyChanged(nameof(Projects));
		    OnPropertyChanged(nameof(ProjectCount));

            //         try {
            //	string url = "https://ecs.incresearch.com/ECS/mobile/project";

            //	HttpClient client = new HttpClient();
            //	client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

            //	HttpResponseMessage response = await client.GetAsync(url);

            //	if (response.IsSuccessStatusCode) {
            //		string content = await response.Content.ReadAsStringAsync();
            //		List<Project> projects = Utility.DeserializeResponse<List<Project>>(content, "data/projects/projectinfo");

            //                 Projects.Clear();

            //		foreach (Project project in projects) {
            //			Projects.Add(project);
            //		}
            //	}

            //} catch (Exception ex) {
            //	ContentPage page = new ContentPage();
            //	await page.DisplayAlert("Error", "Unable to load projects.", "OK");
            //}

		    if(App.SelectedProject != null) {
		        Project selectedProject = Projects.FirstOrDefault(proj => proj.projectId == App.SelectedProject.projectId && proj.protocolId == App.SelectedProject.protocolId); //TODO: Ensure this is a good way to determine project equality (are protocolId and/or projectId always unique?)

		        if(selectedProject != null) {
		            selectedProject.IsSelected = true;
		        }

		        Projects = new ObservableCollection<Project>(Projects);
            }

            IsBusy = false;
		}
    }
}
