using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Connect.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Connect.Helpers {

    public class Utility {

        /// <summary>
        /// Grouping of property info for properties with the <see cref="ChartXAxisAttribute"/>.
        /// </summary>
        private static readonly Dictionary<string, PropertyInfo> XAxisSiteStatusCategoryProps;

        static Utility() {
            XAxisSiteStatusCategoryProps = new Dictionary<string, PropertyInfo>();
            List<PropertyInfo> info = new List<PropertyInfo>();

            List<string> categories = new List<string>();

            IEnumerable<PropertyInfo> propertyInfos = typeof(SiteStats).GetRuntimeProperties()?.Where(p => p != null && p.GetCustomAttributes(false).Any(a => a?.GetType() == typeof(ChartXAxisAttribute)));

            if(propertyInfos == null) {
                return;
            }

            foreach(PropertyInfo propertyInfo in propertyInfos) {
                ChartXAxisAttribute attr = propertyInfo.GetCustomAttributes(typeof(ChartXAxisAttribute), true).Cast<ChartXAxisAttribute>().Single();

                categories.Insert(attr.Priority, attr.DisplayName);
                info.Insert(attr.Priority, propertyInfo);
            }

            for(int i = 0; i < categories.Count; i++) {
                XAxisSiteStatusCategoryProps[categories[i]] = info[i];
            }
        }

        public Utility() { }

        public static string GetDateString(DateTime dateTime) {
            return dateTime.ToString("mM/d/YY h:mm");
        }

        public static List<SiteStatCategory> GetChartCategories(IList<SiteStats> siteStatus) {
            List<SiteStatCategory> siteCats = new List<SiteStatCategory>();

            foreach(KeyValuePair<string, PropertyInfo> prop in XAxisSiteStatusCategoryProps) {
                siteCats.Add(new SiteStatCategory {
                    Group = prop.Key,
                    Value = siteStatus.Sum(stat => (int)prop.Value.GetValue(stat))
                });
            }

            return siteCats;
        }

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
    }
}
