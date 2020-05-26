using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightControlWeb.Models.Managers
{
    public class FlightPlansManager
    {
        private static List<FlightPlan> flightPlans = new List<FlightPlan>()
        {
            new FlightPlan{ FlightId = "LY319", Passengers = 217, CompanyName = "El AL",
                InitialLocation = new InitialLocation{ Longitude = 34.88, Latitude = 32.01,
                DateTime = DateTime.Parse("2020-05-24T09:04:00Z") },
                Segments = new Segment[2] { new Segment { Longitude = 38.74, Latitude = 8.98, TimespanSeconds = 60 },
                new Segment { Longitude = 47.44, Latitude = -18.92, TimespanSeconds = 30 } } },
            new FlightPlan{ FlightId = "SWS899", Passengers = 359, CompanyName = "Swiss Air",
                InitialLocation = new InitialLocation{ Longitude = 8.55, Latitude = 47.45,
                DateTime = DateTime.Parse("2020-05-24T09:04:26Z") },
                Segments = new Segment[2] { new Segment { Longitude = 10.70, Latitude = 59.95, TimespanSeconds = 700 },
                new Segment { Longitude = -22.63, Latitude = 63.98, TimespanSeconds = 550 } } },
            new FlightPlan{ FlightId = "TK6140", Passengers = 143, CompanyName = "Turkish Airlines",
                InitialLocation = new InitialLocation{ Longitude = 30.8, Latitude = 36.9, 
                DateTime = DateTime.Parse("2020-05-24T09:04:36Z")},
                Segments = new Segment[1] { new Segment { Longitude = 44.82, Latitude = 41.72, TimespanSeconds = 600 } } },
            new FlightPlan{ FlightId = "UA091", Passengers = 450, CompanyName = "United Airlines",
                InitialLocation = new InitialLocation{ Longitude = -87.9, Latitude = 41.97, 
                DateTime = DateTime.Parse("2020-05-24T09:04:03Z") },
                Segments = new Segment[3] { new Segment { Longitude = -73.82, Latitude = 45.6, TimespanSeconds = 450 },
                new Segment { Longitude = -52.16, Latitude = 47.81, TimespanSeconds = 600 },
                new Segment { Longitude = -56.46, Latitude = 53.46, TimespanSeconds = 500 } } },
            new FlightPlan{ FlightId = "BA413", Passengers = 330, CompanyName = "British Airways",
                InitialLocation = new InitialLocation{ Longitude = -0.45, Latitude = 51.47, 
                DateTime = DateTime.Parse("2020-05-24T09:04:56Z") },
                Segments = new Segment[4] { new Segment { Longitude = 4.8, Latitude = 45.91, TimespanSeconds = 200 },
                new Segment { Longitude = 12.41, Latitude = 41.86, TimespanSeconds = 520 },
                new Segment { Longitude = 23.7, Latitude = 37.96, TimespanSeconds = 600 },
                new Segment { Longitude = 34.88, Latitude = 32.01, TimespanSeconds = 550 } } }
        };

        public void AddFlightPlan(FlightPlan flightPlan)
        {
            flightPlans.Add(flightPlan);
        }

        public void DeleteFlightPlan(string flightId)
        {
            FlightPlan flightPlan = flightPlans.Where(fp => fp.FlightId.Equals(flightId)).FirstOrDefault();

            if (flightPlan != null)
            {
                flightPlans.Remove(flightPlan);
            }
        }

        public List<FlightPlan> GetAllFlightPlans()
        {
            return flightPlans;
        }

        public FlightPlan GetFlightPlanById(string flightId)
        {
            FlightPlan flightPlan = flightPlans.Where(fp => fp.FlightId.Equals(flightId)).FirstOrDefault();

            return flightPlan;
        }

        public void UpdateFlightPlan(FlightPlan updatedFlightPlan)
        {
            FlightPlan flightPlan = flightPlans.Where(fp => fp.FlightId.Equals(updatedFlightPlan.FlightId)).FirstOrDefault();

            if (flightPlan != null)
            {
                flightPlan.Passengers = updatedFlightPlan.Passengers;
                flightPlan.CompanyName = updatedFlightPlan.CompanyName;
                flightPlan.InitialLocation = updatedFlightPlan.InitialLocation;
                flightPlan.Segments = updatedFlightPlan.Segments;
            }
        }


    }
}
