using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

        public const string DefaultSelectedGridStatus = "selected";

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

        ///// <summary>
        ///// gets or sets the planned bottom chart site stats.
        ///// </summary>
        //public ObservableCollection<GraphCategory> PlannedBottomChartSiteStats => new ObservableCollection<GraphCategory>(Utility.GetSiteStatusChartCategories(PlannedSiteStats));

        private ObservableCollection<GraphCategory> _plannedBottomChartSiteStats;

        /// <summary>
        /// gets or sets the planned bottom chart site stats.
        /// </summary>
        public ObservableCollection<GraphCategory> PlannedBottomChartSiteStats {
            get => _plannedBottomChartSiteStats ?? (_plannedBottomChartSiteStats = new ObservableCollection<GraphCategory>());
            set {
                if(_plannedBottomChartSiteStats != value) {
                    _plannedBottomChartSiteStats = value;
                    OnPropertyChanged();
                }
            }
        }

        ///// <summary>
        ///// gets or sets the actual bottom chart site stats.
        ///// </summary>
        //public ObservableCollection<GraphCategory> ActualBottomChartSiteStats => new ObservableCollection<GraphCategory>(Utility.GetSiteStatusChartCategories(ActualSiteStats));

        private ObservableCollection<GraphCategory> _actualBottomChartSiteStats;

        /// <summary>
        /// gets or sets the actual bottom chart site stats.
        /// </summary>
        public ObservableCollection<GraphCategory> ActualBottomChartSiteStats {
            get => _actualBottomChartSiteStats ?? (_actualBottomChartSiteStats = new ObservableCollection<GraphCategory>());
            set {
                if(_actualBottomChartSiteStats != value) {
                    _actualBottomChartSiteStats = value;
                    OnPropertyChanged();
                }
            }
        }

        ///// <summary>
        ///// gets or sets the total bottom chart site stats.
        ///// </summary>
        //public ObservableCollection<GraphCategory> TotalBottomChartSiteStats => new ObservableCollection<GraphCategory>(Utility.GetSiteStatusChartCategories(TotalSiteStats));

        private ObservableCollection<GraphCategory> _totalBottomChartSiteStats;

        /// <summary>
        /// gets or sets the total bottom chart site stats.
        /// </summary>
        public ObservableCollection<GraphCategory> TotalBottomChartSiteStats {
            get => _totalBottomChartSiteStats ?? (_totalBottomChartSiteStats = new ObservableCollection<GraphCategory>());
            set {
                if(_totalBottomChartSiteStats != value) {
                    _totalBottomChartSiteStats = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<SiteTrends> _plannedTopChartSiteTrends;

        /// <summary>
        /// gets or sets the planned site stats.
        /// </summary>
        public ObservableCollection<SiteTrends> PlannedTopChartSiteTrends {
            get => _plannedTopChartSiteTrends ?? (_plannedTopChartSiteTrends = new ObservableCollection<SiteTrends>());
            set {
                if(_plannedTopChartSiteTrends != value) {
                    _plannedTopChartSiteTrends = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<SiteTrends> _actualTopChartSiteTrends;

        /// <summary>
        /// gets or sets the planned site stats.
        /// </summary>
        public ObservableCollection<SiteTrends> ActualTopChartSiteTrends {
            get => _actualTopChartSiteTrends ?? (_actualTopChartSiteTrends = new ObservableCollection<SiteTrends>());
            set {
                if(_actualTopChartSiteTrends != value) {
                    _actualTopChartSiteTrends = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<SiteTrends> _totalTopChartSiteTrends;

        /// <summary>
        /// gets or sets the planned site stats.
        /// </summary>
        public ObservableCollection<SiteTrends> TotalTopChartSiteTrends {
            get => _totalTopChartSiteTrends ?? (_totalTopChartSiteTrends = new ObservableCollection<SiteTrends>());
            set {
                if(_totalTopChartSiteTrends != value) {
                    _totalTopChartSiteTrends = value;
                    OnPropertyChanged();
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

        public string ProjectId;

        public SitesViewModel() {
            Title = "Sites";

            SiteTrends  = new ObservableCollection<SiteTrends>();
            SiteDetails = new ObservableCollection<SiteDetails>();
        }

        public async Task RefreshData(string projectId) {
            await ExecuteLoadProjectCommand(projectId);
            await ExecuteLoadSiteStatsCommand(projectId);
            await ExecuteLoadSiteDetailsCommand(projectId);
            //Big request... takes long to execute
            await ExecuteLoadSiteTrendsCommand(projectId);

            SetTopChartData(DefaultSelectedGridStatus);    //Defaulting to selected row
        }

        private Command _loadSiteDetailsCommand;
        /// <summary>
        /// Command to load/refresh site details.
        /// </summary>
        public Command LoadSiteDetailsCommand => _loadSiteDetailsCommand ?? (_loadSiteDetailsCommand = new Command(async () => await ExecuteLoadSiteDetailsCommand(ProjectId)));

        private async Task ExecuteLoadSiteDetailsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

//#if DEBUG
//            int year = await Task.FromResult(2015);  //Getting rid of compiler warnings

//            SiteDetails = new ObservableCollection<SiteDetails> {
//                new SiteDetails {
//                    projectId      = projectId,
//                    eventType      = "PSSV",
//                    activationDate = Utility.GetDateString(new DateTime(year, 7, 30)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piId           = "Project 2",
//                    piName         = "{redacted}",
//                    siteId         = "100446452",
//                    siteName       = "{redacted}"
//                }, new SiteDetails {
//                    projectId      = projectId,
//                    eventType      = "PSSV",
//                    activationDate = Utility.GetDateString(new DateTime(year, 8, 17)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piId           = "Project 2",
//                    piName         = "{redacted}",
//                    siteId         = "109043379",
//                    siteName       = "{redacted}"
//                }, new SiteDetails {
//                    projectId      = projectId,
//                    eventType      = "PSSV",
//                    activationDate = Utility.GetDateString(new DateTime(year, 9, 21)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piId           = "Project 2",
//                    piName         = "{redacted}",
//                    siteId         = "115286140",
//                    siteName       = "{redacted}"
//                }, new SiteDetails {
//                    projectId      = projectId,
//                    eventType      = "Selected",
//                    activationDate = Utility.GetDateString(new DateTime(year, 9, 21)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piId           = "Project 2",
//                    piName         = "{redacted}",
//                    siteId         = "115286140",
//                    siteName       = "{redacted}"
//                }, new SiteDetails {
//                    projectId      = projectId,
//                    eventType      = "PSSV",
//                    activationDate = Utility.GetDateString(new DateTime(year, 9, 29)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piId           = "Project 2",
//                    piName         = "{redacted}",
//                    siteId         = "115391071",
//                    siteName       = "{redacted}"
//                }, new SiteDetails {
//                    projectId      = projectId,
//                    eventType      = "PSSV",
//                    activationDate = Utility.GetDateString(new DateTime(year, 10, 5)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piId           = "Project 2",
//                    piName         = "{redacted}",
//                    siteId         = "115391062",
//                    siteName       = "{redacted}"
//                }, new SiteDetails {
//                    projectId      = projectId,
//                    eventType      = "PSSV",
//                    activationDate = Utility.GetDateString(new DateTime(year, 10, 6)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piId           = "Project 2",
//                    piName         = "{redacted}",
//                    siteId         = "115391067",
//                    siteName       = "{redacted}"
//                }, new SiteDetails {
//                    projectId      = projectId,
//                    eventType      = "PSSV",
//                    activationDate = Utility.GetDateString(new DateTime(year, 10, 6)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piId           = "Project 2",
//                    piName         = "{redacted}",
//                    siteId         = "115391105",
//                    siteName       = "{redacted}"
//                }
//            };
//#else
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
//#endif

            IsBusy = false;
        }


        private Command _loadSiteTrendsCommand;
        /// <summary>
        /// Command to load/refresh site trends.
        /// </summary>
        public Command LoadSiteTrendsCommand => _loadSiteTrendsCommand ?? (_loadSiteTrendsCommand = new Command(async () => await ExecuteLoadSiteTrendsCommand(ProjectId)));

        private async Task ExecuteLoadSiteTrendsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

//#if DEBUG
//            int year = await Task.FromResult(2016); //Getting rid of compiler warnings

//            SiteTrends = new ObservableCollection<SiteTrends> {
//                new SiteTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSitesColumn",
//                    high      = "0",
//                    low       = "0",
//                    month     = Utility.GetDateString(new DateTime(year, 4, 1)),
//                }, new SiteTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSitesColumn",
//                    high      = "1",
//                    low       = "1",
//                    month     = Utility.GetDateString(new DateTime(year, 5, 1)),
//                }, new SiteTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSitesColumn",
//                    high      = "1",
//                    low       = "1",
//                    month     = Utility.GetDateString(new DateTime(year, 6, 1)),
//                }, new SiteTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSitesColumn",
//                    high      = "2",
//                    low       = "2",
//                    month     = Utility.GetDateString(new DateTime(year, 7, 1)),
//                }, new SiteTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSitesColumn",
//                    high      = "3",
//                    low       = "3",
//                    month     = Utility.GetDateString(new DateTime(year, 8, 1)),
//                }, new SiteTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSitesColumn",
//                    high      = "4",
//                    low       = "4",
//                    month     = Utility.GetDateString(new DateTime(year, 9, 1))
//                }
//            };
//#else
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

                    if(SiteTrends.Count > 0) {
                        PlannedBottomChartSiteStats = new ObservableCollection<GraphCategory>(Utility.GetSiteTrendsNumberOfPlanned(SiteTrends));
                        ActualBottomChartSiteStats  = new ObservableCollection<GraphCategory>(Utility.GetSiteTrendsNumberOfActual(SiteTrends));
                        TotalBottomChartSiteStats   = new ObservableCollection<GraphCategory>(Utility.GetSiteTrendsNumberOfTotal(SiteTrends));
                    }
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load projects.", "OK");
            }
//#endif

            IsBusy = false;
        }


        private Command _loadSiteStatsCommand;
        /// <summary>
        /// Command to load/refresh site stats.
        /// </summary>
        public Command LoadSiteStatsCommand  => _loadSiteStatsCommand ?? (_loadSiteStatsCommand = new Command(async () => await ExecuteLoadSiteStatsCommand(ProjectId)));

        private async Task ExecuteLoadSiteStatsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

//#if DEBUG
            await Task.FromResult(0);

            //PlannedSiteStats = new ObservableCollection<SiteStats> {
            //    new SiteStats {
            //        projectId = projectId,
            //        activated = 3,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(1),
            //        isoDate = DateTime.UtcNow.AddMonths(1).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 0,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 2,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(2),
            //        isoDate = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 5,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 1,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(3),
            //        isoDate = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 14,
            //        selected = 0,
            //        siv = 1
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 1,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(4),
            //        isoDate = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 15,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 1,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(5),
            //        isoDate = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 15,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 2,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(6),
            //        isoDate = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 15,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 2,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(7),
            //        isoDate = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 16,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 4,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(8),
            //        isoDate = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 16,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 1,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(9),
            //        isoDate = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 17,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 2,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(10),
            //        isoDate = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 17,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 0,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(11),
            //        isoDate = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 19,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 1,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(12),
            //        isoDate = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 20,
            //        selected = 0,
            //        siv = 0
            //    }
            //};

            //ActualSiteStats = new ObservableCollection<SiteStats> {
            //    new SiteStats {
            //        projectId = projectId,
            //        activated = 4,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(2),
            //        isoDate = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 2,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 2,
            //        closed = 3,
            //        enrolling = 16,
            //        inactive = 2,
            //        isoDateTime = DateTime.UtcNow.AddMonths(3),
            //        isoDate = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 10,
            //        selected = 1,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 1,
            //        closed = 10,
            //        enrolling = 18,
            //        inactive = 3,
            //        isoDateTime = DateTime.UtcNow.AddMonths(4),
            //        isoDate = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
            //        nonEnrolling = 4,
            //        pssv = 10,
            //        selected = 2,
            //        siv = 1
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 0,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(5),
            //        isoDate = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 11,
            //        selected = 3,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 2,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(6),
            //        isoDate = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 11,
            //        selected = 2,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 1,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(7),
            //        isoDate = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 12,
            //        selected = 1,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 3,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(8),
            //        isoDate = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 12,
            //        selected = 2,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 2,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(9),
            //        isoDate = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 12,
            //        selected = 1,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 1,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(10),
            //        isoDate = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 12,
            //        selected = 2,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 0,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(11),
            //        isoDate = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 12,
            //        selected = 1,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 1,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(12),
            //        isoDate = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 13,
            //        selected = 2,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 3,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(13),
            //        isoDate = DateTime.UtcNow.AddMonths(13).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 20,
            //        selected = 3,
            //        siv = 0
            //    }
            //};

            //TotalSiteStats = new ObservableCollection<SiteStats> {
            //    new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(1),
            //        isoDate = DateTime.UtcNow.AddMonths(1).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(2),
            //        isoDate = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 3,
            //        enrolling = 16,
            //        inactive = 2,
            //        isoDateTime = DateTime.UtcNow.AddMonths(3),
            //        isoDate = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 21,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 10,
            //        enrolling = 18,
            //        inactive = 3,
            //        isoDateTime = DateTime.UtcNow.AddMonths(4),
            //        isoDate = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
            //        nonEnrolling = 4,
            //        pssv = 5,
            //        selected = 24,
            //        siv = 1
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(5),
            //        isoDate = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(6),
            //        isoDate = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(7),
            //        isoDate = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(8),
            //        isoDate = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(9),
            //        isoDate = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(10),
            //        isoDate = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(11),
            //        isoDate = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(12),
            //        isoDate = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(13),
            //        isoDate = DateTime.UtcNow.AddMonths(13).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(14),
            //        isoDate = DateTime.UtcNow.AddMonths(14).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(15),
            //        isoDate = DateTime.UtcNow.AddMonths(15).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(16),
            //        isoDate = DateTime.UtcNow.AddMonths(16).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }, new SiteStats {
            //        projectId = projectId,
            //        activated = 20,
            //        closed = 0,
            //        enrolling = 0,
            //        inactive = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(17),
            //        isoDate = DateTime.UtcNow.AddMonths(17).ToString("ddMMMyyyy"),
            //        nonEnrolling = 0,
            //        pssv = 4,
            //        selected = 0,
            //        siv = 0
            //    }
            //};
//#else //TODO: Figure out how to separate out planned, actual and total site stats from each other
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

            IsBusy = false;
        }

        private Command _loadProjectCommand;
        /// <summary>
        /// Command to load/refresh project info.
        /// </summary>
        public Command LoadProjectCommand => _loadProjectCommand ?? (_loadProjectCommand = new Command(async () => await ExecuteLoadProjectCommand(ProjectId)));

        private async Task ExecuteLoadProjectCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

//#if DEBUG
//            await Task.FromResult(0);

//            Project = new Project {
//                customerName           = "Generic Customer",
//                owningBu               = "9500 Biometrics",
//                phase                  = 4,
//                primaryIndication      = "Dry Eye",
//                primaryTherapeuticArea = "Psychiatry",
//                projectDirector        = "Sally Smith",
//                projectId              = projectId,
//                protocolId             = "9083-E1-ES3"
//            };
//#else
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
//#endif

            IsBusy = false;
        }

        public void SetTopChartData(string status) {

            status = status.ToLowerInvariant() + Utility.SiteTrendEventTypePostFix;

            if(SiteTrends.IsNullOrEmpty()) {
                return;
            }

            List<SiteTrends> groupedTrends = Utility.FilterSiteTrends(SiteTrends).Where(trend => trend.eventType.ToLowerInvariant() == status).OrderBy(trend => trend.MonthDateTime).ToList();

            PlannedTopChartSiteTrends = new ObservableCollection<SiteTrends>(groupedTrends.Where(trend => !string.IsNullOrEmpty(trend.high)));
            ActualTopChartSiteTrends  = new ObservableCollection<SiteTrends>(groupedTrends.Where(trend => !string.IsNullOrEmpty(trend.actual)));

            List<SiteTrends> totalSiteTrends = groupedTrends.Where(trend => !string.IsNullOrEmpty(trend.ceiling)).ToList();

            if(totalSiteTrends.Count < 1) { //BUG: If the total collection is empty, neither of the other 2 collections displays properly
                totalSiteTrends.Add(new SiteTrends {
                    ceiling = "10",
                    month   = "2016-01"
                });
            }

            TotalTopChartSiteTrends   = new ObservableCollection<SiteTrends>(totalSiteTrends);
        }
    }
}