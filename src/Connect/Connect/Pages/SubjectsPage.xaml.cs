using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Helpers;
using Connect.Models;
using Xamarin.Forms;

namespace Connect.Pages {
    public partial class SubjectsPage : ContentPage {

        private HttpClient _client;
        private readonly string _projectId;
        private List<SubjectDetails> _subjectDetailsList;
        private List<SubjectStats> _subjectStatsList;
        private List<SubjectTrends> _subjectTrendsList;

        public SubjectsPage() : this(null) { }

        public SubjectsPage(Milestone project) {
            InitializeComponent();

            if(project != null) {
                _projectId = project.projectId;
            }
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            if(App.LoggedIn) {
                if(!string.IsNullOrEmpty((_projectId))) {
                    await GetSubjectDetails();
                    await GetSubjectStats();
                    await GetSubjectTrends();
                }
            }
        }

        private async Task GetSubjectDetails() {
            string url = $"https://ecs.incresearch.com/ECS/mobile/subdetails/projectId/{_projectId}";

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

            HttpResponseMessage response = await _client.GetAsync(url);

            if(response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();

                _subjectDetailsList = Utility.DeserializeResponse<List<SubjectDetails>>(content, "data/subjects/details");
            }
        }

        private async Task GetSubjectStats() {
            string url = $"https://ecs.incresearch.com/ECS/mobile/substats/projectId/{_projectId}";

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

            HttpResponseMessage response = await _client.GetAsync(url);

            if(response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();

                _subjectStatsList = Utility.DeserializeResponse<List<SubjectStats>>(content, "data/project/subjectStats");
            }
        }

        private async Task GetSubjectTrends() {
            string url = $"https://ecs.incresearch.com/ECS/mobile/subtrends/projectId/{_projectId}";

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

            HttpResponseMessage response = await _client.GetAsync(url);

            if(response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();

                _subjectTrendsList = Utility.DeserializeResponse<List<SubjectTrends>>(content, "data/project/subjectTrends");
            }
        }
    }
}