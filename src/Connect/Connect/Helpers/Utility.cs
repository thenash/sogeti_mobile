using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Connect.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Connect.Helpers {

    public class Utility {

        /// <summary>
        /// Grouping of property info for Site Status properties with the <see cref="ChartXAxisAttribute"/>.
        /// </summary>
        private static Dictionary<string, PropertyInfo> _xAxisSiteStatusCategoryProps;

        /// <summary>
        /// Grouping of property info for Subject Status properties with the <see cref="ChartXAxisAttribute"/>.
        /// </summary>
        private static Dictionary<string, PropertyInfo> _xAxisSubjectStatusCategoryProps;

        ///// <summary>
        ///// Grouping of property info for Site Visit Status properties with the <see cref="ChartXAxisAttribute"/>.
        ///// </summary>
        //private static Dictionary<string, PropertyInfo> _xAxisVisitStatusCategoryProps;

        #region Constructors

        static Utility() {
            InitXAxisSiteStatusCategory();
            InitXAxisSubjectStatusCategory();
            //InitXAxisVisitStatusCategory();
        }

        public Utility() { }

        #endregion

        public static string GetDateString(DateTime dateTime) => dateTime.ToString("mM/d/YY h:mm");

        public static List<GraphCategory> GetSiteStatusChartCategories(IList<SiteStats> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            foreach(KeyValuePair<string, PropertyInfo> prop in _xAxisSiteStatusCategoryProps) {
                categories.Add(new GraphCategory {
                    Group = prop.Key,
                    Value = chartData.Sum(data => (int)prop.Value.GetValue(data))
                });
            }

            return categories;
        }

        public static List<GraphCategory> GetSubjectStatusChartCategories(IList<SubjectStats> chartData) {
            List<GraphCategory> categories = new List<GraphCategory>();

            foreach(KeyValuePair<string, PropertyInfo> prop in _xAxisSubjectStatusCategoryProps) {
                categories.Add(new GraphCategory {
                    Group = prop.Key,
                    Value = chartData.Sum(data => (int)prop.Value.GetValue(data))
                });
            }

            return categories;
        }

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

        /// <summary>
        /// Initializes the X Axis Site Status Categories list with the priority, display name, and property object for each property in the <see cref="SiteStats"/> model with the <see cref="ChartXAxisAttribute"/>.
        /// </summary>
        private static void InitXAxisSiteStatusCategory() {
            _xAxisSiteStatusCategoryProps = new Dictionary<string, PropertyInfo>();

            IEnumerable<PropertyInfo> propertyInfos = typeof(SiteStats).GetRuntimeProperties()?.Where(p => p != null && p.GetCustomAttributes(false).Any(a => a?.GetType() == typeof(ChartXAxisAttribute))); //Gets all properties in the SiteStats class with the ChartXAxisAttribute attribute

            if(propertyInfos == null) {
                return;
            }

            List<PropertyInfo> propertyInfosList = propertyInfos.ToList();

            List<ChartPropertyInfo> chartInfos = new List<ChartPropertyInfo>();

            foreach(PropertyInfo propertyInfo in propertyInfosList) {   //Go through each ChartXAxisAttribute property and save the ChartXAxisAttribute.Priority, ChartXAxisAttribute.DisplayName, and the PropertyInfo itself into our chartInfos collection
                ChartXAxisAttribute attr = propertyInfo.GetCustomAttributes(typeof(ChartXAxisAttribute), true).Cast<ChartXAxisAttribute>().Single();

                chartInfos.Add(new ChartPropertyInfo(attr.Priority, attr.DisplayName, propertyInfo));
            }

            chartInfos = chartInfos.OrderBy(attr => attr.Priority).ToList();    //Order by priority so the Insert()s below start at 0

            foreach(ChartPropertyInfo chartInfo in chartInfos) {    //Create static list of the display names and property so values can be pulled from the properties of the passed in classes in other Utility methods
                _xAxisSiteStatusCategoryProps[chartInfo.DisplayName] = chartInfo.PropInfo;
            }
        }

        /// <summary>
        /// Initializes the X Axis Subject Status Categories list with the priority, display name, and property object for each property in the <see cref="SubjectStats"/> model with the <see cref="ChartXAxisAttribute"/>.
        /// </summary>
        private static void InitXAxisSubjectStatusCategory() {
            _xAxisSubjectStatusCategoryProps = new Dictionary<string, PropertyInfo>();

            IEnumerable<PropertyInfo> propertyInfos = typeof(SubjectStats).GetRuntimeProperties()?.Where(p => p != null && p.GetCustomAttributes(false).Any(a => a?.GetType() == typeof(ChartXAxisAttribute))); //Gets all properties in the SubjectStats class with the ChartXAxisAttribute attribute

            if(propertyInfos == null) {
                return;
            }

            List<PropertyInfo> propertyInfosList = propertyInfos.ToList();

            List<ChartPropertyInfo> chartInfos = new List<ChartPropertyInfo>();

            foreach(PropertyInfo propertyInfo in propertyInfosList) {   //Go through each ChartXAxisAttribute property and save the ChartXAxisAttribute.Priority, ChartXAxisAttribute.DisplayName, and the PropertyInfo itself into our chartInfos collection
                ChartXAxisAttribute attr = propertyInfo.GetCustomAttributes(typeof(ChartXAxisAttribute), true).Cast<ChartXAxisAttribute>().Single();

                chartInfos.Add(new ChartPropertyInfo(attr.Priority, attr.DisplayName, propertyInfo));
            }

            chartInfos = chartInfos.OrderBy(attr => attr.Priority).ToList();    //Order by priority so the Insert()s below start at 0

            foreach(ChartPropertyInfo chartInfo in chartInfos) {    //Create static list of the display names and property so values can be pulled from the properties of the passed in classes in other Utility methods
                _xAxisSubjectStatusCategoryProps[chartInfo.DisplayName] = chartInfo.PropInfo;
            }
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

        private class ChartPropertyInfo {

            public int Priority { get; set; }

            public string DisplayName { get; set; }

            public PropertyInfo PropInfo { get; set; }

            public ChartPropertyInfo(int priority, string displayName, PropertyInfo propInfo) {
                Priority    = priority;
                DisplayName = displayName;
                PropInfo    = propInfo;
            }
        }
    }
}
