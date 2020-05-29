using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using FlightControlWeb.Models;
using FlightControlWeb.Models.Managers;
using Newtonsoft.Json;

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

            //DateTime dateTime = TimeZoneInfo.ConvertTimeToUtc(relative_to);
            bool syncAll = Request.Query.Keys.Contains("sync_all");

            foreach (Flight flight in flightsManager.GetAllFlights()) {
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

            foreach (Flight flight in flightsManager.GetAllFlights())
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

            DateTime flightTime = flight.DateTime;
            DateTime timeAfterSegment = flight.DateTime;
            /*double lastLongitude = flight.Longitude;
            double lastLatitude = flight.Latitude;*/
            double lastLongitude = flightPlan.InitialLocation.Longitude;
            double lastLatitude = flightPlan.InitialLocation.Latitude;

            foreach(Segment segment in flightPlan.Segments)
            {
                timeAfterSegment = timeAfterSegment.AddSeconds(segment.TimespanSeconds);

                if ((flightTime <= relative_to) && (relative_to <= timeAfterSegment))
                {
                    location = calculateFlightLocation(lastLongitude, lastLatitude, flightTime,
                        timeAfterSegment, segment, relative_to);
                    //Location location = calculateFlightLocation(flight, segment, relative_to, timeAfterSegment);
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
