using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Connect.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Connect.Helpers {

    public static class Utility {

        /// <summary>
        /// Text
        /// </summary>
        public const string SiteTrendEventTypePostFix = "sitescolumn";

        /// <summary>
        /// Currently the mockups only show the Event Types contained in this list but the API sends over more. Nash said to just filter out the ones we want for now.
        /// </summary>
        private static readonly List<string> ValidSiteTrendEventTypes;

        /// <summary>
        /// Currently the mockups only show the Event Types contained in this list but the API sends over more. Nash said to just filter out the ones we want for now.
        /// </summary>
        private static readonly List<string> ValidSubjectTrendEventTypes;

        /// <summary>
        /// Currently the mockups only show the Event Types contained in this list but the API sends over more. Nash said to just filter out the ones we want for now.
        /// </summary>
        private static readonly List<string> ValidVisitMetricEventTypes;

        #region Constructors

        static Utility() {
            ValidSiteTrendEventTypes = new List<string> { //The string checks are done in lowercase
                "selected" + SiteTrendEventTypePostFix,
                "active"   + SiteTrendEventTypePostFix,
                "enroling" + SiteTrendEventTypePostFix, //The APIs have the word spelled wrong
                "dormant"  + SiteTrendEventTypePostFix,
                "closed"   + SiteTrendEventTypePostFix
            };

            ValidSubjectTrendEventTypes = new List<string> { //The string checks are done in lowercase
                "screened",
                "enrolled",
                "early term",
                "complete"
            };

            ValidVisitMetricEventTypes = new List<string> { //The string checks are done in lowercase
                "pssv",
                "siv",
                "imv",
                "cov",
                "total"
            };
        }

        #endregion

        public static string GetDateString(DateTime dateTime) => dateTime.ToString("mM/d/YY h:mm");

        public static bool IsNullOrEmpty<T>(this IList<T> list) => list == null || list.Count < 1;

        public static bool IsNotNullOrEmpty(this IList list) => list != null && list.Count > 0;

        #region Site Trends

        public static IEnumerable<SiteTrends> FilterSiteTrends(IList<SiteTrends> chartData) => chartData.Where(dt => ValidSiteTrendEventTypes.Contains(dt.eventType.ToLowerInvariant()));

        public static List<IGrouping<string, SiteTrends>> GroupSiteTrends(IList<SiteTrends> chartData) {
            IEnumerable<SiteTrends> filteredTrends = FilterSiteTrends(chartData);

            return filteredTrends.GroupBy(dt => dt.eventType).ToList(); //Group by EventType
        }

        public static IEnumerable<GraphCategory> GetSiteTrendsNumberOfPlanned(IList<SiteTrends> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            List<IGrouping<string, SiteTrends>> groupedCats = GroupSiteTrends(chartData); //Group by EventType

            List<IGrouping<string, SiteTrends>> sortedGroupedCats = SortGroupedSiteTrends(groupedCats); //Sort groups

            foreach(IGrouping<string, SiteTrends> prop in sortedGroupedCats) {
                categories.Add(new GraphCategory {
                    Group = prop.Key.ToLowerInvariant().Replace(SiteTrendEventTypePostFix, string.Empty).ToTitleCase(),
                    Value = prop.Sum(data => string.IsNullOrEmpty(data.high) ? 0 : int.Parse(data.high))
                });
            }

            return categories;
        }

        public static IEnumerable<GraphCategory> GetSiteTrendsNumberOfActual(IList<SiteTrends> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            List<IGrouping<string, SiteTrends>> groupedCats = GroupSiteTrends(chartData); //Group by EventType

            List<IGrouping<string, SiteTrends>> sortedGroupedCats = SortGroupedSiteTrends(groupedCats); //Sort groups

            foreach(IGrouping<string, SiteTrends> prop in sortedGroupedCats) {
                categories.Add(new GraphCategory {
                    Group = prop.Key.ToLowerInvariant().Replace(SiteTrendEventTypePostFix, string.Empty).ToTitleCase(),
                    Value = prop.Sum(data => string.IsNullOrEmpty(data.actual) ? 0 : int.Parse(data.actual))
                });
            }

            return categories;
        }

        public static IEnumerable<GraphCategory> GetSiteTrendsNumberOfTotal(IList<SiteTrends> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            List<IGrouping<string, SiteTrends>> groupedCats = GroupSiteTrends(chartData); //Group by EventType

            List<IGrouping<string, SiteTrends>> sortedGroupedCats = SortGroupedSiteTrends(groupedCats); //Sort groups

            foreach(IGrouping<string, SiteTrends> prop in sortedGroupedCats) {
                categories.Add(new GraphCategory {
                    Group = prop.Key.ToLowerInvariant().Replace(SiteTrendEventTypePostFix, string.Empty).ToTitleCase(),
                    Value = prop.Sum(data => string.IsNullOrEmpty(data.ceiling) ? 0 : int.Parse(data.ceiling))
                });
            }

            return categories;
        }

        #endregion

        #region Subject Trends

        public static List<IGrouping<string, SubjectTrends>> GroupSubjectTrends(IList<SubjectTrends> chartData) {

            IEnumerable<SubjectTrends> filteredTrends = chartData.Where(dt => ValidSubjectTrendEventTypes.Contains(dt.eventType.ToLowerInvariant()));

            return filteredTrends.GroupBy(dt => dt.eventType).ToList(); //Group by EventType
        }

        public static IEnumerable<GraphCategory> GetSubjectTrendsNumberOfPlanned(IList<SubjectTrends> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            List<IGrouping<string, SubjectTrends>> groupedCats = GroupSubjectTrends(chartData); //Group by EventType

            List<IGrouping<string, SubjectTrends>> sortedGroupedCats = SortGroupedSubjectTrends(groupedCats); //Sort groups

            foreach(IGrouping<string, SubjectTrends> prop in sortedGroupedCats) {
                categories.Add(new GraphCategory {
                    Group = prop.Key.ToTitleCase(),
                    Value = prop.Sum(data => string.IsNullOrEmpty(data.high) ? 0 : int.Parse(data.high))
                });
            }

            return categories;
        }

        public static IEnumerable<GraphCategory> GetSubjectTrendsNumberOfActual(IList<SubjectTrends> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            List<IGrouping<string, SubjectTrends>> groupedCats = GroupSubjectTrends(chartData); //Group by EventType

            List<IGrouping<string, SubjectTrends>> sortedGroupedCats = SortGroupedSubjectTrends(groupedCats); //Sort groups

            foreach(IGrouping<string, SubjectTrends> prop in sortedGroupedCats) {
                categories.Add(new GraphCategory {
                    Group = prop.Key.ToTitleCase(),
                    Value = prop.Sum(data => string.IsNullOrEmpty(data.actual) ? 0 : int.Parse(data.actual))
                });
            }

            return categories;
        }

        public static IEnumerable<GraphCategory> GetSubjectTrendsNumberOfTotal(IList<SubjectTrends> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            List<IGrouping<string, SubjectTrends>> groupedCats = GroupSubjectTrends(chartData); //Group by EventType

            List<IGrouping<string, SubjectTrends>> sortedGroupedCats = SortGroupedSubjectTrends(groupedCats); //Sort groups

            foreach(IGrouping<string, SubjectTrends> prop in sortedGroupedCats) {
                categories.Add(new GraphCategory {
                    Group = prop.Key.ToTitleCase(),
                    Value = prop.Sum(data => string.IsNullOrEmpty(data.ceiling) ? 0 : int.Parse(data.ceiling))
                });
            }

            return categories;
        }

        public static IEnumerable<GraphCategory> GetSubjectTrendsMonthlyRate(IList<SubjectTrends> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            List<IGrouping<string, SubjectTrends>> groupedCats = GroupSubjectTrends(chartData); //Group by EventType

            List<IGrouping<string, SubjectTrends>> sortedGroupedCats = SortGroupedSubjectTrends(groupedCats); //Sort groups

            foreach(IGrouping<string, SubjectTrends> prop in sortedGroupedCats) {
                categories.Add(new GraphCategory {
                    Group = prop.Key.ToTitleCase(),
                    Value = prop.Sum(data => string.IsNullOrEmpty(data.low) ? 0 : int.Parse(data.low))  //TODO: Find out if low is the correct property for the Monthly Rate column or if there is a different property that needs to be used
                });
            }

            return categories;
        }

        #endregion

        #region Visit Metrics

        public static List<IGrouping<string, VisitMetrics>> GroupVisitMetrics(IList<VisitMetrics> chartData) {
            List<VisitMetrics> filterMetrics = CreateTotalVisitMetric(chartData.Where(dt => ValidVisitMetricEventTypes.Contains(dt.EventType.ToLowerInvariant())).ToList());    //Filter EventType and insert Total model

            return filterMetrics.GroupBy(dt => dt.EventType).ToList(); //Group by EventType
        }

        public static IEnumerable<GraphCategory> GetVisitMetricNumberOfSites(IList<VisitMetrics> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            List<IGrouping<string, VisitMetrics>> groupedCats = GroupVisitMetrics(chartData); //Group by EventType

            List<IGrouping<string, VisitMetrics>> sortedGroupedCats = SortGroupedVisitMetrics(groupedCats); //Sort groups

            foreach(IGrouping<string, VisitMetrics> prop in sortedGroupedCats) {
                categories.Add(new GraphCategory {
                    Group = prop.Key,
                    Value = prop.Sum(data => data.NumSites)
                });
            }

            return categories;
        }

        public static IEnumerable<GraphCategory> GetVisitMetricNumberOfVisits(IList<VisitMetrics> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            List<IGrouping<string, VisitMetrics>> groupedCats = GroupVisitMetrics(chartData); //Group by EventType

            List<IGrouping<string, VisitMetrics>> sortedGroupedCats = SortGroupedVisitMetrics(groupedCats); //Sort groups

            foreach(IGrouping<string, VisitMetrics> prop in sortedGroupedCats) {
                categories.Add(new GraphCategory {
                    Group = prop.Key,
                    Value = prop.Sum(data => data.NumVisits)
                });
            }

            return categories;
        }

        public static IEnumerable<GraphCategory> GetVisitMetricReportsCompleted(IList<VisitMetrics> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            List<IGrouping<string, VisitMetrics>> groupedCats = GroupVisitMetrics(chartData); //Group by EventType

            List<IGrouping<string, VisitMetrics>> sortedGroupedCats = SortGroupedVisitMetrics(groupedCats); //Sort groups

            foreach(IGrouping<string, VisitMetrics> prop in sortedGroupedCats) {
                categories.Add(new GraphCategory {
                    Group = prop.Key,
                    Value = prop.Sum(data => data.ReportsCompleted)
                });
            }

            return categories;
        }

        #endregion

        //public static List<GraphCategory> GetVisitStatusChartCategories(IList<VisitStats> chartData) {
        //    List<GraphCategory> categories = new List<GraphCategory>();

        //    foreach(KeyValuePair<string, PropertyInfo> prop in _xAxisVisitStatusCategoryProps) {
        //        categories.Add(new GraphCategory {
        //            Group = prop.Key,
        //            Value = chartData.Sum(data => (int)prop.Value.GetValue(data))
        //        });
        //    }

        //    return categories;
        //}

        public static string ToTitleCase(this string value) => value.Substring(0, 1).ToUpperInvariant() + value.Substring(1);

        /// <summary>
        /// Returns a resource dictionary item from <c>Application.Current.Resources</c>.
        /// </summary>
        /// <typeparam name="T">The type of object being returned.</typeparam>
        /// <param name="styleKey">The key used for looking up the resource.</param>
        /// <returns>The type of resource requested.</returns>
        /// <example><![CDATA[Utility.GetResource<Color>("Gray");]]></example>
        public static T GetResource<T>(string styleKey) => (T)Application.Current.Resources[styleKey];

        public static T DeserializeResponse<T>(string jsonResponse) => DeserializeResponse<T>(jsonResponse, string.Empty);

        /// <summary>
        /// Accepts a JSON string and deserializes it to a given object of type T
        /// </summary>
        /// <typeparam name="T">Type of the parameter to add</typeparam>
        /// <param name="jsonResponse">JSON data to deserialize</param>
        /// <param name="rootNode">Name of the root node (if any) to grab the data to deserialize</param>
        /// <returns></returns>
        public static T DeserializeResponse<T>(string jsonResponse, string rootNode) {
            if(string.IsNullOrEmpty(jsonResponse)) {
                return Activator.CreateInstance<T>();
            }

            if(!string.IsNullOrEmpty(rootNode)) {
                foreach(string node in rootNode.Split('/')) {
                    if(node == null || jsonResponse == null) {
                        continue;
                    }

                    try {
                        jsonResponse = JObject.Parse(jsonResponse)[node]?.ToString();
                    } catch(Exception e) {
                        System.Diagnostics.Debug.WriteLine(e);
#if DEBUG
                        throw;
#else
                        return Activator.CreateInstance<T>();
#endif
                    }
                }
            }

            if(string.IsNullOrEmpty(jsonResponse)) {
                return Activator.CreateInstance<T>();
            }

            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        ///// <summary>
        ///// Initializes the X Axis Site Visit Status Categories list with the priority, display name, and property object for each property in the <see cref="VisitStats"/> model with the <see cref="ChartXAxisAttribute"/>.
        ///// </summary>
        //private static void InitXAxisVisitStatusCategory() {
        //    _xAxisVisitStatusCategoryProps = new Dictionary<string, PropertyInfo>();

        //    IEnumerable<PropertyInfo> propertyInfos = typeof(VisitStats).GetRuntimeProperties()?.Where(p => p != null && p.GetCustomAttributes(false).Any(a => a?.GetType() == typeof(ChartXAxisAttribute))); //Gets all properties in the VisitStats class with the ChartXAxisAttribute attribute

        //    if(propertyInfos == null) {
        //        return;
        //    }

        //    List<PropertyInfo> propertyInfosList = propertyInfos.ToList();

        //    List<ChartPropertyInfo> chartInfos = new List<ChartPropertyInfo>();

        //    foreach(PropertyInfo propertyInfo in propertyInfosList) {   //Go through each ChartXAxisAttribute property and save the ChartXAxisAttribute.Priority, ChartXAxisAttribute.DisplayName, and the PropertyInfo itself into our chartInfos collection
        //        ChartXAxisAttribute attr = propertyInfo.GetCustomAttributes(typeof(ChartXAxisAttribute), true).Cast<ChartXAxisAttribute>().Single();

        //        chartInfos.Add(new ChartPropertyInfo(attr.Priority, attr.DisplayName, propertyInfo));
        //    }

        //    chartInfos = chartInfos.OrderBy(attr => attr.Priority).ToList();    //Order by priority so the Insert()s below start at 0

        //    foreach(ChartPropertyInfo chartInfo in chartInfos) {    //Create static list of the display names and property so values can be pulled from the properties of the passed in classes in other Utility methods
        //        _xAxisVisitStatusCategoryProps[chartInfo.DisplayName] = chartInfo.PropInfo;
        //    }
        //}

        private static List<IGrouping<string, SiteTrends>> SortGroupedSiteTrends(List<IGrouping<string, SiteTrends>> groupedTrends) {
            List<IGrouping<string, SiteTrends>> sortedGroupedCats = new List<IGrouping<string, SiteTrends>>();

            IGrouping<string, SiteTrends> selected = groupedTrends.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == ValidSiteTrendEventTypes[0]);

            if(selected != null) {
                sortedGroupedCats.Add(selected);
            }

            IGrouping<string, SiteTrends> activated = groupedTrends.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == ValidSiteTrendEventTypes[1]);

            if(activated != null) {
                sortedGroupedCats.Add(activated);
            }

            IGrouping<string, SiteTrends> enrolling = groupedTrends.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == ValidSiteTrendEventTypes[2]);

            if(enrolling != null) {
                sortedGroupedCats.Add(enrolling);
            }

            IGrouping<string, SiteTrends> dormant = groupedTrends.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == ValidSiteTrendEventTypes[3]);

            if(dormant != null) {
                sortedGroupedCats.Add(dormant);
            }

            IGrouping<string, SiteTrends> closed = groupedTrends.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == ValidSiteTrendEventTypes[4]);

            if(closed != null) {
                sortedGroupedCats.Add(closed);
            }

            return sortedGroupedCats;
        }

        private static List<IGrouping<string, SubjectTrends>> SortGroupedSubjectTrends(List<IGrouping<string, SubjectTrends>> groupedTrends) {
            List<IGrouping<string, SubjectTrends>> sortedGroupedCats = new List<IGrouping<string, SubjectTrends>>();

            IGrouping<string, SubjectTrends> screened = groupedTrends.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == ValidSubjectTrendEventTypes[0]);

            if(screened != null) {
                sortedGroupedCats.Add(screened);
            }

            IGrouping<string, SubjectTrends> enrolled = groupedTrends.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == ValidSubjectTrendEventTypes[1]);

            if(enrolled != null) {
                sortedGroupedCats.Add(enrolled);
            }

            IGrouping<string, SubjectTrends> earlyTerm = groupedTrends.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == ValidSubjectTrendEventTypes[2]);

            if(earlyTerm != null) {
                sortedGroupedCats.Add(earlyTerm);
            }

            IGrouping<string, SubjectTrends> complete = groupedTrends.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == ValidSubjectTrendEventTypes[3]);

            if(complete != null) {
                sortedGroupedCats.Add(complete);
            }

            return sortedGroupedCats;
        }

        private static List<IGrouping<string, VisitMetrics>> SortGroupedVisitMetrics(List<IGrouping<string, VisitMetrics>> groupedMetrics) {
            List<IGrouping<string, VisitMetrics>> sortedGroupedCats = new List<IGrouping<string, VisitMetrics>>();

            IGrouping<string, VisitMetrics> pssv = groupedMetrics.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == "pssv");

            if(pssv != null) {
                sortedGroupedCats.Add(pssv);
            }

            IGrouping<string, VisitMetrics> siv = groupedMetrics.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == "siv");

            if(siv != null) {
                sortedGroupedCats.Add(siv);
            }

            IGrouping<string, VisitMetrics> imv = groupedMetrics.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == "imv");

            if(imv != null) {
                sortedGroupedCats.Add(imv);
            }

            IGrouping<string, VisitMetrics> cov = groupedMetrics.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == "cov");

            if(cov != null) {
                sortedGroupedCats.Add(cov);
            }

            IGrouping<string, VisitMetrics> total = groupedMetrics.FirstOrDefault(cat => cat.Key.ToLowerInvariant() == "total");

            if(total != null) {
                sortedGroupedCats.Add(total);
            }

            return sortedGroupedCats;
        }

        private static List<VisitMetrics> CreateTotalVisitMetric(List<VisitMetrics> visitMetrics) {
            if(visitMetrics.FirstOrDefault(v => v.EventType.ToLower() == "total") != null) {
                return visitMetrics;
            }

            visitMetrics.Add(new VisitMetrics {
                EventType        = "Total",
                NumVisits        = visitMetrics.Sum(v => v.NumVisits),
                NumSites         = visitMetrics.Sum(v => v.NumSites),
                ReportsCompleted = visitMetrics.Sum(v => v.ReportsCompleted)
            });

            return visitMetrics;
        }
    }
}
