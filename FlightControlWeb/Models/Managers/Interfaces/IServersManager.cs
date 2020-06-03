using System;
using System.Collections.Generic;

namespace FlightControlWeb.Models.Managers.Interfaces
{
    public interface IServersManager
    {
        public void AddServer(Server server);

        public void DeleteServer(string serverId);

        public List<Server> GetAllServers();

        public Server GetServerById(string serverId);

        public void UpdateServer(Server updatedServer);
    }
}
