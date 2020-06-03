using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using FlightControlWeb.Models;
using FlightControlWeb.Models.Managers;
using FlightControlWeb.Models.Managers.Interfaces;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private IServersManager serversManager = new ServersManager();

        public ServersController()
        {

        }

        public ServersController(IServersManager serversManager)
        {
            this.serversManager = serversManager;
        }

        // GET: api/Servers
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(serversManager.GetAllServers());
        }

        // POST: api/Servers
        [HttpPost]
        public void Post([FromBody] Server server)
        {
            serversManager.AddServer(server);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            serversManager.DeleteServer(id);
        }
    }
}
