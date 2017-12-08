using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Connect.Helpers {

    public class Utility {

        public Utility() { }

        public static T DeserializeResponse<T>(string jsonResponse) {
            return DeserializeResponse<T>(jsonResponse, string.Empty);
        }

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
