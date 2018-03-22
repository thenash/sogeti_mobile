using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Helpers;
using Connect.Models;
using Connect.Views;
using Xamarin.Forms;

namespace Connect.ViewModels {

    public class ProjectInfoViewModel : BaseViewModel {

        private const int IncreaseMilestoneAmount = 3;

        private int _milestonesToShowCount = 3;

        private Variances _selectedVariance = Variances.White;

        public ProjectInfoViewModel() {
            Title      = "Project Information";
            Milestones = new ObservableCollection<Milestone>();
        }

        //private async void RefreshData() {
        //    //await ExecuteLoadProjectDetailsCommand();
        //    //await ExecuteLoadMilestonesCommand();
        //}

        //private Color _backgroundColorReset;

        //public Color BackgroundColorReset {//BUG: The VarianceViewCard.BackgroundColorReset binding is not working, so using this workaround instead
        //    get => _backgroundColorReset;
        //    set {
        //        _backgroundColorReset = value;
        //        OnPropertyChanged();
        //    }
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

        private List<ContactInfo> _contacts;
        public List<ContactInfo> Contacts {
            get => _contacts ?? (_contacts = new List<ContactInfo>());
            set {
                if(_contacts != value) {
                    _contacts = value;
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

        private Command _showMoreMilestones;
        /// <summary>
        /// Command to display additional milestones in the list.
        /// </summary>
        public Command ShowMoreMilestones => _showMoreMilestones ?? (_showMoreMilestones = new Command(ExecuteShowMoreMilestones));

        public void ExecuteShowMoreMilestones() {
            if(_milestonesToShowCount >= Milestones.Count) {
                return;
            }

            _milestonesToShowCount += IncreaseMilestoneAmount;
            FilterMilestonesByVariance(_selectedVariance);
        }

        private Command _showLessMilestones;
        /// <summary>
        /// Command to display additional milestones in the list.
        /// </summary>
        public Command ShowLessMilestones => _showLessMilestones ?? (_showLessMilestones = new Command(ExecuteShowLessMilestones));

        public void ExecuteShowLessMilestones() {
            if(_milestonesToShowCount <= IncreaseMilestoneAmount) {
                return;
            }

            _milestonesToShowCount -= IncreaseMilestoneAmount;
            FilterMilestonesByVariance(_selectedVariance);
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

                using(HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) }) {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                    using(HttpResponseMessage response = await client.GetAsync(url)) {

                        if(response.IsSuccessStatusCode) {
                            string content = await response.Content.ReadAsStringAsync();

                            List<ProjectDetails> projectDetails = Utility.DeserializeResponse<List<ProjectDetails>>(content, "data/projects/project");
                            ProjectDetails = projectDetails[0];

                            if(ProjectDetails != null) {
                                Contacts = ContactInfo.GetContacts(ProjectDetails);
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

        private Command _loadMilestones;
        /// <summary>
        /// Command to load/refresh project milestones.
        /// </summary>
        public Command LoadMilestonesCommand => _loadMilestones ?? (_loadMilestones = new Command(async () => await ExecuteLoadMilestonesCommand()));

        public async Task ExecuteLoadMilestonesCommand() {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

            try {
                string url = $"https://ecs.incresearch.com/ECS/mobile/milestones/projectId/{Project.projectId}";

                Milestones.Clear();
                DisplayMilestones.Clear();

                using(HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(60) }) {

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                    using(HttpResponseMessage response = await client.GetAsync(url)) {
                        if(response.IsSuccessStatusCode) {
                            string content = await response.Content.ReadAsStringAsync();
                            List<Milestone> miletones = Utility.DeserializeResponse<List<Milestone>>(content, "data/project/milestone");

                            foreach(Milestone milestone in miletones) {
                                Milestones.Add(milestone);

                                if(DisplayMilestones.Count < _milestonesToShowCount) {
                                    DisplayMilestones.Add(milestone);
                                }
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

        public void FilterMilestonesByVariance(Variances variance) {

            DisplayMilestones.Clear();

            foreach(Milestone milestone in Milestone.GetMilestonesByVariance(variance, Milestones).Take(_milestonesToShowCount)) {
                DisplayMilestones.Add(milestone);
            }

            _selectedVariance = variance;
        }
    }
}