using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightControlWeb.Models.Managers
{
    public class FlightPlansManager
    {
        private static List<FlightPlan> flightPlans = new List<FlightPlan>()
        {

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
