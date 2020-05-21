using System;
using System.Text.Json.Serialization;

namespace FlightControlWeb.Models
{
    public class InitialLocation
    {
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("date_time")]
        public DateTime DateTime { get; set; }
    }
}
