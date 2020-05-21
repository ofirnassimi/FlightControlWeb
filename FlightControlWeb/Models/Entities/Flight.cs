﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FlightControlWeb.Models
{
    public class Flight
    {
        [JsonPropertyName("flight_id")]
        public string FlightId { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("passengers")]
        public int Passengers { get; set; }

        [JsonPropertyName("company_name")]
        public string CompanyName { get; set; }
        
        [JsonPropertyName("date_time")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("is_external")]
        public bool IsExternal { get; set; }


        public string ToJson() => JsonSerializer.Serialize<Flight>(this);
    }
}
