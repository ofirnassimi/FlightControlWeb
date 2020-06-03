using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using FlightControlWeb.Models;
using FlightControlWeb.Models.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace FlightControlWeb.Controllers
{
    public class Location
    {
        public Double Longitude { get; set; }
        public Double Latitude { get; set; }
    }

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

            bool syncAll = Request.Query.Keys.Contains("sync_all");

            foreach (Flight flight in flightsManager.GetAllFlights(relative_to)) {
                if (ValidAccordingToSyncAll(syncAll, flight) && 
                    CheckIfFlightIsRelevant(flight, relative_to))
                {
                    activeFlights.Add(flight);
                }
            }

            updateFlightsLocation(relative_to);

            return JsonConvert.SerializeObject(activeFlights);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{flightId}")]
        public void Delete(string flightId)
        {
            flightsManager.DeleteFlight(flightId);
        }


        [HttpPost]
        public string Post([FromBody] Flight flight)
        {
            string x = flight.ToJson();
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
            return f.FlightId;
        }   



        private bool CheckIfFlightIsRelevant(Flight flight, DateTime relative_to)
        {
            DateTime landingTime = flight.DateTime;
            FlightPlan flightPlan = flightPlansManager.GetFlightPlanById(flight.FlightId);

            if (flightPlan != null)
            {
                foreach (Segment segment in flightPlan.Segments)
                {
                    landingTime = landingTime.AddSeconds(segment.TimespanSeconds);
                }
            }

            DateTime relative_to_UTC = TimeZoneInfo.ConvertTimeToUtc(relative_to);
            DateTime dateTimeUTC = TimeZoneInfo.ConvertTimeToUtc(flight.DateTime);
            DateTime landingTimeUTC = TimeZoneInfo.ConvertTimeToUtc(landingTime);

            if (dateTimeUTC <= relative_to_UTC && 
                relative_to_UTC <= landingTimeUTC)
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

        private void updateFlightsLocation(DateTime relative_to)
        {
            Location location;
            List<Flight> allFlights = flightsManager.GetAllFlights(relative_to);
            foreach (Flight flight in allFlights)
            {
                location = updateLocationBySegment(flight,
                    flightPlansManager.GetFlightPlanById(flight.FlightId), relative_to);
                if (location != null)
                {
                    flight.Longitude = location.Longitude;
                    flight.Latitude = location.Latitude;
                }
            }
        } 

        private Location updateLocationBySegment(Flight flight, FlightPlan flightPlan, DateTime relative_to)
        {
            Location location = null;

            if(flightPlan == null)
            {
                return location;
            }

            /*DateTime flightTime = flight.DateTime;
            DateTime timeAfterSegment = flight.DateTime;*/
            double lastLongitude = flightPlan.InitialLocation.Longitude;
            double lastLatitude = flightPlan.InitialLocation.Latitude;

            DateTime flightTime = TimeZoneInfo.ConvertTimeToUtc(flight.DateTime);
            DateTime timeAfterSegment = flightTime;
            DateTime relativeToUTC = TimeZoneInfo.ConvertTimeToUtc(relative_to);

            foreach (Segment segment in flightPlan.Segments)
            {
                timeAfterSegment = timeAfterSegment.AddSeconds(segment.TimespanSeconds);

                if ((flightTime <= relativeToUTC) && (relativeToUTC <= timeAfterSegment))
                {
                    location = calculateFlightLocation(lastLongitude, lastLatitude, flightTime,
                        timeAfterSegment, segment, relativeToUTC);
                }
                flightTime = timeAfterSegment;
                lastLongitude = segment.Longitude;
                lastLatitude = segment.Latitude;
            }

            return location;
        }

        private Location calculateFlightLocation(double initialLongitude, double initialLatitude,
            DateTime currentflightTime, DateTime timeAfterSegment, Segment segment, DateTime relative_to)
        {
            double finalLongitude = segment.Longitude;
            double finalLatitude = segment.Latitude;

            DateTime finalTime = timeAfterSegment;

            double seconds = (finalTime - currentflightTime).TotalSeconds;
            double relativeSeconds = (relative_to - currentflightTime).TotalSeconds;

            double totalDistance = Math.Sqrt(Math.Pow(finalLongitude - initialLongitude, 2) + Math.Pow(finalLatitude - initialLatitude, 2));
            double relativeDistance = (relativeSeconds * totalDistance) / seconds;

            double relativeLongitude = initialLongitude + ((finalLongitude - initialLongitude) * (relativeDistance / totalDistance));
            double relativeLatitude = initialLatitude + ((finalLatitude - initialLatitude) * (relativeDistance / totalDistance));

            return new Location { Longitude = relativeLongitude, Latitude = relativeLatitude };
        }

    }
}
