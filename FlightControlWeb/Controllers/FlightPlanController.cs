using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using FlightControlWeb.Models;
using FlightControlWeb.Models.Managers;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private FlightPlansManager flightPlansManager = new FlightPlansManager();
        private FlightsManager flightsManager = new FlightsManager();
        Dictionary<string, string> flightIdByCompanyName = new Dictionary<string, string>()
        {
            {"El AL", "LY" }, {"ElAL", "LY" }, {"Swiss Air", "LX" }, {"Turkish Airlines", "TK" },
            {"United Airlines", "UA" }, {"British Airways", "BA" }, {"Alitalia", "AZ" },
            {"Lufthansa", "LH" }, {"Aeroflot", "SU" }, {"Air Baltic", "BT" },
            {"Air Canada", "AC" }, {"American Airlines", "AA" }, {"Brussels Airlines", "SN" },
            {"Czech Airlines", "OK" }, {"Delta", "DL" }, {"Emirates", "EK" },
            {"Etihad Airways", "EY" }, {"Hainan Airlines", "HU" }, {"Iberia", "IB" },
            {"KLM", "KL" }, {"Latam Airlines", "LA" }, {"LOT", "LO" }, {"Norwegian", "DY" },
            {"Qantas", "QF" }, {"Virgin Atlantic", "VS" }, {"Air Europa", "UX" },
            {"Air France", "AF" }
        };

        // GET: api/FlightPlan/5
        [HttpGet("{flightPlanId}", Name = "Get")]
        public string Get(string flightPlanId)
        {
            FlightPlan flightPlan = flightPlansManager.GetFlightPlanById(flightPlanId);
            
            return JsonConvert.SerializeObject(flightPlan);
        }

        // POST: api/FlightPlan
        [HttpPost]
        public string Post([FromBody] FlightPlan flightPlan)
        {
            string x = flightPlan.ToJson();
            JObject json = JObject.Parse(x);

            string companyName = json["company_name"].ToString();
            flightPlan.FlightId = GenerateFlightID(companyName);

            InitialLocation initialLocation = json["initial_location"].ToObject<InitialLocation>();

            Flight flight = new Flight()
            {
                FlightId = flightPlan.FlightId,
                Longitude = initialLocation.Longitude,
                Latitude = initialLocation.Latitude,
                Passengers = Int32.Parse(json["passengers"].ToString()),
                CompanyName = json["company_name"].ToString(),
                DateTime = initialLocation.DateTime,
                IsExternal = false
            };

            flightsManager.AddFlight(flight);
            flightPlansManager.AddFlightPlan(flightPlan);

            return flightPlan.FlightId;


            /*string x = flight.ToJson();
            JObject json = JObject.Parse(x);

            Flight f = new Flight();

            f.FlightId = json["flight_id"].ToString();
            f.Longitude = Convert.ToDouble(json["longitude"].ToString());
            f.Latitude = Convert.ToDouble(json["latitude"].ToString());
            f.Passengers = Int32.Parse(json["passengers"].ToString());
            f.CompanyName = json["company_name"].ToString();
            f.DateTime = DateTime.Parse(json["date_time"].ToString());
            f.IsExternal = true;

            flightsManager.AddFlight(f);
            return f.FlightId;*/



            /*flightPlan.FlightId = GenerateFlightID();

            Flight flight = new Flight()
            {
                FlightId = flightPlan.FlightId,
                Longitude = flightPlan.InitialLocation.Longitude,
                Latitude = flightPlan.InitialLocation.Latitude,
                Passengers = flightPlan.Passengers,
                CompanyName = flightPlan.CompanyName,
                DateTime = flightPlan.InitialLocation.DateTime,
                IsExternal = false
            };

            flightsManager.AddFlight(flight);
            flightPlansManager.AddFlightPlan(flightPlan);

            return flightPlan.FlightId;*/
        }

        public string GenerateFlightID(string companyName)
        {
            Random rand = new Random();
            int num = rand.Next(100, 10000);
            try
            {
                string initialId = flightIdByCompanyName[companyName];
                return initialId + num;
            }
            catch (Exception e)
            {
                return "FLT" + num;
            }
        }
    }
}
