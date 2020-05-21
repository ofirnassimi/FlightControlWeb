using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightControlWeb.Models.Managers
{
    public class FlightsManager
    {
        private static List<Flight> flights = new List<Flight>()
        {
            new Flight{ FlightId = "LY319", Longitude = 32.08, Latitude = 34.66, Passengers = 217,
                CompanyName = "El Al", DateTime = DateTime.Now, IsExternal = true },
            new Flight{ FlightId = "SWS899", Longitude = 39.48, Latitude = -73.22, Passengers = 359,
                CompanyName = "Swiss Air", DateTime = DateTime.Now, IsExternal = true },
            new Flight{ FlightId = "TK6140", Longitude = 26.65, Latitude = 41.13, Passengers = 143,
                CompanyName = "Turkish Airlines", DateTime = DateTime.Now, IsExternal = false },
            new Flight{ FlightId = "UA091", Longitude = -65.3, Latitude = 48.43, Passengers = 450,
                CompanyName = "United Airlines", DateTime = DateTime.Now, IsExternal = true }
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
