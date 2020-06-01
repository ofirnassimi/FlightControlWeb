using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        [JsonPropertyName("flight_id")]
        [JsonProperty("flight_id")]
        public string FlightId { get; set; }

        [JsonPropertyName("passengers")]
        [JsonProperty("passengers")]
        public int Passengers { get; set; }

        [JsonPropertyName("company_name")]
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonPropertyName("initial_location")]
        [JsonProperty("initial_location")]
        public InitialLocation InitialLocation { get; set; }

        [JsonPropertyName("segments")]
        [JsonProperty("segments")]
        public Segment[] Segments { get; set; }


        public string ToJson() => System.Text.Json.JsonSerializer.Serialize<FlightPlan>(this);
    }
}
