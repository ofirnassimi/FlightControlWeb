using FlightControlWeb.BL;
using System.Collections.Generic;
using System.Linq;

namespace FlightControlWeb.Models.Managers
{
    public class ServersManager
    {
        private static List<Server> servers = new List<Server>()
        {
            new Server{ ServerId = "srv-1432", ServerURL = "www.sapir.com" },
            new Server{ ServerId = "srv-6367", ServerURL = "www.ofir.com" },
            new Server{ ServerId = "srv-8903", ServerURL = "www.mor.com" }
        };

        public static void AddServer(Server server)
        {
            // Servers.Add(server);

            servers.Add(server);
        }

        public static void DeleteServer(string serverId)
        {
            // Server server = Servers.GetAll().Where(srv => srv.ServerId.Equals(serverId)).FirstOrDefault();
            //
            // if (server != null) {
            //     Servers.Delete(server);
            // }

            Server server = servers.Where(srv => srv.ServerId.Equals(serverId)).FirstOrDefault();

            if (server != null) {
                servers.Remove(server);
            }
        }
    
        public static List<Server> GetAllServers()
        {
            return Servers.GetAll();

            //return servers;
        } 
        
        public static Server GetServerById(string serverId)
        {
            // return Servers.GetAll().Where(srv => srv.ServerId.Equals(serverId)).FirstOrDefault();

            return servers.Where(srv => srv.ServerId.Equals(serverId)).FirstOrDefault();
        }
        
        public static void UpdateServer(Server updatedServer)
        {
            // Server server = Servers.GetAll().Where(srv => srv.ServerId.Equals(updatedServer.ServerId)).FirstOrDefault();
            //
            // if (server != null) {
            //     Servers.Update(updatedServer);
            // }

            Server server = servers.Where(srv => srv.ServerId.Equals(updatedServer.ServerId)).FirstOrDefault();

            if (server != null)
            {
                server.ServerURL = updatedServer.ServerURL;
            }
        }
    }
}
