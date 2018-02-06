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

    public class SubjectsViewModel : BaseViewModel {

        public const string DefaultSelectedGridStatus = "screened";

        private ObservableCollection<SubjectDetails> _sibjectDetails;

        /// <summary>
        /// gets or sets the feed items
        /// </summary>
        public ObservableCollection<SubjectDetails> SubjectDetails {
            get => _sibjectDetails ?? (_sibjectDetails = new ObservableCollection<SubjectDetails>());
            set {
                if(_sibjectDetails != value) {
                    _sibjectDetails = value;
                    OnPropertyChanged();
                }
            }
        }

        ///// <summary>
        ///// gets or sets the planned bottom chart subject stats.
        ///// </summary>
        //public ObservableCollection<GraphCategory> PlannedBottomChartSubjectStats => new ObservableCollection<GraphCategory>(Utility.GetSubjectStatusChartCategories(PlannedSubjectStats));

        private ObservableCollection<GraphCategory> _plannedBottomChartSubjectStats;

        /// <summary>
        /// gets or sets the planned bottom chart subject stats.
        /// </summary>
        public ObservableCollection<GraphCategory> PlannedBottomChartSubjectStats {
            get => _plannedBottomChartSubjectStats ?? (_plannedBottomChartSubjectStats = new ObservableCollection<GraphCategory>());
            set {
                if(_plannedBottomChartSubjectStats != value) {
                    _plannedBottomChartSubjectStats = value;
                    OnPropertyChanged();
                }
            }
        }

        ///// <summary>
        ///// gets or sets the actual bottom chart subject stats.
        ///// </summary>
        //public ObservableCollection<GraphCategory> ActualBottomChartSubjectStats => new ObservableCollection<GraphCategory>(Utility.GetSubjectStatusChartCategories(ActualSubjectStats));

        private ObservableCollection<GraphCategory> _actualBottomChartSubjectStats;

        /// <summary>
        /// gets or sets the actual bottom chart subject stats.
        /// </summary>
        public ObservableCollection<GraphCategory> ActualBottomChartSubjectStats {
            get => _actualBottomChartSubjectStats ?? (_actualBottomChartSubjectStats = new ObservableCollection<GraphCategory>());
            set {
                if(_actualBottomChartSubjectStats != value) {
                    _actualBottomChartSubjectStats = value;
                    OnPropertyChanged();
                }
            }
        }

        ///// <summary>
        ///// gets or sets the total bottom chart subject stats.
        ///// </summary>
        //public ObservableCollection<GraphCategory> TotalBottomChartSubjectStats => new ObservableCollection<GraphCategory>(Utility.GetSubjectStatusChartCategories(TotalSubjectStats));

        private ObservableCollection<GraphCategory> _totalBottomChartSubjectStats;

        /// <summary>
        /// gets or sets the total bottom chart subject stats.
        /// </summary>
        public ObservableCollection<GraphCategory> TotalBottomChartSubjectStats {
            get => _totalBottomChartSubjectStats ?? (_totalBottomChartSubjectStats = new ObservableCollection<GraphCategory>());
            set {
                if(_totalBottomChartSubjectStats != value) {
                    _totalBottomChartSubjectStats = value;
                    OnPropertyChanged();
                }
            }
        }

        ///// <summary>
        ///// gets or sets the total bottom chart subject stats.
        ///// </summary>
        //public ObservableCollection<GraphCategory> MonthlyRateBottomChartSubjectStats => new ObservableCollection<GraphCategory>(Utility.GetSubjectStatusChartCategories(MonthlyRateSubjectStats));

        private ObservableCollection<GraphCategory> _monthlyRateBottomChartSubjectStats;

        /// <summary>
        /// gets or sets the monthly rate bottom chart subject stats.
        /// </summary>
        public ObservableCollection<GraphCategory> MonthlyRateBottomChartSubjectStats {
            get => _monthlyRateBottomChartSubjectStats ?? (_monthlyRateBottomChartSubjectStats = new ObservableCollection<GraphCategory>());
            set {
                if(_monthlyRateBottomChartSubjectStats != value) {
                    _monthlyRateBottomChartSubjectStats = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<SubjectTrends> _plannedTopChartSubjectTrends;

        /// <summary>
	    /// gets or sets the planned subject stats.
	    /// </summary>
		public ObservableCollection<SubjectTrends> PlannedTopChartSubjectTrends {
            get => _plannedTopChartSubjectTrends ?? (_plannedTopChartSubjectTrends = new ObservableCollection<SubjectTrends>());
            set {
                if(_plannedTopChartSubjectTrends != value) {
                    _plannedTopChartSubjectTrends = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<SubjectTrends> _actualTopChartSubjectTrends;

        /// <summary>
        /// gets or sets the planned subject stats.
        /// </summary>
        public ObservableCollection<SubjectTrends> ActualTopChartSubjectTrends {
            get => _actualTopChartSubjectTrends ?? (_actualTopChartSubjectTrends = new ObservableCollection<SubjectTrends>());
            set {
                if(_actualTopChartSubjectTrends != value) {
                    _actualTopChartSubjectTrends = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<SubjectTrends> _totalTopChartSubjectTrends;

        /// <summary>
        /// gets or sets the planned subject stats.
        /// </summary>
        public ObservableCollection<SubjectTrends> TotalTopChartSubjectTrends {
            get => _totalTopChartSubjectTrends ?? (_totalTopChartSubjectTrends = new ObservableCollection<SubjectTrends>());
            set {
                if(_totalTopChartSubjectTrends != value) {
                    _totalTopChartSubjectTrends = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<SubjectTrends> _monthlyRateTopChartSubjectTrends;

        /// <summary>
        /// gets or sets the monthly rate subject stats.
        /// </summary>
        public ObservableCollection<SubjectTrends> MonthlyRateTopChartSubjectTrends {
            get => _monthlyRateTopChartSubjectTrends ?? (_monthlyRateTopChartSubjectTrends = new ObservableCollection<SubjectTrends>());
            set {
                if(_monthlyRateTopChartSubjectTrends != value) {
                    _monthlyRateTopChartSubjectTrends = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<SubjectTrends> _subjectTrends;

        /// <summary>
        /// gets or sets the subject trends.
        /// </summary>
        public ObservableCollection<SubjectTrends> SubjectTrends {
            get => _subjectTrends ?? (_subjectTrends = new ObservableCollection<SubjectTrends>());
            set {
                if(_subjectTrends != value) {
                    _subjectTrends = value;
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

        public SubjectsViewModel(string projectId) {
            Title = "Subjects";

            _projectId = projectId;

            SubjectTrends  = new ObservableCollection<SubjectTrends>();
            SubjectDetails = new ObservableCollection<SubjectDetails>();
        }

        public async Task RefreshData(string projectId) {
            await ExecuteLoadProjectCommand(projectId);
            await ExecuteLoadSubjectStatsCommand(projectId);
            await ExecuteLoadSubjectDetailsCommand(projectId);
            //Big request... takes long to execute
            await ExecuteLoadSubjectTrendsCommand(projectId);

            SetTopChartData(DefaultSelectedGridStatus);    //Defaulting to selected row
        }

        private Command _loadSubjectDetailsCommand;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadSubjectDetailsCommand => _loadSubjectDetailsCommand ?? (_loadSubjectDetailsCommand = new Command(async () => await ExecuteLoadSubjectDetailsCommand(_projectId)));

        private async Task ExecuteLoadSubjectDetailsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

//#if DEBUG
//            int year = await Task.FromResult(2015);  //Getting rid of compiler warnings

//            SubjectDetails = new ObservableCollection<SubjectDetails> {
//                new SubjectDetails {
//                    projectId      = projectId,
//                    subjectId      = "171608722",
//                    isoDate        = Utility.GetDateString(new DateTime(year, 1, 4)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piName         = "{redacted}",
//                    siteId         = "100446452",
//                    siteName       = "{redacted}",
//                    subjectStatus  = "Screened",
//                    siteStatus     = "Enrolling"
//                }, new SubjectDetails {
//                    projectId      = projectId,
//                    subjectId      = "171608722",
//                    isoDate        = Utility.GetDateString(new DateTime(year, 1, 15)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piName         = "{redacted}",
//                    siteId         = "109043379",
//                    siteName       = "{redacted}",
//                    subjectStatus  = "Enrolling"
//                }, new SubjectDetails {
//                    projectId      = projectId,
//                    subjectId      = "171608722",
//                    isoDate        = Utility.GetDateString(new DateTime(year, 1, 15)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piName         = "{redacted}",
//                    siteId         = "115286140",
//                    siteName       = "{redacted}",
//                    subjectStatus  = "Enrolling"
//                }, new SubjectDetails {
//                    projectId      = projectId,
//                    subjectId      = "Selected",
//                    isoDate        = Utility.GetDateString(new DateTime(year, 1, 28)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piName         = "{redacted}",
//                    siteId         = "115286140",
//                    siteName       = "{redacted}",
//                    subjectStatus  = "Enrolling"
//                }, new SubjectDetails {
//                    projectId      = projectId,
//                    subjectId      = "171608722",
//                    isoDate = Utility.GetDateString(new DateTime(year, 2, 1)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piName         = "{redacted}",
//                    siteId         = "115391071",
//                    siteName       = "{redacted}",
//                    subjectStatus  = "Enrolling"
//                }, new SubjectDetails {
//                    projectId      = projectId,
//                    subjectId      = "171608722",
//                    isoDate = Utility.GetDateString(new DateTime(year, 2, 3)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piName         = "{redacted}",
//                    siteId         = "115391062",
//                    siteName       = "{redacted}",
//                    subjectStatus  = "Enrolling"
//                }, new SubjectDetails {
//                    projectId      = projectId,
//                    subjectId      = "171608722",
//                    isoDate = Utility.GetDateString(new DateTime(year, 2, 11)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piName         = "{redacted}",
//                    siteId         = "115391067",
//                    siteName       = "{redacted}",
//                    subjectStatus  = "Inactive"
//                }, new SubjectDetails {
//                    projectId      = projectId,
//                    subjectId      = "171608722",
//                    isoDate = Utility.GetDateString(new DateTime(year, 2, 12)),
//                    country        = "USA",
//                    dataSource     = "Medidata CTMS",
//                    piName         = "{redacted}",
//                    siteId         = "115391105",
//                    siteName       = "{redacted}",
//                    subjectStatus  = "Activated"
//                }
//            };
//#else
            try {
                string url = "https://ecs.incresearch.com/ECS/mobile/subjectdetails/projectId/" + projectId;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    List<SubjectDetails> subjectDetails = Utility.DeserializeResponse<List<SubjectDetails>>(content, "data/subjects/details");

                    SubjectDetails.Clear();

                    foreach(SubjectDetails subjectDetail in subjectDetails) {
                        SubjectDetails.Add(subjectDetail);
                    }
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load projects.", "OK");
            }
//#endif

            IsBusy = false;
        }


        private Command _loadSubjectTrendsCommand;
        /// <summary>
        /// Command to load/refresh artitists
        /// </summary>
        public Command LoadSubjectTrendsCommand => _loadSubjectTrendsCommand ?? (_loadSubjectTrendsCommand = new Command(async () => await ExecuteLoadSubjectTrendsCommand(_projectId)));

        private async Task ExecuteLoadSubjectTrendsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

//#if DEBUG
//            int year = await Task.FromResult(2016); //Getting rid of compiler warnings

//            SubjectTrends = new ObservableCollection<SubjectTrends> {
//                new SubjectTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSubjectsColumn",
//                    high      = "0",
//                    low       = "0",
//                    month     = Utility.GetDateString(new DateTime(year, 4, 1)),
//                }, new SubjectTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSubjectsColumn",
//                    high      = "1",
//                    low       = "1",
//                    month     = Utility.GetDateString(new DateTime(year, 5, 1)),
//                }, new SubjectTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSubjectsColumn",
//                    high      = "1",
//                    low       = "1",
//                    month     = Utility.GetDateString(new DateTime(year, 6, 1)),
//                }, new SubjectTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSubjectsColumn",
//                    high      = "2",
//                    low       = "2",
//                    month     = Utility.GetDateString(new DateTime(year, 7, 1)),
//                }, new SubjectTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSubjectsColumn",
//                    high      = "3",
//                    low       = "3",
//                    month     = Utility.GetDateString(new DateTime(year, 8, 1)),
//                }, new SubjectTrends {
//                    projectId = projectId,
//                    actual    = "8",
//                    ceiling   = "8",
//                    eventType = "activeSubjectsColumn",
//                    high      = "4",
//                    low       = "4",
//                    month     = Utility.GetDateString(new DateTime(year, 9, 1))
//                }
//            };
//#else
            try {
                string url = "https://ecs.incresearch.com/ECS/mobile/subtrends/projectId/" + projectId;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

                HttpResponseMessage response = await client.GetAsync(url);

                if(response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    List<SubjectTrends> subjectTrends = Utility.DeserializeResponse<List<SubjectTrends>>(content, "data/project/subjectTrends");

                    SubjectTrends.Clear();

                    foreach(SubjectTrends subjectTrend in subjectTrends) {
                        SubjectTrends.Add(subjectTrend);
                    }

                    if(SubjectTrends.Count > 0) {
                        PlannedBottomChartSubjectStats     = new ObservableCollection<GraphCategory>(Utility.GetSubjectTrendsNumberOfPlanned(SubjectTrends));
                        ActualBottomChartSubjectStats      = new ObservableCollection<GraphCategory>(Utility.GetSubjectTrendsNumberOfActual(SubjectTrends));
                        TotalBottomChartSubjectStats       = new ObservableCollection<GraphCategory>(Utility.GetSubjectTrendsNumberOfTotal(SubjectTrends));
                        MonthlyRateBottomChartSubjectStats = new ObservableCollection<GraphCategory>(Utility.GetSubjectTrendsMonthlyRate(SubjectTrends));
                    }
                }
            } catch(Exception ex) {
                ContentPage page = new ContentPage();
                await page.DisplayAlert("Error", "Unable to load projects.", "OK");
            }
//#endif

            IsBusy = false;
        }


        private Command _loadSubjectStatsCommand;
        /// <summary>
        /// Command to load/refresh Subject Stats.
        /// </summary>
        public Command LoadSubjectStatsCommand  => _loadSubjectStatsCommand ?? (_loadSubjectStatsCommand = new Command(async () => await ExecuteLoadSubjectStatsCommand(_projectId)));

        private async Task ExecuteLoadSubjectStatsCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

//#if DEBUG
            //await Task.FromResult(0);

            //PlannedSubjectStats = new ObservableCollection<SubjectStats> {
            //    new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 5,
            //        enrolled    = 1,
            //        early_Term  = 0,
            //        complete    = 3,
            //        isoDateTime = DateTime.UtcNow.AddMonths(1),
            //        isoDate     = DateTime.UtcNow.AddMonths(1).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 0,
            //        safetySae   = 5
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 3,
            //        enrolled    = 7,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(2),
            //        isoDate     = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 5,
            //        safetySae   = 9
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 8,
            //        enrolled    = 6,
            //        early_Term  = 1,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(3),
            //        isoDate     = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 14,
            //        safetySae   = 10
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 9,
            //        enrolled    = 5,
            //        early_Term  = 3,
            //        complete    = 2,
            //        isoDateTime = DateTime.UtcNow.AddMonths(4),
            //        isoDate     = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 15,
            //        safetySae   = 13
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 7,
            //        enrolled    = 6,
            //        early_Term  = 1,
            //        complete    = 1,
            //        isoDateTime = DateTime.UtcNow.AddMonths(5),
            //        isoDate     = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 15,
            //        safetySae   = 13
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 8,
            //        enrolled    = 8,
            //        early_Term  = 0,
            //        complete    = 3,
            //        isoDateTime = DateTime.UtcNow.AddMonths(6),
            //        isoDate     = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 15,
            //        safetySae   = 15
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 10,
            //        enrolled    = 7,
            //        early_Term  = 2,
            //        complete    = 2,
            //        isoDateTime = DateTime.UtcNow.AddMonths(7),
            //        isoDate     = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 16,
            //        safetySae   = 15
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 1,
            //        enrolled    = 3,
            //        early_Term  = 0,
            //        complete    = 2,
            //        isoDateTime = DateTime.UtcNow.AddMonths(8),
            //        isoDate     = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 16,
            //        safetySae   = 15
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 4,
            //        enrolled    = 6,
            //        early_Term  = 2,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(9),
            //        isoDate     = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 17,
            //        safetySae   = 20
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 6,
            //        enrolled    = 1,
            //        early_Term  = 1,
            //        complete    = 1,
            //        isoDateTime = DateTime.UtcNow.AddMonths(10),
            //        isoDate     = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 17,
            //        safetySae   = 25
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 6,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 1,
            //        isoDateTime = DateTime.UtcNow.AddMonths(11),
            //        isoDate     = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 19,
            //        safetySae   = 30
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 13,
            //        enrolled    = 1,
            //        early_Term  = 3,
            //        complete    = 4,
            //        isoDateTime = DateTime.UtcNow.AddMonths(12),
            //        isoDate     = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 35
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(13),
            //        isoDate     = DateTime.UtcNow.AddMonths(13).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 35
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(14),
            //        isoDate     = DateTime.UtcNow.AddMonths(14).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 40
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(15),
            //        isoDate     = DateTime.UtcNow.AddMonths(15).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 50
            //    }
            //};

            //ActualSubjectStats = new ObservableCollection<SubjectStats> {
            //    new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 2,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(1),
            //        isoDate     = DateTime.UtcNow.AddMonths(1).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 0,
            //        safetySae   = 0
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 7,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(2),
            //        isoDate     = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 5,
            //        safetySae   = 4
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 6,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(3),
            //        isoDate     = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 14,
            //        safetySae   = 5
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 5,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(4),
            //        isoDate     = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 15,
            //        safetySae   = 8
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 6,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(5),
            //        isoDate     = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 15,
            //        safetySae   = 10
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 8,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(6),
            //        isoDate     = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 15,
            //        safetySae   = 13
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 7,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(7),
            //        isoDate     = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 16,
            //        safetySae   = 13
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 3,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(8),
            //        isoDate     = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 16,
            //        safetySae   = 13
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 6,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(9),
            //        isoDate     = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 17,
            //        safetySae   = 17
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 1,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(10),
            //        isoDate     = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 17,
            //        safetySae   = 21
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(11),
            //        isoDate     = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 19,
            //        safetySae   = 27
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 1,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(12),
            //        isoDate     = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 31
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(13),
            //        isoDate     = DateTime.UtcNow.AddMonths(13).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 37
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(14),
            //        isoDate     = DateTime.UtcNow.AddMonths(14).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 37
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(15),
            //        isoDate     = DateTime.UtcNow.AddMonths(15).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 41
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(16),
            //        isoDate     = DateTime.UtcNow.AddMonths(16).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 49
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(17),
            //        isoDate     = DateTime.UtcNow.AddMonths(17).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 50
            //    }
            //};

            //TotalSubjectStats = new ObservableCollection<SubjectStats> {
            //    new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 5,
            //        enrolled    = 1,
            //        early_Term  = 0,
            //        complete    = 3,
            //        isoDateTime = DateTime.UtcNow,
            //        isoDate     = DateTime.UtcNow.ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 0,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 5,
            //        enrolled    = 1,
            //        early_Term  = 0,
            //        complete    = 3,
            //        isoDateTime = DateTime.UtcNow.AddMonths(1),
            //        isoDate     = DateTime.UtcNow.AddMonths(1).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 0,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 7,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(2),
            //        isoDate     = DateTime.UtcNow.AddMonths(2).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 5,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 8,
            //        enrolled    = 6,
            //        early_Term  = 1,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(3),
            //        isoDate     = DateTime.UtcNow.AddMonths(3).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 14,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 5,
            //        early_Term  = 3,
            //        complete    = 2,
            //        isoDateTime = DateTime.UtcNow.AddMonths(4),
            //        isoDate     = DateTime.UtcNow.AddMonths(4).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 15,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 7,
            //        enrolled    = 6,
            //        early_Term  = 1,
            //        complete    = 1,
            //        isoDateTime = DateTime.UtcNow.AddMonths(5),
            //        isoDate     = DateTime.UtcNow.AddMonths(5).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 15,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 8,
            //        enrolled    = 8,
            //        early_Term  = 0,
            //        complete    = 3,
            //        isoDateTime = DateTime.UtcNow.AddMonths(6),
            //        isoDate     = DateTime.UtcNow.AddMonths(6).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 15,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 10,
            //        enrolled    = 7,
            //        early_Term  = 2,
            //        complete    = 2,
            //        isoDateTime = DateTime.UtcNow.AddMonths(7),
            //        isoDate     = DateTime.UtcNow.AddMonths(7).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 16,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 1,
            //        enrolled    = 3,
            //        early_Term  = 0,
            //        complete    = 2,
            //        isoDateTime = DateTime.UtcNow.AddMonths(8),
            //        isoDate     = DateTime.UtcNow.AddMonths(8).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 16,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 4,
            //        enrolled    = 6,
            //        early_Term  = 2,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(9),
            //        isoDate     = DateTime.UtcNow.AddMonths(9).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 17,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 6,
            //        enrolled    = 1,
            //        early_Term  = 1,
            //        complete    = 1,
            //        isoDateTime = DateTime.UtcNow.AddMonths(10),
            //        isoDate     = DateTime.UtcNow.AddMonths(10).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 17,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 6,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 1,
            //        isoDateTime = DateTime.UtcNow.AddMonths(11),
            //        isoDate     = DateTime.UtcNow.AddMonths(11).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 19,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 13,
            //        enrolled    = 1,
            //        early_Term  = 3,
            //        complete    = 4,
            //        isoDateTime = DateTime.UtcNow.AddMonths(12),
            //        isoDate     = DateTime.UtcNow.AddMonths(12).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(13),
            //        isoDate     = DateTime.UtcNow.AddMonths(13).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(14),
            //        isoDate     = DateTime.UtcNow.AddMonths(14).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(15),
            //        isoDate     = DateTime.UtcNow.AddMonths(15).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(16),
            //        isoDate     = DateTime.UtcNow.AddMonths(16).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 50
            //    }, new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 0,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(17),
            //        isoDate     = DateTime.UtcNow.AddMonths(17).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 20,
            //        safetySae   = 50
            //    }
            //};

            //MonthlyRateSubjectStats = new ObservableCollection<SubjectStats> {
            //    new SubjectStats {
            //        projectId   = projectId,
            //        screened    = 0,
            //        enrolled    = 15,
            //        early_Term  = 0,
            //        complete    = 0,
            //        isoDateTime = DateTime.UtcNow.AddMonths(1),
            //        isoDate     = DateTime.UtcNow.AddMonths(1).ToString("ddMMMyyyy"),
            //        screenFail  = 0,
            //        safetyPd    = 0,
            //        safetySae   = 0
            //    }
            //};
//#else //TODO: Figure out how to separate out planned, actual and total subject stats from each other
//            try {
//                string url = "https://ecs.incresearch.com/ECS/mobile/substats/projectId/" + projectId;

//                HttpClient client = new HttpClient();
//                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", App.AuthKey);

//                HttpResponseMessage response = await client.GetAsync(url);

//                if(response.IsSuccessStatusCode) {
//                    string content = await response.Content.ReadAsStringAsync();

//                    List<SubjectStats> subjectStats = Utility.DeserializeResponse<List<SubjectStats>>(content, "data/project/subjectStats");

//                    PlannedSubjectStats.Clear();

//                    foreach(SubjectStats subjectStat in subjectStats) {
//                        PlannedSubjectStats.Add(subjectStat);
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
        /// Command to load/refresh a project
        /// </summary>
        public Command LoadProjectCommand => _loadProjectCommand ?? (_loadProjectCommand = new Command(async () => await ExecuteLoadProjectCommand(_projectId)));

        private async Task ExecuteLoadProjectCommand(string projectId) {
            if(IsBusy) {
                return;
            }

            IsBusy = true;

//#if DEBUG
//            await Task.FromResult(0);

//            Project = new Project {
//                customerName = "Generic Customer",
//                owningBu = "9500 Biometrics",
//                phase = 4,
//                primaryIndication = "Dry Eye",
//                primaryTherapeuticArea = "Psychiatry",
//                projectDirector = "Sally Smith",
//                projectId = projectId,
//                protocolId = "9083-E1-ES3"
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

            status = status.ToLowerInvariant();

            if(SubjectTrends.IsNullOrEmpty()) {
                return;
            }

            List<SubjectTrends> groupedTrends = Utility.FilterSubjectTrends(SubjectTrends).Where(trend => trend.eventType.ToLowerInvariant() == status).OrderBy(trend => trend.MonthDateTime).ToList();

            PlannedTopChartSubjectTrends = new ObservableCollection<SubjectTrends>(groupedTrends.Where(trend => !string.IsNullOrEmpty(trend.high)));
            ActualTopChartSubjectTrends  = new ObservableCollection<SubjectTrends>(groupedTrends.Where(trend => !string.IsNullOrEmpty(trend.actual)));

            List<SubjectTrends> totalSubjectTrends = groupedTrends.Where(trend => !string.IsNullOrEmpty(trend.ceiling)).ToList();

            if(totalSubjectTrends.Count < 1) { //BUG: If the total collection is empty, neither of the other 2 collections displays properly
                totalSubjectTrends.Add(new SubjectTrends {
                    ceiling = "10",
                    month   = "2016-01"
                });
            }

            TotalTopChartSubjectTrends = new ObservableCollection<SubjectTrends>(totalSubjectTrends);
        }
    }
}