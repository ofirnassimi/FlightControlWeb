using MongoDB.Driver;

namespace FlightControlWeb.Models.Managers
{
    public class DBManager
    {
        private static IMongoDatabase instance;
        private static IMongoCollection<Server> serversCollection;

        private DBManager() { }
        
        public static IMongoDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    MongoClient client = new MongoClient("mongodb+srv://flightControlApp:FCWfcw@flightcontrolweb-gho9t.mongodb.net/test?retryWrites=true&w=majority");
                    instance = client.GetDatabase("flight_control_web");

                }

                return instance;
            }
        }

        public static IMongoCollection<Server> ServersCollection
        {
            get
            {
                if (serversCollection == null)
                {
                    serversCollection = Instance.GetCollection<Server>("servers");
                }

                return serversCollection;
            }
        }
    }
}