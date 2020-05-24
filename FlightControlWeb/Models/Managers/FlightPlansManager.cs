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
                InitialLocation = new InitialLocation{ Longitude = 32.08, Latitude = 34.66,
                DateTime = DateTime.Parse("2020-05-24T09:04:00Z") },
                Segments = new Segment[2] { new Segment { Longitude = 32.08, Latitude = 34.66, TimespanSeconds = 650 },
                new Segment { Longitude = 33.08, Latitude = 35.66, TimespanSeconds = 650 } } },
            new FlightPlan{ FlightId = "SWS899", Passengers = 359, CompanyName = "Swiss Air",
                InitialLocation = new InitialLocation{ Longitude = 39.48, Latitude = -73.22,
                DateTime = DateTime.Parse("2020-04-27T19:30:26Z") },
                Segments = new Segment[2] { new Segment { Longitude = 39.48, Latitude = -73.22, TimespanSeconds = 760 },
                new Segment { Longitude = 40.48, Latitude = -74.22, TimespanSeconds = 850 } } },
            new FlightPlan{ FlightId = "TK6140", Passengers = 143, CompanyName = "Turkish Airlines",
                InitialLocation = new InitialLocation{ Longitude = 26.65, Latitude = 41.13, 
                DateTime = DateTime.Parse("2020-04-27T19:29:26Z")},
                Segments = new Segment[1] { new Segment { Longitude = 26.65, Latitude = 41.13, TimespanSeconds = 950 } } }
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
