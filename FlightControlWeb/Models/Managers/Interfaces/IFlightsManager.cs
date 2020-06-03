using System;
using System.Collections.Generic;

namespace FlightControlWeb.Models.Managers.Interfaces
{
    public interface IFlightsManager
    {
        public void AddFlight(Flight flight);
        public void DeleteFlight(string flightId);
        public List<Flight> GetAllFlights(DateTime relative_to);
        public Flight GetFlightById(string flightId);
        public void UpdateFlight(Flight updatedFlight);
    }
}
