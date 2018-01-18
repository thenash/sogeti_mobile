using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Connect.Models;
using Xamarin.Forms;
using Connect.Helpers;

namespace Connect.ViewModels {

    public class MonitoringViewModel : BaseViewModel {

        private ObservableCollection<VisitDetails> _visitDetails;

        /// <summary>
        /// gets or sets the feed items
        /// </summary>
        public ObservableCollection<VisitDetails> VisitDetails {
            get => _visitDetails ?? (_visitDetails = new ObservableCollection<VisitDetails>());
            set {
                if(_visitDetails != value) {
                    _visitDetails = value;
                    OnPropertyChanged();
                }
            }
        }

        ///// <summary>
        ///// gets or sets the planned bottom chart site stats.
        ///// </summary>
        //public ObservableCollection<GraphCategory> PlannedBottomChartSiteStats => new ObservableCollection<GraphCategory>(Utility.GetSiteStatusChartCategories(PlannedSiteStats));

        /// <summary>
        /// gets or sets the actual bottom chart site stats.
        /// </summary>
        public ObservableCollection<GraphCategory> ActualBottomChartVisitMetrics => new ObservableCollection<GraphCategory>(Utility.GetVisitMetricNumberOfSites(VisitMetrics));

        /// <summary>
        /// gets or sets the total bottom chart site stats.
        /// </summary>
        public ObservableCollection<GraphCategory> TotalBottomChartVisitMetrics => new ObservableCollection<GraphCategory>(Utility.GetVisitMetricNumberOfVisits(VisitMetrics));

  //      private ObservableCollection<SiteStats> _plannedSiteStats;

  //      /// <summary>
	 //   /// gets or sets the planned site stats.
	 //   /// </summary>
		//public ObservableCollection<SiteStats> PlannedSiteStats {
  //          get => _plannedSiteStats ?? (_plannedSiteStats = new ObservableCollection<SiteStats>());
  //          set {
  //              if(_plannedSiteStats != value) {
  //                  _plannedSiteStats = value;
  //                  OnPropertyChanged();
  //                  OnPropertyChanged(nameof(PlannedBottomChartSiteStats));
  //              }
  //          }
  //      }

        private ObservableCollection<ReportCompliance> _completionCompliance;

        /// <summary>
        /// gets or sets the report completion compliance.
        /// </summary>
        public ObservableCollection<ReportCompliance> CompletionCompliance {
            get => _completionCompliance ?? (_completionCompliance = new ObservableCollection<ReportCompliance>());
            set {
                if(_completionCompliance != value) {
                    _completionCompliance = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<VisitMetrics> _visitMetrics;

        /// <summary>
        /// gets or sets the feed items
        /// </summary>
        public ObservableCollection<VisitMetrics> VisitMetrics {
            get => _visitMetrics ?? (_visitMetrics = new ObservableCollection<VisitMetrics>());
            set {
                if(_visitMetrics != value) {
                    _visitMetrics = value;
                    OnPropertyChanged();
                }
            }
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

        public MonitoringViewModel(string projectId) {
            Title = "Monitoring";

            _projectId = projectId;

            CompletionCompliance = new ObservableCollection<ReportCompliance>();

            VisitMetrics = new ObservableCollection<VisitMetrics>();
            VisitDetails = new ObservableCollection<VisitDetails>();
        }

        public async Task RefreshData(string projectId) {
            await ExecuteLoadProjectCommand(projectId);
            await ExecuteLoadReportCompletionsCommand(projectId);
            await ExecuteLoadVisitMetricsCommand(projectId);
        }

        private Command _oadReportCompletionsCommand;
        /// <summary>
        /// Command to load/refresh report compliance data.
        /// </summary>
        public Command LoadReportCompletionsCommand => _oadReportCompletionsCommand ?? (_oadReportCompletionsCommand = new Command(async () => await ExecuteLoadReportCompletionsCommand(_projectId)));

        private async Task ExecuteLoadReportCompletionsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

//#if DEBUG
//            int year = await Task.FromResult(2015);  //Getting rid of compiler warnings

//            CompletionCompliance = new ObservableCollection<ReportCompliance>(Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReportCompliance>>("{\"data\":{\"project\":{\"reportcompliance\":[{\"projectId\":\"Project 1\",\"reportsCompleted\":\"8\",\"compliance\":\"75\",\"eventMonth\":\"2016-11\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"6\",\"compliance\":\"100\",\"eventMonth\":\"2016-05\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"6\",\"compliance\":\"50\",\"eventMonth\":\"2016-06\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"6\",\"compliance\":\"100\",\"eventMonth\":\"2016-03\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"9\",\"compliance\":\"89\",\"eventMonth\":\"2016-07\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"11\",\"compliance\":\"100\",\"eventMonth\":\"2015-10\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"7\",\"compliance\":\"100\",\"eventMonth\":\"2015-12\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"6\",\"compliance\":\"50\",\"eventMonth\":\"2016-04\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"3\",\"compliance\":\"100\",\"eventMonth\":\"2016-02\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"10\",\"compliance\":\"100\",\"eventMonth\":\"2015-11\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"4\",\"compliance\":\"100\",\"eventMonth\":\"2016-08\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"14\",\"compliance\":\"86\",\"eventMonth\":\"2016-09\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"10\",\"compliance\":\"100\",\"eventMonth\":\"2017-02\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"9\",\"compliance\":\"100\",\"eventMonth\":\"2016-12\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"1\",\"compliance\":\"100\",\"eventMonth\":\"2015-09\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"10\",\"compliance\":\"70\",\"eventMonth\":\"2016-10\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"13\",\"compliance\":\"77\",\"eventMonth\":\"2016-01\"},{\"projectId\":\"Project 1\",\"reportsCompleted\":\"12\",\"compliance\":\"100\",\"eventMonth\":\"2017-01\"}]},\"summary\":{\"recordCount\":18,\"status\":0,\"statusMessage\":\"SUCCESS\",\"startTime\":\"2018-01-11T23:12:53.498-05:00\",\"endTime\":\"2018-01-11T23:12:53.601-05:00\",\"copyright\":\"Copyright INC Research Inc \\ufffd 2016\",\"disclaimer\":\"Contents of this data are provided for information purposes only. The data represented should not be used to satisfy a GxP regulation or be used in submission to a regulatory agency.\"}}}")));
//#else
            try {
                string url = "https://ecs.incresearch.com/ECS/mobile/reportcompliance/projectId/" + projectId;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    List<ReportCompliance> compliances = Utility.DeserializeResponse<List<ReportCompliance>>(content, "data/project/reportcompliance");

                    IOrderedEnumerable<ReportCompliance> orderedCompliances = compliances.OrderBy(c => c.EventMonthDateTime);

                    CompletionCompliance.Clear();

                    foreach(ReportCompliance compliance in orderedCompliances) {
                        //compliance.Compliance = "0";
                        CompletionCompliance.Add(compliance);
                    }
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load report compliance data.", "OK");
            }
//#endif

            IsBusy = false;
        }


        private Command _loadVisitMetricsCommand;
        /// <summary>
        /// Command to load/refresh visit metrics collection.
        /// </summary>
        public Command LoadVisitMetricsCommand => _loadVisitMetricsCommand ?? (_loadVisitMetricsCommand = new Command(async () => await ExecuteLoadVisitMetricsCommand(_projectId)));

        private async Task ExecuteLoadVisitMetricsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

//#if DEBUG
//            VisitMetrics = new ObservableCollection<VisitMetrics>(Newtonsoft.Json.JsonConvert.DeserializeObject<List<VisitMetrics>>("{\"data\":{\"project\":{\"visitmetrics\":[{\"projectId\":\"Project 1\",\"numSites\":\"16\",\"numVisits\":\"16\",\"reportsCompleted\":\"0\",\"compliance\":\"\",\"eventType\":\"Enrolling\"},{\"projectId\":\"Project 1\",\"numSites\":\"1\",\"numVisits\":\"1\",\"reportsCompleted\":\"0\",\"compliance\":\"\",\"eventType\":\"Non Enrolling\"},{\"projectId\":\"Project 1\",\"numSites\":\"25\",\"numVisits\":\"25\",\"reportsCompleted\":\"25\",\"compliance\":\"96\",\"eventType\":\"PSSV\"},{\"projectId\":\"Project 1\",\"numSites\":\"20\",\"numVisits\":\"20\",\"reportsCompleted\":\"0\",\"compliance\":\"\",\"eventType\":\"Activated\"},{\"projectId\":\"Project 1\",\"numSites\":\"16\",\"numVisits\":\"98\",\"reportsCompleted\":\"96\",\"compliance\":\"84\",\"eventType\":\"IMV\"},{\"projectId\":\"Project 1\",\"numSites\":\"2\",\"numVisits\":\"2\",\"reportsCompleted\":\"0\",\"compliance\":\"\",\"eventType\":\"Inactive\"},{\"projectId\":\"Project 1\",\"numSites\":\"3\",\"numVisits\":\"3\",\"reportsCompleted\":\"0\",\"compliance\":\"\",\"eventType\":\"Closed\"},{\"projectId\":\"Project 1\",\"numSites\":\"4\",\"numVisits\":\"4\",\"reportsCompleted\":\"4\",\"compliance\":\"100\",\"eventType\":\"COV\"},{\"projectId\":\"Project 1\",\"numSites\":\"21\",\"numVisits\":\"21\",\"reportsCompleted\":\"0\",\"compliance\":\"\",\"eventType\":\"Selected\"},{\"projectId\":\"Project 1\",\"numSites\":\"20\",\"numVisits\":\"20\",\"reportsCompleted\":\"20\",\"compliance\":\"95\",\"eventType\":\"SIV\"}]},\"summary\":{\"recordCount\":10,\"status\":0,\"statusMessage\":\"SUCCESS\",\"startTime\":\"2018-01-11T23:17:28.239-05:00\",\"endTime\":\"2018-01-11T23:17:28.395-05:00\",\"copyright\":\"Copyright INC Research Inc \\ufffd 2016\",\"disclaimer\":\"Contents of this data are provided for information purposes only. The data represented should not be used to satisfy a GxP regulation or be used in submission to a regulatory agency.\"}}}"));
//#else
            try {
                string url = "https://ecs.incresearch.com/ECS/mobile/visitmetrics/projectId/" + projectId;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    List<VisitMetrics> metrics = Utility.DeserializeResponse<List<VisitMetrics>>(content, "data/project/visitmetrics");

                    VisitMetrics.Clear();

                    foreach(VisitMetrics metric in metrics) {
                        VisitMetrics.Add(metric);
                    }
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load visit metrics.", "OK");
            }
//#endif

            IsBusy = false;
        }


//        private Command _loadSiteStatsCommand;

//        /// <summary>
//        /// Command to load/refresh artitists
//        /// </summary>
//        public Command LoadSiteStatsCommand  => _loadSiteStatsCommand ?? (_loadSiteStatsCommand = new Command(async () => await ExecuteLoadSiteStatsCommand(_projectId)));

//        private async Task ExecuteLoadSiteStatsCommand(string projectId) {
//            if(IsBusy) {
//                return;
//            }

//            IsBusy = true;

//#if DEBUG
//            await Task.FromResult(0);

//            ActualSiteVisits = new ObservableCollection<SiteStats> {
//                new SiteStats {
//                    projectId = projectId,
//                    activated = 4,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(2),
//                    isoDate = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 2,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 2,
//                    closed = 3,
//                    enrolling = 16,
//                    inactive = 2,
//                    isoDateTime = DateTime.UtcNow.AddMonths(3),
//                    isoDate = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 10,
//                    selected = 1,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 1,
//                    closed = 10,
//                    enrolling = 18,
//                    inactive = 3,
//                    isoDateTime = DateTime.UtcNow.AddMonths(4),
//                    isoDate = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
//                    nonEnrolling = 4,
//                    pssv = 10,
//                    selected = 2,
//                    siv = 1
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 0,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(5),
//                    isoDate = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 11,
//                    selected = 3,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 2,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(6),
//                    isoDate = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 11,
//                    selected = 2,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 1,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(7),
//                    isoDate = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 12,
//                    selected = 1,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 3,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(8),
//                    isoDate = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 12,
//                    selected = 2,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 2,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(9),
//                    isoDate = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 12,
//                    selected = 1,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 1,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(10),
//                    isoDate = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 12,
//                    selected = 2,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 0,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(11),
//                    isoDate = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 12,
//                    selected = 1,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 1,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(12),
//                    isoDate = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 13,
//                    selected = 2,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 3,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(13),
//                    isoDate = DateTime.UtcNow.AddMonths(13).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 20,
//                    selected = 3,
//                    siv = 0
//                }
//            };

//            TotalSiteStats = new ObservableCollection<SiteStats> {
//                new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(1),
//                    isoDate = DateTime.UtcNow.AddMonths(1).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(2),
//                    isoDate = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 3,
//                    enrolling = 16,
//                    inactive = 2,
//                    isoDateTime = DateTime.UtcNow.AddMonths(3),
//                    isoDate = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 21,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 10,
//                    enrolling = 18,
//                    inactive = 3,
//                    isoDateTime = DateTime.UtcNow.AddMonths(4),
//                    isoDate = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
//                    nonEnrolling = 4,
//                    pssv = 5,
//                    selected = 24,
//                    siv = 1
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(5),
//                    isoDate = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(6),
//                    isoDate = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(7),
//                    isoDate = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(8),
//                    isoDate = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(9),
//                    isoDate = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(10),
//                    isoDate = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(11),
//                    isoDate = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(12),
//                    isoDate = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(13),
//                    isoDate = DateTime.UtcNow.AddMonths(13).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(14),
//                    isoDate = DateTime.UtcNow.AddMonths(14).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(15),
//                    isoDate = DateTime.UtcNow.AddMonths(15).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(16),
//                    isoDate = DateTime.UtcNow.AddMonths(16).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }, new SiteStats {
//                    projectId = projectId,
//                    activated = 20,
//                    closed = 0,
//                    enrolling = 0,
//                    inactive = 0,
//                    isoDateTime = DateTime.UtcNow.AddMonths(17),
//                    isoDate = DateTime.UtcNow.AddMonths(17).ToString("ddMMMyyyy"),
//                    nonEnrolling = 0,
//                    pssv = 4,
//                    selected = 0,
//                    siv = 0
//                }
//            };
//#else
//            try {
//                string url = "https://ecs.incresearch.com/ECS/mobile/sitestats/projectId/" + projectId;

//                HttpClient client = new HttpClient();
//                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

//                HttpResponseMessage response = await client.GetAsync(url);

//                if(response.IsSuccessStatusCode) {
//                    string content = await response.Content.ReadAsStringAsync();

//                    List<SiteStats> siteStats = Utility.DeserializeResponse<List<SiteStats>>(content, "data/project/siteStats");

//                    PlannedSiteStats.Clear();

//                    foreach(SiteStats siteStat in siteStats) {
//                        PlannedSiteStats.Add(siteStat);
//                    }
//                }
//            } catch(Exception ex) {
//                ContentPage page = new ContentPage();
//                await page.DisplayAlert("Error", "Unable to load projects.", "OK");
//            }
//#endif

//            IsBusy = false;
//        }

        private Command _loadProjectCommand;

        /// <summary>
        /// Command to load/refresh project info.
        /// </summary>
        public Command LoadProjectCommand => _loadProjectCommand ?? (_loadProjectCommand = new Command(async () => await ExecuteLoadProjectCommand(_projectId)));

        private async Task ExecuteLoadProjectCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

#if DEBUG
            await Task.FromResult(0);

            Project = new Project {
                customerName           = "Generic Customer",
                owningBu               = "9500 Biometrics",
                phase                  = 4,
                primaryIndication      = "Dry Eye",
                primaryTherapeuticArea = "Psychiatry",
                projectDirector        = "Sally Smith",
                projectId              = projectId,
                protocolId             = "9083-E1-ES3"
            };
#else
            try {
                string url = "https://ecs.incresearch.com/ECS/mobile/project/projectId/" + projectId;

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
#endif

            IsBusy = false;
        }
    }
}