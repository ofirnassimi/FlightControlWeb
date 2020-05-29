using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightControlWeb.Models.Managers
{
    public class FlightsManager
    {
        private ServersManager serversManager = new ServersManager();

        private static List<Flight> flights = new List<Flight>()
        {
            new Flight{ FlightId = "LY319", Longitude = 34.88, Latitude = 32.01, Passengers = 217,
                CompanyName = "El Al", DateTime = DateTime.Parse("2020-05-24T09:04:00Z"), IsExternal = true },
            new Flight{ FlightId = "SWS899", Longitude = 8.55, Latitude = 47.45, Passengers = 359,
                CompanyName = "Swiss Air", DateTime = DateTime.Parse("2020-05-24T09:04:26Z"), IsExternal = true },
            new Flight{ FlightId = "TK6140", Longitude = 30.8, Latitude = 36.9, Passengers = 143,
                CompanyName = "Turkish Airlines", DateTime = DateTime.Parse("2020-05-24T09:04:36Z"), IsExternal = false },
            new Flight{ FlightId = "UA091", Longitude = -87.9, Latitude = 41.97, Passengers = 450,
                CompanyName = "United Airlines", DateTime = DateTime.Parse("2020-05-24T09:04:03Z"), IsExternal = true },
            new Flight{ FlightId = "BA413", Longitude = -0.45, Latitude = 51.47, Passengers = 330,
                CompanyName = "British Airways", DateTime = DateTime.Parse("2020-05-24T09:04:56Z"), IsExternal = false }
        };

        public void AddFlight(Flight flight)
        {
            flights.Add(flight);
        }

        public void DeleteFlight(string flightId)
        {
            Flight flight = flights.Where(flt => flt.FlightId.Equals(flightId)).FirstOrDefault();
            
            if (flight != null)
            {
                flights.Remove(flight);
            }
        }

        public List<Flight> GetAllFlights()
        {
            // fetch data from other servers too

            return flights;
        }

        public Flight GetFlightById(string flightId)
        {
            Flight flight = flights.Where(flt => flt.FlightId.Equals(flightId)).FirstOrDefault();
            
            return flight;
        }

        public void UpdateFlight(Flight updatedFlight)
        {
            Flight flight = flights.Where(flt => flt.FlightId.Equals(updatedFlight.FlightId)).FirstOrDefault();

            if (flight != null)
            {
                flight.Longitude = updatedFlight.Longitude;
                flight.Latitude = updatedFlight.Latitude;
                flight.Passengers = updatedFlight.Passengers;
                flight.CompanyName = updatedFlight.CompanyName;
                flight.DateTime = updatedFlight.DateTime;
                flight.IsExternal = updatedFlight.IsExternal;
            }
        }
    }
}
