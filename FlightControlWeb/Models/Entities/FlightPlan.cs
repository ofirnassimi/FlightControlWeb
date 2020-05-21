using System.Text.Json;
using System.Text.Json.Serialization;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        [JsonPropertyName("flight_id")]
        public string FlightId { get; set; }

        [JsonPropertyName("passengers")]
        public int Passengers { get; set; }

        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; }

        [JsonPropertyName("initial_location")]
        public InitialLocation InitialLocation { get; set; }

        [JsonPropertyName("segments")]
        public Segment[] Segments { get; set; }


        public string ToJson() => JsonSerializer.Serialize<FlightPlan>(this);
    }
}
