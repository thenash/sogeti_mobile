using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Connect.Models;
using Connect.Views;
using Xamarin.Forms;

#if DEBUG
using System.Net.Http;
using Connect.Helpers;
using System.Collections.Generic;
#endif

namespace Connect.ViewModels {

    public class ProjectInfoViewModel : BaseViewModel {

        private VarianceViewCard _varianceViewCard;

        public ProjectInfoViewModel(Project project) {
            Title      = "Project Information";
            Project    = project;
            Milestones = new ObservableCollection<Milestone>();
        }

        //private async void RefreshData() {
        //    //await ExecuteLoadProjectDetailsCommand();
        //    //await ExecuteLoadMilestonesCommand();
        //}

        private ProjectDetails _projectDetails;

        public ProjectDetails ProjectDetails {
            get => _projectDetails;
            set {
                if(_projectDetails != value) {
                    _projectDetails = value;
                    OnPropertyChanged();
                }
            }
        }

        private Project _project;

        public Project Project {
            get => _project;
            set {
                if(_project != value) {
                    _project = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Milestone> _milestones;
        public ObservableCollection<Milestone> Milestones {
            get => _milestones ?? (_milestones = new ObservableCollection<Milestone>());
            set {
                if(_milestones != value) {
                    _milestones = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Milestone> _displayMilestones;
        public ObservableCollection<Milestone> DisplayMilestones {
            get => _displayMilestones ?? (_displayMilestones = new ObservableCollection<Milestone>());
            set {
                if(_displayMilestones != value) {
                    _displayMilestones = value;
                    OnPropertyChanged();
                }
            }
        }

        private Command _loadProjectDetails;
        /// <summary>
        /// Command to load/refresh project details.
        /// </summary>
        public Command LoadProjectDetailsCommand => _loadProjectDetails ?? (_loadProjectDetails = new Command(async () => await ExecuteLoadProjectDetailsCommand()));

        public async Task ExecuteLoadProjectDetailsCommand() {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

            try {
                string url = $"https://ecs.incresearch.com/ECS/mobile/projectdetails/projectId/{Project.projectId}";

                using(HttpClient client = new HttpClient()) {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                    using(HttpResponseMessage response = await client.GetAsync(url)) {

                        if(response.IsSuccessStatusCode) {
                            string content = await response.Content.ReadAsStringAsync();

                            List<ProjectDetails> projectDetails = Utility.DeserializeResponse<List<ProjectDetails>>(content, "data/projects/project");
                            ProjectDetails = projectDetails[0];
                        }
                    }
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
        public Command LoadMilestonesCommand => _loadMilestones ?? (_loadMilestones = new Command(async () => await ExecuteLoadMilestonesCommand()));

        public async Task ExecuteLoadMilestonesCommand() {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

            try {
                string url = $"https://ecs.incresearch.com/ECS/mobile/milestones/projectId/{Project.projectId}";

                using(HttpClient client = new HttpClient()) {

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                    using(HttpResponseMessage response = await client.GetAsync(url)) {
                        if(response.IsSuccessStatusCode) {
                            string content = await response.Content.ReadAsStringAsync();
                            List<Milestone> miletones = Utility.DeserializeResponse<List<Milestone>>(content, "data/project/milestone");

                            Milestones.Clear();
                            DisplayMilestones.Clear();

                            foreach(Milestone milestone in miletones) {
                                Milestones.Add(milestone);
                                DisplayMilestones.Add(milestone);
                            }
                        }
                    }
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load project milestones.", "OK");
            }

            IsBusy = false;
        }

        public void FilterMilestonesByVariance(Variances variance) => DisplayMilestones = new ObservableCollection<Milestone>(Milestone.GetMilestonesByVariance(variance, Milestones));
    }
}