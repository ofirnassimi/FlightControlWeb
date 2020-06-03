using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;
using FlightControlWeb.Models;
using FlightControlWeb.Models.Managers.Interfaces;

namespace FlightControlWeb.Controllers.Tests
{
    [TestClass()]
    public class FlightsControllerTests
    {
        [TestMethod()]
        public void Get_OneFlightList_ReturnsOneFlightJsonArray()
        {
            // Arrange
            var flightsManagerStub = new Mock<IFlightsManager>();
            List<Flight> flights = new List<Flight>()
            {
                new Flight{ FlightId = "LY319", Longitude = 34.88, Latitude = 32.01, Passengers = 217,
                CompanyName = "El Al", DateTime = DateTime.Parse("2020-05-24T09:04:00Z"), IsExternal = false }
            };
            var expected = "[{\"flight_id\":\"LY319\",\"longitude\":36.81," +
                "\"latitude\":20.494999999999997,\"passengers\":217," +
                "\"company_name\":\"El Al\",\"date_time\":\"2020 - 05 - 24T12: 04:00 + 03:00\"," +
                "\"is_external\":false}]";
            var flightsController = new FlightsController();
            var relative_to = new DateTime(2020, 05, 24, 09, 04, 30);

            flightsManagerStub.Setup(fm => fm.GetAllFlights(relative_to)).Returns(flights);

            // Act
            var actual = flightsController.Get(relative_to);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}