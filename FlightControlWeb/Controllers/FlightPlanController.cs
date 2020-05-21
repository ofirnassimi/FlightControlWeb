using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using FlightControlWeb.Models;
using FlightControlWeb.Models.Managers;
using System.Collections.Generic;
using System;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private FlightPlansManager flightPlansManager = new FlightPlansManager();
        private FlightsManager flightsManager = new FlightsManager();
        private Dictionary<string, string> idPref = new Dictionary<string, string>()
        {
            {"El Al", "LY" },
            {"United Airlines", "UA" },
            {"Delta", "DL" },
            {"Turkish Airlines", "TK" }
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
            flightPlan.FlightId = GenerateFlightID();

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

            return flightPlan.FlightId;
        }

        public string GenerateFlightID()
        {
            Random rand = new Random();
            int num = rand.Next(100, 10000);

            return "flt-" + num;
        }
    }
}
