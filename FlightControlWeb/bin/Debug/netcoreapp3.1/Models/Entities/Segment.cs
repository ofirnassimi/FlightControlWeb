using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class Segment
    {
        [JsonPropertyName("longitude")]
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("timespan_seconds")]
        [JsonProperty("timespan_seconds")]
        public int TimespanSeconds { get; set; }
    }
}
