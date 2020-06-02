using System.Text.Json;

namespace FlightControlWeb.Models
{
    public class Server
    {
        public string ServerId { get; set; }

        public string ServerURL { get; set; }


        public string ToJson() => JsonSerializer.Serialize<Server>(this);
    }
}
