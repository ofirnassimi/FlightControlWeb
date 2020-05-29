using FlightControlWeb.Models;
using FlightControlWeb.Models.Managers;
using MongoDB.Driver;
using System.Collections.Generic;

namespace FlightControlWeb.BL
{
    public class Servers
    {
        public static List<Server> GetAll()
        {
            List<Server> result = DBManager.ServersCollection.Find(Builders<Server>.Filter.Empty).ToList<Server>();

            return result;
        }
    }
}