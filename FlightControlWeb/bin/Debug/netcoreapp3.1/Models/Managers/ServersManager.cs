using System.Collections.Generic;
using System.Linq;

namespace FlightControlWeb.Models.Managers
{
    public class ServersManager
    {
        private static List<Server> servers = new List<Server>()
        {
            new Server{ ServerId = "srv-4996", ServerURL = "http://rony6.atwebpages.com" },
            new Server { ServerId = "srv-6367", ServerURL = "www.ofir.com" }
        };

        public void AddServer(Server server)
        {
            servers.Add(server);
        }

        public void DeleteServer(string serverId)
        {
            Server server = servers.Where(srv => srv.ServerId.Equals(serverId)).FirstOrDefault();

            if (server != null) {
                servers.Remove(server);
            }
        }
    
        public List<Server> GetAllServers()
        {
            return servers;
        } 
        
        public Server GetServerById(string serverId)
        {
            Server server = servers.Where(srv => srv.ServerId.Equals(serverId)).FirstOrDefault();

            return server;
        }
        
        public void UpdateServer(Server updatedServer)
        {
            Server server = servers.Where(srv => srv.ServerId.Equals(updatedServer.ServerId)).FirstOrDefault();

            if (server != null)
            {
                server.ServerURL = updatedServer.ServerURL;
            }
        }
    }
}
