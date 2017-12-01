using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Helpers;
using Connect.Models;
using Xamarin.Forms;

namespace Connect.Pages
{
    public partial class MonitoringPage : ContentPage
    {
		HttpClient client;
		private string _projectID;
        List<VisitDetails> visitDetailsList;
		List<VisitMetrics> visitMetricsList;

		public MonitoringPage() : this(null) { }

		public MonitoringPage(Milestone project)
		{
			InitializeComponent();

			if (project != null)
				_projectID = project.projectId;
		}

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            if(App.LoggedIn)
			{
                if (!string.IsNullOrEmpty((_projectID)))
                {
                    await GetVisitDetails();
                    await GetVisitMetrics();
                }
            }
        }

		async Task GetVisitDetails()
		{
			string url = $"https://ecs.incresearch.com/ECS/mobile/visitdetails/projectId/{_projectID}";

			client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

                visitDetailsList = Utility.DeserializeResponse<List<VisitDetails>>(content, "data/project/visitdetail");
			}

		}

		async Task GetVisitMetrics()
		{
			string url = $"https://ecs.incresearch.com/ECS/mobile/visitmetrics/projectId/{_projectID}";

			client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				visitMetricsList = Utility.DeserializeResponse<List<VisitMetrics>>(content, "data/project/visitmetrics");
			}

		}
    }
}
