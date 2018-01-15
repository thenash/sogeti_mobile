using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Connect.Models;
using Xamarin.Forms;
using Connect.Helpers;

#if !DEBUG
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
#endif

namespace Connect.ViewModels {

    public class SitesViewModel : BaseViewModel {

        private ObservableCollection<SiteDetails> _siteDetails;

        /// <summary>
        /// gets or sets the feed items
        /// </summary>
        public ObservableCollection<SiteDetails> SiteDetails {
            get => _siteDetails ?? (_siteDetails = new ObservableCollection<SiteDetails>());
            set {
                if(_siteDetails != value) {
                    _siteDetails = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// gets or sets the planned bottom chart site stats.
        /// </summary>
        public ObservableCollection<GraphCategory> PlannedBottomChartSiteStats => new ObservableCollection<GraphCategory>(Utility.GetSiteStatusChartCategories(PlannedSiteStats));

        /// <summary>
        /// gets or sets the actual bottom chart site stats.
        /// </summary>
        public ObservableCollection<GraphCategory> ActualBottomChartSiteStats => new ObservableCollection<GraphCategory>(Utility.GetSiteStatusChartCategories(ActualSiteStats));

        /// <summary>
        /// gets or sets the total bottom chart site stats.
        /// </summary>
        public ObservableCollection<GraphCategory> TotalBottomChartSiteStats => new ObservableCollection<GraphCategory>(Utility.GetSiteStatusChartCategories(TotalSiteStats));

        private ObservableCollection<SiteStats> _plannedSiteStats;

        /// <summary>
	    /// gets or sets the planned site stats.
	    /// </summary>
		public ObservableCollection<SiteStats> PlannedSiteStats {
            get => _plannedSiteStats ?? (_plannedSiteStats = new ObservableCollection<SiteStats>());
            set {
                if(_plannedSiteStats != value) {
                    _plannedSiteStats = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PlannedBottomChartSiteStats));
                }
            }
        }

        private ObservableCollection<SiteStats> _actualSiteStats;

        /// <summary>
        /// gets or sets the planned site stats.
        /// </summary>
        public ObservableCollection<SiteStats> ActualSiteStats {
            get => _actualSiteStats ?? (_actualSiteStats = new ObservableCollection<SiteStats>());
            set {
                if(_actualSiteStats != value) {
                    _actualSiteStats = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ActualBottomChartSiteStats));
                }
            }
        }

        private ObservableCollection<SiteStats> _totalSiteStats;

        /// <summary>
        /// gets or sets the planned site stats.
        /// </summary>
        public ObservableCollection<SiteStats> TotalSiteStats {
            get => _totalSiteStats ?? (_totalSiteStats = new ObservableCollection<SiteStats>());
            set {
                if(_totalSiteStats != value) {
                    _totalSiteStats = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalBottomChartSiteStats));
                }
            }
        }

        private ObservableCollection<SiteTrends> _siteTrends;

        /// <summary>
        /// gets or sets the feed items
        /// </summary>
        public ObservableCollection<SiteTrends> SiteTrends {
            get => _siteTrends ?? (_siteTrends = new ObservableCollection<SiteTrends>());
            set {
                if(_siteTrends != value) {
                    _siteTrends = value;
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

        public SitesViewModel(string projectId) {
            Title = "Sites";

            _projectId = projectId;

            PlannedSiteStats = new ObservableCollection<SiteStats>();
            ActualSiteStats  = new ObservableCollection<SiteStats>();
            TotalSiteStats   = new ObservableCollection<SiteStats>();
            SiteTrends       = new ObservableCollection<SiteTrends>();
            SiteDetails      = new ObservableCollection<SiteDetails>();
        }

        public async Task RefreshData(string projectId) {
            await ExecuteLoadProjectCommand(projectId);
            await ExecuteLoadSiteStatsCommand(projectId);
            await ExecuteLoadSiteDetailsCommand(projectId);
            //Big request... takes long to execute
            await ExecuteLoadSiteTrendsCommand(projectId);
        }

        private Command _loadSiteDetailsCommand;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadSiteDetailsCommand => _loadSiteDetailsCommand ?? (_loadSiteDetailsCommand = new Command(async () => await ExecuteLoadSiteDetailsCommand(_projectId)));

        private async Task ExecuteLoadSiteDetailsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

#if DEBUG
            int year = await Task.FromResult(2015);  //Getting rid of compiler warnings

            SiteDetails = new ObservableCollection<SiteDetails> {
                new SiteDetails {
                    projectId      = projectId,
                    eventType      = "PSSV",
                    activationDate = Utility.GetDateString(new DateTime(year, 7, 30)),
                    country        = "USA",
                    dataSource     = "Medidata CTMS",
                    piId           = "Project 2",
                    piName         = "{redacted}",
                    siteId         = "100446452",
                    siteName       = "{redacted}"
                }, new SiteDetails {
                    projectId      = projectId,
                    eventType      = "PSSV",
                    activationDate = Utility.GetDateString(new DateTime(year, 8, 17)),
                    country        = "USA",
                    dataSource     = "Medidata CTMS",
                    piId           = "Project 2",
                    piName         = "{redacted}",
                    siteId         = "109043379",
                    siteName       = "{redacted}"
                }, new SiteDetails {
                    projectId      = projectId,
                    eventType      = "PSSV",
                    activationDate = Utility.GetDateString(new DateTime(year, 9, 21)),
                    country        = "USA",
                    dataSource     = "Medidata CTMS",
                    piId           = "Project 2",
                    piName         = "{redacted}",
                    siteId         = "115286140",
                    siteName       = "{redacted}"
                }, new SiteDetails {
                    projectId      = projectId,
                    eventType      = "Selected",
                    activationDate = Utility.GetDateString(new DateTime(year, 9, 21)),
                    country        = "USA",
                    dataSource     = "Medidata CTMS",
                    piId           = "Project 2",
                    piName         = "{redacted}",
                    siteId         = "115286140",
                    siteName       = "{redacted}"
                }, new SiteDetails {
                    projectId      = projectId,
                    eventType      = "PSSV",
                    activationDate = Utility.GetDateString(new DateTime(year, 9, 29)),
                    country        = "USA",
                    dataSource     = "Medidata CTMS",
                    piId           = "Project 2",
                    piName         = "{redacted}",
                    siteId         = "115391071",
                    siteName       = "{redacted}"
                }, new SiteDetails {
                    projectId      = projectId,
                    eventType      = "PSSV",
                    activationDate = Utility.GetDateString(new DateTime(year, 10, 5)),
                    country        = "USA",
                    dataSource     = "Medidata CTMS",
                    piId           = "Project 2",
                    piName         = "{redacted}",
                    siteId         = "115391062",
                    siteName       = "{redacted}"
                }, new SiteDetails {
                    projectId      = projectId,
                    eventType      = "PSSV",
                    activationDate = Utility.GetDateString(new DateTime(year, 10, 6)),
                    country        = "USA",
                    dataSource     = "Medidata CTMS",
                    piId           = "Project 2",
                    piName         = "{redacted}",
                    siteId         = "115391067",
                    siteName       = "{redacted}"
                }, new SiteDetails {
                    projectId      = projectId,
                    eventType      = "PSSV",
                    activationDate = Utility.GetDateString(new DateTime(year, 10, 6)),
                    country        = "USA",
                    dataSource     = "Medidata CTMS",
                    piId           = "Project 2",
                    piName         = "{redacted}",
                    siteId         = "115391105",
                    siteName       = "{redacted}"
                }
            };
#else
            try {
                string url = "https://ecs.incresearch.com/ECS/mobile/sitedetails/projectId/" + projectId;

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
#endif

            IsBusy = false;
        }


        private Command _loadSiteTrendsCommand;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadSiteTrendsCommand => _loadSiteTrendsCommand ?? (_loadSiteTrendsCommand = new Command(async () => await ExecuteLoadSiteTrendsCommand(_projectId)));

        private async Task ExecuteLoadSiteTrendsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

#if DEBUG
            int year = await Task.FromResult(2016); //Getting rid of compiler warnings

            SiteTrends = new ObservableCollection<SiteTrends> {
                new SiteTrends {
                    projectId = projectId,
                    actual    = "8",
                    ceiling   = "8",
                    eventType = "activeSitesColumn",
                    high      = "0",
                    low       = "0",
                    month     = Utility.GetDateString(new DateTime(year, 4, 1)),
                }, new SiteTrends {
                    projectId = projectId,
                    actual    = "8",
                    ceiling   = "8",
                    eventType = "activeSitesColumn",
                    high      = "1",
                    low       = "1",
                    month     = Utility.GetDateString(new DateTime(year, 5, 1)),
                }, new SiteTrends {
                    projectId = projectId,
                    actual    = "8",
                    ceiling   = "8",
                    eventType = "activeSitesColumn",
                    high      = "1",
                    low       = "1",
                    month     = Utility.GetDateString(new DateTime(year, 6, 1)),
                }, new SiteTrends {
                    projectId = projectId,
                    actual    = "8",
                    ceiling   = "8",
                    eventType = "activeSitesColumn",
                    high      = "2",
                    low       = "2",
                    month     = Utility.GetDateString(new DateTime(year, 7, 1)),
                }, new SiteTrends {
                    projectId = projectId,
                    actual    = "8",
                    ceiling   = "8",
                    eventType = "activeSitesColumn",
                    high      = "3",
                    low       = "3",
                    month     = Utility.GetDateString(new DateTime(year, 8, 1)),
                }, new SiteTrends {
                    projectId = projectId,
                    actual    = "8",
                    ceiling   = "8",
                    eventType = "activeSitesColumn",
                    high      = "4",
                    low       = "4",
                    month     = Utility.GetDateString(new DateTime(year, 9, 1))
                }
            };
#else
            try {
                string url = "https://ecs.incresearch.com/ECS/mobile/sitetrends/projectId/" + projectId;

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
#endif

            IsBusy = false;
        }


        private Command _loadSiteStatsCommand;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadSiteStatsCommand  => _loadSiteStatsCommand ?? (_loadSiteStatsCommand = new Command(async () => await ExecuteLoadSiteStatsCommand(_projectId)));

        private async Task ExecuteLoadSiteStatsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

#if DEBUG
            await Task.FromResult(0);

            PlannedSiteStats = new ObservableCollection<SiteStats> {
                new SiteStats {
                    projectId = projectId,
                    activated = 3,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(1),
                    isoDate = DateTime.UtcNow.AddMonths(1).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 0,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 2,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(2),
                    isoDate = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 5,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 1,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(3),
                    isoDate = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 14,
                    selected = 0,
                    siv = 1
                }, new SiteStats {
                    projectId = projectId,
                    activated = 1,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(4),
                    isoDate = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 15,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 1,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(5),
                    isoDate = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 15,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 2,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(6),
                    isoDate = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 15,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 2,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(7),
                    isoDate = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 16,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 4,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(8),
                    isoDate = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 16,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 1,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(9),
                    isoDate = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 17,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 2,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(10),
                    isoDate = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 17,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 0,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(11),
                    isoDate = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 19,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 1,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(12),
                    isoDate = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 20,
                    selected = 0,
                    siv = 0
                }
            };

            ActualSiteStats = new ObservableCollection<SiteStats> {
                new SiteStats {
                    projectId = projectId,
                    activated = 4,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(2),
                    isoDate = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 2,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 2,
                    closed = 3,
                    enrolling = 16,
                    inactive = 2,
                    isoDateTime = DateTime.UtcNow.AddMonths(3),
                    isoDate = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 10,
                    selected = 1,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 1,
                    closed = 10,
                    enrolling = 18,
                    inactive = 3,
                    isoDateTime = DateTime.UtcNow.AddMonths(4),
                    isoDate = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
                    nonEnrolling = 4,
                    pssv = 10,
                    selected = 2,
                    siv = 1
                }, new SiteStats {
                    projectId = projectId,
                    activated = 0,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(5),
                    isoDate = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 11,
                    selected = 3,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 2,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(6),
                    isoDate = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 11,
                    selected = 2,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 1,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(7),
                    isoDate = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 12,
                    selected = 1,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 3,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(8),
                    isoDate = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 12,
                    selected = 2,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 2,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(9),
                    isoDate = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 12,
                    selected = 1,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 1,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(10),
                    isoDate = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 12,
                    selected = 2,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 0,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(11),
                    isoDate = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 12,
                    selected = 1,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 1,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(12),
                    isoDate = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 13,
                    selected = 2,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 3,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(13),
                    isoDate = DateTime.UtcNow.AddMonths(13).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 20,
                    selected = 3,
                    siv = 0
                }
            };

            TotalSiteStats = new ObservableCollection<SiteStats> {
                new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(1),
                    isoDate = DateTime.UtcNow.AddMonths(1).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(2),
                    isoDate = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 3,
                    enrolling = 16,
                    inactive = 2,
                    isoDateTime = DateTime.UtcNow.AddMonths(3),
                    isoDate = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 21,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 10,
                    enrolling = 18,
                    inactive = 3,
                    isoDateTime = DateTime.UtcNow.AddMonths(4),
                    isoDate = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
                    nonEnrolling = 4,
                    pssv = 5,
                    selected = 24,
                    siv = 1
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(5),
                    isoDate = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(6),
                    isoDate = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(7),
                    isoDate = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(8),
                    isoDate = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(9),
                    isoDate = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(10),
                    isoDate = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(11),
                    isoDate = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(12),
                    isoDate = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(13),
                    isoDate = DateTime.UtcNow.AddMonths(13).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(14),
                    isoDate = DateTime.UtcNow.AddMonths(14).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(15),
                    isoDate = DateTime.UtcNow.AddMonths(15).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(16),
                    isoDate = DateTime.UtcNow.AddMonths(16).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }, new SiteStats {
                    projectId = projectId,
                    activated = 20,
                    closed = 0,
                    enrolling = 0,
                    inactive = 0,
                    isoDateTime = DateTime.UtcNow.AddMonths(17),
                    isoDate = DateTime.UtcNow.AddMonths(17).ToString("ddMMMyyyy"),
                    nonEnrolling = 0,
                    pssv = 4,
                    selected = 0,
                    siv = 0
                }
            };
#else
            try {
                string url = "https://ecs.incresearch.com/ECS/mobile/sitestats/projectId/" + projectId;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    List<SiteStats> siteStats = Utility.DeserializeResponse<List<SiteStats>>(content, "data/project/siteStats");

                    PlannedSiteStats.Clear();

                    foreach(SiteStats siteStat in siteStats) {
                        PlannedSiteStats.Add(siteStat);
                    }
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load projects.", "OK");
            }
#endif

            IsBusy = false;
        }

        private Command _loadProjectCommand;
        /// <summary>
        /// Command to load/refresh artitists
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