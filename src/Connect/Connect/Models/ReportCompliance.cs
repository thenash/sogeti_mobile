using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Connect.Models {

    public class ReportCompliance {

        [JsonProperty("projectId")]
        public string ProjectId { get; set; }

        [JsonProperty("reportsCompleted")]
        public string ReportsCompleted { get; set; }

        [JsonProperty("compliance")]
        public string Compliance { get; set; }

        [JsonProperty("eventMonth")]
        public string EventMonth { get; set; }

        /// <summary>
        /// <see cref="Compliance"/> is being returned as a <c>string</c> so, for now, it must be parsed.
        /// </summary>
        public double CompliancePercent => string.IsNullOrEmpty(Compliance) || !double.TryParse(Compliance, out double number) ? 0 : number / 100;

        /// <summary>
        /// <see cref="EventMonth"/> is being returned as a <c>string</c> so, for now, it must be parsed.
        /// </summary>
        public DateTime EventMonthDateTime => string.IsNullOrEmpty(EventMonth) ? DateTime.MinValue : DateTime.ParseExact(EventMonth, "yyyy-MM", new DateTimeFormatInfo());

        public ReportCompliance() { }
    }
}