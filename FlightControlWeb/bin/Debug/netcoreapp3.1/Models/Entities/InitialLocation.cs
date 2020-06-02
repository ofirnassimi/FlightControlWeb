using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class InitialLocation
    {
        [JsonPropertyName("longitude")]
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("date_time")]
        [JsonProperty("date_time")]
        public DateTime DateTime { get; set; }
    }
}
