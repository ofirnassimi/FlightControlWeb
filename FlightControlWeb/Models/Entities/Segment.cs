using System.Text.Json.Serialization;

namespace FlightControlWeb.Models
{
    public class Segment
    {
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("timespan_seconds")]
        public int TimespanSeconds { get; set; }
    }
}
