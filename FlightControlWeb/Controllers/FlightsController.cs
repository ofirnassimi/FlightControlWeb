using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using FlightControlWeb.Models;
using FlightControlWeb.Models.Managers;
using Newtonsoft.Json;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private FlightsManager flightsManager = new FlightsManager();
        private FlightPlansManager flightPlansManager = new FlightPlansManager();

        // GET: api/Flights
        [HttpGet]
        public string Get(DateTime relative_to)
        {
            List<Flight> activeFlights = new List<Flight>();
            DateTime dateTime = TimeZoneInfo.ConvertTimeToUtc(relative_to);
            bool syncAll = Request.Query.Keys.Contains("sync_all");

            foreach (Flight flight in flightsManager.GetAllFlights()) {
                if (ValidAccordingToSyncAll(syncAll, flight) && 
                    CheckIfFlightIsRelevant(flight, dateTime))
                {
                    activeFlights.Add(flight);
                }
            }

            return JsonConvert.SerializeObject(activeFlights);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string flightId)
        {
            flightsManager.DeleteFlight(flightId);
        }


        private bool CheckIfFlightIsRelevant(Flight flight, DateTime relative_to)
        {
            DateTime landingTime = flight.DateTime;
            FlightPlan flightPlan = flightPlansManager.GetFlightPlanById(flight.FlightId);
            
            foreach (Segment segment in flightPlan.Segments)
            {
                landingTime = landingTime.AddSeconds(segment.TimespanSeconds);
            }

            if (flight.DateTime <= relative_to && 
                relative_to <= landingTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidAccordingToSyncAll(bool sync_all, Flight flight)
        {
            if (!flight.IsExternal)
            {
                return true;
            } 
            else if (sync_all)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
