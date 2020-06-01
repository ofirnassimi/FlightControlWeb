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

            flightPlan.FlightId = GenerateFlightID();

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

        public string GenerateFlightID()
        {
            Random rand = new Random();
            int num = rand.Next(100, 10000);

            return "flt-" + num;
        }
    }
}
