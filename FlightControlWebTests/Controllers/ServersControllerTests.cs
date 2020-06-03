using FlightControlWeb.Models.Managers.Interfaces;
using FlightControlWeb.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace FlightControlWeb.Controllers.Tests
{
    [TestClass()]
    public class ServersControllerTests
    {
        [TestMethod()]
        public void Get_SingleServer_ReturnsServerAsJson()
        {
            // Arrange
            var serversManagerStub = new Mock<IServersManager>();
            List<Server> servers = new List<Server>()
            {
                new Server{ ServerId = "srv-4996", ServerURL = "http://rony6.atwebpages.com" }
            };
            var expected = "[{\"ServerId\":\"srv-4996\",\"ServerURL\":\"http://rony6.atwebpages.com\"}]";
            var serversController = new ServersController(serversManagerStub.Object);

            serversManagerStub.Setup(sm => sm.GetAllServers()).Returns(servers);

            // Act
            var actual = serversController.Get();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}