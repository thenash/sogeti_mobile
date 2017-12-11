using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Helpers;
using Connect.Models;
using Xamarin.Forms;

namespace Connect.ViewModels {

    public class SitesViewModel : BaseViewModel {

        /// <summary>
        /// gets or sets the feed items
        /// </summary>
        public ObservableCollection<SiteDetails> SiteDetails {
            get;
            private set;
        }

        /// <summary>
	    /// gets or sets the feed items
	    /// </summary>
		public ObservableCollection<SiteStats> SiteStats {
            get;
            private set;
        }

        /// <summary>
        /// gets or sets the feed items
        /// </summary>
        public ObservableCollection<SiteTrends> SiteTrends {
            get;
            private set;
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

        private readonly string _projectId;

        public SitesViewModel(string projectId) {
            Title = "Sites";

            _projectId = projectId;

            SiteStats   = new ObservableCollection<SiteStats>();
            SiteTrends  = new ObservableCollection<SiteTrends>();
            SiteDetails = new ObservableCollection<SiteDetails>();

            RefreshData();
        }

        private async void RefreshData() {
            //await ExecuteLoadProjectCommand();
            //await ExecuteLoadSiteStatsCommand();
            //await ExecuteLoadSiteDetailsCommand();
            //Big request... takes long to execute
            //await ExecuteLoadSiteTrendsCommand();
        }

        private Command _loadSiteDetailsCommand;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadSiteDetailsCommand {
            get {
                return _loadSiteDetailsCommand ?? (_loadSiteDetailsCommand = new Command(async () => await ExecuteLoadSiteDetailsCommand()));
            }
        }

        private async Task ExecuteLoadSiteDetailsCommand() {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

            try {
                string url = $"https://ecs.incresearch.com/ECS/mobile/sitedetails/projectId/{_projectId}";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    List<SiteDetails> siteDetails = Utility.DeserializeResponse<List<SiteDetails>>(content, "data/sites/details");

                    SiteDetails.Clear();

                    foreach(SiteDetails siteDetail in siteDetails) {
                        SiteDetails.Add(siteDetail);
                    }
                }

            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load projects.", "OK");
            }

            IsBusy = false;
        }


        private Command _loadSiteTrendsCommand;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadSiteTrendsCommand {
            get {
                return _loadSiteTrendsCommand ?? (_loadSiteTrendsCommand = new Command(async () => await ExecuteLoadSiteTrendsCommand()));
            }
        }

        private async Task ExecuteLoadSiteTrendsCommand() {
            if(IsBusy)
                return;

            IsBusy = true;

            try {
                string url = $"https://ecs.incresearch.com/ECS/mobile/sitetrends/projectId/{_projectId}";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    List<SiteTrends> siteTrends = Utility.DeserializeResponse<List<SiteTrends>>(content, "data/project/siteTrends");

                    SiteTrends.Clear();

                    foreach(SiteTrends siteTrend in siteTrends) {
                        SiteTrends.Add(siteTrend);
                    }
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load projects.", "OK");
            }

            IsBusy = false;
        }


        private Command _loadSiteStatsCommand;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadSiteStatsCommand {
            get {
                return _loadSiteStatsCommand ?? (_loadSiteStatsCommand = new Command(async () => await ExecuteLoadSiteStatsCommand()));
            }
        }

        private async Task ExecuteLoadSiteStatsCommand() {
            if(IsBusy)
                return;

            IsBusy = true;

            try {
                string url = $"https://ecs.incresearch.com/ECS/mobile/sitestats/projectId/{_projectId}";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    List<SiteStats> siteStats = Utility.DeserializeResponse<List<SiteStats>>(content, "data/project/siteStats");

                    SiteStats.Clear();

                    foreach(SiteStats siteStat in siteStats) {
                        SiteStats.Add(siteStat);
                    }
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load projects.", "OK");
            }

            IsBusy = false;
        }

        private Command _loadProjectCommand;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadProjectCommand {
            get {
                return _loadSiteStatsCommand ?? (_loadSiteStatsCommand = new Command(async () => await ExecuteLoadProjectCommand()));
            }
        }

        private async Task ExecuteLoadProjectCommand() {
            if(IsBusy)
                return;

            IsBusy = true;

            try {
                string url = $"https://ecs.incresearch.com/ECS/mobile/project/projectId/{_projectId}";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    List<Project> projects = Utility.DeserializeResponse<List<Project>>(content, "data/projects/projectinfo");
                    Project = projects[0];
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load projects.", "OK");
            }

            IsBusy = false;
        }
    }
}
