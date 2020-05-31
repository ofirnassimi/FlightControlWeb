using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace FlightControlWeb.Models.Managers
{
    public class FlightsManager
    {
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

        private ServersManager serversManager = new ServersManager();

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

        public List<Flight> GetAllFlights(DateTime relative_to)
        {
            try
            {
                GetFlightsFromExternalServers(relative_to);
            }
            catch (Exception e)
            {
                return flights;
            }

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

        private void GetFlightsFromExternalServers(DateTime relative_to)
        {
            List<Flight> flightsFromServer;
            foreach(Server server in serversManager.GetAllServers())
            {
                try
                {
                    flightsFromServer = FetchFlightsFromServer(server, relative_to);
                    flights.AddRange(flightsFromServer);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

       
        private List<Flight> FetchFlightsFromServer(Server server, DateTime relative_to)
        {
            List<Flight> flightsFromServer = new List<Flight>();
            HttpClient httpClient = new HttpClient();
            string dateTimeString = relative_to.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
            string url = server.ServerURL + "/api/Flights?relative_to=" + dateTimeString;
            try
            {
                WebRequest requestObject = WebRequest.Create(url);
                requestObject.Method = "GET";
                HttpWebResponse response = null;
                response = (HttpWebResponse)requestObject.GetResponse();
                string test = null;
                using (Stream stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    test = sr.ReadToEnd();
                    flightsFromServer = (List<Flight>)JsonConvert.DeserializeObject(test, typeof(List<Flight>));
                    sr.Close();
                }

                foreach (Flight flight in flightsFromServer)
                {
                    flight.IsExternal = true;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return flightsFromServer;
        }
    }
}
