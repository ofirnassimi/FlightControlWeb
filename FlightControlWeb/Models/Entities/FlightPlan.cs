using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        [JsonProperty("flight_id")]
        public string FlightId { get; set; }

        [JsonProperty("passengers")]
        public int Passengers { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("initial_location")]
        public InitialLocation InitialLocation { get; set; }

        [JsonProperty("segments")]
        public Segment[] Segments { get; set; }


        public string ToJson() => System.Text.Json.JsonSerializer.Serialize<FlightPlan>(this);
    }
}
