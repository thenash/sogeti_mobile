using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Models;
using Xamarin.Forms;
using Connect.Helpers;
using System.Collections.Generic;

namespace Connect.ViewModels {

    public class ProjectInfoViewModel : BaseViewModel {

        public ProjectInfoViewModel(Project project) {
            Title      = "Project Information";
            Project    = project;
            Milestones = new ObservableCollection<Milestone>();

            RefreshData();
        }

        private async void RefreshData() {
            //await ExecuteLoadProjectDetailsCommand();
            //await ExecuteLoadMilestonesCommand();
        }

        private ProjectDetails _projectDetails;

        public ProjectDetails ProjectDetails {
            get => _projectDetails;
            set {
                _projectDetails = value;
                OnPropertyChanged();
            }
        }

        private Project _project;

        public Project Project {
            get => _project;
            set {
                _project = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Milestone> Milestones {
            get;
            private set;
        }

        private Command _loadProjectDetails;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadProjectDetailsCommand {
            get {
                return _loadProjectDetails ?? (_loadProjectDetails = new Command(async () => await ExecuteLoadProjectDetailsCommand()));
            }
        }

        private async Task ExecuteLoadProjectDetailsCommand() {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

            try {
                string url = $"https://ecs.incresearch.com/ECS/mobile/projectdetails/projectId/{Project.projectId}";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    List<ProjectDetails> projectDetails = Utility.DeserializeResponse<List<ProjectDetails>>(content, "data/projects/project");
                    ProjectDetails = projectDetails[0];
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load project milestones.", "OK");
            }

            IsBusy = false;
        }

        private Command _loadMilestones;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadMilestonesCommand {
            get {
                return _loadMilestones ?? (_loadMilestones = new Command(async () => await ExecuteLoadMilestonesCommand()));
            }
        }

        private async Task ExecuteLoadMilestonesCommand() {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

            try {
                string url = $"https://ecs.incresearch.com/ECS/mobile/milestones/projectId/{Project.projectId}";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Milestone> miletones = Utility.DeserializeResponse<List<Milestone>>(content, "data/project/milestone");

                    Milestones.Clear();

                    foreach(Milestone milestone in miletones) {
                        Milestones.Add(milestone);
                    }
                }


            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load project milestones.", "OK");
            }

            IsBusy = false;
        }
    }
}