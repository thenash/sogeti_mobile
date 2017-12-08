﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Helpers;
using Connect.Models;
using Xamarin.Forms;
using Connect.ViewModels;

namespace Connect.Pages {
    public partial class ProjectInfoPage : ContentPage {
        HttpClient client;
        private string _projectID;
        List<Milestone> _miletones;
        List<ProjectDetails> _projectDetails;

        ProjectInfoViewModel viewModel;

        public ProjectInfoPage() : this(new Project()) { }

        public ProjectInfoPage(Project project) {

            BindingContext = viewModel = new ProjectInfoViewModel(project);

            InitializeComponent();

            if(project != null) {
                _projectID = project.projectId;
            }
        }

        //protected override async void OnAppearing() {
        //    base.OnAppearing();

        //    milestonesList.ItemsSource = new List<Milestone>()
        //    {
        //    new Milestone() { milestoneName = "Milestone 1", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" },
        //    new Milestone() { milestoneName = "Milestone 2", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" },
        //    new Milestone() { milestoneName = "Milestone 3", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" },
        //    new Milestone() { milestoneName = "Milestone 4", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" }
        //    };

        //    //if(App.LoggedIn) {
        //    //    if(!string.IsNullOrEmpty((_projectID))) {
        //    //        //await GetMilestones();
        //    //        //await GetProjectDetails();

        //    //        milestonesList.ItemsSource = _miletones;
        //    //    }
        //    //}
        //}

        protected override async void OnAppearing() {
            base.OnAppearing();

            await GetMilestones();

            //milestonesList.ItemsSource = new List<Milestone> {
            //    new Milestone { milestoneName = "Milestone 1", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" },
            //    new Milestone { milestoneName = "Milestone 2", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" },
            //    new Milestone { milestoneName = "Milestone 3", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" },
            //    new Milestone { milestoneName = "Milestone 4", plannedDate = "02Jul2015", actualDate = "22Jul2015", status = "20" }
            //};

            milestonesList.ItemsSource = _miletones;

            if(App.LoggedIn) {
                if(viewModel.IsInitialized) {
                    return;
                }

                viewModel.IsInitialized = true;
            }
        }

        async Task GetMilestones() {
            string url = $"https://ecs.incresearch.com/ECS/mobile/milestones/projectId/{_projectID}";

            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

#if DEBUG
            _miletones = new List<Milestone> {
                new Milestone {
                    projectId = "1001234",
                    actualDate = DateTime.UtcNow.AddDays(2).ToString("ddMMMyyyy"),
                    actualDateTime = DateTime.UtcNow.AddDays(2),
                    milestoneName = "Milestone 1",
                    plannedDate = DateTime.UtcNow.AddDays(4).ToString("ddMMMyyyy"),
                    plannedDateTime = DateTime.UtcNow.AddDays(4),
                    sortIndex = 1,
                    status = "20"
                }, new Milestone {
                    projectId = "1001234",
                    actualDate = DateTime.UtcNow.AddDays(6).ToString("ddMMMyyyy"),
                    actualDateTime = DateTime.UtcNow.AddDays(6),
                    milestoneName = "Milestone 2",
                    plannedDate = DateTime.UtcNow.AddDays(4).ToString("ddMMMyyyy"),
                    plannedDateTime = DateTime.UtcNow.AddDays(4),
                    sortIndex = 2,
                    status = "10"
                }, new Milestone {
                    projectId = "1001234",
                    actualDate = DateTime.UtcNow.AddDays(8).ToString("ddMMMyyyy"),
                    actualDateTime = DateTime.UtcNow.AddDays(8),
                    milestoneName = "Milestone 3",
                    plannedDate = DateTime.UtcNow.AddDays(6).ToString("ddMMMyyyy"),
                    plannedDateTime = DateTime.UtcNow.AddDays(6),
                    sortIndex = 3,
                    status = "10"
                }
            };
#endif

            HttpResponseMessage response = await client.GetAsync(url);

            if(response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                _miletones = Utility.DeserializeResponse<List<Milestone>>(content, "data/project/milestone");
            }
        }

        async Task GetProjectDetails() {
            string url = $"https://ecs.incresearch.com/ECS/mobile/projectdetails/projectId/{_projectID}";

            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

#if DEBUG
            _projectDetails = new List<ProjectDetails> {
                new ProjectDetails {
                    customerName = "Generic Customer",
                    owningBu = "9500 Biometrics",
                    phase = "4",
                    primaryIndication = "Dry Eye",
                    primaryTherapeuticArea = "Psychiatry",
                    projectDirector = "Sally Smith",
                    projectId = "1001234",
                    protocolId = "9083E1-ES3",
                    directorEmail = "director@email.com",
                    directorPhone = "123-456-7890",
                    leadDataManager = "The Lead Data Mgr",
                    leadDmEmail = "data-mgr@email.com",
                    leadDmPhone = "123-456-7890",
                    leadEmail = "lead@email.com",
                    leadPhone = "123-456-7890",
                    projectDescription = "The project description...",
                    projectEndDate = DateTime.UtcNow.AddDays(2).ToString("ddMMMyyyy"),
                    projectLead = "The Project Lead",
                    projectLifecycleStage = "Project Life Cycle Stage",
                    projectName = "Project Name",
                    projectStartDate =  DateTime.UtcNow.ToString("ddMMMyyyy"),
                    protocolDesc = "The protocol description...",
                    totalDirectBudgetAmt = 50000,
                    totalIndirectBudgetAmt = 75000
                }
            };
#endif

            HttpResponseMessage response = await client.GetAsync(url);

            if(response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();

                _projectDetails = Utility.DeserializeResponse<List<ProjectDetails>>(content, "data/projects/project");
            }

        }
    }
}
