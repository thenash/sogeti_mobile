using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Helpers;
using Connect.Models;
using Xamarin.Forms;

namespace Connect.Pages
{
	public partial class SubjectsPage : ContentPage
	{
		HttpClient client;
		private string _projectID;
		List<SubjectDetails> subjectDetailsList;
		List<SubjectStats> subjectStatsList;
		List<SubjectTrends> subjectTrendsList;

		public SubjectsPage() : this(null) { }

		public SubjectsPage(Milestone project)
		{
			InitializeComponent();

			if (project != null)
				_projectID = project.projectId;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			if (App.LoggedIn)
			{
				if (!string.IsNullOrEmpty((_projectID)))
				{
					await GetSubjectDetails();
					await GetSubjectStats();
					await GetSubjectTrends();
				}
			}
		}

		async Task GetSubjectDetails()
		{
			string url = $"https://ecs.incresearch.com/ECS/mobile/subdetails/projectId/{_projectID}";

			client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				subjectDetailsList = Utility.DeserializeResponse<List<SubjectDetails>>(content, "data/subjects/details");
			}

		}

		async Task GetSubjectStats()
		{
			string url = $"https://ecs.incresearch.com/ECS/mobile/substats/projectId/{_projectID}";

			client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				subjectStatsList = Utility.DeserializeResponse<List<SubjectStats>>(content, "data/project/subjectStats");
			}

		}

		async Task GetSubjectTrends()
		{
			string url = $"https://ecs.incresearch.com/ECS/mobile/subtrends/projectId/{_projectID}";

			client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

			var response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				subjectTrendsList = Utility.DeserializeResponse<List<SubjectTrends>>(content, "data/project/subjectTrends");
			}

		}

	}
}
