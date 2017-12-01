using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Helpers;
using Connect.Models;
using Xamarin.Forms;

namespace Connect.ViewModels
{
    public class SitesViewModel : BaseViewModel
    { 
		/// <summary>
		/// gets or sets the feed items
		/// </summary>
		public ObservableCollection<SiteDetails> SiteDetails
		{
			get;
			private set;
		}

        /// <summary>
	    /// gets or sets the feed items
	    /// </summary>
		public ObservableCollection<SiteStats> SiteStats
		{
			get;
			private set;
		}

		/// <summary>
		/// gets or sets the feed items
		/// </summary>
		public ObservableCollection<SiteTrends> SiteTrends
		{
			get;
			private set;
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

        private string _projectID = "Project 1";

        public SitesViewModel(string ProjectID)
		{
			this.Title = "Sites";

            _projectID = ProjectID;
			SiteStats = new ObservableCollection<SiteStats>();
			SiteTrends = new ObservableCollection<SiteTrends>();
			SiteDetails = new ObservableCollection<SiteDetails>();

            refreshData();
		}

        private async void refreshData()
		{
			await ExecuteLoadProjectCommand();
            await ExecuteLoadSiteStatsCommand();
			await ExecuteLoadSiteDetailsCommand();
            //Big request... takes long to execute
			//await ExecuteLoadSiteTrendsCommand();
		}

        private Command loadSiteDetailsCommand;
		/// <summary>
		/// Command to load/refresh artitists
		/// </summary>
        public Command LoadSiteDetailsCommand
		{
			get { return loadSiteDetailsCommand ?? (loadSiteDetailsCommand = new Command(async () => await ExecuteLoadSiteDetailsCommand())); }
		}

        private async Task ExecuteLoadSiteDetailsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var url = $"https://ecs.incresearch.com/ECS/mobile/sitedetails/projectId/{_projectID}";

				var _client = new HttpClient();
				_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

				var response = await _client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					var siteDetails = Utility.DeserializeResponse<List<SiteDetails>>(content, "data/sites/details");

					SiteDetails.Clear();

					foreach (var siteDetail in siteDetails)
					{
						SiteDetails.Add(siteDetail);
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


		private Command loadSiteTrendsCommand;
		/// <summary>
		/// Command to load/refresh artitists
		/// </summary>
		public Command LoadSiteTrendsCommand
        {
			get { return loadSiteTrendsCommand ?? (loadSiteTrendsCommand = new Command(async () => await ExecuteLoadSiteTrendsCommand())); }
		}

        private async Task ExecuteLoadSiteTrendsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var url = $"https://ecs.incresearch.com/ECS/mobile/sitetrends/projectId/{_projectID}";

				var _client = new HttpClient();
				_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

				var response = await _client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					var siteTrends = Utility.DeserializeResponse<List<SiteTrends>>(content, "data/project/siteTrends");

					SiteTrends.Clear();

					foreach (var siteTrend in siteTrends)
					{
						SiteTrends.Add(siteTrend);
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


		private Command loadSiteStatsCommand;
		/// <summary>
		/// Command to load/refresh artitists
		/// </summary>
        public Command LoadSiteStatsCommand
		{
			get { return loadSiteStatsCommand ?? (loadSiteStatsCommand = new Command(async () => await ExecuteLoadSiteStatsCommand())); }
		}

        private async Task ExecuteLoadSiteStatsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var url = $"https://ecs.incresearch.com/ECS/mobile/sitestats/projectId/{_projectID}";

				var _client = new HttpClient();
				_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

				var response = await _client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

					var siteStats = Utility.DeserializeResponse<List<SiteStats>>(content, "data/project/siteStats");

					SiteStats.Clear();

					foreach (var siteStat in siteStats)
					{
						SiteStats.Add(siteStat);
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

		private Command loadProjectCommand;
		/// <summary>
		/// Command to load/refresh artitists
		/// </summary>
		public Command LoadProjectCommand
		{
			get { return loadSiteStatsCommand ?? (loadSiteStatsCommand = new Command(async () => await ExecuteLoadProjectCommand())); }
		}

		private async Task ExecuteLoadProjectCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				string url = $"https://ecs.incresearch.com/ECS/mobile/project/projectId/{_projectID}";

				var _client = new HttpClient();
				_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

				var response = await _client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();

                    var projects = Utility.DeserializeResponse<List<Project>>(content, "data/projects/projectinfo");
                    Project = projects[0];
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
